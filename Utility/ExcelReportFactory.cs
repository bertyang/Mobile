using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Anchor.FA.Utility;
using System.IO;
using System.Timers;

namespace Anchor.FA.Utility
{
    public class ExcelReportFactory
    {
        public static string CreateReport(string rootPath, string modelName, string dsName, Dictionary<string, string> parameters)
        {
            TcpClient client = new TcpClient();

            client.Connect(AppConfig.ReportServerIP, AppConfig.ReportServerPort);      // 与服务器连接
            NetworkStream streamToServer = client.GetStream();

            string msg = CreateMsg(modelName, dsName, parameters);
            byte[] buffer = Encoding.UTF8.GetBytes(msg);     // 获得缓存
            string slen = buffer.Length.ToString();
            slen = "0000".Substring(0, 4 - slen.Length) + slen;
            streamToServer.Write(Encoding.UTF8.GetBytes(slen), 0, 4);//长度

            streamToServer.Write(buffer, 0, buffer.Length);
            streamToServer.Flush();

            string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            filename = filename + modelName;
            //string mappath = Server.MapPath(filename);
            FileStream fs = new FileStream(rootPath+filename, FileMode.Create);

            byte[] Data = new byte[1024];
            int len;
            while((len = streamToServer.Read(Data, 0, Data.Length))>0){
                fs.Write(Data, 0, len);
            }

            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();

            //1小时后自动删除该文件
            ClearFile clearTask = new ClearFile(rootPath + filename);

            return filename;
        }

        class ClearFile
        {
            private string fileName;

            public ClearFile(string file)
            {
                fileName = file;

                //1小时后执行
                System.Timers.Timer t = new System.Timers.Timer(30 * 60 * 1000);
                //
                t.Elapsed += new System.Timers.ElapsedEventHandler(run);
                //设置是执行一次（false）还是一直执行(true)；  
                t.AutoReset = false; 
                t.Enabled = true;  
            }

            public void run(object sender, ElapsedEventArgs e)
            {
                if (File.Exists(fileName))
                {
                    FileInfo info = new FileInfo(fileName);
                    info.Delete();
                }
            }
        }

        /*报文解析类，解析的内容格式：字段名1#字段值1|字段名2#字段值2|字段名3#字段值3
         * 模型文件：modelFile,数据源文件：dsFile
         * **/
        private static string CreateMsg(string modelName, string dsName, Dictionary<string, string> parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("modelFile#");
            sb.Append(modelName);
            sb.Append("|");
            sb.Append("dsFile#");
            sb.Append(dsName);

            foreach (string key in parameters.Keys)
            {
                sb.Append("|");
                sb.Append(key);
                sb.Append("#");
                sb.Append(parameters[key]);
            }
            return sb.ToString();
        }
    }
}
