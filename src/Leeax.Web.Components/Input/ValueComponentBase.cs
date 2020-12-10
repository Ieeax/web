using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public abstract class ValueComponentBase<TFirst, TSecond> : ValueComponentBase<TFirst>
    {
        /// <summary>
        /// Tries to convert <paramref name="newValue"/> to <typeparamref name="TFirst"/> and invoke the specified <paramref name="callback"/>.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="callback">The callback to invoke.</param>
        /// <exception cref="InvalidOperationException" />
        protected async Task TriggerValueCallbackAsync(TSecond? newValue, EventCallback<TFirst> callback)
        {
            if (Converter == null)
            {
                if (newValue is TFirst castedValue)
                {
                    await callback.InvokeAsync(castedValue);
                    return;
                }

                throw new InvalidOperationException($"Cannot convert value of type '{typeof(TSecond).FullName}' to '{typeof(TFirst).FullName}'. Consider to use an converter.");
            }

            // Check whether the value can be converted
            if (!Converter.CanConvertBackTypeSafe<TSecond>(newValue))
            {
                HasInvalidValue = true;
                return;
            }

            // Reset flag if value can be converted now
            if (HasInvalidValue)
            {
                HasInvalidValue = false;
            }

            // Trigger passed callback
            await callback.InvokeAsync(
                Converter.ConvertBackTypeSafe<TFirst>(newValue));
        }

        /// <summary>
        /// Tries to convert the underlying <see cref="ValueComponentBase{TValue}.Value"/> to <typeparamref name="TSecond"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        protected virtual TSecond? GetConvertedValue()
        {
            if (Converter == null)
            {
                if (object.Equals(Value, default))
                {
                    return default;
                }
                else if (Value is TSecond castedValue)
                {
                    return castedValue;
                }

                throw new InvalidOperationException($"Cannot convert value of type '{typeof(TFirst).FullName}' to '{typeof(TSecond).FullName}'. Consider to use an converter.");
            }

            return Converter.ConvertTypeSafe<TSecond>(Value);
        }

        /// <summary>
        /// Gets or sets the value converter.
        /// </summary>
        [Parameter]
        public virtual IConverter? Converter { get; set; }
    }

    public abstract class ValueComponentBase<TValue> : LxComponentBase
    {
        private TValue? _value;
        private bool _hasInvalidValue;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x.Add(ClassNames.Invalid, HasInvalidValue));
        }

        /// <summary>
        /// Gets or sets whether the value is invalid.
        /// If set to <see langword="true"/>, the <see cref="ClassNames.Invalid"/> class will be applied.
        /// </summary>
        public bool HasInvalidValue
        { 
            get => _hasInvalidValue; 
            protected set
            {
                if (_hasInvalidValue != value)
                {
                    _hasInvalidValue = value;
                    StateHasChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Parameter]
        public virtual TValue? Value
        {
            get => _value;
            set
            {
                if (HasInvalidValue)
                {
                    HasInvalidValue = false;
                }

                _value = value;
            }
        }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Items"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }
    }
}