using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Core.Definition;

namespace Core
{
    public class Binary
    {
        internal static BlowFish BlowFish = new BlowFish("gmengpasswordsencrpyt@#!");

        public static byte[] IV { 
            get => BlowFish.IV;
            set => BlowFish.IV = value;
        }

        public static byte[] GenerateIV => BlowFish.SetRandomIV();
        public static void Save<TObject>(string fullpath, TObject data)
        {
            using (Stream stream = File.Open(fullpath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
                stream.Close();
            }
        }
        public static TObject load<TObject>(string fullpath)
        {
            TObject obj;
            using (Stream stream = File.Open(fullpath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                obj = (TObject)binaryFormatter.Deserialize(stream);
                stream.Close();
            }

            return obj;
        }
        public static void SaveAsBinary<TObject>(string fullpath, TObject data, int EncryptCount = 0, FileMode fileMode = FileMode.Create)
        {
            try
            {
                using (var binaryWriter = new BinaryWriter(File.Open(fullpath, fileMode)))
                {
                    var bytes = Serialize(data);
                    for (var i = 0; i < EncryptCount; i++)
                    {
                        bytes = BlowFish.Encrypt_CBC(bytes);
                    }
                    binaryWriter.Write(bytes);
                    binaryWriter.Close();
                }
            }
            catch (IOException)
            {
                throw;
            }
        }

        public static void SaveAsBinaryEx<T>(string fullpath, List<T> data, int EncryptCount = 0)
        {
            try
            {
                using (var binaryWriter = new BinaryWriter(File.Open(fullpath, FileMode.Append)))
                {
                    var dataSize = 5000;

                    for (var i = 0; i <= data.Count; i+= dataSize)
                    {
                        var bytes = (Serialize(data.GetRange(i, Math.Min(dataSize, data.Count - i))));
                        
                        for (var j = 0; j < EncryptCount; j++)
                        {
                            bytes = BlowFish.Encrypt_CBC(bytes);
                        }
                        var bufferSize = bytes.Length;

                        //4Byte
                        binaryWriter.Write(bufferSize);
                        binaryWriter.Write(bytes);
                    }
                    binaryWriter.Close();
                }
            }
            catch (IOException)
            {
                throw;
            }
        }
        public static TObject LoadAsBinary<TObject>(string fullpath, int DecryptCount = 0)
        {
            try
            {
                TObject ret;
                using (var binaryReader = new BinaryReader(File.Open(fullpath, FileMode.Open)))
                {
                    //var bytes = binaryReader.ReadBytes(int.MaxValue);
                    var bytes = binaryReader.ReadAllBytes();
                    for (var i = 0; i < DecryptCount; i++)
                    {
                        bytes = BlowFish.Decrypt_CBC(bytes);
                    }

                    ret = Deserialize<TObject>(bytes);
                    binaryReader.Close();
                }

                return ret;
            }
            catch (IOException)
            {
                throw;
            }
            catch (OutOfMemoryException)
            {
                //binaryReader.ReadBytes(int.MaxValue); will Error on 32-bit system.
                //https://stackoverflow.com/a/8613300
                throw;
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                //
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<T> LoadAsBinaryEx<T>(string fullpath, int DecryptCount = 0)
        {
            try
            {
                List<T> ret = new List<T>();
                

                using (var binaryReader = new BinaryReader(File.Open(fullpath, FileMode.Open)))
                {
                    //var bufferSize = LoadAsBinary<int>(Path.Combine(Path.GetDirectoryName(fullpath) ?? throw new InvalidOperationException(), Path.GetFileNameWithoutExtension(fullpath) + ".didx"), 2);
                    //var bytes = binaryReader.ReadBytes(int.MaxValue);

                    while (!binaryReader.EOF())
                    {
                        var bufferSize = binaryReader.ReadInt32();
                        
                        var bytes = binaryReader.ReadBytes(bufferSize);


                        for (var i = 0; i < DecryptCount; i++)
                        {
                            bytes = BlowFish.Decrypt_CBC(bytes);
                        }

                        var data = Deserialize<List<T>>(bytes);
                        ret.AddRange(data);
                    }

                    binaryReader.Close();
                }

                return ret;
            }
            catch (IOException)
            {
                throw;
            }
            catch (OutOfMemoryException)
            {
                //binaryReader.ReadBytes(int.MaxValue); will Error on 32-bit system.
                //https://stackoverflow.com/a/8613300
                throw;
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                //
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Define other methods and classes here

        public static byte[] Serialize<TObject>(TObject obj)
        {
            var binaryFormatter = new BinaryFormatter();
            byte[] ret = null;
            using (var ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, obj);
                ret = ms.ToArray();
            }
            return ret;
        }
        public static TObject Deserialize<TObject>(byte[] bytes)
        {
            var binaryFormatter = new BinaryFormatter();
            TObject ret;
            using (var ms = new MemoryStream(bytes))
            {
                ret = (TObject)binaryFormatter.Deserialize(ms);
            }
            return ret;
        }
        //converts a byte array to a hex string
        public static string ByteToHex(byte[] bytes)
        {
            StringBuilder s = new StringBuilder();
            foreach (byte b in bytes)
                s.Append(b.ToString("x2"));
            return s.ToString();
        }

        //converts a hex string to a byte array
        public static byte[] HexToByte(string hex)
        {
            byte[] r = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length - 1; i += 2)
            {
                byte a = GetHex(hex[i]);
                byte b = GetHex(hex[i + 1]);
                r[i / 2] = (byte)(a * 16 + b);
            }
            return r;
        }

        //converts a single hex character to it's decimal value
        private static byte GetHex(char x)
        {
            if (x <= '9' && x >= '0')
            {
                return (byte)(x - '0');
            }
            else if (x <= 'z' && x >= 'a')
            {
                return (byte)(x - 'a' + 10);
            }
            else if (x <= 'Z' && x >= 'A')
            {
                return (byte)(x - 'A' + 10);
            }
            return 0;
        }
    }

    public static class BinaryReaderExtender
    {
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            const int bufferSize = 4096;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }

        }
    }
}
