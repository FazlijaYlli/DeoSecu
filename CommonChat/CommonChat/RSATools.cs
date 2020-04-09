/*
 * Auteur      : Ioannis Alimpertis
 * Date        : 08.04.2017
 * Description : Chat sécurisé / Classe RSATools
 * Source      : https://github.com/Obrelix/.NET-Safe-Chat-Application-RSA-Encryption-/blob/master/ChatApp/ChatApp/Classes/RSATools.cs
 */

using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace CommonChat
{
    /// <summary>
    /// Classe d'outils concernant RSA
    /// </summary>
    public static class RSATools
    {
        /// <summary>
        /// Génère une clé publique et privée
        /// </summary>
        public static void GenerateKeys(out string publicKey, out string privateKey)
        {
            publicKey = String.Empty;
            privateKey = String.Empty;

            try
            {
                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {
                    try
                    {
                        privateKey = RSA.ToXmlString(true);
                        publicKey = RSA.ToXmlString(false);
                    }
                    finally
                    {
                        RSA.PersistKeyInCsp = false;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                Debug.WriteLine("Key generation failed.");
            }
        }

        /// <summary>
        /// Encrypte le tableau d'octets en RSA
        /// </summary>
        /// <param name="DataToEncrypt">Tableau d'octets</param>
        /// <param name="RSAKeyInfo">Clé publique du récepteur</param>
        /// <param name="DoOAEPPadding">Padding optimal</param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, string RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {
                    try
                    {
                        //Import the RSA Key information. This only needs
                        //toinclude the public key information.
                        RSA.FromXmlString(RSAKeyInfo);

                        //Encrypt the passed byte array and specify OAEP padding.  
                        //OAEP padding is only available on Microsoft Windows XP or
                        //later.  
                        encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                    }
                    catch (CryptographicException e)
                    {
                        Debug.Write(e.Message);
                        return null;
                    }
                    finally
                    {
                        RSA.PersistKeyInCsp = false;
                    }

                }
                return encryptedData;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Décrypte un tableau d'octets
        /// </summary>
        /// <param name="DataToDecrypt">Tableau d'octets</param>
        /// <param name="RSAKeyInfo">Clé privée du récepteur</param>
        /// <param name="DoOAEPPadding">Padding optimal</param>
        /// <returns></returns>
        public static byte[] RSADecrypt(byte[] DataToDecrypt, string RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData = new byte[256];
                //MessageBox.Show("Data lenght" + DataToDecrypt.Length.ToString());
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {
                    try
                    {
                        //Import the RSA Key information. This needs
                        //to include the private key information.
                        RSA.FromXmlString(RSAKeyInfo);

                        //Decrypt the passed byte array and specify OAEP padding.  
                        //OAEP padding is only available on Microsoft Windows XP or
                        //later.  
                        decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                        ///The method RSA.Decrypt(byte[] array, bool FOAEEP) want array with max lenght 128 bytes
                        ///this is a problem for very large messages...
                    }
                    finally
                    {
                        RSA.PersistKeyInCsp = false;
                    }
                }
                return decryptedData;
            }
            catch
            {
                return null;
            }

        }
    }
}