using System;
using System.Threading;


namespace Medusa.Analyze1553B.UIServices
{
    public interface ISynchronizationContextProvider
    {
        SynchronizationContext SynchronizationContext { get; }
    }
}
