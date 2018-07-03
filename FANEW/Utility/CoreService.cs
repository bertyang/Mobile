using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace Anchor.FA.Utility
{
    public class CoreService
    {
        public static CoreServiceV7.ServiceSoapClient m_AnchorService = null;
        /// <summary>
        /// 得到WEBServeice方法服务
        /// </summary>
        /// <returns></returns>
        public static CoreServiceV7.ServiceSoapClient GetService()
        {
            if (m_AnchorService == null)
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Name = "ServiceSoap";
                binding.CloseTimeout = new TimeSpan(0, 1, 0);
                binding.OpenTimeout = new TimeSpan(0, 1, 0);
                binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                binding.SendTimeout = new TimeSpan(0, 1, 0);
                binding.AllowCookies = false;
                binding.BypassProxyOnLocal = false;
                binding.MaxBufferSize = 65536;
                binding.MaxBufferPoolSize = 524288;
                binding.MaxReceivedMessageSize = 65536;
                binding.MessageEncoding = WSMessageEncoding.Text;
                binding.TextEncoding = Encoding.UTF8;
                binding.TransferMode = TransferMode.Buffered;
                binding.UseDefaultWebProxy = true;
                binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                binding.Security.Mode = BasicHttpSecurityMode.None;

                XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
                readerQuotas.MaxDepth = 32;
                readerQuotas.MaxStringContentLength = 8192;
                readerQuotas.MaxArrayLength = 16384;
                readerQuotas.MaxBytesPerRead = 4096;
                readerQuotas.MaxNameTableCharCount = 16384;
                binding.ReaderQuotas = readerQuotas;

                EndpointAddress baseAddress = new EndpointAddress(AppConfig.CoreServiceUrl);

                m_AnchorService = new CoreServiceV7.ServiceSoapClient(binding, baseAddress);
            }
            return m_AnchorService;
        }


        public static void AmbulancePersonCheckIn(string personCode, string ambCode, int operationOrigin, string operatorCode, DateTime operateTime)
        {
            CoreService.GetService().AmbulancePersonCheckIn(personCode, ambCode, operationOrigin, operatorCode, operateTime);
        }
        public static void AmbulancePersonCheckOut(string personCode, string ambCode, int operationOrigin, string operatorCode, DateTime operateTime)
        {
            CoreService.GetService().AmbulancePersonCheckOut(personCode, ambCode, operationOrigin, operatorCode, operateTime);
        }

        public static void ModifyState(string ambulanceCode, int newState, System.DateTime happendTime, int operationOrigin, string operatePersonCode, string taskCode)
        {
            CoreService.GetService().ModifyState(ambulanceCode, newState, happendTime, operationOrigin, operatePersonCode, taskCode);
        }

        public static CoreServiceV7.ParameterNetInfo GetParameterNetInfo(int centerId)
        {
            return CoreService.GetService().GetParameterNetInfo(centerId);
        }
    }

    
}
