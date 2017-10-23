using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;

namespace vsTunnel
{
    public class Logic
    {
        public static Socket sender = null;
        public static int number;
        private static object syncObj = new object();
        public static string sessionName;

        private Action _stopHandler;
        private Action<string> _logHandler;

        public Logic(Action onStopHandler, Action<string> onLogHandler)
        {
            _stopHandler = onStopHandler;
            _logHandler = onLogHandler;
        }

        public Socket StartClient(string ip, string port)
        {
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());

                IPAddress ipAddress = IPAddress.Parse(ip);//ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, int.Parse(port));

                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    //Console.WriteLine("Socket connected to {0}",
                    //    sender.RemoteEndPoint.ToString());

                    return sender;

                }
                catch (ArgumentNullException ane)
                {
                    //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    Log(ane.ToString());
                }
                catch (SocketException se)
                {
                    //Console.WriteLine("SocketException : {0}", se.ToString());
                    Log(se.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    Log(e.ToString());
                }

            }
            catch (Exception e)
            {
                Log(e.ToString());
                //Console.WriteLine(e.ToString());
            }

            return null;
        }

        

        public List<byte[]> send(Settings settings, Socket sender, List<byte[]> data)
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

            //string reciiveData = string.Empty;

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
                //reciiveData += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                list.Add(truncArray);

                if (bytesRec < 1024)
                    break;
                else isFormat = true;
                /*if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }*/
            }

            if (isFormat)
                list = Utils.Format(list);

            var bI = 0;
            foreach (var b in list)
            {
                //var tmp = Guid.NewGuid() + ".txt";
                bI++;
                var name = Utils.CreateName(sessionName, "rd_to_vs", bI, list.Count, ref syncObj, ref number) + ".bin";
                var file = Path.Combine(Path.GetFullPath(settings.sessionDirectory), name);
                var Writer = new BinaryWriter(File.OpenWrite(file));
                Writer.Write(b);

                Writer.Flush();
                Writer.Close();
                //File.Move(tmp, name);

                //File.WriteAllText(name, reciiveData, Encoding.ASCII);
            }

            //Log("rd_to_vs: " + reciiveData);
            Log("vs to rd: send " + list.Sum(i => i.Length) + " bytes" + (list.Count > 1 ? string.Format(" ({0} parts)", list.Count) : ""));

            // Release the socket.
            //sender.Shutdown(SocketShutdown.Both);
            //sender.Close();

            // return reciiveData;
            return list;
        }

        //private Socket listener;

        private Socket listener;

        public void StartListening(Settings settings)
        {
            IPAddress ipAddress = IPAddress.Parse(settings.ip); //ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, int.Parse(settings.portVS));

            listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //listener.Blocking = false;
                listener.Bind(localEndPoint);
                listener.Listen(1/*10*/);

                //performListen(listener, settings);
                Socket handler = listener.Accept();

                handling(handler, settings);
            }
            catch (Exception e)
            {
                Log(e.ToString());

                //Console.WriteLine(e.ToString());
                //CloseNode(ref Handler);
                CloseNode(ref sender);
                CloseNode(ref listener);

                StopHandler();
            }
        }

        /*private void performListen(Socket listener, Settings settings)
        {
            listener.BeginAccept(new AsyncCallback(AcceptCallback), new Utils.StateListen
            {
                listener = listener,
                settings = settings
            });
        }

        public Socket Handler;

        private void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request
            Utils.StateListen statelistener = (Utils.StateListen)ar.AsyncState;
            Socket handler = statelistener.listener.EndAccept(ar);
            Handler = handler;

            // Create the state object
            /*StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);#1#
            handling(handler, statelistener.settings);
        }*/

        private void handling(Socket handler, Settings settings)
        {
            byte[] bytes = new Byte[1024];

            //string data = null;

            try
            {
                List<string> files = new List<string>();
                if (!settings.watchDirectory)
                {
                    var directory = new DirectoryInfo(settings.sessionsDirectory);
                    foreach (var fileInfo in directory.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        files.Add(fileInfo.FullName);
                    }
                }

                while (true)
                {
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
                        //data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (bytesRec < 1024)
                            break;
                        else isFormat = true;
                        /*if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }*/
                    }

                    if (isFormat)
                        list = Utils.Format(list);

                    var bI = 0;
                    foreach (var b in list)
                    {
                        //var tmp = Guid.NewGuid() + ".txt";
                        bI++; //bI == list.Count
                        var name = Utils.CreateName(sessionName, "vs_to_rd", bI, list.Count, ref syncObj, ref number) + ".bin";
                        var file = Path.Combine(Path.GetFullPath(settings.sessionDirectory), name);
                        var Writer = new BinaryWriter(File.OpenWrite(file));
                        Writer.Write(b);
                        Writer.Flush();
                        Writer.Close();
                        //File.Move(tmp, name);

                        //File.WriteAllText(name, data, Encoding.ASCII);
                    }

                    Log("vs to rd: send " + list.Sum(i => i.Length) + " bytes" + (list.Count > 1 ? string.Format(" ({0} parts)", list.Count) : ""));

                   

                    /*if (sender == null)
                        sender = StartClient(settings.ip, settings.portDebugger);*/

                    //var reciiveData = send(sender, data);

                    List<byte[]> reciiveData = new List<byte[]>();

                    var isHandled = false;

                    FileSystemWatcher watcher = null;

                    var action = new Action<string, string>((name, fullpatch) =>
                    {
                        if(name.Contains("rd_to_vs"))
                        {
                            var n = int.Parse(name.Split('.').First());
                            if (n > number) number = n;

                            var Reader = new BinaryReader(File.OpenRead(fullpatch));
                            var b = new byte[Reader.BaseStream.Length];
                            Reader.Read(b, 0, Convert.ToInt32(Reader.BaseStream.Length));
                            Reader.Close();

                            //reciiveData = new List<byte[]>();
                            reciiveData.Add(b);

                            if (!name.Contains("part") || name.Contains("last"))
                                isHandled = true;
                        }
                    });

                    watcher = Utils.watchDirectory(settings.sessionDirectory, (o, args) =>
                    {
                        var fullpatch = args.FullPath;
                        var name = Path.GetFileName(fullpatch);

                        if (args.ChangeType == WatcherChangeTypes.Changed)
                            action(name, fullpatch);
                    });
                    
                    //var reciiveDataA = send(sender, list);

                    while (!isHandled)
                    {
                        if (!settings.watchDirectory)
                        {
                            var directory = new DirectoryInfo(settings.sessionDirectory);
                            foreach (var fileInfo in directory.GetFiles("*.*", SearchOption.AllDirectories).OrderBy(i => i.LastWriteTime))
                            {
                                if (!files.Contains(fileInfo.FullName))
                                {
                                    var fullpatch = fileInfo.FullName;
                                    var name = Path.GetFileName(fullpatch);

                                    action(name, fullpatch);

                                    files.Add(fileInfo.FullName);
                                }
                            }
                        }
                    }

                    if (watcher != null)
                    {
                        watcher.Dispose();
                        watcher = null;
                    }

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
            catch (Exception ex)
            {
                Log(ex.ToString());

                //CloseNode(ref Handler);
                CloseNode(ref sender);
                CloseNode(ref listener);

                StopHandler();
            }
        }

        public void StopHandler()
        {
            _stopHandler();
        }

        public void Log(string message)
        {
            _logHandler(message);
        }

        public void Stop()
        {
            //CloseNode(ref Handler);
            CloseNode(ref sender);
            CloseNode(ref listener);
        }

        public void CloseNode(ref Socket node)
        {
            try
            {
                if (node != null)
                {
                    node.Shutdown(SocketShutdown.Both);
                    node.Close();
                    node.Dispose();
                    node = null;
                }
            }
            catch (Exception e)
            {
                Log(e.ToString());
            }
        }

        public class Settings
        {
            public string ip;
            public string portVS;
            public string portDebugger;
            public bool useSessionsDirectory;
            public string sessionsDirectory;
            public string vsToRDDirectory;
            public string rdToVSDirectory;

            public string sessionDirectory;

            public bool watchDirectory;
        }

        public void StartWaitFromVS(Settings settings)
        {
            if (settings.useSessionsDirectory)
            {
                var name = "session_" + Guid.NewGuid().ToString().Substring(0, 8);
                sessionName = name;

                var sessionsDirectory = Path.GetFullPath(settings.sessionsDirectory);

                var sessionDirectory = Path.Combine(sessionsDirectory, name);
                settings.sessionDirectory = sessionDirectory;

                //var res = new NetworkShare("Rt-n18u-6d40", "Download2\\Debug", "V", "admin", "raihouse").Connect();

                Directory.CreateDirectory(sessionDirectory);
            }

            /*Directory.CreateDirectory(name + "\\in");
            Directory.CreateDirectory(name + "\\out");*/

            StartListening(settings);
        }

        public void StartWaitToRD(Settings settings)
        {
            try
            {
                List<string> files = new List<string>();
                if (!settings.watchDirectory)
                {
                    var directory = new DirectoryInfo(settings.sessionsDirectory);
                    foreach (var fileInfo in directory.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        files.Add(fileInfo.FullName);
                    }
                }

                while (true)
                {
                    var list = new List<byte[]>();

                    var isHandled = false;

                    FileSystemWatcher watcher = null;

                    var action = new Func<string, string, bool>((name, fullpatch) =>
                    {
                        if (name.Contains("vs_to_rd"))
                        {
                            if (string.IsNullOrEmpty(settings.sessionDirectory))
                            {
                                settings.sessionDirectory = Path.GetDirectoryName(fullpatch);
                                sessionName = Path.GetFileName(settings.sessionDirectory);
                            }

                            if (sender == null)
                                sender = StartClient(settings.ip, settings.portDebugger);

                            var n = int.Parse(name.Split('.').First());
                            if (n > number) number = n;

                            var Reader = new BinaryReader(File.OpenRead(fullpatch));
                            var b = new byte[Reader.BaseStream.Length];
                            Reader.Read(b, 0, Convert.ToInt32(Reader.BaseStream.Length));
                            Reader.Close();

                            //reciiveData = new List<byte[]>();
                            list.Add(b);

                            if (!name.Contains("part") || name.Contains("last"))
                                isHandled = true;

                            return true;
                        }

                        return false;
                    });

                    if(settings.watchDirectory)
                    watcher = Utils.watchDirectory(settings.sessionsDirectory, (o, args) =>
                    {
                        var fullpatch = args.FullPath;
                        var name = Path.GetFileName(fullpatch);
                        
                        if(args.ChangeType == WatcherChangeTypes.Changed)
                            action(name, fullpatch);
                    });

                    while (!isHandled)
                    {
                        if (!settings.watchDirectory)
                        {
Label:
                            var isNotSessionDirectory = string.IsNullOrEmpty(settings.sessionDirectory);
                            var directory = new DirectoryInfo(isNotSessionDirectory
                                ? settings.sessionsDirectory : settings.sessionDirectory);

                            foreach (var fileInfo in directory.GetFiles("*.*", SearchOption.AllDirectories).OrderBy(i => i.Name))
                            {
                                if(!files.Contains(fileInfo.FullName))
                                {
                                    var fullpatch = fileInfo.FullName;
                                    var name = Path.GetFileName(fullpatch);

                                    files.Add(fileInfo.FullName);

                                    if (action(name, fullpatch))
                                    {
                                        if (isNotSessionDirectory && !string.IsNullOrEmpty(settings.sessionDirectory))
                                        {
                                            goto Label;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (watcher != null)
                    {
                        watcher.Dispose();
                        watcher = null;
                    }

                    var reciiveDataA = send(settings, sender, list);
                }
            }
            catch (Exception e)
            {
                Log(e.ToString());

                //CloseNode(ref Handler);
                CloseNode(ref sender);
                CloseNode(ref listener);

                StopHandler();
            }
        }
    }
}