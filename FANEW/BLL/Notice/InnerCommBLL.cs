using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.ServiceModel;

using Anchor120V7.InnerComm;
using Anchor120V7.InnerComm.InnerCommModel;
//using Anchor.FA.BLL.Notice.CoreServiceV7;
using Anchor.FA.Utility;
using Anchor.FA.Utility.CoreServiceV7;


namespace Anchor.FA.BLL.Notice
{
    internal class InnerCommBLL : IBLL.ISMS
    {
        private static object lockObj = new object();
        private static InnerCommEntrance Entrance = null;//内部通信接口 

        /// <summary>
        /// 构造函数 
        /// </summary>
        public InnerCommBLL()
        {
         
        }

        private InnerCommEntrance GetInstance()
        {
            if (Entrance == null)
            {
                lock (lockObj)
                {
                    if (Entrance == null)
                    {
                        //BasicHttpBinding binding = new BasicHttpBinding();
                        //binding.Name = "ServiceSoap";
                        //binding.CloseTimeout = new TimeSpan(0, 1, 0);
                        //binding.OpenTimeout = new TimeSpan(0, 1, 0);
                        //binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                        //binding.SendTimeout = new TimeSpan(0, 1, 0);
                        //binding.AllowCookies = false;
                        //binding.BypassProxyOnLocal = false;
                        //binding.MaxBufferSize = 65536;
                        //binding.MaxBufferPoolSize = 524288;
                        //binding.MaxReceivedMessageSize = 65536;
                        //binding.MessageEncoding = WSMessageEncoding.Text;
                        //binding.TextEncoding = Encoding.UTF8;
                        //binding.TransferMode = TransferMode.Buffered;
                        //binding.UseDefaultWebProxy = true;
                        //binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                        //binding.Security.Mode = BasicHttpSecurityMode.None;
                        
                        //XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
                        //readerQuotas.MaxDepth=32 ;
                        //readerQuotas.MaxStringContentLength=8192;
                        //readerQuotas.MaxArrayLength=16384;
                        //readerQuotas.MaxBytesPerRead=4096;
                        //readerQuotas.MaxNameTableCharCount=16384;
                        //binding.ReaderQuotas = readerQuotas;

                        //EndpointAddress baseAddress = new EndpointAddress(AppConfig.CoreServiceUrl);

                        //CoreServiceV7.ServiceSoap coreService = new CoreServiceV7.ServiceSoapClient(binding,baseAddress);


                        ParameterNetInfo netinfo = CoreService.GetParameterNetInfo(1); ;//中心
                        List<string> GPSIPlist = new List<string>(netinfo.GpsServerIPList);
                        Entrance = new InnerCommEntrance(netinfo.BroadcastIP, netinfo.CommonPort, netinfo.CtiServerIP, netinfo.CtiPort, GPSIPlist, netinfo.GpsPort, netinfo.RecordPort);
                    }
                }
            }

            return Entrance;
        }

        #region 通知&短信
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="TelList">电话列表</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        public bool SendSMG(List<string> TelList, string Content, string OperatorCode)
        {
            try
            {
                GetInstance();

                Other_SMGInfo smginfo = new Other_SMGInfo();
                smginfo.DeskCode = "00";
                smginfo.TelCodeList = TelList;
                smginfo.SMGContent = Content;
                smginfo.OperatorCode = OperatorCode;
                Entrance.GetSendInstence().Other_SendSMG(smginfo);
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogError("Notice", ex.ToString());
               
                return false ;
            }
        }

        /// <summary>
        /// 发送车辆通知
        /// </summary>
        /// <returns></returns>
        public bool SendAmbNotice(string Content, List<string> Codelist, string OperationCode)
        {
            try
            {
                GetInstance();

                AmbDevice_NoticeInfo ambInfo = new AmbDevice_NoticeInfo();
                ambInfo.AmbulanceCodeList = Codelist;
                ambInfo.Content = Content;
                ambInfo.TypeId = NoticeTpyeSign.NTS_CheckedAmbs;//发送的车辆范围（所选车辆）
                ambInfo.SendNoticeAimType = SendNoticeAimType.SNAT_Ambs;//目的地
                ambInfo.OperatorCode = OperationCode;
                Entrance.GetSendInstence().AmbDevice_SendNotice(ambInfo);

                NoticeList notice = new NoticeList();
               
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogError("Notice", ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// 发送分站通知
        /// </summary>
        /// <returns></returns>
        public bool SendStationNotice(string Content, List<string> IPlist)
        {
            try
            {
                GetInstance();

                Other_NoticeInfo noticeInfo = new Other_NoticeInfo();
                noticeInfo.Content = Content;
                noticeInfo.SendNoticeAimType = SendNoticeAimType.SNAT_SubStations;
                noticeInfo.StationIPList = IPlist;           
                Entrance.GetSendInstence().Other_SendNotice(noticeInfo);
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogError("Notice", ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// 发送台通知
        /// </summary>
        /// <returns></returns>
        public bool SendDeskNotice(string Content, List<string> IPlist, bool IsBroadCast,string name)
        {
            try
            {
                GetInstance();

                BroadCast_DeskNoticeInfo deskInfo = new BroadCast_DeskNoticeInfo();
                if (!IsBroadCast && IPlist != null)
                {
                    foreach (string ip in IPlist)
                    {
                        deskInfo.OperationName = name;
                        deskInfo.RemoteAddress = IPAddress.Parse(ip.Trim());
                        deskInfo.DeskNoticeTpyeSign = DeskNoticeTpyeSign.DNTS_AllDesk;
                        deskInfo.Info = Content;
                        Entrance.GetSendInstence().Broadcast_DeskNotice(deskInfo);
                    }
                }
                else
                {
                    deskInfo.DeskNoticeTpyeSign = DeskNoticeTpyeSign.DNTS_AllDesk;
                    deskInfo.OperationName = name;
                    deskInfo.Info = Content;
                    Entrance.GetSendInstence().Broadcast_DeskNotice(deskInfo);
                }

                return true;

            }
            catch (Exception ex)
            {
                Log4Net.LogError("Notice", ex.ToString());
             
                return false;
            }
        }

        /// <summary>
        /// 存储通知单和短信
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="SendTime"></param>
        /// <param name="OperationCode"></param>
        /// <param name="TypeId"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        public void InsertNoticeMsg(string Content, DateTime SendTime, string OperationCode, int TypeId, List<string> Code, List<string> Name)
        {
            try
            {
                CoreService.GetService().SaveNoticeMessage(Content, SendTime, OperationCode, TypeId, Code.ToArray(), Name.ToArray());
            }
            catch (Exception ex)
            {
                Log4Net.LogError("NoticeBLL/InsertNoticeMsg", ex.Message);
            }
        }

        #endregion
    }
}
