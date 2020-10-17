using DynamicData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpWPF
{
    public class Service
    {
        private readonly SourceList<bool> _items = new SourceList<bool>();

        // We expose the Connect() since we are interested in a stream of changes.
        // If we have more than one subscriber, and the subscribers are known, 
        // it is recommended you look into the Reactive Extension method Publish().
        public IObservable<IChangeSet<bool>> Connect() => _items.Connect();

        public Service()
        {
            // With DynamicData you can easily manage mutable datasets,
            // even if they are extremely large. In this complex scenario 
            // a service mutates the collection, by using .Add(), .Remove(), 
            // .Clear(), .Insert(), etc. DynamicData takes care of
            // allowing you to observe all of those changes.
            _items.Add(true);
            _items.RemoveAt(0);
            _items.Add(false);

            _items.Add(true);
            _items.Add(false);
            _items.Add(true);
            _items.Add(false);
            _items.Add(false);
            _items.Add(false);
            _items.Add(true);
            _items.Add(true);
            _items.Add(true);
            _items.Add(true);
            _items.Add(true);
            _items.Add(true);
            _items.Add(true);
        }

        public void AddOne()
        {

        }
    }
}
