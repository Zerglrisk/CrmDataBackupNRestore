using System;
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
        public static void SaveAsBinary<TObject>(string fullpath, TObject data, int EncryptCount = 0)
        {
            try
            {
                using (var binaryWriter = new BinaryWriter(File.Open(fullpath, FileMode.Create)))
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
