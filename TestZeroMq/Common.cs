using System;
using System.Collections.Generic;
using System.Text;

namespace TestZeroMq
{
    public static class Common
    {
        public static readonly byte[] Ping = new byte[] { 0x50, 0x49, 0x4E, 0x47, };
        public static readonly byte[] Pong = new byte[] { 0x50, 0x4F, 0x4E, 0x47, };
    }
}
