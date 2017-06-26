using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace LinYong.PublicService
{
    /// <summary>
    /// ����������
    /// </summary>
    public class PublicService
    {
        /// <summary>
        /// �ж��ַ������Ƿ��������
        /// </summary>
        /// <param name="srcString">��Ҫ�жϵ��ַ���</param>
        /// <returns>true���������ģ�false�����������ġ�</returns>
        public bool ContainChinese(string srcString)
        {
            int strLen = srcString.Length;
            //�ַ����ĳ��ȣ�һ����ĸ�ͺ��ֶ���һ��                               

            int bytLeng = System.Text.Encoding.UTF8.GetBytes(srcString).Length;
            //�ַ������ֽ�������ĸռ1λ������ռ2λ,ע�⣬һ��ҪUTF8              

            bool chkResult = false;
            if (strLen < bytLeng)
            //����ַ����ĳ��ȱ��ַ������ֽ���С����Ȼ���������к�����^-^        
            {
                chkResult = true;
            }


            //���ؽ��
            return chkResult;
        }

        #region
        ///// <summary>
        ///// DES���ܣ�����ʮ�������ַ���
        ///// </summary>
        ///// <param name="encryptString">��Ҫ���ܵ��ַ���</param>
        ///// <returns>����DES���ܺ��ʮ�������ַ���</returns>
        //public string DESEncrypt(string encryptString)
        //{
        //    byte[] buffer;
        //    DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
        //    MemoryStream ms = new MemoryStream();//�ȴ��� һ���ڴ���
        //    CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateEncryptor(), CryptoStreamMode.Write);//���ڴ������ӵ�����ת����
        //    StreamWriter sw = new StreamWriter(cryStream);
        //    sw.WriteLine(encryptString);//��Ҫ���ܵ��ַ���д�����ת����
        //    sw.Close();
        //    cryStream.Close();
        //    buffer = ms.ToArray();//�����ܺ����ת��Ϊ�ֽ�����
        //    //txtjiami.Text = Convert.ToBase64String(buffer);//�����ܺ���ֽ�����ת��Ϊ�ַ���
        //    return bytesToHexStr(buffer);
        //}

        /// <summary>
        /// �ֽ�����ת16�����ַ���
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
        /// DES�����㷨 
        /// </summary> 
        /// <param name="encryptString">Ҫ���ܵ��ַ���</param> 
        /// <param name="encryptKey">������Key</param> 
        /// <returns>��ȷ���ؼ��ܺ�Ľ�������󷵻�Դ�ַ���</returns> 
        public string DESEncrypt(string encryptString, string encryptKey)
        {
            try
            {

                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptKey);
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();

                // java Ĭ�ϵ���ECBģʽ��PKCS5padding��c#Ĭ�ϵ�CBCģʽ��PKCS7padding ������������Ĭ��ʹ��ECB��ʽ 
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
