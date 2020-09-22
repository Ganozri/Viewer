using Medusa.Analyze1553B.Common;
using Olympus.Translation;
using System;
using System.IO;
using System.Linq;

namespace CSharpTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\Data\20200609-123143.bmd";


            var loader = new Medusa.Analyze1553B.Loader.BMD.Loader(new TranslationRepository());
            var fstream = File.OpenRead(path);
            var dataRecords = loader.ReadStream(fstream);
            var DataRecordsList = dataRecords.Cast<DataRecord>().ToArray();

            //foreach (var item in DataRecordsList)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            Console.WriteLine("{0}", DataRecordsList[4].Cw1.Direction);

        }
    }
}
