using System;

namespace Peleng.Gorgona.Analyze1553B.Common
{
    [Flags]
    public enum Error
    {
        None = 0,
        NoResp = 1,
        ErrBits = 2,
        Protocol = 4,
        Generic = 0x100
    }
}