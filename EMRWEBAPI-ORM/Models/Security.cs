using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace EMRWEBAPI.Models
{
    public static class Security
    {
        #region 檔案加密
        private static XmlDocument doc = new XmlDocument();
        public static string GetMD5Hash(string str)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index <= hash.Length - 1; ++index)
                stringBuilder.Append(hash[index].ToString("x2"));
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 讀取rtf檔案
        /// </summary>
        /// <param name="rtf_path">讀取檔案</param>
        /// <param name="us">回傳 UserInfo</param>
        /// <param name="hs">回傳 HostInfo</param>
        /// <returns></returns>
        public static bool readrtfs(string rtf_path, out UserInfo us, out HostInfo hs)
        {
            #region
            //<?xml version='1.0'?> 
            //<Info>
            //  <UserInfo>
            //    <Userid></Userid>
            //    <Username></Username>
            //    <Deptcod></Deptcod>"
            //    <DeptName></DeptName>
            //    <Titles></Titles>
            //    <Cardid></Cardid>
            //  </UserInfo>
            //  <HostInfo>
            //    <Ip></Ip>
            //    <HostName></HostName>
            //  </HostInfo>
            //</Info>
            #endregion
            HostInfo hs_tmp = new HostInfo();
            UserInfo us_tmp = new UserInfo();

            bool rtns = false;
            try
            {
                if (File.Exists(rtf_path))
                {
                    string xmls = DecryptFile(rtf_path, "concordtech00299");
                    //xmls = xmls.Replace("\r\n", "").Replace(" ", "");
                    doc.LoadXml(xmls);
                    XmlNode nUserid = doc.SelectSingleNode("/*/UserInfo/Userid");
                    XmlNode nUsername = doc.SelectSingleNode("/*/UserInfo/Username");

                    XmlNode nDeptcod = doc.SelectSingleNode("/*/UserInfo/Deptcod");
                    XmlNode nDeptName = doc.SelectSingleNode("/*/UserInfo/DeptName");
                    XmlNode nTitles = doc.SelectSingleNode("/*/UserInfo/Titles");
                    XmlNode nCardid = doc.SelectSingleNode("/*/UserInfo/Cardid");

                    XmlNode nIpd = doc.SelectSingleNode("/*/HostInfo/Ip");
                    XmlNode nHostName = doc.SelectSingleNode("/*/HostInfo/HostName");

                    us_tmp.Userid = nUserid.InnerText.Trim();
                    us_tmp.Username = nUsername.InnerText.Trim();
                    us_tmp.Deptcod = nDeptcod.InnerText.Trim();
                    us_tmp.DeptName = nDeptName.InnerText.Trim();
                    us_tmp.Title = nTitles.InnerText.Trim();
                    us_tmp.Cardid = nCardid.InnerText.Trim();

                    hs_tmp.Ip = nIpd.InnerText.Trim();
                    hs_tmp.Name = nHostName.InnerText.Trim();
                    //contents += nUserid.InnerText + "\n";
                    //contents += nUsername.InnerText + "\n";
                    //contents += nIpd.InnerText + "\n";
                    //contents += nHostName.InnerText + "\n";  
                    rtns = true;
                }
                else
                    rtns = false;
            }
            catch (Exception ex)
            {
                rtns = false;
            }

            us = us_tmp;
            hs = hs_tmp;

            return rtns;
        }
        /// <summary>
        /// 產生登入者訊息
        /// </summary>
        /// <param name="rtf_path">登入者訊息檔案路徑</param>
        /// <returns>回傳: True 正常,False 失敗</returns>
        public static bool markrtfs(string rtf_path)
        {
            #region 格式
            //<Info>
            //  <UserInfo>
            //    <Userid>Tommyex</Userid>
            //    <Username>Tommyex</Username>
            //  </UserInfo>
            //  <HostInfo>
            //    <Ip>Tommyex</Ip>
            //    <HostName>Tommyex</HostName>
            //  </HostInfo>
            //</Info>
            #endregion

            bool rtns = false;
            try
            {
                if (File.Exists(rtf_path))
                {
                    File.Delete(rtf_path);
                }

                string xml_contents = "<?xml version='1.0'?> "
                                    + "<Info>"
                                    + "<UserInfo>"
                                    //+ "<Userid>" + UserInfo.Userid.Trim() + "</Userid>"
                                    //+ "<Username>" + UserInfo.Username.Trim() + "</Username>"
                                    //+ "<Deptcod>" + UserInfo.Deptcod.Trim() + "</Deptcod>"
                                    //+ "<DeptName>" + UserInfo.DeptName.Trim() + "</DeptName>"
                                    //+ "<Titles>" + UserInfo.Title.Trim() + "</Titles>"
                                    //+ "<Cardid>" + UserInfo.Cardid.Trim() + "</Cardid>"
                                    + "</UserInfo>"
                                    + "<HostInfo>"
                                    //+ "<Ip>" + HostInfo.Ip.Trim() + "</Ip>"
                                    //+ "<HostName>" + HostInfo.Name.Trim() + "</HostName>"
                                    + "</HostInfo>"
                                    + "</Info>";
                doc.LoadXml(xml_contents);
                doc.Save(rtf_path);
                rtns = true;
            }
            catch (Exception ex)
            {
                rtns = false;
                PubCLass.LogToFile(ex.Message);
            }
            return rtns;
        }
        /// <summary>
        /// 檔案加密
        /// </summary>
        /// <param name="inputFile">加密檔案</param>
        /// <param name="outputFile">輸出檔案</param>
        /// <param name="skey">這個演算法支援長度為 128、192 或 256 位元的金鑰。 16/24/32字元</param>
        /// <returns>回傳: True 正常,False 失敗</returns>
        public static bool EncryptFile(string inputFile, string outputFile, string skey)
        {
            bool rtns = false;
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
                rtns = true;
            }
            catch (Exception ex)
            {
                rtns = false;
                PubCLass.LogToFile(ex.Message);
            }
            return rtns;
        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="inputFile">解密文件路徑</param>
        /// <param name="skey">密碼</param>
        /// <returns>回傳解密後的文字內容, 假如是空白為解密失敗</returns>
        public static string DecryptFile(string inputFile, string skey)
        {
            string rtns = "";
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);
                    string tmpe_file = Path.GetTempPath() + "~" + Guid.NewGuid().GetHashCode().ToString();
                    FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                    FileStream fsOut = new FileStream(tmpe_file, FileMode.Create);
                    ICryptoTransform decryptor = aes.CreateDecryptor(key, IV);
                    CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read);
                    int data;
                    while ((data = cs.ReadByte()) != -1)
                    {
                        fsOut.WriteByte((byte)data);
                    }
                    cs.Close();
                    decryptor.Dispose();
                    fsOut.Close();
                    fsCrypt.Close();

                    FileStream fsOut1 = new FileStream(tmpe_file, FileMode.Open);
                    StreamReader streamReader = new StreamReader(fsOut1);
                    rtns = streamReader.ReadToEnd(); //回傳值
                    fsOut1.Close();

                    streamReader.Close();
                    File.Delete(tmpe_file);
                }
            }
            catch (Exception ex)
            {
                //PubCLass.LogToFile(ex.Message);
                rtns = "";
            }
            return rtns;
        }
        #endregion

        #region 字串加密
        private static byte[] _salt = Encoding.ASCII.GetBytes("CCH_1896"); //用來衍生金鑰的金鑰 Salt。
        private static string DefaulsharedSecret = "CCH_1896"; //用來衍生金鑰的密碼。
        //source : http://stackoverflow.com/questions/202011/encrypt-and-decrypt-a-string
        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static string EncryptStringAES(string plainText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("加密字串是空值!");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("密碼是空值!");

            string outStr = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            catch //(Exception ex)
            {
                outStr = "加密失敗!";
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }
        /// <summary>
        /// 字串加密
        /// </summary>
        /// <param name="plainText">加密字串</param>
        /// <returns>回傳加密後字串</returns>
        public static string EncryptStringAES(string plainText)
        {
            string rtns = "";

            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("加密字串是空值!");

            try
            {
                rtns = EncryptStringAES(plainText, DefaulsharedSecret);
            }
            catch
            { }
            return rtns;
        }
        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static string DecryptStringAES(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("加密字串是空值!");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("密碼是空值!");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            catch //(Exception ex)
            {
                plaintext = "解密失敗!";
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
        /// <summary>
        /// 字串解密
        /// </summary>
        /// <param name="cipherText">加密後字串</param>
        /// <returns>回傳解密後字串</returns>
        public static string DecryptStringAES(string cipherText)
        {
            string rtns = "";

            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("加密字串是空值!");

            try
            {
                rtns = DecryptStringAES(cipherText, DefaulsharedSecret);
            }
            catch
            { }
            return rtns;
        }
        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

        #endregion

        //http://stackoverflow.com/questions/13570029/how-can-i-fill-in-rsaparameters-value-in-c-sharp

        private static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //private static RSAParameters RSAp = new RSAParameters();
        private static RSAParameters public_RSAp = RSA.ExportParameters(false);  // export only the public key 
        private static RSAParameters private_RSAp = RSA.ExportParameters(true);  // export the private key 
        //private static byte[] _salt = Encoding.ASCII.GetBytes("CCH_1896"); //用來衍生金鑰的金鑰 Salt。
        private static string KeyContainerName = "MyKeyContainer"; //用來衍生金鑰的密碼。

        public static string _EncryptRSA(string plainText)
        {
            string rtns = "";

            // with simplifications....
            XmlSerializer public_x = new XmlSerializer(public_RSAp.GetType());
            public_x.Serialize(File.Create(@"c:\public.cer"), public_RSAp);            

            XmlSerializer private_x = new XmlSerializer(private_RSAp.GetType());
            private_x.Serialize(File.Create(@"c:\private.cer"), private_RSAp);

            //x.Serialize(Console.Out, private_RSAp);
            //Console.WriteLine();        
            //BigInteger n = new BigInteger("19579160939939334264971282204525611731944172893619019759209712156289528980860378672033164235760825723282900348193871051950190013953658941960463089031452404364269503721476236241284015792700835264262839734314564696723261501877759107784604657504350348081273959965406686529089170062268136253938904906635532824296510859016002105655690559115059267476786307037941751235763572931501055146976797606538425089134251611194500570922973015579287289778637105402129208324300035518642730384616767241853993887666288072512402523498267733725021939287517009966986976768028023180137546958580922532786773172365428677544232641888174470601681", 10);

            //BigInteger e = new BigInteger("65537", 10);

            ////rsap.Modulus = ByteConverter.GetBytes(publicKey);
            //rsap.Exponent = e.getBytes();
            //rsap.Modulus = n.getBytes();
            ///*rsap.Exponent = ByteConverter.GetBytes(publicKey);
            //  rsap.D = ByteConverter.GetBytes(publicKey);
            //  rsap.DP = ByteConverter.GetBytes(publicKey);
            //  rsap.DQ = ByteConverter.GetBytes(publicKey);
            //  rsap.P = ByteConverter.GetBytes(publicKey);
            //  rsap.Q = ByteConverter.GetBytes(publicKey);
            //  rsap.InverseQ = ByteConverter.GetBytes(publicKey);*/

            //RSACryptoServiceProvider provider = new System.Security.Cryptography.RSACryptoServiceProvider();
            //RSAParameters rp = new RSAParameters(); 
            //provider.ImportParameters(your_rsa_key);

            //var encryptedBytes = provider.Encrypt(
            //System.Text.Encoding.UTF8.GetBytes("Hello World!"), true);

            return rtns;
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="plainText">欲加密字串</param>
        /// <param name="errors">false : 沒錯誤 , True: 發生錯誤</param>
        /// <param name="error_msg">錯誤訊息</param>
        /// <returns>回傳加密後BASE64字串 </returns>
        public static string EncryptRSAt(string plainText,ref bool errors,ref string error_msg)
        {
            string rtns = ""; //回傳加密後字串
            error_msg = "";
            errors = false;     
                   
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.                
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                //Create byte arrays to hold original, encrypted, and decrypted data.                
                byte[] dataToEncrypt =  ByteConverter.GetBytes(plainText);  
                byte[] encryptedData;            
                //Pass the data to ENCRYPT, the name of the key container,
                //and a boolean flag specifying no OAEP padding.
                encryptedData = RSAEncrypt(dataToEncrypt, KeyContainerName, false);
                //rtns = ByteConverter.GetString(encryptedData);
                rtns = Convert.ToBase64String(encryptedData);
                //string decodedString = Encoding.UTF8.GetString(encryptedData);
                ////ByteConverter.GetString(encryptedData)
                //string s1 = Encoding.UTF8.GetString(encryptedData); // ���
                //string s2 = BitConverter.ToString(encryptedData);
                //string s3 = Convert.ToBase64String(encryptedData);
                //string s4 = HttpServerUtility.UrlTokenEncode(encryptedData);
                //System.Convert.FromBase64String(s3)
                //System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(s3))
                //System.Text.Encoding.UTF8.GetString(encryptedData)  
            }
            catch (ArgumentNullException)
            {
                error_msg = "Argument is Null !";
                errors = true;
            }
            catch(Exception ex)
            {
                error_msg = ex.Message;
                errors = true;
            }
            return rtns;
        }
        
        /// <summary>
        /// RSA 解密
        /// </summary>
        /// <param name="plainText">欲解密BASE64字串</param>
        /// <param name="errors">false : 沒錯誤 , True: 發生錯誤</param>
        /// <param name="error_msg">錯誤訊息</param>
        /// <returns>回傳解密後字串</returns>
        public static string DecryptRSAt(string plainText, ref bool errors, ref string error_msg)
        {
            string rtns = ""; //回傳解密後字串
            error_msg = "";
            errors = false;

            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] dataToEncrypt = System.Convert.FromBase64String(plainText);
                byte[] decryptedData;
                decryptedData = RSADecrypt(dataToEncrypt, KeyContainerName, false);
                rtns = ByteConverter.GetString(decryptedData);

                ////Create a UnicodeEncoder to convert between byte array and string.
                //UnicodeEncoding ByteConverter = new UnicodeEncoding();
                ////Create byte arrays to hold original, encrypted, and decrypted data.
                //byte[] dataToEncrypt = ByteConverter.GetBytes(plainText); 
                //byte[] decryptedData; 
                ////Pass the data to DECRYPT, the name of the key container,
                ////and a boolean flag specifying no OAEP padding.
                //decryptedData = RSADecrypt(dataToEncrypt, KeyContainerName, false);                 
                //rtns = ByteConverter.GetString(decryptedData);                
            }
            catch (ArgumentNullException)
            {
                error_msg = "Argument is Null !";
                errors = true;
            }
            catch (Exception ex)
            {
                error_msg = ex.Message;
                errors = true;
            }
            return rtns;
        }

        //public static bool EncryptRSAt(string plainText)
        //{
        //    bool rtns = false;
        //    try
        //    {
        //        //Create a new key and persist it in the key container.
        //        RSAPersistKeyInCSP(KeyContainerName);

        //        //Create a UnicodeEncoder to convert between byte array and string.
        //        UnicodeEncoding ByteConverter = new UnicodeEncoding();

        //        //Create byte arrays to hold original, encrypted, and decrypted data.
        //        byte[] dataToEncrypt = ByteConverter.GetBytes(plainText);
        //        byte[] encryptedData;
        //        byte[] decryptedData;

        //        //Pass the data to ENCRYPT, the name of the key container,
        //        //and a boolean flag specifying no OAEP padding.
        //        encryptedData = RSAEncrypt(dataToEncrypt, KeyContainerName, false);

        //        //Pass the data to DECRYPT, the name of the key container,
        //        //and a boolean flag specifying no OAEP padding.
        //        decryptedData = RSADecrypt(encryptedData, KeyContainerName, false);

        //        //Display the decrypted plaintext to the console. 
        //        Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));

        //        RSADeleteKeyInCSP(KeyContainerName);
        //        rtns = true;
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        //Catch this exception in case the encryption did
        //        //not succeed.
        //        Console.WriteLine("Encryption failed.");
        //    }
        //    return rtns;

        //    //RSAParameters rsap_tmp = new RSAParameters();
        //    //UnicodeEncoding ByteConverter = new UnicodeEncoding();

        //    //byte[] dataToEncrypt = ByteConverter.GetBytes("123456789");
        //    ////Create a UnicodeEncoder to convert between byte array and string.
        //    ////BigInteger n = new BigInteger("19579160939939334264971282204525611731944172893619019759209712156289528980860378672033164235760825723282900348193871051950190013953658941960463089031452404364269503721476236241284015792700835264262839734314564696723261501877759107784604657504350348081273959965406686529089170062268136253938904906635532824296510859016002105655690559115059267476786307037941751235763572931501055146976797606538425089134251611194500570922973015579287289778637105402129208324300035518642730384616767241853993887666288072512402523498267733725021939287517009966986976768028023180137546958580922532786773172365428677544232641888174470601681", 10);
        //    ////BigInteger e = new BigInteger("65537", 10);          

        //    //byte[] ndData = ByteConverter.GetBytes("19579160939939334264971282204525611731944172893619019759209712156289528980860378672033164235760825723282900348193871051950190013953658941960463089031452404364269503721476236241284015792700835264262839734314564696723261501877759107784604657504350348081273959965406686529089170062268136253938904906635532824296510859016002105655690559115059267476786307037941751235763572931501055146976797606538425089134251611194500570922973015579287289778637105402129208324300035518642730384616767241853993887666288072512402523498267733725021939287517009966986976768028023180137546958580922532786773172365428677544232641888174470601681");
        //    //byte[] edData = ByteConverter.GetBytes("65537");
        //    //rsap_tmp.Exponent = edData;
        //    //rsap_tmp.Modulus = ndData;

        //    ///*rsap.Exponent = ByteConverter.GetBytes(publicKey);
        //    //  rsap.D = ByteConverter.GetBytes(publicKey);
        //    //  rsap.DP = ByteConverter.GetBytes(publicKey);
        //    //  rsap.DQ = ByteConverter.GetBytes(publicKey);
        //    //  rsap.P = ByteConverter.GetBytes(publicKey);
        //    //  rsap.Q = ByteConverter.GetBytes(publicKey);
        //    //  rsap.InverseQ = ByteConverter.GetBytes(publicKey);*/
        //    ////RSA.PublicOnly = false;

        //    //RSA.ImportParameters(rsap_tmp);
        //    //Console.WriteLine("PublicOnly: " + RSA.PublicOnly);
        //    ////Debug.Log("PublicOnly: " + RSA.PublicOnly);
        //    //Console.WriteLine("Modulus_Length: " + rsap_tmp.Modulus.Length);
        //    //Console.WriteLine("RSA: " + RSA.ToString());
        //    ////Debug.Log(rsap.Modulus.Length);
        //    ////Debug.Log (RSA.ToString());  
        //    //return rsap_tmp; 
        //}

        public static void RSAPersistKeyInCSP(string ContainerName)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider to generate
                //a new key pair.  Pass the CspParameters class to persist the 
                //key in the container.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Indicate that the key was persisted.
                Console.WriteLine("The RSA key was persisted in the container, \"{0}\".", ContainerName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

            }
        }

        public static void RSADeleteKeyInCSP(string ContainerName)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider. 
                //Pass the CspParameters class to use the 
                //key in the container.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Delete the key entry in the container.
                RSAalg.PersistKeyInCsp = false;

                //Call Clear to release resources and delete the key from the container.
                RSAalg.Clear();

                //Indicate that the key was persisted.
                Console.WriteLine("The RSA key was deleted from the container, \"{0}\".", ContainerName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

            }
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, string ContainerName, bool DoOAEPPadding)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider.
                //Pass the CspParameters class to use the key 
                //from the key in the container.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSAalg.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, string ContainerName, bool DoOAEPPadding)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider.
                //Pass the CspParameters class to use the key 
                //from the key in the container.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSAalg.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

        ////http://blog.wahahajk.com/2009/04/c-byte.html
        ////C# byte 轉 文字
        //byte轉char或 byte轉string
        //Convert.ToChar是把hex轉成相對應ascii code
        //像a的ascii code是0x61
        //byte[] b = new byte[2] { 0x61, 0x62 };
        //string s = Convert.ToChar(b[0]); => s="a";
        //string s = Convert.ToChar(b[1]); => s="b";
        //如果你要把byte code轉成"字面上"的數值 應該這樣寫
        //byte[] b = new byte[2] { 0x61, 0x62 };
        //string s = b[0].ToString("X2"); => s="61";
        //string s = b[1].ToString("X2"); => s="62";

        //string decryptedTest = System.Text.Encoding.UTF8.GetString(
        //provider.Decrypt(encryptedBytes, true));

        //byte[] modulusBytes = Convert.FromBase64String(modulus);
        //byte[] exponentBytes = Convert.FromBase64String(exponent);

        //自己測試改的
        //public static string EncryptString(string _EncryptString, string sharedSecret)
        //{
        //    string rtns = "";

        //    if (string.IsNullOrEmpty(_EncryptString))
        //        throw new ArgumentNullException("加密字串是空值!");
        //    if (string.IsNullOrEmpty(sharedSecret))
        //        throw new ArgumentNullException("密碼是空值!");

        //    try
        //    {
        //        using (RijndaelManaged aes = new RijndaelManaged())
        //        {
        //            // generate the key from the shared secret and the salt
        //            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);//衍生金鑰
        //            aes.Key = key.GetBytes(aes.KeySize / 8);

        //            // Create a RijndaelManaged object    
        //            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
        //            {
        //                // Create the streams used for encryption.
        //                using (MemoryStream msEncrypt = new MemoryStream())
        //                {
        //                    // prepend the IV
        //                    msEncrypt.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
        //                    msEncrypt.Write(aes.IV, 0, aes.IV.Length);
        //                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //                    {
        //                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                        {
        //                            //Write all data to the stream.
        //                            swEncrypt.Write(_EncryptString);
        //                        }
        //                    }
        //                    rtns = Convert.ToBase64String(msEncrypt.ToArray());
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rtns = "";
        //        PubCLass.LogToFile(ex.Message);
        //    }
        //    return rtns;
        //}
    }

    public class UserInfo : System.Object
    {
        private string v_Userid = "";
        private string v_Username = "";
        private string v_department_cod = "";
        private string v_department_name = "";
        private string v_titles = "";
        private string v_cardid = "";

        public void Clear()
        {
            this.v_Userid = "";
            this.v_Username = "";
            this.v_department_cod = "";
            this.v_department_name = "";
            this.v_titles = "";
            this.v_cardid = "";
        }
        public UserInfo()
        {
            this.v_Userid = "";
            this.v_Username = "";
            this.v_department_cod = "";
            this.v_department_name = "";
            this.v_titles = "";
            this.v_cardid = "";
        }
        public string Userid
        {
            get { return this.v_Userid; }
            set { this.v_Userid = value; }
        }
        public string Username
        {
            get { return this.v_Username; }
            set { this.v_Username = value; }
        }
        public string Deptcod
        {
            get { return this.v_department_cod; }
            set { this.v_department_cod = value; }
        }
        public string DeptName
        {
            get { return this.v_department_name; }
            set { this.v_department_name = value; }
        }

        public string Title
        {
            get { return this.v_titles; }
            set { this.v_titles = value; }
        }
        public string Cardid
        {
            get { return this.v_cardid; }
            set { this.v_cardid = value; }
        }

    }

    public class HostInfo : System.Object
    {
        private string v_Ip = "";
        private string v_Num = "";
        private string v_Name = "";

        public void Clear()
        {
            this.v_Ip = "";
            this.v_Num = "";
            this.v_Name = "";
        }
        public HostInfo()
        {
            this.v_Ip = "";
            this.v_Num = "";
            this.v_Name = "";
        }
        public string Ip
        {
            get { return this.v_Ip; }
            set { this.v_Ip = value; }
        }
        public string Num
        {
            get { return this.v_Num; }
            set { this.v_Num = value; }
        }
        public string Name
        {
            get { return this.v_Name; }
            set { this.v_Name = value; }
        }
    }

    public static class PubCLass
    {
        public static void LogToFile(string msg)
        {
            try
            {
                string logmsg = string.Format("[{0}] {1}{2}",
                 DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), msg, Environment.NewLine);
                System.IO.File.AppendAllText(@"log.txt", logmsg, System.Text.Encoding.Default);
            }
            catch (Exception ex)
            {
            }
        }
    }

}