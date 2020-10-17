using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.Loader.BMD;
using Olympus.Checkers;
using Olympus.Translation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace CSharpTesting
{
    public class Program
    {
        static void Main(string[] args)
        {
            // An asynchronous command created from IObservable<int> that 
            // waits 2 seconds and then returns 42 integer.
            var command = ReactiveCommand.CreateFromObservable<Unit, int>(
                _ => Observable.Return(42).Delay(TimeSpan.FromSeconds(2)));

            // Subscribing to the observable returned by `Execute()` will 
            // tick through the value `42` with a 2-second delay.
            command.Execute(Unit.Default).Subscribe();

            // We can also subscribe to _all_ values that a command
            // emits by using the `Subscribe()` method on the
            // ReactiveCommand itself.
            command.Subscribe(value => Console.WriteLine(value));
            Thread.Sleep(2100);


        }




    }

    
}

 