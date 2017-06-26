using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace LinYong.PublicService
{
    /// <summary>
    /// 公共服务类
    /// </summary>
    public class PublicService
    {
        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="srcString">需要判断的字符串</param>
        /// <returns>true：含有中文；false：不含有中文。</returns>
        public bool ContainChinese(string srcString)
        {
            int strLen = srcString.Length;
            //字符串的长度，一个字母和汉字都算一个                               

            int bytLeng = System.Text.Encoding.UTF8.GetBytes(srcString).Length;
            //字符串的字节数，字母占1位，汉字占2位,注意，一定要UTF8              

            bool chkResult = false;
            if (strLen < bytLeng)
            //如果字符串的长度比字符串的字节数小，当然就是其中有汉字啦^-^        
            {
                chkResult = true;
            }


            //返回结果
            return chkResult;
        }

        #region
        ///// <summary>
        ///// DES加密，返回十六进制字符串
        ///// </summary>
        ///// <param name="encryptString">需要加密的字符串</param>
        ///// <returns>返回DES加密后的十六进制字符串</returns>
        //public string DESEncrypt(string encryptString)
        //{
        //    byte[] buffer;
        //    DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
        //    MemoryStream ms = new MemoryStream();//先创建 一个内存流
        //    CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateEncryptor(), CryptoStreamMode.Write);//将内存流连接到加密转换流
        //    StreamWriter sw = new StreamWriter(cryStream);
        //    sw.WriteLine(encryptString);//将要加密的字符串写入加密转换流
        //    sw.Close();
        //    cryStream.Close();
        //    buffer = ms.ToArray();//将加密后的流转换为字节数组
        //    //txtjiami.Text = Convert.ToBase64String(buffer);//将加密后的字节数组转换为字符串
        //    return bytesToHexStr(buffer);
        //}

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string bytesToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        #endregion

        /// <summary> 
        /// DES加密算法 
        /// </summary> 
        /// <param name="encryptString">要加密的字符串</param> 
        /// <param name="encryptKey">加密码Key</param> 
        /// <returns>正确返回加密后的结果，错误返回源字符串</returns> 
        public string DESEncrypt(string encryptString, string encryptKey)
        {
            try
            {

                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptKey);
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();

                // java 默认的是ECB模式，PKCS5padding；c#默认的CBC模式，PKCS7padding 所以这里我们默认使用ECB方式 
                desProvider.Mode = CipherMode.CBC;
                MemoryStream memStream = new MemoryStream();
                CryptoStream crypStream = new CryptoStream(memStream, desProvider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);

                crypStream.Write(inputByteArray, 0, inputByteArray.Length);
                crypStream.FlushFinalBlock();
                //return Convert.ToBase64String(memStream.ToArray());
                return bytesToHexStr(memStream.ToArray()); 
            }
            catch
            {
                return "";
            }
        } 
 

    }

    public enum FlightOrientation { In = 0, Out = 1 };


}
