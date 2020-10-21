using System;
using System.Threading;


namespace Program.ByteSumCountingProgram.UIServices
{
    public interface ISynchronizationContextProvider
    {
        SynchronizationContext SynchronizationContext { get; }
    }
}
