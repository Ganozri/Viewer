using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Mono.Options;
using Medusa.Analyze1553B.DataModel;

namespace Medusa.Analyze1553B.Console
{
    using System;

    public class Options
    {
        public List<string> Files { get; set; }

        public string Product { get; set; }

        public string Loader { get; set; }

        public string List { get; set; }

        public static Options Parse(string[] args)
        {
            Options result = new Options();

            var rules = new OptionSet
            {
                "Check 1553 data file",
                {"list=", "List option: loaders, products", v => result.List = v},
                {"loader=", "Check files", v => result.Loader = v},
                {"product=", "[Required] Check as product", v => result.Product = v},
            };
            
            result.Files = rules.Parse(args);

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //args = new [] { "--list=loaders" , "test.bmd", "test2.bmd"};
            args = new[] { "--list=products", "test.bmd", "test2.bmd" };

            if (GetOptions(args) is Options opt)
            {
                if (opt.List != null)
                {
                    switch (opt.List)
                    {
                        case "loaders":
                            Console.WriteLine("LOADERS");
                            ShowLoaders(Console.Out);
                            break;
                        case "products":
                            Console.WriteLine("PRODUCTS");
                            ShowProducts(Console.Out);
                            Console.Out.WriteLine("HUI");
                            break;
                        default:
                            //opt.WriteOptionDescriptions(Console.Error);
                            break;
                    }
                }
                //Run(opt);
            }
           
            

        }

        private static Options GetOptions(string[] args)
        {
            return Options.Parse(args);
        }

        private static void ShowProducts(TextWriter @out)
        {
            GetGlobalContext()
                .ProductLoaders
                .ToList()
                .ForEach(@out.WriteLine);
        }

        private static void ShowLoaders(TextWriter @out)
        {
            GetGlobalContext()
                .DataLoaders
                .ToList()
                .ForEach(x => @out.WriteLine(x.Name));
        } 

        private static GlobalContext context;

        private static GlobalContext GetGlobalContext()
        {
            if (context == null)
            {
                Trace.Write("Loading context...");
                context = new GlobalContext();
                context.LoadAssemblies(null);
                Trace.WriteLine(" [complete]");
            }
            return context;
        }


        private static void Run(Options opt)
        {
            Console.WriteLine(opt);
        }
    }
}
