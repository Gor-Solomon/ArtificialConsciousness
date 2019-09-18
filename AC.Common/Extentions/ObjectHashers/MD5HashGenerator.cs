using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AC.Common.Extentions.ObjectHashers
{
    /// <summary>
    /// This class takes an object, and generates a key to it. There are several possibilities:
    /// This generator can generate keys of type integer,float,double. The generated key is not necessarly
    /// unique!
    /// </summary>
    public class MD5HashGenerator : IObjectHasher
    {
        readonly object _locker = new object();

        /// <summary>
        /// Generates a hashed - key for an instance of a class.
        /// The hash is a classic MD5 hash (e.g. BF20EB8D2C4901112179BF5D242D996B). So you can distinguish different 
        /// instances of a class. Because the object is hashed on the internal state, you can also hash it, then send it to
        /// someone in a serialized way. Your client can then deserialize it and check if it is in
        /// the same state.
        /// The method just requires that the object should have Serilizeble Attribute.
        /// <b>The method is thread-safe!</b>
        /// </summary>
        /// <param name="sourceObject">The object you'd like to have a key out of it.</param>
        /// <returns>An string representing a MD5 Hashkey corresponding to the object or null if the passed object is null 
        /// or couldn't be serialized.</returns>
        /// <exception cref="ArgumentException">Will be thrown if the object  doesn't have Serializable attribute.</exception>
        public string ComputeHash(object sourceObject)
        {
            string hashString = null;

            if (sourceObject != null)
            {
                if (sourceObject.GetType().GetCustomAttribute(typeof(SerializableAttribute)) == null)
                {
                    throw new ArgumentException("Object Type {" + sourceObject.GetType() + "} doesn't have Serializable attribute which is required for hashing.");
                }
                else
                {
                    //Now we begin to do the real work.
                    hashString = ComputeHash(ObjectToByteArray(sourceObject));
                }
            }

            return hashString;
        }

        /// <summary>
        /// Converts an object to an array of bytes. This array is used to hash the object.
        /// </summary>
        /// <param name="objectToSerialize">Just an object</param>
        /// <returns>A byte - array representation of the object.</returns>
        /// <exception cref="SerializationException">Is thrown if something went wrong during serialization.</exception>
        private byte[] ObjectToByteArray(object objectToSerialize)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] result = null;

            using (MemoryStream fs = new MemoryStream())
            {
                //Here's the core functionality! One Line!
                //To be thread-safe we lock the object
                lock (_locker)
                {
                    formatter.Serialize(fs, objectToSerialize);
                }

                result = fs.ToArray();
            }

            return result;
        }

        /// <summary>
        /// Generates the hashcode of an given byte-array. The byte-array can be an object. Then the
        /// method "hashes" this object. The hash can then be used e.g. to identify the object.
        /// </summary>
        /// <param name="objectAsBytes">bytearray representation of an object.</param>
        /// <returns>The MD5 hash of the object as a string or null if it couldn't be generated.</returns>
        private string ComputeHash(byte[] objectAsBytes)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(objectAsBytes);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            // And return it
            return sb.ToString();
        }
    }
}
