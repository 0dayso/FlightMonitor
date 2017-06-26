using System;
using System.IO;
using System.Text;
using System.Data;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CompressDataSet.Common
{
	/// <summary>
	/// ѹ��ǿ�ȡ�
	/// </summary>
	public enum CompressionLevel
	{
		/// <summary>
		/// ������õ�ѹ���ʡ�
		/// </summary>
		BestCompression,

		/// <summary>
		/// ����Ĭ�ϵ�ѹ���ʡ�
		/// </summary>
		DefaultCompression,

		/// <summary>
		/// ��������ѹ���ٶȡ�
		/// </summary>
		BestSpeed,

		/// <summary>
		/// �������κ�ѹ����
		/// </summary>
		NoCompression
	}

	/// <summary>
	/// CompressionHelper ��ժҪ˵����
	/// </summary>
	public class CompressionHelper
	{
		/// <summary>
		/// ��ȡ������ѹ��ǿ�ȡ�
		/// </summary>
		public CompressionLevel Level;

		public CompressionHelper()
		{
            Level = CompressionLevel.DefaultCompression;
		}

		public CompressionHelper(CompressionLevel level)
		{
			Level = level;
		}

		#region Public Methods
		/// <summary>
		/// ��ԭʼ�ֽ�����������ѹ�����ֽ����顣
		/// </summary>
		/// <param name="bytesToCompress">ԭʼ�ֽ����顣</param>
		/// <returns>������ѹ�����ֽ�����</returns>
		public byte[] CompressToBytes(byte[] bytesToCompress)
		{
			MemoryStream ms = new MemoryStream();
			Stream s = GetOutputStream(ms);
			s.Write(bytesToCompress, 0, bytesToCompress.Length);
            byte[] bArray = ms.ToArray();
            s.Close();
          //  ms.Close();
         //   return bArray;
            return ms.ToArray();
		}

		/// <summary>
		/// ��ԭʼ�ַ���������ѹ�����ַ�����
		/// </summary>
		/// <param name="stringToCompress">ԭʼ�ַ�����</param>
		/// <returns>������ѹ�����ַ�����</returns>
		public string CompressToString(string stringToCompress)
		{
			byte[] compressedData = CompressToBytes(stringToCompress);
			string strOut = Convert.ToBase64String(compressedData);
			return strOut;
		}

		/// <summary>
		/// ��ԭʼ�ַ���������ѹ�����ֽ����顣
		/// </summary>
		/// <param name="stringToCompress">ԭʼ�ַ�����</param>
		/// <returns>������ѹ�����ֽ����顣</returns>
		public byte[] CompressToBytes(string stringToCompress)
		{
			byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);
			return CompressToBytes(bytData);
		}
        
		/// <summary>
		/// ����ѹ�����ַ�������ԭʼ�ַ�����
		/// </summary>
		/// <param name="stringToDecompress">��ѹ�����ַ�����</param>
		/// <returns>����ԭʼ�ַ�����</returns>
		public string DecompressToString(string stringToDecompress)
		{
			string outString = string.Empty;
			if (stringToDecompress == null)
			{
				throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
			}

			try
			{
				byte[] inArr = Convert.FromBase64String(stringToDecompress.Trim());
				outString = Encoding.Unicode.GetString(DecompressToBytes(inArr));
			}
			catch (NullReferenceException nEx)
			{
				return nEx.Message;
			}

			return outString;
		}

		/// <summary>
		/// ����ѹ�����ֽ���������ԭʼ�ֽ����顣
		/// </summary>
		/// <param name="bytesToDecompress">��ѹ�����ֽ����顣</param>
		/// <returns>����ԭʼ�ֽ����顣</returns>
		public byte[] DecompressToBytes(byte[] bytesToDecompress)
		{
			byte[] writeData = new byte[4096];
			Stream s2 = GetInputStream(new MemoryStream(bytesToDecompress));
			MemoryStream outStream = new MemoryStream();

			while (true)
			{
				int size = s2.Read(writeData, 0, writeData.Length);
				if (size > 0)
				{
					outStream.Write(writeData, 0, size);
				}
				else
				{
					break;
				}
			}
			s2.Close();
			byte[] outArr = outStream.ToArray();
			outStream.Close();
			return outArr;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// ����ѹ��ǿ�ȷ���ʹ���˲���ѹ���㷨�� Deflate ����
		/// </summary>
		/// <param name="level">ѹ��ǿ�ȡ�</param>
		/// <returns>����ʹ���˲���ѹ���㷨�� Deflate ����</returns>
		private Deflater GetDeflater(CompressionLevel level)
		{
			switch (level)
			{
				case CompressionLevel.DefaultCompression:
					return new Deflater(Deflater.DEFAULT_COMPRESSION);

				case CompressionLevel.BestCompression:
					return new Deflater(Deflater.BEST_COMPRESSION);

				case CompressionLevel.BestSpeed:
					return new Deflater(Deflater.BEST_SPEED);

				case CompressionLevel.NoCompression:
					return new Deflater(Deflater.NO_COMPRESSION);

				default:
					return new Deflater(Deflater.DEFAULT_COMPRESSION);
			}
		}

		/// <summary>
		/// �Ӹ�����������ѹ���������
		/// </summary>
		/// <param name="inputStream">ԭʼ����</param>
		/// <returns>����ѹ���������</returns>
		private DeflaterOutputStream GetOutputStream(Stream inputStream)
		{
			return new DeflaterOutputStream(inputStream, GetDeflater(Level));
		}

		/// <summary>
		/// �Ӹ�����������ѹ����������
		/// </summary>
		/// <param name="inputStream">ԭʼ����</param>
		/// <returns>����ѹ����������</returns>
		private InflaterInputStream GetInputStream(Stream inputStream)
		{
			return new InflaterInputStream(inputStream);
		}
		#endregion


        //����2
        #region
        public string Compress(string uncompressedString)
        {
            byte[] bytData = System.Text.Encoding.Unicode.GetBytes(uncompressedString);
            MemoryStream ms = new MemoryStream();
            Stream s = new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(ms);
            s.Write(bytData, 0, bytData.Length);
            s.Close();
            byte[] compressedData = (byte[])ms.ToArray();
            return System.Convert.ToBase64String(compressedData, 0, compressedData.Length);
        }
        //��ѹ   
        public string DeCompress(string compressedString)
        {
            System.Text.StringBuilder uncompressedString = new System.Text.StringBuilder();
            int totalLength = 0;
            byte[] bytInput = System.Convert.FromBase64String(compressedString); ;
            byte[] writeData = new byte[4096];
            Stream s2 = new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(new MemoryStream(bytInput));
            while (true)
            {
                int size = s2.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    totalLength += size;
                    uncompressedString.Append(System.Text.Encoding.Unicode.GetString(writeData, 0, size));
                }
                else
                {
                    break;
                }
            }
            s2.Close();
            return uncompressedString.ToString();
        }   

        #endregion

        //����ӵķ���--2009.10.21
        #region �����ݱ�ѹ����ϵ�л��ɶ�������
        /// <summary>
        /// �����ݱ�ѹ���ɶ�������
        /// </summary>
        /// <param name="datatableToCompress">��Ҫѹ�������ݱ�</param>
        /// <returns>���ؽ��Ϊѹ����ϵ�л����DataTable�����������</returns>
        public byte[] CompressToBytes(DataTable datatableToCompress)
        {
            byte[] bArrayResult = null;

            try
            {
                datatableToCompress.RemotingFormat = SerializationFormat.Binary;
                MemoryStream ms = new MemoryStream();
                IFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, datatableToCompress);
                bArrayResult = ms.ToArray();

                CompressDataSet.Common.CompressionHelper commPress = new CompressionHelper();
                bArrayResult = commPress.CompressToBytes(bArrayResult);
            }
            catch
            {
                bArrayResult = null;
            }

            //���ؽ��
            return bArrayResult;

        }
        #endregion

        #region �����ݱ�ѹ����ϵ�л��ɶ�������
        /// <summary>
        /// �����ݱ�ѹ���ɶ�������
        /// </summary>
        /// <param name="datatableToCompress">��Ҫѹ�������ݱ�</param>
        /// <param name="iLengthBeforeCompress">ѹ��֮ǰ��С��byte��</param>
        /// <param name="iLengthAfterCompress">ѹ��֮���С��byte��</param>
        /// <returns>���ؽ��Ϊѹ����ϵ�л����DataTable�����������</returns>
        public byte[] CompressToBytes(DataTable datatableToCompress, ref int iLengthBeforeCompress, ref int iLengthAfterCompress)
        {
            byte[] bArrayResult = null;

            try
            {
                datatableToCompress.RemotingFormat = SerializationFormat.Binary;
                MemoryStream ms = new MemoryStream();
                IFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, datatableToCompress);
                bArrayResult = ms.ToArray();
                iLengthBeforeCompress = bArrayResult.Length;

                CompressDataSet.Common.CompressionHelper commPress = new CompressionHelper();
                bArrayResult = commPress.CompressToBytes(bArrayResult);
                iLengthAfterCompress = bArrayResult.Length;
            }
            catch
            {
                bArrayResult = null;
                iLengthAfterCompress = iLengthBeforeCompress;
            }

            //���ؽ��
            return bArrayResult;

        }
        #endregion


        #region ����Ҫ����ѹ���Ķ���������ԭΪ���ݱ�
        /// <summary>
        /// ����Ҫ����ѹ���Ķ���������ԭΪ���ݱ�
        /// </summary>
        /// <param name="bytesToDecompress">��Ҫ����ѹ���Ķ���������Ϊϵ�л���ѹ�����DataTable����</param>
        /// <returns>����Ҫ����ѹ���Ķ���������ԭΪ���ݱ�</returns>
        public DataTable DecompressToDataTable(byte[] bytesToDecompress)
        {
            DataTable dt = null;
            byte[] bResult = null;

            try
            {
                CompressDataSet.Common.CompressionHelper commPress = new CompressDataSet.Common.CompressionHelper();
                bResult = commPress.DecompressToBytes(bytesToDecompress);

                MemoryStream ms = new MemoryStream(bResult);
                IFormatter bf = new BinaryFormatter();
                object objResult = bf.Deserialize(ms);
                dt = (DataTable)objResult;
            }
            catch
            {
                dt = null;
            }

            //���ؽ��
            return dt;
        }
        #endregion

    }
}
