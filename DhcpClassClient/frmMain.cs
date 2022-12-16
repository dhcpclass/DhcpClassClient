using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using DhcpClassClient.Dhcp;

namespace DhcpClassClient
{
    public partial class frmMain : Form
    {
        private NetworkInterface[] m_interfaces;
        private NetworkInterface m_interfaceMain;
        private Socket m_ss;
        private bool running = true;
        private ManualResetEvent rcvAckEvent;
        private ISet<DhcpOffer> offers;
        private DhcpOffer selectedOffer;

        public frmMain()
        {
            InitializeComponent();

            rcvAckEvent = new ManualResetEvent(false);
            selectedOffer = null;
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static int ExecuteNetshCommand(string command)
        {
            Process proc = Process.Start(new ProcessStartInfo()
            {
                FileName = "netsh",
                Arguments = command,
                UseShellExecute = false
            });
            proc.WaitForExit();
            return proc.ExitCode;
        }

        private static IPv4Packet BuildDhcpDiscoverMessage(NetworkInterface inter, ushort packetId, uint dhcpTransactionId)
        {
            var hardwareAddress = new HardwareAddress(HardwareAddressType.Ethernet,
                inter.GetPhysicalAddress().GetAddressBytes());

            var dhcpPacket = new DhcpPacket();
            dhcpPacket.Operation = DhcpOperation.Request;
            dhcpPacket.MessageType = DhcpMessageType.Discover;
            dhcpPacket.TransactionId = dhcpTransactionId;
            dhcpPacket.HardwareAddress = hardwareAddress;

            var dhcpUdpPacket = new UdpPacket();
            dhcpUdpPacket.SourcePort = 68;
            dhcpUdpPacket.DestinationPort = 67;
            dhcpUdpPacket.Payload = dhcpPacket;

            var dhcpIpPacket = new IPv4Packet();
            dhcpIpPacket.Source = IPAddress.Any;
            dhcpIpPacket.Destination = IPAddress.Broadcast;
            dhcpIpPacket.Identifiation = packetId;
            dhcpIpPacket.Payload = dhcpUdpPacket;
            return dhcpIpPacket;
        }

        private static IPv4Packet BuildDhcpRequestMessage(DhcpOffer offer, NetworkInterface inter, ushort packetId, uint dhcpTransactionId)
        {
            var hardwareAddress = new HardwareAddress(HardwareAddressType.Ethernet,
                inter.GetPhysicalAddress().GetAddressBytes());

            var dhcpPacket = new DhcpPacket();
            dhcpPacket.Operation = DhcpOperation.Request;
            dhcpPacket.MessageType = DhcpMessageType.Request;
            dhcpPacket.TransactionId = dhcpTransactionId;
            dhcpPacket.HardwareAddress = hardwareAddress;

            dhcpPacket.Options.Add(new DhcpServerIdentifierOption(offer.DhcpServer));
            dhcpPacket.Options.Add(new DhcpClientIdentifier(hardwareAddress));
            dhcpPacket.Options.Add(new DhcpRequestedIpAddressOption(offer.ClientAddress));

            var dhcpUdpPacket = new UdpPacket();
            dhcpUdpPacket.SourcePort = 68;
            dhcpUdpPacket.DestinationPort = 67;
            dhcpUdpPacket.Payload = dhcpPacket;

            var dhcpIpPacket = new IPv4Packet();
            dhcpIpPacket.Source = IPAddress.Any;
            dhcpIpPacket.Destination = IPAddress.Broadcast;
            dhcpIpPacket.Identifiation = packetId;
            dhcpIpPacket.Payload = dhcpUdpPacket;

            return dhcpIpPacket;
        }

        private static IPv4Packet BuildDhcpRebindMessage(DhcpOffer offer, NetworkInterface inter, ushort packetId, uint dhcpTransactionId)
        {
            var hardwareAddress = new HardwareAddress(HardwareAddressType.Ethernet,
                inter.GetPhysicalAddress().GetAddressBytes());

            var dhcpPacket = new DhcpPacket();
            dhcpPacket.Operation = DhcpOperation.Request;
            dhcpPacket.MessageType = DhcpMessageType.Request;
            dhcpPacket.TransactionId = dhcpTransactionId;
            dhcpPacket.HardwareAddress = hardwareAddress;
            dhcpPacket.ClientAddress = offer.ClientAddress;

            dhcpPacket.Options.Add(new DhcpClientIdentifier(hardwareAddress));

            var dhcpUdpPacket = new UdpPacket();
            dhcpUdpPacket.SourcePort = 68;
            dhcpUdpPacket.DestinationPort = 67;
            dhcpUdpPacket.Payload = dhcpPacket;

            var dhcpIpPacket = new IPv4Packet();
            dhcpIpPacket.Source = offer.ClientAddress;
            dhcpIpPacket.Destination = offer.DhcpServer;
            dhcpIpPacket.Identifiation = packetId;
            dhcpIpPacket.Payload = dhcpUdpPacket;

            return dhcpIpPacket;
        }

        private static Socket SetupPromiscousSocket(NetworkInterface inter)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Udp);
            IPAddress listeningAddress = inter.GetIPProperties().UnicastAddresses
                .Where(info => info.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(info => info.Address)
                .First();
            Console.WriteLine($" Binding to address {listeningAddress}");
            socket.Bind(new IPEndPoint(listeningAddress, 0));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);

            byte[] byOn = BitConverter.GetBytes(1);
            socket.IOControl(IOControlCode.ReceiveAll, byOn, BitConverter.GetBytes(0));
            socket.EnableBroadcast = true;
            return socket;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            m_interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var item in m_interfaces.Select((i, idx) => new { nr = idx + 1, inter = i }))
            {
                //Console.WriteLine($"-{item.nr}- {item.inter.Name}");
                comboInterface.Items.Add($"{item.nr}- {item.inter.Name}");
            }
        }

        private void comboInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Trace.WriteLine(comboInterface.SelectedIndex);
            if (comboInterface.SelectedIndex >= 0)
            {
                m_interfaceMain = m_interfaces[comboInterface.SelectedIndex];
                if (m_ss != null)
                {
                //    m_ss.Close();
                }
                //m_ss = SetupPromiscousSocket(m_interfaceMain);
            }

        }

        private void Log(string strLog)
        {
            txtLog.Text += strLog + "\r\n";
        }

        protected void TestDiscover()
        {
            var random = new Random();
            ushort packetId = (ushort)random.Next();
            uint dhcpTransactionId = (uint)random.Next();

            ITransportPacketFactory transportPacketFactory = new TransportPacketFactoryImpl();
            IApplicationPacketFactory applicationPacketFactory = new ApplicationPacketFactoryImpl();

            var dhcpDiscover = BuildDhcpDiscoverMessage(m_interfaceMain, packetId, dhcpTransactionId);

            running = true;
            bool rcvAck = false;
            rcvAckEvent = new ManualResetEvent(false);
            //DhcpOffer selectedOffer = null;
            m_ss = SetupPromiscousSocket(m_interfaceMain);

            Log("Send DHCP Discover...");
            dhcpDiscover.Send(m_ss);

            offers = new HashSet<DhcpOffer>();
            var recieverThread = new Thread(() =>
            {
                while (running)
                {
                    if (m_ss == null)
                        break;

                    IPv4Packet rcvPacket = IPv4Packet.Recieve(m_ss, transportPacketFactory, applicationPacketFactory);

                    if (rcvPacket != null && rcvPacket.Payload is UdpPacket udpPacket)
                    {
                        if (udpPacket.Payload is DhcpPacket responseDhcpPacket)
                        {
                            switch (responseDhcpPacket.MessageType)
                            {
                                case DhcpMessageType.Offer:
                                    DhcpOffer offer = DhcpOffer.FromDhcpPacket(responseDhcpPacket);
                                    offers.Remove(offer); // remove old offer from server if necassary
                                    offers.Add(offer);
                                    Trace.WriteLine($"New Offer from {rcvPacket.Source}");
                                    break;
                                case DhcpMessageType.Ack:
                                    if (!rcvAck)
                                    {
                                        Trace.WriteLine("OK from server - ready to launch!");
                                        if(selectedOffer==null)
                                        {
                                            DhcpOffer[] finalOffers = offers.ToArray();
                                            if (finalOffers.Length != 0)
                                                selectedOffer = finalOffers[0];
                                            else
                                                break;
                                        }

                                        int exitCodeInter = ExecuteNetshCommand(
                                            $"interface ipv4 set address name=\"{m_interfaceMain.Name}\" static" +
                                            $" {selectedOffer.ClientAddress} {selectedOffer.SubnetMask} {selectedOffer.Gateway} 1"
                                        );
                                        Trace.WriteLine($"netsh Interface exit code - {exitCodeInter}");

                                        int exitCodeDns = ExecuteNetshCommand(
                                            $"interface ipv4 set dnsservers \"{m_interfaceMain.Name}\" static {selectedOffer.DnsServer} primary no"
                                        );
                                        Trace.WriteLine($"netsh DNS exit code - {exitCodeDns}");
                                        rcvAckEvent.Set();
                                    }
                                    rcvAck = true;
                                    break;
                            }
                        }
                    }
                }
            });
            recieverThread.Start();

            Thread.Sleep(500);
            if (offers.Count == 0)
            {
                Log("Resending DHCP Discover...");
                dhcpDiscover.Send(m_ss);
            }
        }

         private void llDiscover_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TestDiscover();
        }

        private void TestRequest()
        {
            var random = new Random();
            ushort packetId = (ushort)random.Next();
            uint dhcpTransactionId = (uint)random.Next();

            //Console.WriteLine();

            Log("Send DHCP Request");
            IPv4Packet dhcpRequest = BuildDhcpRequestMessage(selectedOffer, m_interfaceMain, ++packetId, dhcpTransactionId);
            dhcpRequest.Send(m_ss);

            Log("Waiting for DHCP Ack and netsh...");
            rcvAckEvent.WaitOne();
            running = false;
            if (m_ss == null)
                return;

            m_ss.Close();
            m_ss = null;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TestRequest();
        }

        private void llUpdateDiscover_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log("-- Select DHCP server offer --");
            DhcpOffer[] finalOffers = offers.ToArray();
            foreach (var item in finalOffers.Select((o, idx) => new { nr = idx + 1, offer = o }))
            {
                string strTemp = $"-{item.nr}- Offer from {item.offer.DhcpServer}";
                Log(strTemp);
                comboDiscover.Items.Add(strTemp);
                Log($"   Client address: {item.offer.ClientAddress}");
                Log($"   Gateway address: {item.offer.Gateway}");
                Log($"   DNS server: {item.offer.DnsServer}");
                Log($"   Domain: {item.offer.Domain}");
            }
        }

        private void comboDiscover_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDiscover.SelectedIndex < 0)
                return;

            DhcpOffer[] finalOffers = offers.ToArray();
            int selection = comboDiscover.SelectedIndex;
            //DhcpOffer selectedOffer = null;
            selectedOffer = finalOffers[selection];

            Trace.WriteLine("comboDiscover_SelectedIndexChanged" + selectedOffer.DhcpServer);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_ss == null)
                return;

            m_ss.Close();
        }
    }
}
