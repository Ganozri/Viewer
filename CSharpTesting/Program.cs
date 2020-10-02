using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.Loader.BMD;
using Olympus.Checkers;
using Olympus.Translation;
using System;
using System.IO;
using System.Linq;

namespace CSharpTesting
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Data\20200609-123143.bmd";
            var dataRecord = GetDataByBMDLoader(path);

            for (int i = 0; i < 10; i++)
            {
                var cw1 = dataRecord[i].Cw1;

                Console.WriteLine(cw1.Address);
                Console.WriteLine(cw1.Direction);
                Console.WriteLine(cw1.Subaddress);
                Console.WriteLine(cw1.Length);

                switch (cw1)
                {
                    case var _ when (cw1.Address == 31 
                                     && cw1.Direction == DataDirection.R 
                                     && cw1.Subaddress == 31 
                                     && cw1.Length == 17) :
                        Console.WriteLine("Green");
                        break;
                    default:
                        Console.WriteLine("White");
                        break; 
                }

                Console.WriteLine();
            }
           
        }

         public static DataRecord[] GetDataByBMDLoader(string path)
        {
            return BaseGetData(new Loader(new TranslationRepository())
                               .ReadStream(File.OpenRead(path))
                               .ToArray());
        }

         public static DataRecord[] BaseGetData(DataRecord[] rawData)
        {
            DataRecord[] DataRecords = new DataRecord[0];
            try
            {
                DataRecords = rawData;
            }
            catch (Exception)
            {
              
            }
            return DataRecords;
        }

    }
}
