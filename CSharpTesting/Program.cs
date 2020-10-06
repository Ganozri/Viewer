using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.Loader.BMD;
using Microsoft.FSharp.Core;
using Olympus.Checkers;
using Olympus.Translation;
using System;
using System.IO;
using System.Linq;

namespace CSharpTesting
{
    public class Program
    {
        private static FSharpOption<T> opt;

        static void Main(string[] args)
        {
            var x = (FSharpOption<T>.GetTag(opt) switch
            {
                FSharpOption<T>.Tags.None => "None",
                FSharpOption<T>.Tags.Some => "Some",
                _ => "imposibru"
            });
        }
        


}

    internal class T
    {
    }
}

