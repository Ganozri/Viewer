using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.Loader.BMD;
using Olympus.Translation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UjeenConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TranslationRepository translationRepository = new TranslationRepository();
            Loader loader = new Loader(translationRepository);

            string path = @"D:\Peleng\Project\medusa-apps-analyze1553b\20200314-173833 (norm).bmd";

            FileStream fstream = File.OpenRead(path);

            IEnumerable<DataRecord> dataRecords = loader.ReadStream(fstream);

            List<DataRecord> dataRecordsList = new List<DataRecord>(dataRecords.ToList<DataRecord>());

            Console.WriteLine("----------------------------------------------");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Index "       + dataRecordsList[i].Index);
                Console.WriteLine("MonitorTime " + dataRecordsList[i].MonitorTime);
                Console.WriteLine("Channel "     + dataRecordsList[i].Channel);
                Console.WriteLine("Error "       + dataRecordsList[i].Error);
                Console.WriteLine("Cw1 {0:X}"    , dataRecordsList[i].Cw1.Value);
                Console.WriteLine("Cw2 {0:X}"    , dataRecordsList[i].Cw2.Value.Value);
                Console.WriteLine("Rw1 {0:X}"    , dataRecordsList[i].Rw1.Value.Value);
                Console.WriteLine("Rw2 {0:X}"    , dataRecordsList[i].Rw2.Value.Value);
                
                for (int j = 0; j < dataRecordsList[i].Data.Length; j++)
                {
                    Console.WriteLine("Data " + j + " {0:X}", dataRecordsList[i].Data[j]);
                }
              

                Console.WriteLine("----------------------------------------------");
            }
           

            Console.ReadKey();
        }
    }
}
