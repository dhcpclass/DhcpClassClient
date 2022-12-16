﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DhcpClassClient
{
    public interface ITransportPacket
    {
        ProtocolType Type { get; }

        byte[] Build();
    }
}
