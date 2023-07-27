using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;
using NetFwTypeLib;
using System.Net.NetworkInformation;
namespace FreeMyPlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool getstate = false;
        public static ThreadStart threadstart;
        public static Thread thread;
        private static List<string> scaledips = new List<string>();
        private static string hostname = "";
        private static INetFwRule2 newRule;
        private static INetFwPolicy2 firewallpolicy;
        private static string RemoteAdrr = "0.0.0.0", Scaledip = "0.0.0.0";
        private static IPAddress Addr;
        private static bool checking, checkingname;
        private static List<string> names = new List<string>();
        private static ProcessStartInfo startInfo;
        private static string Game = "", DNSIP = "", namein;
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (File.Exists("tempsave"))
            {
                using (StreamReader file = new StreamReader("tempsave"))
                {
                    comboBox1.SelectedItem = file.ReadLine();
                    comboBox2.SelectedItem = file.ReadLine();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Game = comboBox1.SelectedItem.ToString().Replace("Game ", "");
            if (Game == "Fortnite")
            {
                DialogResult result = MessageBox.Show(@"Open Fortnite game executable : ""FortniteClient-Win64-Shipping.exe"" !", "Open", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "All Files(*.*)|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        rulesToFirewall("everything", "", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "");
                        rulesToFirewall("alllocalports", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "0-49151", "");
                        rulesToFirewall("alllocalports", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "0-49151", "");
                        rulesToFirewall("allports", "0.0.0.1-223.255.255.255", false, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "", "", "");
                        rulesToFirewall("be", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "BEService", "", "");
                        rulesToFirewall("dhcp", "224.0.0.0-255.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "Dhcp", "", "");
                        rulesToFirewall("eac", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "EasyAntiCheat", "", "");
                        rulesToFirewall("epiceoso", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ProgramFiles% (x86)\Epic Games\Launcher\Portal\Extras\Overlay\EOSOverlayRenderer-Win64-Shipping.exe", "", "49152-65535", "1-49151");
                        rulesToFirewall("epichelper", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ProgramFiles% (x86)\Epic Games\Launcher\Engine\Binaries\Win64\EpicWebHelper.exe", "", "49152-65535", "80, 443");
                        rulesToFirewall("epiclauncher", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ProgramFiles% (x86)\Epic Games\Launcher\Portal\Binaries\Win64\EpicGamesLauncher.exe", "", "49152-65535", "80, 443");
                        rulesToFirewall("fortniteauth", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, op.FileName, "", "49152-65535", "443");
                        rulesToFirewall("fortniteserver", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, op.FileName, "", "49152-65535", "1024-49151");
                        rulesToFirewall("127.0.0.0-127.0.255.255", "127.0.0.0-127.0.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "");
                        rulesToFirewall("0.0.0.0-0.0.255.255", "0.0.0.0-0.0.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "");
                    }
                }
            }
            if (Game == "Call of Duty")
            {
                DialogResult result = MessageBox.Show(@"Open Vanguard game executable : ""Vanguard.exe"" !", "Open", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Filter = "All Files(*.*)|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        rulesToFirewall("everything", "", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "");
                        rulesToFirewall("alllocalports", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "0-1023", "");
                        rulesToFirewall("alllocalports", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "0-1023", "");
                        rulesToFirewall("alllocalports", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "49152-65535");
                        rulesToFirewall("alllocalports", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "49152-65535");
                        rulesToFirewall("allports", "0.0.0.1-223.255.255.255", false, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "", "", "");
                        rulesToFirewall("dhcp", "224.0.0.0-255.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "Dhcp", "", "");
                        rulesToFirewall("battlenetagentauth", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ALLUSERSPROFILE%\Battle.net\Agent\Agent.7614\Agent.exe", "", "1024-65535", "1-49151");
                        rulesToFirewall("battlenetagentserver", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ALLUSERSPROFILE%\Battle.net\Agent\Agent.7614\Agent.exe", "", "1024-65535", "1-49151");
                        rulesToFirewall("battlenetauth", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ProgramFiles% (x86)\Battle.net\Battle.net.exe", "", "1024-65535", "1-49151");
                        rulesToFirewall("battlenetserver", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, @"%ProgramFiles% (x86)\Battle.net\Battle.net.exe", "", "1024-65535", "1-49151");
                        rulesToFirewall("vanguardauth", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, op.FileName, "", "1024-65535", "80, 443, 1119, 3074-3075");
                        rulesToFirewall("vanguardauth", "0.0.0.1-223.255.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, op.FileName, "", "3074-3075", "1-49151");
                        rulesToFirewall("vanguardserver", "24.105.52.1-24.105.52.254, 24.105.53.1-24.105.53.254, 24.105.54.1-24.105.54.254, 185.34.107.128", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, op.FileName, "", "1024-65535", "3074");
                        rulesToFirewall("vanguardserver", "24.105.52.1-24.105.52.254, 24.105.53.1-24.105.53.254, 24.105.54.1-24.105.54.254, 185.34.107.128", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, op.FileName, "", "3074", "1024-49151");
                        rulesToFirewall("127.0.0.0-127.0.255.255", "127.0.0.0-127.0.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "");
                        rulesToFirewall("0.0.0.0-0.0.255.255", "0.0.0.0-0.0.255.255", true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, "", "", "", "");
                    }
                }
            }
            DNSIP = comboBox1.SelectedItem.ToString().Replace("DNS ", "");
            rulesToFirewall("dns", DNSIP, true, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, "", "Dnscache", "49152-65535", "53");
            string readtext = File.ReadAllText("setup.cmd");
            string readtextreplaced = readtext.Replace("DNSIP", DNSIP);
            File.WriteAllText("setup.cmd", readtextreplaced);
            try
            {
                startInfo = new ProcessStartInfo("setup.cmd");
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Verb = "runas";
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            File.WriteAllText("setup.cmd", readtext);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!getstate)
            {
                getstate = true;
                button2.Text = "Stop";
                DNSIP = comboBox1.SelectedItem.ToString().Replace("DNS ", "");
                Game = comboBox2.SelectedItem.ToString().Replace("Game ", "");
                using (StreamReader file = new StreamReader(Game))
                {
                    while (true)
                    {
                        string name = file.ReadLine();
                        if (name == "")
                        {
                            file.Close();
                            break;
                        }
                        else
                            names.Add(name);
                    }
                }
                Task.Run(() => Start());
            }
            else
            {
                getstate = false;
                button2.Text = "Start";
            }
        }
        public void Start()
        {
            while (getstate)
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
                foreach (TcpConnectionInformation connection in connections)
                {
                    try
                    {
                        Addr = connection.RemoteEndPoint.Address;
                        RemoteAdrr = Addr.ToString();
                        if (RemoteAdrr != "::1" & RemoteAdrr != "127.0.0.1" & RemoteAdrr != "0.0.0.0" & RemoteAdrr != DNSIP)
                        {
                            Scaledip = getScaleIP(RemoteAdrr);
                            checking = false;
                            foreach (string scaledip in scaledips)
                            {
                                if (Scaledip == scaledip)
                                {
                                    checking = true;
                                    break;
                                }
                                Thread.Sleep(1);
                            }
                            if (!checking)
                            {
                                scaledips.Add(Scaledip);
                                if ((Game == "Fortnite") | (Game == "Call of Duty" & !(IsInRange("37.244.0.0", "37.244.255.255", RemoteAdrr) | IsInRange("24.105.0.0", "24.105.62.255", RemoteAdrr) | IsInRange("185.34.106.0", "185.34.107.255", RemoteAdrr))))
                                {
                                    hostname = Dns.GetHostEntry(RemoteAdrr).HostName;
                                    checkingname = false;
                                    foreach (string name in names)
                                    {
                                        if (hostname.EndsWith(name))
                                        {
                                            namein = name;
                                            checkingname = true;
                                            break;
                                        }
                                        Thread.Sleep(1);
                                    }
                                    if (!checkingname)
                                    {
                                        if (Uri.CheckHostName(hostname) != UriHostNameType.Dns | Addr.IsIPv4MappedToIPv6 | Addr.IsIPv6LinkLocal | Addr.IsIPv6Multicast | Addr.IsIPv6SiteLocal | Addr.IsIPv6Teredo)
                                        {
                                            addToFirewall(Scaledip);
                                            using (StreamWriter createdfile = File.AppendText("record.txt"))
                                            {
                                                createdfile.WriteLine(hostname + ", " + RemoteAdrr);
                                                createdfile.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        using (StreamWriter sw = File.AppendText(namein + ".txt"))
                                        {
                                            sw.WriteLine(hostname + ", " + RemoteAdrr);
                                            sw.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        addToFirewall(Scaledip);
                        using (StreamWriter createdfile = File.AppendText("record.txt"))
                        {
                            createdfile.WriteLine(RemoteAdrr);
                            createdfile.Close();
                        }
                    }
                    if (!getstate)
                    {
                        return;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        private static string getScaleIP(string IP)
        {
            IP = IP.Substring(0, IP.LastIndexOf("."));
            IP = IP.Substring(0, IP.LastIndexOf("."));
            string startip = IP + ".0.0";
            string endip = IP + ".255.255";
            IP = startip + "-" + endip;
            return IP;
        }
        private static void addToFirewall(string IP)
        {
            newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            newRule.Name = IP;
            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
            newRule.RemoteAddresses = IP;
            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            newRule.Enabled = true;
            newRule.InterfaceTypes = "All";
            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            newRule.EdgeTraversal = false;
            firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallpolicy.Rules.Add(newRule);
        }
        private static void rulesToFirewall(string name, string ip, bool enabled, NET_FW_IP_PROTOCOL_ protocol, NET_FW_RULE_DIRECTION_ direction, NET_FW_ACTION_ action, string appname, string svcname, string localports, string remoteports)
        {
            newRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            newRule.Name = name;
            newRule.Protocol = (int)protocol;
            if (ip != "")
                newRule.RemoteAddresses = ip;
            newRule.Enabled = enabled;
            newRule.Direction = direction;
            newRule.InterfaceTypes = "All";
            newRule.Action = action;
            if (appname != "")
                newRule.ApplicationName = appname;
            if (svcname != "")
                newRule.serviceName = svcname;
            if (localports != "")
                newRule.LocalPorts = localports;
            if (remoteports != "")
                newRule.RemotePorts = remoteports;
            newRule.EdgeTraversal = false;
            firewallpolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallpolicy.Rules.Remove(name);
            firewallpolicy.Rules.Add(newRule);
        }
        public static bool IsInRange(string startIpAddr, string endIpAddr, string address)
        {
            long ipStart = BitConverter.ToInt32(IPAddress.Parse(startIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
            long ipEnd = BitConverter.ToInt32(IPAddress.Parse(endIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
            long ip = BitConverter.ToInt32(IPAddress.Parse(address).GetAddressBytes().Reverse().ToArray(), 0);
            return ip >= ipStart && ip <= ipEnd;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            getstate = false;
            using (StreamWriter createdsw = new StreamWriter("tempsave"))
            {
                createdsw.WriteLine(comboBox1.SelectedItem.ToString());
                createdsw.WriteLine(comboBox2.SelectedItem.ToString());
            }
        }
    }
}