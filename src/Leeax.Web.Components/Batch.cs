using System;
using System.Collections.Generic;
using System.Linq;

namespace Leeax.Web.Components
{
    public class Batch<TItem>
    {
        public Batch(IEnumerable<TItem> items, int total)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items), $"{nameof(Batch<TItem>)} cannot be empty.");
            Total = total < 0 
                ? throw new ArgumentOutOfRangeException(nameof(total), "The total cannot be negative.") 
                : total;
        }

        public static Batch<TItem> Empty()
        {
            return new Batch<TItem>(Enumerable.Empty<TItem>(), 0);
        }

        public IEnumerable<TItem> Items { get; }

        public int Total { get; }
    }
}