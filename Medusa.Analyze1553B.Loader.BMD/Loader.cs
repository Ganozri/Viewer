using Medusa.Analyze1553B.Common;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Olympus.Translation;
using Olympus.Streams;

namespace Medusa.Analyze1553B.Loader.BMD
{
    public class Loader : IDataLoader
    {
        private const string TranslationId = nameof(Analyze1553B);
        private const string TranslationGroup = "Loader.BMD";

        private readonly TranslationRepository repository;

        public Loader(TranslationRepository repository)
        {
            this.repository = repository;

            FileExtensions = GetFileExtensions();
            repository.DataChanged += delegate { FileExtensions = GetFileExtensions(); };
        }

        private IDictionary<string, string> GetFileExtensions()
        {
            return new Dictionary<string, string>
            {
                ["bmd"] = repository[TranslationId, TranslationGroup, nameof(FileExtensions), "Данные монитора шины"],
            };
        }

        public string Name => repository[TranslationId, TranslationGroup, nameof(Name), "Встроенный монитор шины"];

        public IDictionary<string, string> FileExtensions { get; private set; }

        private static readonly char[] CommentDelimiter = "#".ToCharArray();
        private static readonly char[] TabDelimiter = "\t".ToCharArray();

        public IEnumerable<DataRecord> ReadStream(Stream input)
        {
            return new StreamReader(input).AsStrings()
                                          .Select(ParseLine)
                                          .Where(p => p != null);
        }

        private DataRecord ParseLine(string line, int index)
        {
            var parts = line.Split(CommentDelimiter)[0].Split(TabDelimiter);
            if (parts.Length == 42)
            {
                var cw1 = new ControlWord(Word(parts, Control1));
                DataRecordBuilder builder = new DataRecordBuilder(
                    index: index,
                    monitorTime: BuildTime(int.Parse(parts[TimeLo]), int.Parse(parts[TimeHi])),
                    channel: (BusChannel)int.Parse(parts[BusChannel]),
                    error: StatusToError(Word(parts, Errors), Word(parts, Response1)),
                    cw1: cw1,
                    cw2: new ControlWord(Word(parts, Control2)),
                    rw1: new ResponseWord(Word(parts, Response1)),
                    rw2: new ResponseWord(Word(parts, Response2))
                );

                if (cw1.IsCommand)
                {
                    if (cw1.CommandCode.HasDataWord())
                    {
                        builder.Data(Word(parts, DataStart));
                    }
                }
                else
                {
                    builder.Data(Enumerable.Range(0, cw1.Length).Select(x => Word(parts, DataStart + x)));
                }

                return builder.GetRecord();
            }
            else
            {
                return null;
            }
           
            
        }

        private Error StatusToError(ushort status, ushort resp)
        {
            Error result = Error.None;
            int format = (status >> 10) & 0xF;

            if ((format <= 5) && resp == ushort.MaxValue)
            {
                result |= Error.NoResp;
            }

            if ((status & 0x0008) != 0)
            {
                result |= Error.ErrBits;
            }

            if ((status & 0x0300) != 0)
            {
                result |= Error.Protocol;
            }

            switch (status & 0x0007)
            {
                case 0:
                    break;
                case 1:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    result |= Error.Protocol;
                    break;
                case 2:
                    if (format <= 5)
                    {
                        result |= Error.NoResp;
                    }
                    break;
                default:
                    result |= Error.Generic;
                    break;
            }

            return result;

        }

        private static ushort Word(string[] parts, int index)
        {
            return ushort.Parse(parts[index], NumberStyles.HexNumber);
        }

        private double BuildTime(long low, long high)
        {
            return (low + (high << 32)) * 1e-6;
        }

        private const int TimeLo = 0;
        private const int TimeHi = 1;
        private const int Status = 2;
        private const int Errors = 3;
        private const int BusChannel = 4;
        private const int Control1 = 5;
        private const int Control2 = 6;
        private const int Response1 = 7;
        private const int Response2 = 8;
        private const int DataStart = 9;
    }
}
