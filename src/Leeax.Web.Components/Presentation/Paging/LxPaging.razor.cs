using Leeax.Web.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxPaging<TItem> : IPaging
    {
        public const string ClassName = "lx-paging";

        private CancellationTokenSource? _tokenSource;
        private IBatchProvider<TItem>? _provider;
        private IEnumerable<TItem>? _items;
        private int _totalItems;
        private int _totalPages;
        private int _itemsPerPage = 20;
        private int _page;
        private bool _isFetching;
        private string? _errorMessage;
        private bool _showRetryButton;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.FlexColumn)
                .Add("fetching", _isFetching));
        }

        protected AttributeSet? GetItemAttributeSet(TItem item)
        {
            if (ItemAttributeSetFactory != null)
            {
                return ItemAttributeSetFactory.Invoke(item);
            }

            return ItemAttributeSet;
        }

        private void OnRetryClicked()
        {
            _showRetryButton = false;
            Reload();
        }

        private string GetPagerText()
        {
            if (_totalItems > 0
                && _items != null
                && (_page > 0 || _items.Count() > 1))
            {
                var offset = _page * _itemsPerPage;
                return $"{offset + 1} - {offset + _items.Count()} of {_totalItems} items";
            }

            return _totalItems + (_totalItems == 1 ? " item" : " items");
        }

        /// <inheritdoc/>
        public void Reload() => FetchBatch(_page);

        protected void FetchBatch(int batchIndex)
        {
            if (_provider == null)
            {
                return;
            }

            _isFetching = true;

            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }

            _tokenSource = new CancellationTokenSource();

            Task.Run(
                async () =>
                {
                    var tokenSource = _tokenSource;

                    try
                    {
                        var batch = await _provider.FetchAsync(batchIndex, ItemsPerPage, tokenSource.Token);

                        if (!tokenSource.IsCancellationRequested)
                        {
                            var previousPage = _page;

                            _page = batchIndex;
                            _totalItems = batch.Total;
                            _totalPages = (int)Math.Ceiling((double)batch.Total / ItemsPerPage);
                            _items = batch.Items;

                            // Check whether we changed the page or just reloaded it
                            if (_page != previousPage)
                            {
                                await PageChanged.InvokeAsync(batchIndex);
                            }

                            StateHasChanged();
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorMessage = "Items couldn't be fetched";
                        _showRetryButton = true;

                        ConsoleHelper.WriteExceptionFromComponent(ex, typeof(LxPaging<>));
                    }
                    finally
                    {
                        tokenSource.Dispose();

                        if (_tokenSource == tokenSource)
                        {
                            _isFetching = false;
                            _tokenSource = null;

                            StateHasChanged();
                        }
                    }
                });
        }

        /// <summary>
        /// Gets or sets a factory for creating an <see cref="AttributeSet"/> for each &lt;li&gt; item.
        /// </summary>
        [Parameter]
        public Func<TItem?, AttributeSet>? ItemAttributeSetFactory { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="AttributeSet"/> which gets applied to each &lt;li&gt; item.
        /// </summary>
        [Parameter]
        public AttributeSet? ItemAttributeSet { get; set; }

        /// <summary>
        /// Gets or sets the page index.
        /// The value is zero based.
        /// </summary>
        [Parameter]
        public int Page
        {
            get => _page;
            set
            {
                if (_page != value)
                {
                    _page = value;

                    if (_provider != null)
                    {
                        // Update batch if possible
                        FetchBatch(value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Page"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<int> PageChanged { get; set; }

        /// <summary>
        /// Gets or sets the count of items per page.
        /// </summary>
        [Parameter]
        public int ItemsPerPage
        { 
            get => _itemsPerPage; 
            set
            {
                if (_itemsPerPage != value)
                {
                    _itemsPerPage = value;

                    // Update batch if possible
                    FetchBatch(_page);
                }
            }
        }

        /// <summary>
        /// Gets or sets the provider which supplies the items.
        /// </summary>
        [Parameter]
        public IBatchProvider<TItem>? ItemsProvider
        {
            get => _provider;
            set
            {
                if (_provider != value)
                {
                    _provider = value;

                    // Update batch if possible
                    FetchBatch(_page);
                }
            }
        }

        [Parameter]
        public RenderFragment<TItem>? ItemTemplate { get; set; }
    }
}