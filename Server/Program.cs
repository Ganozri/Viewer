using Medusa.Analyze1553B.Common;
using Olympus.Translation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        private static TranslationRepository translationRepository;
        private static Medusa.Analyze1553B.Loader.BMD.Loader loader;

        static int Main(string[] args)
        {
            //
            translationRepository = new TranslationRepository();
            loader = new Medusa.Analyze1553B.Loader.BMD.Loader(translationRepository);
            //
            
            //
            try
            {
                StartListener(1234).Wait();
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }

        private static async Task StartListener(int port)
        {
            var tcpListener = TcpListener.Create(port);
            tcpListener.Start();
            //
            //string text = "";
            ObservableCollection<string> data = new ObservableCollection<string>();
            string path = @"D:\Data\20200314-173833 (norm).bmd";
            using (StreamReader fs = new StreamReader(path))
            {
                while (true)
                {
                    // Читаем строку из файла во временную переменную.
                    string temp = fs.ReadLine();

                    // Если достигнут конец файла, прерываем считывание.
                    if (temp == null) break;

                    // Пишем считанную строку в итоговую переменную.
                    //text += temp;
                    data.Add(temp);
                }
            }
            //
            int count = 0;
            for (; ; )
            {
                Console.WriteLine("[Server] waiting for clients...");

                using (var tcpClient = await tcpListener.AcceptTcpClientAsync())
                {
                    try
                    {
                        Console.WriteLine("[Server] Client has connected");
                        using var networkStream = tcpClient.GetStream();
                        using var reader = new StreamReader(networkStream);
                        using var writer = new StreamWriter(networkStream) { AutoFlush = true };

                        for (int i = 0; i < 40; i++)
                        {
                            await writer.WriteLineAsync(data[count]);
                            count++;
                        }

                        while (true)
                        {
                            
                            if (count < data.Count)
                            {
                                await writer.WriteLineAsync(data[count]);
                                count++;
                                Console.WriteLine(data[count]);
                                await Task.Delay(TimeSpan.FromMilliseconds(10));
                            }
                            else
                            {
                                Console.WriteLine("End!\n Press to do it again!");
                                await Task.Delay(TimeSpan.FromMilliseconds(10));
                                count = 0;
                                Console.ReadLine();
                                break;
                            }
                        }
                    }
                        catch (Exception)
                        {
                            Console.WriteLine("[Server] client connection lost");
                        }
                    
                   

                }

            }
        }
    }
}
