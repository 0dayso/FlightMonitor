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
	/// 压缩强度。
	/// </summary>
	public enum CompressionLevel
	{
		/// <summary>
		/// 采用最好的压缩率。
		/// </summary>
		BestCompression,

		/// <summary>
		/// 采用默认的压缩率。
		/// </summary>
		DefaultCompression,

		/// <summary>
		/// 采用最快的压缩速度。
		/// </summary>
		BestSpeed,

		/// <summary>
		/// 不采用任何压缩。
		/// </summary>
		NoCompression
	}

	/// <summary>
	/// CompressionHelper 的摘要说明。
	/// </summary>
	public class CompressionHelper
	{
		/// <summary>
		/// 获取和设置压缩强度。
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
		/// 从原始字节数组生成已压缩的字节数组。
		/// </summary>
		/// <param name="bytesToCompress">原始字节数组。</param>
		/// <returns>返回已压缩的字节数组</returns>
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
		/// 从原始字符串生成已压缩的字符串。
		/// </summary>
		/// <param name="stringToCompress">原始字符串。</param>
		/// <returns>返回已压缩的字符串。</returns>
		public string CompressToString(string stringToCompress)
		{
			byte[] compressedData = CompressToBytes(stringToCompress);
			string strOut = Convert.ToBase64String(compressedData);
			return strOut;
		}

		/// <summary>
		/// 从原始字符串生成已压缩的字节数组。
		/// </summary>
		/// <param name="stringToCompress">原始字符串。</param>
		/// <returns>返回已压缩的字节数组。</returns>
		public byte[] CompressToBytes(string stringToCompress)
		{
			byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);
			return CompressToBytes(bytData);
		}
        
		/// <summary>
		/// 从已压缩的字符串生成原始字符串。
		/// </summary>
		/// <param name="stringToDecompress">已压缩的字符串。</param>
		/// <returns>返回原始字符串。</returns>
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
		/// 从已压缩的字节数组生成原始字节数组。
		/// </summary>
		/// <param name="bytesToDecompress">已压缩的字节数组。</param>
		/// <returns>返回原始字节数组。</returns>
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
		/// 根据压缩强度返回使用了不用压缩算法的 Deflate 对象。
		/// </summary>
		/// <param name="level">压缩强度。</param>
		/// <returns>返回使用了不用压缩算法的 Deflate 对象。</returns>
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
		/// 从给定的流生成压缩输出流。
		/// </summary>
		/// <param name="inputStream">原始流。</param>
		/// <returns>返回压缩输出流。</returns>
		private DeflaterOutputStream GetOutputStream(Stream inputStream)
		{
			return new DeflaterOutputStream(inputStream, GetDeflater(Level));
		}

		/// <summary>
		/// 从给定的流生成压缩输入流。
		/// </summary>
		/// <param name="inputStream">原始流。</param>
		/// <returns>返回压缩输入流。</returns>
		private InflaterInputStream GetInputStream(Stream inputStream)
		{
			return new InflaterInputStream(inputStream);
		}
		#endregion


        //测试2
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
        //解压   
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

        //新添加的方法--2009.10.21
        #region 把数据表压缩和系列化成二进制流
        /// <summary>
        /// 把数据表压缩成二进制流
        /// </summary>
        /// <param name="datatableToCompress">需要压缩的数据表</param>
        /// <returns>返回结果为压缩和系列化后的DataTable对象二进制流</returns>
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

            //返回结果
            return bArrayResult;

        }
        #endregion

        #region 把数据表压缩和系列化成二进制流
        /// <summary>
        /// 把数据表压缩成二进制流
        /// </summary>
        /// <param name="datatableToCompress">需要压缩的数据表</param>
        /// <param name="iLengthBeforeCompress">压缩之前大小（byte）</param>
        /// <param name="iLengthAfterCompress">压缩之后大小（byte）</param>
        /// <returns>返回结果为压缩和系列化后的DataTable对象二进制流</returns>
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

            //返回结果
            return bArrayResult;

        }
        #endregion


        #region 把需要被解压缩的二进制流还原为数据表
        /// <summary>
        /// 把需要被解压缩的二进制流还原为数据表
        /// </summary>
        /// <param name="bytesToDecompress">需要被解压缩的二进制流（为系列化和压缩后的DataTable对象）</param>
        /// <returns>把需要被解压缩的二进制流还原为数据表</returns>
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

            //返回结果
            return dt;
        }
        #endregion

    }
}
