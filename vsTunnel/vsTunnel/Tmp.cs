namespace vsTunnel
{
    public class Tmp
    {
        /*private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //StartClient(textBox1.Text, textBox2.Text);
        }

        public static Socket StartClient(string ip, string port)
        {
            // Data buffer for incoming data.
            //byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());

                IPAddress ipAddress = IPAddress.Parse(ip);//ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, int.Parse(port));

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    //Console.WriteLine("Socket connected to {0}",
                    //    sender.RemoteEndPoint.ToString());

                    return sender;

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }

            return null;
        }

        class Bytes { public byte[] buf { get; set; } public int count { get; set; } }

        private static List<byte[]> Format(List<byte[]> data)
        {
            var list = new List<List<byte[]>>();
            var result = new List<byte[]>();

            List<byte[]> lastItem = null;
            int lastLen = 0;

            foreach (var bytes in data)
            {
                if (lastItem == null)
                {
                    lastItem = new List<byte[]>();
                    lastItem.Add(bytes);
                    lastLen = bytes.Length;
                    list.Add(lastItem);
                }
                else
                {
                    if (lastLen + bytes.Length > 1460)
                    {
                        var one = new byte[1460 - lastLen];
                        Array.Copy(bytes, one, one.Length);
                        lastItem.Add(one);


                        var two = new byte[bytes.Length - one.Length];
                        Array.Copy(bytes, one.Length, two, 0, two.Length);

                        lastItem = new List<byte[]>();
                        lastItem.Add(two);
                        lastLen = two.Length;
                        list.Add(lastItem);
                    }
                    else
                    {
                        lastItem.Add(bytes);
                        lastLen = lastLen + bytes.Length;
                    }
                }
            }

            foreach (var l in list)
            {
                var count = l.Sum(i => i.Length);

                var bytes = new byte[count];
                var startIndex = 0;

                foreach (var b in l)
                {
                    Array.Copy(b, 0, bytes, startIndex, b.Length);
                    startIndex = startIndex + b.Length;
                }

                result.Add(bytes);
            }

            return result;
        }

        public static List<byte[]> send(Socket sender, List<byte[]> data)
        {
            byte[] bytes = new byte[1024];

            // Encode the data string into a byte array.
            //byte[] msg = Encoding.ASCII.GetBytes(data);
            //string msg = Encoding.ASCII.GetString(data);

            // Send the data through the socket.
            //int bytesSent = sender.Send(msg);

            foreach (var b in data)
            {
                int bytesSent = sender.Send(b);
            }
            // Receive the response from the remote device.
            //int bytesRec = sender.Receive(bytes);
            //Console.WriteLine("Echoed test = {0}",
            //    Encoding.ASCII.GetString(bytes, 0, bytesRec));

            string reciiveData = string.Empty;

            var list = new List<byte[]>();
            var isFormat = false;
            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = sender.Receive(bytes);

                if (bytesRec == 0) break;

                byte[] truncArray = new byte[bytesRec];
                Array.Copy(bytes, truncArray, truncArray.Length);

                //if (bytesRec == 0) break;
                reciiveData += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                list.Add(truncArray);

                if (bytesRec < 1024)
                    break;
                else isFormat = true;
                /*if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }#1#
            }

            if (isFormat)
                list = Format(list);


            foreach (var b in list)
            {
                var name = CreateName("rd_to_vs");
                var Writer = new BinaryWriter(File.OpenWrite(name + ".bin"));
                Writer.Write(b);

                Writer.Flush();
                Writer.Close();

                //File.WriteAllText(name, reciiveData, Encoding.ASCII);
            }


            // Release the socket.
            //sender.Shutdown(SocketShutdown.Both);
            //sender.Close();

            // return reciiveData;
            return list;
        }

        private void watchDirectory(string directory)
        {
            var watcher = new FileSystemWatcher
            {
                Path = directory,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            watcher.Changed += Watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static Socket sender = null;
        public static volatile int number;
        public static void StartListening(string ip, string port, string port2)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            string data = null;

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(ip); //ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, int.Parse(port));

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Socket handler = listener.Accept();

                // Start listening for connections.
                while (true)
                {
                    //Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.


                    data = null;

                    // An incoming connection needs to be processed.
                    var list = new List<byte[]>();
                    var isFormat = false;
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);

                        if (bytesRec == 0) break;

                        byte[] truncArray = new byte[bytesRec];
                        Array.Copy(bytes, truncArray, truncArray.Length);

                        //if(bytesRec == 0) break;
                        list.Add(truncArray);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (bytesRec < 1024)
                            break;
                        else isFormat = true;
                        /*if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }#1#
                    }

                    if (isFormat)
                        list = Format(list);

                    foreach (var b in list)
                    {
                        var name = CreateName("vs_to_rd");
                        var Writer = new BinaryWriter(File.OpenWrite(name + ".bin"));
                        Writer.Write(b);
                        Writer.Flush();
                        Writer.Close();

                        //File.WriteAllText(name, data, Encoding.ASCII);
                    }

                    if (sender == null)
                        sender = StartClient(ip, port2);
                    //var reciiveData = send(sender, data);

                    var reciiveData = send(sender, list);

                    // Show the data on the console.
                    //Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.
                    //byte[] msg = Encoding.ASCII.GetBytes(reciiveData);

                    //handler.Send(msg);
                    foreach (var b in reciiveData)
                    {
                        handler.Send(b);
                    }

                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                sender = null;
                listener.Close();
                number = 0;
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();
        }

        private static object syncObj = new object();
        private static string CreateName(string direction)
        {
            string name;
            lock (syncObj)
            {
                number++;
                var len = number.ToString().Length;

                name = sessionName + "\\" + new string('0', 3 - len) + number.ToString() + "." + direction + "." +
                           Guid.NewGuid().ToString() + ".data";

            }

            return name;
        }

        public static string sessionName;

        private void btStartDebugger_Click(object sender, EventArgs e)
        {
            StartWaitFromVS(txtIPAddress.Text, txtPortFromVS.Text, txtPortToRD.Text);
        }

        private void StartWaitFromVS(string ip, string portVS, string portDebugger)
        {
            var name = "session_" + Guid.NewGuid().ToString();
            sessionName = name;
            Directory.CreateDirectory(name);
            /*Directory.CreateDirectory(name + "\\in");
            Directory.CreateDirectory(name + "\\out");#1#
            StartListening(ip, portVS, portDebugger);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private static void StartListenerUDP(string ip, string port)
        {
            bool done = false;

            IPAddress ipAddress = IPAddress.Parse(ip);

            UdpClient listener = new UdpClient(int.Parse(port));
            IPEndPoint groupEP = new IPEndPoint(ipAddress, int.Parse(port));

            try
            {
                while (!done)
                {
                    // Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);



                    /*Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                        groupEP.ToString(),
                        Encoding.ASCII.GetString(bytes, 0, bytes.Length));#1#
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }

        public struct Received
        {
            public IPEndPoint Sender;
            public byte[] Message;
        }

        abstract class UdpBase
        {
            protected UdpClient Client;

            protected UdpBase()
            {
                Client = new UdpClient();
            }

            public async Task<Received> Receive()
            {
                var result = await Client.ReceiveAsync();
                return new Received()
                {
                    Message = result.Buffer,// Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                    Sender = result.RemoteEndPoint
                };
            }
        }

        //Server
        class UdpListener : UdpBase
        {
            private IPEndPoint _listenOn;

            public UdpListener() : this(new IPEndPoint(IPAddress.Any, 32123))
            {
            }

            public UdpListener(IPEndPoint endpoint)
            {
                _listenOn = endpoint;
                Client = new UdpClient(_listenOn);
            }

            public void Reply(string message, IPEndPoint endpoint)
            {
                var datagram = Encoding.ASCII.GetBytes(message);
                Client.Send(datagram, datagram.Length, endpoint);
            }

        }

        //Client
        class UdpUser : UdpBase
        {
            private UdpUser() { }

            public static UdpUser ConnectTo(string hostname, int port)
            {
                var connection = new UdpUser();
                connection.Client.Connect(hostname, port);
                return connection;
            }

            public void Send(string message)
            {
                var datagram = Encoding.ASCII.GetBytes(message);
                Client.Send(datagram, datagram.Length);
            }

        }

        private void rbListenFromVS_CheckedChanged(object sender, EventArgs e)
        {
            txtPortFromVS.Enabled = rbListenFromVS.Enabled;
            txtPortToRD.Enabled = !rbListenFromVS.Enabled;
        }

        private void rbConnectToRD_CheckedChanged(object sender, EventArgs e)
        {
            txtPortFromVS.Enabled = !rbListenFromVS.Enabled;
            txtPortToRD.Enabled = rbListenFromVS.Enabled;
        }

        private void rbSession_CheckedChanged(object sender, EventArgs e)
        {
            txtSessionDirectory.Enabled = rbSession.Enabled;
            txtVSToRDDirectory.Enabled = !rbSession.Enabled;
            txtRDToVSDirectory.Enabled = !rbSession.Enabled;
        }

        private void rbSeparately_CheckedChanged(object sender, EventArgs e)
        {
            txtSessionDirectory.Enabled = !rbSession.Enabled;
            txtVSToRDDirectory.Enabled = rbSession.Enabled;
            txtRDToVSDirectory.Enabled = rbSession.Enabled;
        }

        /*static void StartUdp(string ip, string port1, string port2)
        {
            //create a new server
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint groupEP = new IPEndPoint(ipAddress, int.Parse(port1));
            var server = new UdpListener(groupEP);

            //start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async () => {
                while (true)
                {
                    var received = await server.Receive();
                    //server.Reply("copy " + received.Message, received.Sender);

                    var client = UdpUser.ConnectTo(ip, port2);

                    Task.Factory.StartNew(async () => {
                        while (true)
                        {
                            try
                            {
                                var received = await client.Receive();
                                Console.WriteLine(received.Message);
                                if (received.Message.Contains("quit"))
                                    break;
                            }
                            catch (Exception ex)
                            {
                                // Debug.Write(ex);
                            }
                        }
                    });

                    client.Send(received.Message);

                    /*if (received.Message == "quit")
                        break;#2#
                }
            });
        }#1#*/


        /*public void StartListeningOld(string ip, string port, string port2)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            string data = null;

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(ip); //ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, int.Parse(port));

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(1/*10#1#);
                listener.Blocking = false;

                var a = listener.AcceptAsync(new SocketAsyncEventArgs());

                //Socket handler = listener.Accept();
                Socket handler = null;
                // Start listening for connections.
                while (true)
                {
                    //Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.


                    data = null;

                    // An incoming connection needs to be processed.
                    var list = new List<byte[]>();
                    var isFormat = false;
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);

                        if (bytesRec == 0) break;

                        byte[] truncArray = new byte[bytesRec];
                        Array.Copy(bytes, truncArray, truncArray.Length);

                        //if(bytesRec == 0) break;
                        list.Add(truncArray);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (bytesRec < 1024)
                            break;
                        else isFormat = true;
                        /*if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }#1#
                    }

                    if (isFormat)
                        list = list = Utils.Format(list);

                    foreach (var b in list)
                    {
                        var name = Utils.CreateName(sessionName, "vs_to_rd", ref syncObj, ref number);
                        var Writer = new BinaryWriter(File.OpenWrite(name + ".bin"));
                        Writer.Write(b);
                        Writer.Flush();
                        Writer.Close();

                        //File.WriteAllText(name, data, Encoding.ASCII);
                    }

                    if (sender == null)
                        sender = StartClient(ip, port2);
                    //var reciiveData = send(sender, data);


                    var reciiveData = send(sender, list);

                    // Show the data on the console.
                    //Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.
                    //byte[] msg = Encoding.ASCII.GetBytes(reciiveData);

                    //handler.Send(msg);
                    foreach (var b in reciiveData)
                    {
                        handler.Send(b);
                    }

                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                sender = null;
                listener.Close();
                number = 0;
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();
        }*/
    }
}