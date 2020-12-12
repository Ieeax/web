using Leeax.Web.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComponentsDemo
{
    public class AnimalBatchProvider : IBatchProvider<Animal>
    {
        private readonly IList<Animal> _items;
        private readonly Random _random;
        private readonly bool _throwException;

        public AnimalBatchProvider(int total, bool throwException = false)
        {
            if (total < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(total));
            }

            _items = new List<Animal>();
            _random = new Random();

            for (int i = 0; i < total; i++)
            {
                _items.Add(Animal.Create());
            }

            _throwException = throwException;
        }

        public async Task<Batch<Animal>> FetchAsync(int batchIndex, int batchSize, CancellationToken token)
        {
            await Task.Delay(_random.Next(50, 1000), token);

            if (_throwException)
            {
                throw new ApplicationException("An error occured.");
            }

            return new Batch<Animal>(
                _items.Skip(batchIndex * batchSize).Take(batchSize), 
                _items.Count);
        }
    }
}