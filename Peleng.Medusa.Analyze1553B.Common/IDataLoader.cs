using System;
using System.Collections.Generic;
using System.IO;

namespace Peleng.Medusa.Analyze1553B.Common
{
    public interface IDataLoader
    {
        IEnumerable<DataRecord> ReadStream(Stream input);

        string Name { get; }

        IReadOnlyList<string> FileExtensions { get; }
    }
}
