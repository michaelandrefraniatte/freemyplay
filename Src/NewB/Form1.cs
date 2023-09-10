using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using NetFwTypeLib;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace NewB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static BackgroundWorker backgroundWorker0 = new BackgroundWorker(), backgroundWorkerflood = new BackgroundWorker();
        private static System.Threading.Thread Thrf;
        private static System.Threading.Thread Thrl;
        private static bool notmouseover = true;
        private static string IPs = "0.0.0.0";
        private void NewB_thrf()
        {
            string port = textBox21.Text;
            Int64 iPort = Int64.Parse(port.ToString().Replace("Port: ", ""));
            string path = richTextBox1.Text;
            string iPath = path.ToString().Replace("Path of the program: ", "");
            string pname = textBox1.Text;
            string iPName = pname.ToString();
            string rIP0 = "0.0.0.0";
            string rIP1 = "0.0.0.0";
            INetFwRule2 newRule;
            INetFwPolicy2 firewallpolicy;
            string RemoteAdrr = "0.0.0.0";
            int RemotePort = 0;
            string LocalAdrr = "0.0.0.0";
            int LocalPort = 0;
            IPAddress[] addresslist;
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Socket server = null;
            string elapsed = string.Empty;
            string IPS1 = textBoxS1.Text;
            string IPE1 = textBoxE1.Text;
            string IPS2 = textBoxS2.Text;
            string IPS3 = textBoxS3.Text;
            string IPS4 = textBoxS4.Text;
            string IPE4 = textBoxE4.Text;
            string IPE3 = textBoxE3.Text;
            string IPE2 = textBoxE2.Text;
            Int64 iIPS1 = Int64.Parse(IPS1.ToString().Replace(".", ""));
            Int64 iIPE1 = Int64.Parse(IPE1.ToString().Replace(".", ""));
            Int64 iIPS2 = Int64.Parse(IPS2.ToString().Replace(".", ""));
            Int64 iIPS3 = Int64.Parse(IPS3.ToString().Replace(".", ""));
            Int64 iIPS4 = Int64.Parse(IPS4.ToString().Replace(".", ""));
            Int64 iIPE4 = Int64.Parse(IPE4.ToString().Replace(".", ""));
            Int64 iIPE3 = Int64.Parse(IPE3.ToString().Replace(".", ""));
            Int64 iIPE2 = Int64.Parse(IPE2.ToString().Replace(".", ""));
            if (!checkBox1.Checked)
            {
                file = new System.IO.StreamWriter(charstore);
                for (; ; )
                {
                    byte[] buffer = new byte[20000];
                    int pdwSize = 20000;
                    int res = GetTcpTable(buffer, out pdwSize, true);
                    if (res != NO_ERROR)
                    {
                        buffer = new byte[pdwSize];
                        res = GetTcpTable(buffer, out pdwSize, true);
                        if (res != NO_ERROR)
                        {
                            file.Close();
                            StartRec = false;
                            button4.BackColor = Color.Black;
                            return;
                        }
                    }
                    TcpConnexion = new MIB_TCPTABLE();
                    int nOffset = 0;
                    TcpConnexion.dwNumEntries = Convert.ToInt32(buffer[nOffset]);
                    nOffset += 4;
                    TcpConnexion.table = new MIB_TCPROW[TcpConnexion.dwNumEntries];
                    for (int n = 0; n < TcpConnexion.dwNumEntries; n++)
                    {
                        int st = Convert.ToInt32(buffer[nOffset]);
                        TcpConnexion.table[n].StrgState = convert_state(st);
                        TcpConnexion.table[n].iState = st;
                        nOffset += 4;
                        LocalAdrr = buffer[nOffset].ToString() + "." + buffer[nOffset + 1].ToString() + "." + buffer[nOffset + 2].ToString() + "." + buffer[nOffset + 3].ToString();
                        nOffset += 4;
                        LocalPort = (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) +
                            (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);
                        nOffset += 4;
                        TcpConnexion.table[n].Local = new IPEndPoint(IPAddress.Parse(LocalAdrr), LocalPort);
                        RemoteAdrr = buffer[nOffset].ToString() + "." + buffer[nOffset + 1].ToString() + "." + buffer[nOffset + 2].ToString() + "." + buffer[nOffset + 3].ToString();
                        nOffset += 4;
                        if (RemoteAdrr == "0.0.0.0")
                        {
                            RemotePort = 0;
                        }
                        else
                        {
                            RemotePort = (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) +
                                (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);
                        }
                        nOffset += 4;
                        TcpConnexion.table[n].Remote = new IPEndPoint(IPAddress.Parse(RemoteAdrr), RemotePort);
                        try
                        {
                            addresslist = Dns.GetHostAddresses(RemoteAdrr);
                            foreach (IPAddress theaddress in addresslist)
                            {
                                bool tcpaddressin = false;
                                rIP0 = theaddress.ToString();
                                if (rIP0 != "0")
                                {
                                    System.IO.File.Copy(charstore, "temp.txt", true);
                                    using (System.IO.FileStream fsr = new System.IO.FileStream("temp.txt", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read))
                                    {
                                        using (System.IO.StreamReader sr = new System.IO.StreamReader(fsr))
                                        {
                                            while (!sr.EndOfStream)
                                            {
                                                string line = sr.ReadLine();
                                                if (rIP1 == line)
                                                    tcpaddressin = true;
                                                new System.Threading.ManualResetEvent(false).WaitOne(1);
                                            }
                                        }
                                    }
                                    if (!tcpaddressin)
                                    {
                                        IPs = rIP0;
                                        rIP1 = IPs + "-" + IPs.Remove(IPs.Length - 3) + "255";
                                        richTextBox2.AppendText("Searching IP " + IPs + "...\r\n");
                                        searchRouter(iPath, iPName, rIP1, iPort, server);
                                    }
                                }
                                new System.Threading.ManualResetEvent(false).WaitOne(1);
                            }
                        }
                        catch { }
                        new System.Threading.ManualResetEvent(false).WaitOne(1);
                    }
                    buffer = new byte[20000];
                    pdwSize = 20000;
                    res = GetUdpTable(buffer, out pdwSize, true);
                    if (res != NO_ERROR)
                    {
                        buffer = new byte[pdwSize];
                        res = GetUdpTable(buffer, out pdwSize, true);
                        if (res != NO_ERROR)
                        {
                            file.Close();
                            StartRec = false;
                            button4.BackColor = Color.Black;
                            return;
                        }
                    }
                    UdpConnexion = new MIB_UDPTABLE();
                    nOffset = 0;
                    UdpConnexion.dwNumEntries = Convert.ToInt32(buffer[nOffset]);
                    nOffset += 4;
                    UdpConnexion.table = new MIB_UDPROW[UdpConnexion.dwNumEntries];
                    for (int n = 0; n < UdpConnexion.dwNumEntries; n++)
                    {
                        LocalAdrr = buffer[nOffset].ToString() + "." + buffer[nOffset + 1].ToString() + "." + buffer[nOffset + 2].ToString() + "." + buffer[nOffset + 3].ToString();
                        nOffset += 4;
                        LocalPort = (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) +
                            (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);
                        nOffset += 4;
                        UdpConnexion.table[n].Local = new IPEndPoint(IPAddress.Parse(LocalAdrr), LocalPort);
                        try
                        {
                            addresslist = Dns.GetHostAddresses(LocalAdrr);
                            foreach (IPAddress theaddress in addresslist)
                            {
                                bool udpaddressin = false;
                                rIP0 = theaddress.ToString();
                                if (rIP0 != "0")
                                {
                                    System.IO.File.Copy(charstore, "temp.txt", true);
                                    using (System.IO.FileStream fsr = new System.IO.FileStream("temp.txt", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read))
                                    {
                                        using (System.IO.StreamReader sr = new System.IO.StreamReader(fsr))
                                        {
                                            while (!sr.EndOfStream)
                                            {
                                                string line = sr.ReadLine();
                                                if (rIP1 == line)
                                                    udpaddressin = true;
                                                new System.Threading.ManualResetEvent(false).WaitOne(1);
                                            }
                                        }
                                    }
                                    if (!udpaddressin)
                                    {
                                        IPs = rIP0;
                                        rIP1 = IPs + "-" + IPs.Remove(IPs.Length - 3) + "255";
                                        richTextBox2.AppendText("Searching IP " + IPs + "...\r\n");
                                        searchRouter(iPath, iPName, rIP1, iPort, server);
                                    }
                                }
                                new System.Threading.ManualResetEvent(false).WaitOne(1);
                            }
                        }
                        catch { }
                    }
                    if (notmouseover)
                        richTextBox2.ScrollToCaret();
                    notmouseover = true;
                    new System.Threading.ManualResetEvent(false).WaitOne(1);
                    if (!StartRec)
                    {
                        file.Close();
                        return;
                    }
                }
            }
            if (checkBox1.Checked)
            {
                file = new System.IO.StreamWriter(charstore);
                for (; ; )
                {
                    if (iIPS1 <= iIPE1 & iIPS2 <= iIPE2 & iIPS3 <= iIPE3 & iIPS4 <= iIPE4)
                    {
                        IPs = iIPS1.ToString() + "." + iIPS2.ToString() + "." + iIPS3.ToString() + "." + iIPS4.ToString();
                        rIP1 = IPs.ToString() + "-" + iIPS1.ToString() + "." + iIPS2.ToString() + "." + iIPS3.ToString() + ".255";
                        bool ipsin = false;
                        System.IO.File.Copy(charstore, "temp.txt", true);
                        using (System.IO.FileStream fsr = new System.IO.FileStream("temp.txt", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read))
                        {
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(fsr))
                            {
                                while (!sr.EndOfStream)
                                {
                                    string line = sr.ReadLine();
                                    if (rIP1 == line)
                                        ipsin = true;
                                    new System.Threading.ManualResetEvent(false).WaitOne(1);
                                }
                            }
                        }
                        if (ipsin)
                        {
                            richTextBox2.AppendText("Warning IP " + IPs + "...\r\n");
                            if ((comboBox2.Text.EndsWith("Outbound") & comboBox2.Text.StartsWith("Outbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                            {
                                newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                                newRule.Name = "Warning, " + IPs;
                                newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                                newRule.RemoteAddresses = IPs;
                                newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
                                newRule.Enabled = true;
                                newRule.InterfaceTypes = "All";
                                newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                                newRule.EdgeTraversal = false;
                                firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                                firewallpolicy.Rules.Remove("Warning, " + IPs);
                                firewallpolicy.Rules.Add(newRule);
                            }
                            if ((comboBox2.Text.EndsWith("Inbound") & comboBox2.Text.StartsWith("Inbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                            {
                                newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                                newRule.Name = "Warning, " + IPs;
                                newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                                newRule.RemoteAddresses = IPs;
                                newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                                newRule.Enabled = true;
                                newRule.InterfaceTypes = "All";
                                newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                                newRule.EdgeTraversal = false;
                                firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                                firewallpolicy.Rules.Remove("Warning, " + IPs);
                                firewallpolicy.Rules.Add(newRule);
                            }
                        }
                        else
                        {
                            richTextBox2.AppendText("Searching server IP " + IPs + "...\r\n");
                            searchRouter(iPath, iPName, rIP1, iPort, server);
                        }
                        iIPS4 = iIPS4 + 1;
                        if (iIPS4 > iIPE4)
                        {
                            iIPS4 = Int64.Parse(IPS4.ToString().Replace(".", ""));
                            iIPS3 = iIPS3 + 1;
                        }
                        if (iIPS3 > iIPE3)
                        {
                            iIPS3 = Int64.Parse(IPS3.ToString().Replace(".", ""));
                            iIPS2 = iIPS2 + 1;
                        }
                        if (iIPS2 > iIPE2)
                        {
                            iIPS2 = Int64.Parse(IPS2.ToString().Replace(".", ""));
                            iIPS1 = iIPS1 + 1;
                        }
                        new System.Threading.ManualResetEvent(false).WaitOne(500);
                    }
                    else
                    {
                        file.Close();
                        StartRec = false;
                        button4.BackColor = Color.Black;
                        return;
                    }
                    if (notmouseover)
                        richTextBox2.ScrollToCaret();
                    notmouseover = true;
                    new System.Threading.ManualResetEvent(false).WaitOne(1);
                    if (!StartRec)
                    {
                        file.Close();
                        return;
                    }
                }
            }
        }
        private async void searchRouter(string iPath, string iPName, string rIP1, Int64 iPort, Socket server)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(System.Net.IPAddress.Parse(IPs));
                IPAddress ipAddress = hostEntry.AddressList[0];
                IPEndPoint ip = new IPEndPoint(ipAddress, Convert.ToInt32(iPort));
                server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { Blocking = false, UseOnlyOverlappedIO = true, DontFragment = false, EnableBroadcast = true };
                server.BeginConnect(ip, null, null).AsyncWaitHandle.WaitOne(1, true);
                file.Write(rIP1);
                file.Write(file.NewLine);
                richTextBox2.AppendText("Blocking server IP " + rIP1 + "...\r\n");
                INetFwRule2 newRule;
                INetFwPolicy2 firewallpolicy;
                if ((comboBox2.Text.EndsWith("Outbound") & comboBox2.Text.StartsWith("Outbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                {
                    newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                    if (iPName == "Process Name")
                        newRule.Name = rIP1;
                    else
                        newRule.Name = iPName + ", " + rIP1;
                    if (comboBox1.Text.EndsWith("UDP") & comboBox1.Text.StartsWith("UDP"))
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                    if (comboBox1.Text.EndsWith("TCP") & comboBox1.Text.StartsWith("TCP"))
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                    if (comboBox1.Text.EndsWith("Both") & comboBox1.Text.StartsWith("Both"))
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                    if (iPath != "")
                        newRule.ApplicationName = iPath;
                    newRule.RemoteAddresses = rIP1;
                    newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
                    newRule.Enabled = true;
                    newRule.InterfaceTypes = "All";
                    newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                    newRule.EdgeTraversal = false;
                    firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                    if (iPName == "Process Name")
                        firewallpolicy.Rules.Remove(rIP1);
                    else
                        firewallpolicy.Rules.Remove(iPName + ", " + rIP1);
                    firewallpolicy.Rules.Add(newRule);
                }
                if ((comboBox2.Text.EndsWith("Inbound") & comboBox2.Text.StartsWith("Inbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                {
                    newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                    if (iPName == "Process Name")
                        newRule.Name = rIP1;
                    else
                        newRule.Name = iPName + ", " + rIP1;
                    if (comboBox1.Text.EndsWith("UDP") & comboBox1.Text.StartsWith("UDP"))
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                    if (comboBox1.Text.EndsWith("TCP") & comboBox1.Text.StartsWith("TCP"))
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                    if (comboBox1.Text.EndsWith("Both") & comboBox1.Text.StartsWith("Both"))
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                    if (iPath != "")
                        newRule.ApplicationName = iPath;
                    newRule.RemoteAddresses = rIP1;
                    newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                    newRule.Enabled = true;
                    newRule.InterfaceTypes = "All";
                    newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                    newRule.EdgeTraversal = false;
                    firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                    if (iPName == "Process Name")
                        firewallpolicy.Rules.Remove(rIP1);
                    else
                        firewallpolicy.Rules.Remove(iPName + ", " + rIP1);
                    firewallpolicy.Rules.Add(newRule);
                }
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch {}
            await Task.Delay(1);
        }
        private static void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch { }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    receiveDone.Set();
                }
            }
            catch { }
        }
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static String response = String.Empty;
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                sendDone.Set();
            }
            catch { }
        }
        private static void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), client);
        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private string convert_state(int state)
        {
            string strg_state = "";
            switch (state)
            {
                case MIB_TCP_STATE_CLOSED: strg_state = "CLOSED"; break;
                case MIB_TCP_STATE_LISTEN: strg_state = "LISTEN"; break;
                case MIB_TCP_STATE_SYN_SENT: strg_state = "SYN_SENT"; break;
                case MIB_TCP_STATE_SYN_RCVD: strg_state = "SYN_RCVD"; break;
                case MIB_TCP_STATE_ESTAB: strg_state = "ESTAB"; break;
                case MIB_TCP_STATE_FIN_WAIT1: strg_state = "FIN_WAIT1"; break;
                case MIB_TCP_STATE_FIN_WAIT2: strg_state = "FIN_WAIT2"; break;
                case MIB_TCP_STATE_CLOSE_WAIT: strg_state = "CLOSE_WAIT"; break;
                case MIB_TCP_STATE_CLOSING: strg_state = "CLOSING"; break;
                case MIB_TCP_STATE_LAST_ACK: strg_state = "LAST_ACK"; break;
                case MIB_TCP_STATE_TIME_WAIT: strg_state = "TIME_WAIT"; break;
                case MIB_TCP_STATE_DELETE_TCB: strg_state = "DELETE_TCB"; break;
            }
            return strg_state;
        }
        private const int NO_ERROR = 0;
        private const int MIB_TCP_STATE_CLOSED = 1;
        private const int MIB_TCP_STATE_LISTEN = 2;
        private const int MIB_TCP_STATE_SYN_SENT = 3;
        private const int MIB_TCP_STATE_SYN_RCVD = 4;
        private const int MIB_TCP_STATE_ESTAB = 5;
        private const int MIB_TCP_STATE_FIN_WAIT1 = 6;
        private const int MIB_TCP_STATE_FIN_WAIT2 = 7;
        private const int MIB_TCP_STATE_CLOSE_WAIT = 8;
        private const int MIB_TCP_STATE_CLOSING = 9;
        private const int MIB_TCP_STATE_LAST_ACK = 10;
        private const int MIB_TCP_STATE_TIME_WAIT = 11;
        private const int MIB_TCP_STATE_DELETE_TCB = 12;
        public struct MIB_UDPTABLE
        {
            public int dwNumEntries;
            public MIB_UDPROW[] table;
        }
        public struct MIB_UDPROW
        {
            public IPEndPoint Local;
        }
        public MIB_UDPTABLE UdpConnexion;
        public MIB_TCPTABLE TcpConnexion;
        public struct MIB_TCPTABLE
        {
            public int dwNumEntries;
            public MIB_TCPROW[] table;
        }
        public struct MIB_TCPROW
        {
            public string StrgState;
            public int iState;
            public IPEndPoint Local;
            public IPEndPoint Remote;
        }
        public struct MIB_EXTCPROW
        {
            public string StrgState;
            public int iState;
            public IPEndPoint Local;
            public IPEndPoint Remote;
            public int dwProcessId;
            public string ProcessName;
        }
        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetTcpTable(byte[] pTcpTable, out int pdwSize, bool bOrder);
        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetUdpTable(byte[] UcpTable, out int pdwSize, bool bOrder);
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(IPAddress DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);
        private static bool Start = false;
        private static uint CurrentResolution = 0;
        private static double nbmemoryflooded = 0;
        private static IntPtr processPointer;
        private static Int64 baseAddress;
        private static Int64 lastAddress;
        private static DateTime basedate = new DateTime(1970, 1, 1);
        private static TimeSpan diff = DateTime.Now - basedate;
        private static double watch1, watch2;
        private static Process proc;
        private static string input;
        private static bool Closinggetstate;
        private static int wd = 2;
        private static int wu = 2;
        private static int timeout = 0;
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt64 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        private static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt64 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        private static extern bool VirtualAllocEx(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr size, uint flAllocationType, uint lpflOldProtect);
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, IntPtr size, uint flNewProtect, out uint lpflOldProtect);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint lType;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION64
        {
            public ulong BaseAddress;
            public ulong AllocationBase;
            public int AllocationProtect;
            public int __alignment1;
            public ulong RegionSize;
            public int State;
            public int Protect;
            public int Type;
            public int __alignment2;
        }
        public enum AllocationProtect : uint
        {
            PAGE_EXECUTE = 0x00000010,
            PAGE_EXECUTE_READ = 0x00000020,
            PAGE_EXECUTE_READWRITE = 0x00000040,
            PAGE_EXECUTE_WRITECOPY = 0x00000080,
            PAGE_NOACCESS = 0x00000001,
            PAGE_READONLY = 0x00000002,
            PAGE_READWRITE = 0x00000004,
            PAGE_WRITECOPY = 0x00000008,
            PAGE_GUARD = 0x00000100,
            PAGE_NOCACHE = 0x00000200,
            PAGE_WRITECOMBINE = 0x00000400
        }
        const uint DELETE = 0x00010000;
        const uint READ_CONTROL = 0x00020000;
        const uint WRITE_DAC = 0x00040000;
        const uint WRITE_OWNER = 0x00080000;
        const uint SYNCHRONIZE = 0x00100000;
        const uint END = 0xFFF; //if you have Windows XP or Windows Server 2003 you must change this to 0xFFFF
        const uint PROCESS_ALL_ACCESS = DELETE | READ_CONTROL | WRITE_DAC | WRITE_OWNER | SYNCHRONIZE | END;
        const uint PROCESS_VM_OPERATION = 0x0008;
        const uint PROCESS_VM_READ = 0x0010;
        const uint PROCESS_VM_WRITE = 0x0020;
        public enum AllocationType : int
        {
            commit = 0x1000
        }
        private void NewB_thrfloodstart(uint dwdesiredaccess)
        {
            DialogResult result = DialogResult.OK;
            if (textBox2.Text == "4")
            {
                processPointer = OpenProcess(dwdesiredaccess, 1, (uint)4);
                textBox1.Text = "System";
                baseAddress = 0;
                lastAddress = Convert.ToInt32("0X0FFFFFFF", 16);
                result = MessageBox.Show("The memory used will be up to 2 GB, want you to continue ?", "", MessageBoxButtons.OKCancel);
                if (result != DialogResult.Cancel)
                {
                    backgroundWorkerflood.DoWork += new DoWorkEventHandler(NewB_thrflood);
                    backgroundWorkerflood.RunWorkerAsync();
                }
                else
                {
                    wd = 2;
                    wu = 2;
                    Start = false;
                    button1.BackColor = Color.Black;
                    MessageBox.Show("You can still flood a service like Client DNS or ICS");
                }
            }
            else
            {
                try
                {
                    proc = null;
                    new System.Threading.ManualResetEvent(false).WaitOne(100);
                    input = textBox2.Text;
                    if (input != "PID" & input != "")
                        proc = Process.GetProcessById(Int32.Parse(input));
                    if (proc == null)
                    {
                        input = textBox1.Text;
                        proc = Process.GetProcessesByName(input).FirstOrDefault();
                        textBox2.Text = proc.Id.ToString();
                        processPointer = OpenProcess(dwdesiredaccess, 1, (uint)proc.Id);
                    }
                    else
                    {
                        textBox1.Text = proc.ProcessName.ToString();
                        processPointer = OpenProcess(dwdesiredaccess, 1, UInt32.Parse(input));
                    }
                }
                catch
                {
                    proc = null;
                    new System.Threading.ManualResetEvent(false).WaitOne(100);
                    input = textBox1.Text;
                    proc = Process.GetProcessesByName(input).FirstOrDefault();
                    textBox2.Text = proc.Id.ToString();
                    processPointer = OpenProcess(dwdesiredaccess, 1, (uint)proc.Id);
                }
                finally
                {
                    try
                    {
                        baseAddress = proc.MainModule.BaseAddress.ToInt64();
                        lastAddress = baseAddress + proc.MainModule.ModuleMemorySize;
                    }
                    catch
                    {
                        baseAddress = 0;
                        lastAddress = Convert.ToInt32("0X0FFFFFFF", 16);
                        result = MessageBox.Show("The memory used will be up to 2 GB, want you to continue ?", "", MessageBoxButtons.OKCancel);
                    }
                    finally
                    {
                        if (result != DialogResult.Cancel)
                        {
                            backgroundWorkerflood.DoWork += new DoWorkEventHandler(NewB_thrflood);
                            backgroundWorkerflood.RunWorkerAsync();
                        }
                        else
                        {
                            wd = 2;
                            wu = 2;
                            Start = false;
                            button1.BackColor = Color.Black;
                            MessageBox.Show("You can still flood a service like Client DNS or ICS");
                        }
                    }
                }
            }
        }
        private static Random rnd = new Random();
        private static long proc_address;
        private static int m = 0;
        private static IntPtr bytesRead = IntPtr.Zero, bytesWritten = IntPtr.Zero;
        private static int size = Convert.ToInt32("0XEFFFFFF", 16);
        private static long[] proc_address_m = new long[size];
        private static string[] buffer_m = new string[size];
        private static string buffer = "";
        private static bool waitbool = true;
        private static int index = 0, icount = 0;
        private void NewB_thrflood(object sender, DoWorkEventArgs e)
        {
            diff = DateTime.Now - basedate;
            watch1 = diff.TotalMinutes;
            nbmemoryflooded = 0;
            int sizeofbytes = (int)((lastAddress - baseAddress) / 256);
            byte[] buf = new byte[sizeofbytes];
            byte[] array = new byte[sizeofbytes];
            for (; ; )
            {
                try
                {
                    if (!Start)
                        return;
                    if (waitbool)
                    {
                        do
                        {
                            if (!Start)
                                return;
                            proc_address = baseAddress + m;
                            ReadProcessMemory(processPointer, (IntPtr)proc_address, buf, (ulong)sizeofbytes, out bytesRead);
                            buffer = Encoding.Default.GetString(buf);
                            proc_address_m[icount] = proc_address;
                            buffer_m[icount] = buffer;
                            icount++;
                            m += sizeofbytes;
                        } while (proc_address < lastAddress - sizeofbytes);
                        waitbool = false;
                    }
                    index = rnd.Next(icount);
                    array = Encoding.Default.GetBytes(buffer_m[index]);
                    WriteProcessMemory(processPointer, (IntPtr)proc_address_m[index], array, (ulong)sizeofbytes, out bytesWritten);
                    nbmemoryflooded++;
                }
                catch (Exception exception)
                {
                    wd = 2;
                    wu = 2;
                    Start = false;
                    button1.BackColor = Color.Black;
                    MessageBox.Show(exception.ToString());
                    return;
                }
                finally { System.Threading.Thread.Sleep(1); }
            }
        }
        private void button1_Click(object sender, EventArgs e) //flood
        {
            start();
        }
        private void start()
        {
            if (!Start)
            {
                Start = true;
                button1.BackColor = Color.Red;
                NewB_thrfloodstart(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE);
            }
            else
            {
                wd = 2;
                wu = 2;
                Start = false;
                button1.BackColor = Color.Black;
                backgroundWorkerflood.DoWork -= new DoWorkEventHandler(NewB_thrflood);
                diff = DateTime.Now - basedate;
                watch2 = diff.TotalMinutes;
                textBox5.Text = ((int)(watch2 - watch1)).ToString();
                textBox4.Text = Convert.ToString(icount);
                textBox3.Text = Convert.ToString((int)(nbmemoryflooded / icount));
            }
        }
        private void NewB_thr0(object sender, DoWorkEventArgs e)
        {
            for (; ; )
            {
                if (Closinggetstate)
                    return;
                if (GetAsyncKeyState(System.Windows.Forms.Keys.NumPad0))
                {
                    timeout = timeout + 1;
                    if (wd <= 1)
                        wd = wd + 1;
                    wu = 0;
                }
                else
                {
                    if (wu <= 1)
                        wu = wu + 1;
                    wd = 0;
                }
                if (wd == 1)
                    timeout = 0;
                if (wu == 1 & timeout < 10 & !Start)
                    start();
                if (wu == 1 & timeout >= 10 & Start)
                    start();
                new System.Threading.ManualResetEvent(false).WaitOne(100);
            }
        }
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        public enum AllocationProtectEnum : uint
        {
            PAGE_EXECUTE = 0x00000010,
            PAGE_EXECUTE_READ = 0x00000020,
            PAGE_EXECUTE_READWRITE = 0x00000040,
            PAGE_EXECUTE_WRITECOPY = 0x00000080,
            PAGE_NOACCESS = 0x00000001,
            PAGE_READONLY = 0x00000002,
            PAGE_READWRITE = 0x00000004,
            PAGE_WRITECOPY = 0x00000008,
            PAGE_GUARD = 0x00000100,
            PAGE_NOCACHE = 0x00000200,
            PAGE_WRITECOMBINE = 0x00000400
        }
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern Int32 CloseHandle(IntPtr hObject);
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out UInt32 lpdwProcessId);
        [System.Flags]
        private enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY = 0x00000800,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }
        [DllImport("kernel32.dll")]
        private static extern Int32 WSADuplicateSocketW(Socket s, int dwProcessId, LPWSAPROTOCOLINFO lpProtocolInfo);
        private enum LPWSAPROTOCOLINFO : uint
        {
            lpProtocolInfo = 0x00000001
        }
        [DllImport("ws2_32.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr WSASocket(ADDRESS_FAMILIES af, SOCKET_TYPE socket_type, PROTOCOL protocol,
              LPWSAPROTOCOLINFO lpProtocolInfo, Int32 group, OPTION_FLAGS_PER_SOCKET dwFlags);
        private enum ADDRESS_FAMILIES : short
        {
            AF_UNSPEC = 0
        }
        private enum SOCKET_TYPE : short
        {
            SOCK_DGRAM = 2
        }
        private enum PROTOCOL : short
        {
            IPPROTO_UDP = 17
        }
        private enum OPTION_FLAGS_PER_SOCKET : short
        {
            SO_DONTROUTE = 0x0010,
            SO_BROADCAST = 0x0020,
            SO_USELOOPBACK = 0x0040
        }
        private void button2_Click(object sender, EventArgs e) //save
        {
            String charstore;
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                charstore = saveFileDialog1.FileName;
                System.IO.StreamWriter file = new System.IO.StreamWriter(charstore);
                file.WriteLine(textBox1.Text);
                file.WriteLine(textBox2.Text);
                file.WriteLine(textBoxS1.Text);
                file.WriteLine(textBoxE1.Text);
                file.WriteLine(textBoxS2.Text);
                file.WriteLine(textBoxS3.Text);
                file.WriteLine(textBoxS4.Text);
                file.WriteLine(textBoxE4.Text);
                file.WriteLine(textBoxE3.Text);
                file.WriteLine(textBoxE2.Text);
                file.WriteLine(textBox21.Text);
                file.WriteLine(richTextBox1.Text);
                file.WriteLine(checkBox1.Checked);
                file.WriteLine(comboBox1.Text);
                file.WriteLine(comboBox2.Text);
                file.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e) //open
        {
            String myRead;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                myRead = openFileDialog1.FileName;
                System.IO.StreamReader file = new System.IO.StreamReader(myRead);
                textBox1.Text = file.ReadLine();
                textBox2.Text = file.ReadLine();
                textBoxS1.Text = file.ReadLine();
                textBoxE1.Text = file.ReadLine();
                textBoxS2.Text = file.ReadLine();
                textBoxS3.Text = file.ReadLine();
                textBoxS4.Text = file.ReadLine();
                textBoxE4.Text = file.ReadLine();
                textBoxE3.Text = file.ReadLine();
                textBoxE2.Text = file.ReadLine();
                textBox21.Text = file.ReadLine();
                richTextBox1.Text = file.ReadLine();
                checkBox1.Checked = bool.Parse(file.ReadLine());
                comboBox1.SelectedItem = file.ReadLine();
                comboBox2.SelectedItem = file.ReadLine();
                file.Close();
            }
        }
        private static System.IO.StreamWriter file = null;
        private bool StartRec = false;
        private static string charstore = null;
        private void button4_Click(object sender, EventArgs e) //block IP
        {
            if (!StartRec)
            {
                StartRec = true;
                button4.BackColor = Color.Red;
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    charstore = saveFileDialog1.FileName;
                    Thrf = new System.Threading.Thread(new System.Threading.ThreadStart(NewB_thrf));
                    Thrf.Start();
                }
            }
            else
            {
                StartRec = false;
                button4.BackColor = Color.Black;
            }
        }
        private static bool StartList = false;
        private static string myReadl = null;
        private static string IPl = null;
        private static System.IO.StreamReader filel;
        private void button5_Click(object sender, EventArgs e) //block IP in list
        {
            if (!StartList)
            {
                StartList = true;
                button5.BackColor = Color.Red;
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    myReadl = openFileDialog1.FileName;
                    Thrl = new System.Threading.Thread(new System.Threading.ThreadStart(NewB_thrl));
                    Thrl.Start();
                }
            }
            else
            {
                button5.BackColor = Color.Black;
                StartList = false;
            }
        }
        private void NewB_thrl()
        {
            string path = richTextBox1.Text;
            string iPath = path.ToString().Replace("Path of the program: ", "");
            string pname = textBox1.Text;
            string iPName = pname.ToString();
            INetFwRule2 newRule;
            INetFwPolicy2 firewallpolicy;
            filel = new System.IO.StreamReader(myReadl);
            for (; ; )
            {
                try
                {
                    IPl = filel.ReadLine();
                    if ((comboBox2.Text.EndsWith("Outbound") & comboBox2.Text.StartsWith("Outbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                    {
                        newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                        if (iPName == "Process Name")
                            newRule.Name = IPl;
                        else
                            newRule.Name = iPName + ", " + IPl;
                        if (comboBox1.Text.EndsWith("UDP") & comboBox1.Text.StartsWith("UDP"))
                            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                        if (comboBox1.Text.EndsWith("TCP") & comboBox1.Text.StartsWith("TCP"))
                            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                        if (comboBox1.Text.EndsWith("Both") & comboBox1.Text.StartsWith("Both"))
                            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                        if (iPath != "")
                            newRule.ApplicationName = iPath;
                        newRule.RemoteAddresses = IPl;
                        newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
                        newRule.Enabled = true;
                        newRule.InterfaceTypes = "All";
                        newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                        newRule.EdgeTraversal = false;
                        firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                        if (iPName == "Process Name")
                            firewallpolicy.Rules.Remove(IPl);
                        else
                            firewallpolicy.Rules.Remove(iPName + ", " + IPl);
                        firewallpolicy.Rules.Add(newRule);
                    }
                    if ((comboBox2.Text.EndsWith("Inbound") & comboBox2.Text.StartsWith("Inbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                    {
                        newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                        if (iPName == "Process Name")
                            newRule.Name = IPl;
                        else
                            newRule.Name = iPName + ", " + IPl;
                        if (comboBox1.Text.EndsWith("UDP") & comboBox1.Text.StartsWith("UDP"))
                            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                        if (comboBox1.Text.EndsWith("TCP") & comboBox1.Text.StartsWith("TCP"))
                            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                        if (comboBox1.Text.EndsWith("Both") & comboBox1.Text.StartsWith("Both"))
                            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                        if (iPath != "")
                            newRule.ApplicationName = iPath;
                        newRule.RemoteAddresses = IPl;
                        newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                        newRule.Enabled = true;
                        newRule.InterfaceTypes = "All";
                        newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                        newRule.EdgeTraversal = false;
                        firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                        if (iPName == "Process Name")
                            firewallpolicy.Rules.Remove(IPl);
                        else
                            firewallpolicy.Rules.Remove(iPName + ", " + IPl);
                        firewallpolicy.Rules.Add(newRule);
                    }
                    if (!StartList)
                    {
                        filel.Close();
                        return;
                    }
                    new System.Threading.ManualResetEvent(false).WaitOne(1);
                }
                catch
                {
                    filel.Close();
                    button5.BackColor = Color.Black;
                    StartList = false;
                    return;
                }
                if (string.IsNullOrEmpty(IPl))
                {
                    filel.Close();
                    button5.BackColor = Color.Black;
                    StartList = false;
                    return;
                }
            }
        }
        private void richTextBox2_MouseHover(object sender, EventArgs e)
        {
            notmouseover = false;
        }
        private void richTextBox2_MouseLeave(object sender, EventArgs e)
        {
            notmouseover = true;
        }
        private void richTextBox2_VScroll(object sender, EventArgs e)
        {
            notmouseover = false;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            savePathInitFile();
            Start = false;
            Closinggetstate = true;
            StartRec = false;
            StartList = false;
            try
            {
                bool ipsin = false;
                if (StartRec & checkBox1.Checked)
                {
                    using (System.IO.FileStream fsr = new System.IO.FileStream("temp.txt", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read))
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(fsr))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                if (IPs == line)
                                    ipsin = true;
                                new System.Threading.ManualResetEvent(false).WaitOne(1);
                            }
                        }
                    }
                    if (!ipsin)
                        using (System.IO.FileStream fsw = new System.IO.FileStream("temp.txt", System.IO.FileMode.Append, System.IO.FileAccess.Write))
                        {
                            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fsw))
                            {
                                sw.WriteLine(IPs);
                            }
                        }
                    INetFwRule2 newRule;
                    INetFwPolicy2 firewallpolicy;
                    if ((comboBox2.Text.EndsWith("Outbound") & comboBox2.Text.StartsWith("Outbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                    {
                        newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                        newRule.Name = "Warning, " + IPs;
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                        newRule.RemoteAddresses = IPs;
                        newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
                        newRule.Enabled = true;
                        newRule.InterfaceTypes = "All";
                        newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                        newRule.EdgeTraversal = false;
                        firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                        firewallpolicy.Rules.Remove("Warning, " + IPs);
                        firewallpolicy.Rules.Add(newRule);
                    }
                    if ((comboBox2.Text.EndsWith("Inbound") & comboBox2.Text.StartsWith("Inbound")) | (comboBox2.Text.EndsWith("Both") & comboBox2.Text.StartsWith("Both")))
                    {
                        newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                        newRule.Name = "Warning, " + IPs;
                        newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                        newRule.RemoteAddresses = IPs;
                        newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                        newRule.Enabled = true;
                        newRule.InterfaceTypes = "All";
                        newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                        newRule.EdgeTraversal = false;
                        firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                        firewallpolicy.Rules.Remove("Warning, " + IPs);
                        firewallpolicy.Rules.Add(newRule);
                    }
                    file.Close();
                }
            }
            catch { }
            try
            {
                filel.Close();
            }
            catch { }
            TimeEndPeriod(1);
        }
        public void savePathInitFile()
        {
            System.IO.StreamWriter initfile = new System.IO.StreamWriter("initexe.txt");
            initfile.WriteLine(textBox1.Text);
            initfile.WriteLine(textBox2.Text);
            initfile.Close();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
            process.PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("initexe.txt");
                textBox1.Text = file.ReadLine();
                textBox2.Text = file.ReadLine();
                file.Close();
            }
            catch
            {
                using (System.IO.StreamWriter createdfile = System.IO.File.AppendText("initexe.txt"))
                {
                    createdfile.WriteLine("Process Name");
                    createdfile.WriteLine("PID");
                    createdfile.Close();
                }
            }
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            backgroundWorker0.DoWork += new DoWorkEventHandler(NewB_thr0);
            backgroundWorker0.RunWorkerAsync();
        }
    }
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}