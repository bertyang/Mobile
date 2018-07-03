using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Data;

namespace Anchor.FA.BLL.BasicInfo
{
    public class API
    {
        public DataSet GetModifyRecord(string CNumber)
        {
            return Anchor.FA.DAL.BasicInfo.AlarmEvent.GetModifyRecord(CNumber);
        }


        public string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);
                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }


        //public static bool SavePatientRecord(List<PatientRecordInfo> PatientRecordInfo, List<MeasureInfo> MeasureInfo)
        //{
        //}

    }
}
