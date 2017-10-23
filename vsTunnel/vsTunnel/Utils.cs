using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace vsTunnel
{
    public static class Utils
    {
        public class Bytes
        {
            public byte[] buf { get; set; }
            public int count { get; set; }
        }

        public class StateListen
        {
            public Socket listener;
            public Logic.Settings settings;
        }

        public static List<byte[]> Format(List<byte[]> data)
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

        public static string CreateName(string sessionName, string direction, int index, int count, ref object syncObj, ref int number)
        {
            string name;
            lock (syncObj)
            {
                number++;
                var len = number.ToString().Length;

                var partName = count > 1 ? "_part_" + new string('0', 2 - index.ToString().Length) + index.ToString() 
                    + (index == count ? "_last" : string.Empty)
                    : string.Empty;

                var tmp = new string('0', 3 - len) + number.ToString() + "." + direction + partName + "." +
                          Guid.NewGuid().ToString().Substring(0, 8) + ".data";

                name = tmp;

            }

            return name;
        }

        public static FileSystemWatcher watchDirectory(string directory, Action<object, FileSystemEventArgs> action)
        {
            var watcher = new FileSystemWatcher
            {
                Path = directory,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*",
                IncludeSubdirectories = true
            };
            watcher.Changed += new FileSystemEventHandler(action);
            watcher.EnableRaisingEvents = true;

            return watcher;
        }
    }
}