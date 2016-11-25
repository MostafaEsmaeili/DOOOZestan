using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Framework.Globals;

namespace Framework.Utility
{
    public class FileUtility
    {
        private const char PART_SEP = '-';
        private const char SUFFIX_SEP = '.';
        private const char DIR_SEP = '/';
        private const char DIR_SEP_WIN = '\\';
        private const char UNSUPPORTED_ALT = '.';
        private static readonly char[] _unsupporteds = new[] { ':', '/', '\\', '*', '"', '?', '<', '>', '|' };


        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            long originalPosition = 0;
            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }
            try
            {
                byte[] readBuffer = new byte[4096];
                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
       
        public static string MakeFileName(string suffix, params object[] parts)
        {
            if (parts.Length == 0)
                return null;

            var buf = new StringBuilder();
            foreach (object part in parts)
            {
                string partString;
                if (part is DateTime)
                    partString = ((DateTime)part).ToString("yyMMdd");
                else
                    partString = ("" + part).Trim().ToLower();

                foreach (char c in partString.Where(c => c != PART_SEP))
                {
                    buf.Append(_unsupporteds.Any(unsupported => c == unsupported) ? UNSUPPORTED_ALT : c);
                }

                buf.Append(PART_SEP);
            }
            buf.Length = buf.Length - 1;
            buf.Append(SUFFIX_SEP).Append(suffix);


            return buf.ToString();
        }

        public static string FormatPath(string path)
        {
            path = path.Trim().Replace(DIR_SEP_WIN, DIR_SEP);
            if (string.IsNullOrEmpty(path))
                path = getDefaultPath();

            if (path[path.Length - 1] != DIR_SEP)
                return path;

            return path.Substring(0, path.Length - 1);
        }

        public static string MakePath(params string[] names)
        {
            if (names.Length == 0)
                return getDefaultPath();

            var buf = new StringBuilder();
            foreach (string name in names)
            {
                string subpath = FormatPath(name);
                buf.Append(subpath);
                buf.Append(DIR_SEP);
            }

            return buf.ToString(0, buf.Length - 1).Replace("" + DIR_SEP + DIR_SEP, "" + DIR_SEP);
        }

        public static string GetDirectory(string path)
        {
            var pattern = new Regex(@"^([^/\\]+[/\\])*[^/\\]*$");
            Match match = pattern.Match(path);

            if (match.Success)
            {
                return FormatPath(match.Groups[1].Value);
            }

            return getDefaultPath();
        }

        private static string getDefaultPath()
        {
            return ".";
        }

        public static XmlDocument GetXmlFromByte(byte[] bytes)
        {
            string file = DateTime.Now.Date.Ticks.ToString();
            file = AppPath.PhysicalPath(file);
            File.WriteAllBytes(file, bytes);
            var xml = new XmlDocument();
            xml.Load(file);
            File.Delete(file);
            return xml;
        }



        public static void Compress(FileInfo fileToCompress)
        {
            using (FileStream originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                        }
                    }
                }
            }
        }
    }
}
