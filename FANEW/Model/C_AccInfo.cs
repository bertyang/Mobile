using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_AccInfo
    //{
    //    public string Tel { get; set; }
    //    public string ZhuSu { get; set; }
    //    public string Name { get; set; }
    //    public string Sex { get; set; }
    //    public string Age { get; set; }
    //    public string Nationnality { get; set; }
    //    public string Nation { get; set; }
    //    public string Connector { get; set; }
    //    public string ConnectTel { get; set; }
    //    public string LocalAddr { get; set; }
    //    public string TotalAddr { get; set; }
    //    public string Dispatcher { get; set; }
    //    public string Type { get; set; }
    //    public string RingTime { get; set; }
    //    public string BeginTime { get; set; }
    //    public string EndTime { get; set; }
    //    public string SendTime { get; set; }
    //    public string CarList { get; set; }
    //    public string Remark { get; set; }
    //}
    {
        private string m_EventCode;//事件编码
        /// <summary>
        /// 事件编码--
        /// </summary>
        public string EventCode
        {
            get { return m_EventCode; }
            set { m_EventCode = value; }
        }

        private int m_AcceptOrder;//受理序号
        /// <summary>
        /// 受理序号
        /// </summary>
        public int AcceptOrder
        {
            get { return m_AcceptOrder; }
            set { m_AcceptOrder = value; }
        }

        private int m_TypeId;//受理类型编码
        /// <summary>
        /// 受理类型编码
        /// </summary>
        public int TypeId
        {
            get { return m_TypeId; }
            set { m_TypeId = value; }
        }

        private int m_DetailReasonId;//受理类型子编码
        /// <summary>
        /// 受理类型子编码
        /// </summary>
        public int DetailReasonId
        {
            get { return m_DetailReasonId; }
            set { m_DetailReasonId = value; }
        }

        private string m_AcceptPersonCode;//责任受理人编码
        /// <summary>
        /// 责任受理人编码
        /// </summary>
        public string AcceptPersonCode
        {
            get { return m_AcceptPersonCode; }
            set { m_AcceptPersonCode = value; }
        }

        private string m_AlarmTel;//呼救电话
        /// <summary>
        /// 呼救电话
        /// </summary>
        public string AlarmTel
        {
            get { return m_AlarmTel; }
            set { m_AlarmTel = value; }
        }

        private Nullable<DateTime> m_HangUpTime = null;//挂起时刻
        /// <summary>
        /// 挂起时刻
        /// </summary>
        public Nullable<DateTime> HangUpTime
        {
            get { return m_HangUpTime; }
            set { m_HangUpTime = value; }
        }

        private Nullable<DateTime> m_RingTime = null;//电话震铃时刻
        /// <summary>
        /// 电话震铃时刻
        /// </summary>
        public Nullable<DateTime> RingTime
        {
            get { return m_RingTime; }
            set { m_RingTime = value; }
        }

        private Nullable<DateTime> m_AcceptBeginTime = null;//开始受理时刻
        /// <summary>
        /// 开始受理时刻
        /// </summary>
        public Nullable<DateTime> AcceptBeginTime
        {
            get { return m_AcceptBeginTime; }
            set { m_AcceptBeginTime = value; }
        }

        private Nullable<DateTime> m_AcceptEndTime = null;//结束受理时刻
        /// <summary>
        /// 结束受理时刻
        /// </summary>
        public Nullable<DateTime> AcceptEndTime
        {
            get { return m_AcceptEndTime; }
            set { m_AcceptEndTime = value; }
        }

        private Nullable<DateTime> m_CommandTime = null;//发送指令时刻
        /// <summary>
        /// 发送指令时刻
        /// </summary>
        public Nullable<DateTime> CommandTime
        {
            get { return m_CommandTime; }
            set { m_CommandTime = value; }
        }

        private string m_LocalAddr;//现场地址
        /// <summary>
        /// 现场地址
        /// </summary>
        public string LocalAddr
        {
            get { return m_LocalAddr; }
            set { m_LocalAddr = value; }
        }

        private string m_WaitAddr;//等车地址
        /// <summary>
        /// 等车地址
        /// </summary>
        public string WaitAddr
        {
            get { return m_WaitAddr; }
            set { m_WaitAddr = value; }
        }

        private string m_SendAddr;//送往地点
        /// <summary>
        /// 送往地点
        /// </summary>
        public string SendAddr
        {
            get { return m_SendAddr; }
            set { m_SendAddr = value; }
        }

        private int m_LocalAddrTypeId;//往救地点类型编码
        /// <summary>
        /// 往救地点类型编码
        /// </summary>
        public int LocalAddrTypeId
        {
            get { return m_LocalAddrTypeId; }
            set { m_LocalAddrTypeId = value; }
        }

        private int m_SendAddrTypeId;//送往地点类型编码
        /// <summary>
        /// 送往地点类型编码
        /// </summary>
        public int SendAddrTypeId
        {
            get { return m_SendAddrTypeId; }
            set { m_SendAddrTypeId = value; }
        }

        private string m_LinkMan;//联系人
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            get { return m_LinkMan; }
            set { m_LinkMan = value; }
        }

        private string m_LinkTel;//联系电话
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkTel
        {
            get { return m_LinkTel; }
            set { m_LinkTel = value; }
        }

        private string m_Extension;//分机
        /// <summary>
        /// 分机
        /// </summary>
        public string Extension
        {
            get { return m_Extension; }
            set { m_Extension = value; }
        }

        private string m_PatientName;//患者姓名
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName
        {
            get { return m_PatientName; }
            set { m_PatientName = value; }
        }

        private string m_Sex;//性别
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return m_Sex; }
            set { m_Sex = value; }
        }

        private string m_Age;//年龄
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            get { return m_Age; }
            set { m_Age = value; }
        }

        private string m_Folk;//民族
        /// <summary>
        /// 民族
        /// </summary>
        public string Folk
        {
            get { return m_Folk; }
            set { m_Folk = value; }
        }

        private string m_National;//国籍
        /// <summary>
        /// 国籍
        /// </summary>
        public string National
        {
            get { return m_National; }
            set { m_National = value; }
        }

        private string m_AlarmReason;//主诉
        /// <summary>
        /// 主诉
        /// </summary>
        public string AlarmReason
        {
            get { return m_AlarmReason; }
            set { m_AlarmReason = value; }
        }

        private string m_Judge;//病种判断
        /// <summary>
        /// 病种判断
        /// </summary>
        public string Judge
        {
            get { return m_Judge; }
            set { m_Judge = value; }
        }

        private int m_IllStateId;//病情编码
        /// <summary>
        /// 病情编码
        /// </summary>
        public int IllStateId
        {
            get { return m_IllStateId; }
            set { m_IllStateId = value; }
        }

        private bool m_IsNeedLitter;//是否需要担架
        /// <summary>
        /// 是否需要担架
        /// </summary>
        public bool IsNeedLitter
        {
            get { return m_IsNeedLitter; }
            set { m_IsNeedLitter = value; }
        }

        private int m_PatientCount;//患者人数
        /// <summary>
        /// 患者人数
        /// </summary>
        public int PatientCount
        {
            get { return m_PatientCount; }
            set { m_PatientCount = value; }
        }

        private string m_SpecialNeed;//特殊要求
        /// <summary>
        /// 特殊要求
        /// </summary>
        public string SpecialNeed
        {
            get { return m_SpecialNeed; }
            set { m_SpecialNeed = value; }
        }

        private bool m_IsLabeled;//是否标注
        /// <summary>
        /// 是否标注
        /// </summary>
        public bool IsLabeled
        {
            get { return m_IsLabeled; }
            set { m_IsLabeled = value; }
        }

        private double m_X;//X坐标
        /// <summary>
        /// X坐标
        /// </summary>
        public double X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        private double m_Y;//Y坐标
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        private string m_AmbulanceList;//派车列表
        /// <summary>
        /// 派车列表
        /// </summary>
        public string AmbulanceList
        {
            get { return m_AmbulanceList; }
            set { m_AmbulanceList = value; }
        }

        private string m_BackUpOne;//保留字段1
        /// <summary>
        /// 保留字段1
        /// </summary>
        public string BackUpOne
        {
            get { return m_BackUpOne; }
            set { m_BackUpOne = value; }
        }
        private string m_BackUpTwo;//保留字段2
        /// <summary>
        /// 保留字段2
        /// </summary>
        public string BackUpTwo
        {
            get { return m_BackUpTwo; }
            set { m_BackUpTwo = value; }
        }

        private string m_Remark;//备注
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }

        private int m_CenterId;//中心编码
        /// <summary>
        /// 中心编码
        /// </summary>
        public int CenterId
        {
            get { return m_CenterId; }
            set { m_CenterId = value; }
        }
        //-----------------------------以上为实际表格TAcceptEvent中的 字段名
        private string m_AcceptType;
        /// <summary>
        /// 受理类型
        /// </summary>
        public string AcceptType
        {
            get { return m_AcceptType; }
            set { m_AcceptType = value; }
        }

        private string m_Dispatcher;
        /// <summary>
        /// 责任调度员
        /// </summary>
        public string Dispatcher
        {
            get { return m_Dispatcher; }
            set { m_Dispatcher = value; }
        }


        private string m_MPDSRemark;
        /// <summary>
        /// MPDS备注
        /// </summary>
        public string MPDSRemark
        {
            get { return m_MPDSRemark; }
            set { m_MPDSRemark = value; }
        }


        //张炜2016年1月27日新增
        private string m_Reason;
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason
        {
            get { return m_Reason; }
            set { m_Reason = value; }
        }

        private string m_IllState;//病情
        /// <summary>
        /// 病情
        /// </summary>
        public string IllState
        {
            get { return m_IllState; }
            set { m_IllState = value; }
        }

        private string m_LocalAddrType;//往救地点类型
        /// <summary>
        /// 往救地点类型
        /// </summary>
        public string LocalAddrType
        {
            get { return m_LocalAddrType; }
            set { m_LocalAddrType = value; }
        }

        private string m_SendAddrType;//送往地点类型
        /// <summary>
        /// 送往地点类型
        /// </summary>
        public string SendAddrType
        {
            get { return m_SendAddrType; }
            set { m_SendAddrType = value; }
        }
        //患者人数
//        原因
//患者人数 label_PatientCount


//电话振铃时刻:     开始受理时刻:   2015/8/28 10:07:19  挂起时刻:     
//发送指令时刻:   2015/8/28 10:07:24  结束受理时刻:   2015/8/28 10:07:24      
//病情:     
//往救地点类型



    }
}
