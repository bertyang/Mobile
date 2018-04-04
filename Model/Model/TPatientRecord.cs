using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPatientRecord")]
	public class TPatientRecord
	{
		private string _任务编码;
		/// <summary>
		/// 任务编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "任务编码", DbType = "char(20)", Storage = "_任务编码")]
		public string 任务编码
		{
			get { return _任务编码; }
			set { _任务编码 = value; }
		}
		private int _序号;
		/// <summary>
		/// 序号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "序号", DbType = "int", Storage = "_序号")]
		public int 序号
		{
			get { return _序号; }
			set { _序号 = value; }
		}
		private string _姓名;
		/// <summary>
		/// 姓名
		/// </summary>
		[Column(Name = "姓名", DbType = "varchar(50)", Storage = "_姓名", UpdateCheck = UpdateCheck.Never)]
		public string 姓名
		{
			get { return _姓名; }
			set { _姓名 = value; }
		}
		private string _性别;
		/// <summary>
		/// 性别
		/// </summary>
		[Column(Name = "性别", DbType = "varchar(10)", Storage = "_性别", UpdateCheck = UpdateCheck.Never)]
		public string 性别
		{
			get { return _性别; }
			set { _性别 = value; }
		}
		private string _年龄;
		/// <summary>
		/// 年龄
		/// </summary>
		[Column(Name = "年龄", DbType = "varchar(10)", Storage = "_年龄", UpdateCheck = UpdateCheck.Never)]
		public string 年龄
		{
			get { return _年龄; }
			set { _年龄 = value; }
		}
		private string _国籍;
		/// <summary>
		/// 国籍
		/// </summary>
		[Column(Name = "国籍", DbType = "varchar(50)", Storage = "_国籍", UpdateCheck = UpdateCheck.Never)]
		public string 国籍
		{
			get { return _国籍; }
			set { _国籍 = value; }
		}
		private string _职业;
		/// <summary>
		/// 职业
		/// </summary>
		[Column(Name = "职业", DbType = "varchar(50)", Storage = "_职业", UpdateCheck = UpdateCheck.Never)]
		public string 职业
		{
			get { return _职业; }
			set { _职业 = value; }
		}
		private string _身份;
		/// <summary>
		/// 身份
		/// </summary>
		[Column(Name = "身份", DbType = "varchar(50)", Storage = "_身份", UpdateCheck = UpdateCheck.Never)]
		public string 身份
		{
			get { return _身份; }
			set { _身份 = value; }
		}
		private string _民族;
		/// <summary>
		/// 民族
		/// </summary>
		[Column(Name = "民族", DbType = "varchar(50)", Storage = "_民族", UpdateCheck = UpdateCheck.Never)]
		public string 民族
		{
			get { return _民族; }
			set { _民族 = value; }
		}
		private string _病情;
		/// <summary>
		/// 病情
		/// </summary>
		[Column(Name = "病情", DbType = "varchar(10)", Storage = "_病情", UpdateCheck = UpdateCheck.Never)]
		public string 病情
		{
			get { return _病情; }
			set { _病情 = value; }
		}
		private string _病因;
		/// <summary>
		/// 病因
		/// </summary>
		[Column(Name = "病因", DbType = "varchar(50)", Storage = "_病因", UpdateCheck = UpdateCheck.Never)]
		public string 病因
		{
			get { return _病因; }
			set { _病因 = value; }
		}
		private string _联系人;
		/// <summary>
		/// 联系人
		/// </summary>
		[Column(Name = "联系人", DbType = "varchar(50)", Storage = "_联系人", UpdateCheck = UpdateCheck.Never)]
		public string 联系人
		{
			get { return _联系人; }
			set { _联系人 = value; }
		}
		private string _联系电话;
		/// <summary>
		/// 联系电话
		/// </summary>
		[Column(Name = "联系电话", DbType = "varchar(14)", Storage = "_联系电话", UpdateCheck = UpdateCheck.Never)]
		public string 联系电话
		{
			get { return _联系电话; }
			set { _联系电话 = value; }
		}
		private string _现场地址;
		/// <summary>
		/// 现场地址
		/// </summary>
		[Column(Name = "现场地址", DbType = "varchar(200)", Storage = "_现场地址", UpdateCheck = UpdateCheck.Never)]
		public string 现场地址
		{
			get { return _现场地址; }
			set { _现场地址 = value; }
		}
		private int? _现场地址类型;
		/// <summary>
		/// 现场地址类型
		/// </summary>
		[Column(Name = "现场地址类型", DbType = "int", Storage = "_现场地址类型", UpdateCheck = UpdateCheck.Never)]
		public int? 现场地址类型
		{
			get { return _现场地址类型; }
			set { _现场地址类型 = value; }
		}
		private string _送往地点;
		/// <summary>
		/// 送往地点
		/// </summary>
		[Column(Name = "送往地点", DbType = "varchar(200)", Storage = "_送往地点", UpdateCheck = UpdateCheck.Never)]
		public string 送往地点
		{
			get { return _送往地点; }
			set { _送往地点 = value; }
		}
		private int? _送往地点类型;
		/// <summary>
		/// 送往地点类型
		/// </summary>
		[Column(Name = "送往地点类型", DbType = "int", Storage = "_送往地点类型", UpdateCheck = UpdateCheck.Never)]
		public int? 送往地点类型
		{
			get { return _送往地点类型; }
			set { _送往地点类型 = value; }
		}
		private string _住址;
		/// <summary>
		/// 住址
		/// </summary>
		[Column(Name = "住址", DbType = "varchar(200)", Storage = "_住址", UpdateCheck = UpdateCheck.Never)]
		public string 住址
		{
			get { return _住址; }
			set { _住址 = value; }
		}
		private string _主诉;
		/// <summary>
		/// 主诉
		/// </summary>
		[Column(Name = "主诉", DbType = "varchar(100)", Storage = "_主诉", UpdateCheck = UpdateCheck.Never)]
		public string 主诉
		{
			get { return _主诉; }
			set { _主诉 = value; }
		}
		private string _现病史;
		/// <summary>
		/// 现病史
		/// </summary>
		[Column(Name = "现病史", DbType = "varchar(500)", Storage = "_现病史", UpdateCheck = UpdateCheck.Never)]
		public string 现病史
		{
			get { return _现病史; }
			set { _现病史 = value; }
		}
		private string _既往病史;
		/// <summary>
		/// 既往病史
		/// </summary>
		[Column(Name = "既往病史", DbType = "varchar(500)", Storage = "_既往病史", UpdateCheck = UpdateCheck.Never)]
		public string 既往病史
		{
			get { return _既往病史; }
			set { _既往病史 = value; }
		}
		private string _过敏史;
		/// <summary>
		/// 过敏史
		/// </summary>
		[Column(Name = "过敏史", DbType = "varchar(500)", Storage = "_过敏史", UpdateCheck = UpdateCheck.Never)]
		public string 过敏史
		{
			get { return _过敏史; }
			set { _过敏史 = value; }
		}
		private string _途中变化记录;
		/// <summary>
		/// 途中变化记录
		/// </summary>
		[Column(Name = "途中变化记录", DbType = "varchar(1000)", Storage = "_途中变化记录", UpdateCheck = UpdateCheck.Never)]
		public string 途中变化记录
		{
			get { return _途中变化记录; }
			set { _途中变化记录 = value; }
		}
		private string _救治结果;
		/// <summary>
		/// 救治结果
		/// </summary>
		[Column(Name = "救治结果", DbType = "varchar(20)", Storage = "_救治结果", UpdateCheck = UpdateCheck.Never)]
		public string 救治结果
		{
			get { return _救治结果; }
			set { _救治结果 = value; }
		}
		private string _急救效果;
		/// <summary>
		/// 急救效果
		/// </summary>
		[Column(Name = "急救效果", DbType = "varchar(20)", Storage = "_急救效果", UpdateCheck = UpdateCheck.Never)]
		public string 急救效果
		{
			get { return _急救效果; }
			set { _急救效果 = value; }
		}
		private string _备注;
		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "备注", DbType = "varchar(1000)", Storage = "_备注", UpdateCheck = UpdateCheck.Never)]
		public string 备注
		{
			get { return _备注; }
			set { _备注 = value; }
		}
		private DateTime? _病历完成时间;
		/// <summary>
		/// 病历完成时间
		/// </summary>
		[Column(Name = "病历完成时间", DbType = "datetime", Storage = "_病历完成时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 病历完成时间
		{
			get { return _病历完成时间; }
			set { _病历完成时间 = value; }
		}
		private bool? _是否表单完成;
		/// <summary>
		/// 是否表单完成
		/// </summary>
		[Column(Name = "是否表单完成", DbType = "bit", Storage = "_是否表单完成", UpdateCheck = UpdateCheck.Never)]
		public bool? 是否表单完成
		{
			get { return _是否表单完成; }
			set { _是否表单完成 = value; }
		}
		private string _救治措施;
		/// <summary>
		/// 救治措施
		/// </summary>
		[Column(Name = "救治措施", DbType = "varchar(1000)", Storage = "_救治措施", UpdateCheck = UpdateCheck.Never)]
		public string 救治措施
		{
			get { return _救治措施; }
			set { _救治措施 = value; }
		}
		private string _诊断;
		/// <summary>
		/// 诊断
		/// </summary>
		[Column(Name = "诊断", DbType = "varchar(1000)", Storage = "_诊断", UpdateCheck = UpdateCheck.Never)]
		public string 诊断
		{
			get { return _诊断; }
			set { _诊断 = value; }
		}
		private int _填报人编码;
		/// <summary>
		/// 填报人编码
		/// </summary>
		[Column(Name = "填报人编码", DbType = "int", Storage = "_填报人编码", UpdateCheck = UpdateCheck.Never)]
		public int 填报人编码
		{
			get { return _填报人编码; }
			set { _填报人编码 = value; }
		}
		private string _保留字段1;
		/// <summary>
		/// 保留字段1
		/// </summary>
		[Column(Name = "保留字段1", DbType = "varchar(100)", Storage = "_保留字段1", UpdateCheck = UpdateCheck.Never)]
		public string 保留字段1
		{
			get { return _保留字段1; }
			set { _保留字段1 = value; }
		}
		private string _保留字段2;
		/// <summary>
		/// 保留字段2
		/// </summary>
		[Column(Name = "保留字段2", DbType = "varchar(100)", Storage = "_保留字段2", UpdateCheck = UpdateCheck.Never)]
		public string 保留字段2
		{
			get { return _保留字段2; }
			set { _保留字段2 = value; }
		}
		private string _保留字段3;
		/// <summary>
		/// 保留字段3
		/// </summary>
		[Column(Name = "保留字段3", DbType = "varchar(100)", Storage = "_保留字段3", UpdateCheck = UpdateCheck.Never)]
		public string 保留字段3
		{
			get { return _保留字段3; }
			set { _保留字段3 = value; }
		}
		private string _保留字段4;
		/// <summary>
		/// 保留字段4
		/// </summary>
		[Column(Name = "保留字段4", DbType = "varchar(100)", Storage = "_保留字段4", UpdateCheck = UpdateCheck.Never)]
		public string 保留字段4
		{
			get { return _保留字段4; }
			set { _保留字段4 = value; }
		}
		private string _Status;
		/// <summary>
		/// Status
		/// </summary>
		[Column(Name = "Status", DbType = "char(1)", Storage = "_Status", UpdateCheck = UpdateCheck.Never)]
		public string Status
		{
			get { return _Status; }
			set { _Status = value; }
		}
		private int? _WorkFlowNo;
		/// <summary>
		/// WorkFlowNo
		/// </summary>
		[Column(Name = "WorkFlowNo", DbType = "int", Storage = "_WorkFlowNo", UpdateCheck = UpdateCheck.Never)]
		public int? WorkFlowNo
		{
			get { return _WorkFlowNo; }
			set { _WorkFlowNo = value; }
		}
		private string _OutBillNo;
		/// <summary>
		/// OutBillNo
		/// </summary>
		[Column(Name = "OutBillNo", DbType = "varchar(20)", Storage = "_OutBillNo", UpdateCheck = UpdateCheck.Never)]
		public string OutBillNo
		{
			get { return _OutBillNo; }
			set { _OutBillNo = value; }
		}
		private string _Diagnoses;
		/// <summary>
		/// Diagnoses
		/// </summary>
		[Column(Name = "Diagnoses", DbType = "varchar(1000)", Storage = "_Diagnoses", UpdateCheck = UpdateCheck.Never)]
		public string Diagnoses
		{
			get { return _Diagnoses; }
			set { _Diagnoses = value; }
		}
		private bool? _IsPrint;
		/// <summary>
		/// IsPrint
		/// </summary>
		[Column(Name = "IsPrint", DbType = "bit", Storage = "_IsPrint", UpdateCheck = UpdateCheck.Never)]
		public bool? IsPrint
		{
			get { return _IsPrint; }
			set { _IsPrint = value; }
		}
		private DateTime? _TaskCompleteTime;
		/// <summary>
		/// TaskCompleteTime
		/// </summary>
		[Column(Name = "TaskCompleteTime", DbType = "datetime", Storage = "_TaskCompleteTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? TaskCompleteTime
		{
			get { return _TaskCompleteTime; }
			set { _TaskCompleteTime = value; }
		}
		private string _GlasgowEye;
		/// <summary>
		/// GlasgowEye
		/// </summary>
		[Column(Name = "GlasgowEye", DbType = "varchar(50)", Storage = "_GlasgowEye", UpdateCheck = UpdateCheck.Never)]
		public string GlasgowEye
		{
			get { return _GlasgowEye; }
			set { _GlasgowEye = value; }
		}
		private string _GlasgowVerbal;
		/// <summary>
		/// GlasgowVerbal
		/// </summary>
		[Column(Name = "GlasgowVerbal", DbType = "varchar(50)", Storage = "_GlasgowVerbal", UpdateCheck = UpdateCheck.Never)]
		public string GlasgowVerbal
		{
			get { return _GlasgowVerbal; }
			set { _GlasgowVerbal = value; }
		}
		private string _GlasgowMotor;
		/// <summary>
		/// GlasgowMotor
		/// </summary>
		[Column(Name = "GlasgowMotor", DbType = "varchar(50)", Storage = "_GlasgowMotor", UpdateCheck = UpdateCheck.Never)]
		public string GlasgowMotor
		{
			get { return _GlasgowMotor; }
			set { _GlasgowMotor = value; }
		}
		private string _Glasgow;
		/// <summary>
		/// Glasgow
		/// </summary>
		[Column(Name = "Glasgow", DbType = "varchar(50)", Storage = "_Glasgow", UpdateCheck = UpdateCheck.Never)]
		public string Glasgow
		{
			get { return _Glasgow; }
			set { _Glasgow = value; }
		}
	}
}