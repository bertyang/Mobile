using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAcceptEvent")]
	public class TAcceptEvent
	{
		private string _事件编码;
		/// <summary>
		/// 事件编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "事件编码", DbType = "char(16)", Storage = "_事件编码")]
		public string 事件编码
		{
			get { return _事件编码; }
			set { _事件编码 = value; }
		}
		private int _受理序号;
		/// <summary>
		/// 受理序号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "受理序号", DbType = "int", Storage = "_受理序号")]
		public int 受理序号
		{
			get { return _受理序号; }
			set { _受理序号 = value; }
		}
		private int _受理类型编码;
		/// <summary>
		/// 受理类型编码
		/// </summary>
		[Column(Name = "受理类型编码", DbType = "int", Storage = "_受理类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 受理类型编码
		{
			get { return _受理类型编码; }
			set { _受理类型编码 = value; }
		}
		private int _受理类型子编码;
		/// <summary>
		/// 受理类型子编码
		/// </summary>
		[Column(Name = "受理类型子编码", DbType = "int", Storage = "_受理类型子编码", UpdateCheck = UpdateCheck.Never)]
		public int 受理类型子编码
		{
			get { return _受理类型子编码; }
			set { _受理类型子编码 = value; }
		}
		private string _责任受理人编码;
		/// <summary>
		/// 责任受理人编码
		/// </summary>
		[Column(Name = "责任受理人编码", DbType = "char(5)", Storage = "_责任受理人编码", UpdateCheck = UpdateCheck.Never)]
		public string 责任受理人编码
		{
			get { return _责任受理人编码; }
			set { _责任受理人编码 = value; }
		}
		private string _呼救电话;
		/// <summary>
		/// 呼救电话
		/// </summary>
		[Column(Name = "呼救电话", DbType = "varchar(14)", Storage = "_呼救电话", UpdateCheck = UpdateCheck.Never)]
		public string 呼救电话
		{
			get { return _呼救电话; }
			set { _呼救电话 = value; }
		}
		private DateTime? _挂起时刻;
		/// <summary>
		/// 挂起时刻
		/// </summary>
		[Column(Name = "挂起时刻", DbType = "datetime", Storage = "_挂起时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 挂起时刻
		{
			get { return _挂起时刻; }
			set { _挂起时刻 = value; }
		}
		private DateTime? _电话振铃时刻;
		/// <summary>
		/// 电话振铃时刻
		/// </summary>
		[Column(Name = "电话振铃时刻", DbType = "datetime", Storage = "_电话振铃时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 电话振铃时刻
		{
			get { return _电话振铃时刻; }
			set { _电话振铃时刻 = value; }
		}
		private DateTime _开始受理时刻;
		/// <summary>
		/// 开始受理时刻
		/// </summary>
		[Column(Name = "开始受理时刻", DbType = "datetime", Storage = "_开始受理时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 开始受理时刻
		{
			get { return _开始受理时刻; }
			set { _开始受理时刻 = value; }
		}
		private DateTime _结束受理时刻;
		/// <summary>
		/// 结束受理时刻
		/// </summary>
		[Column(Name = "结束受理时刻", DbType = "datetime", Storage = "_结束受理时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 结束受理时刻
		{
			get { return _结束受理时刻; }
			set { _结束受理时刻 = value; }
		}
		private DateTime? _发送指令时刻;
		/// <summary>
		/// 发送指令时刻
		/// </summary>
		[Column(Name = "发送指令时刻", DbType = "datetime", Storage = "_发送指令时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 发送指令时刻
		{
			get { return _发送指令时刻; }
			set { _发送指令时刻 = value; }
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
		private string _等车地址;
		/// <summary>
		/// 等车地址
		/// </summary>
		[Column(Name = "等车地址", DbType = "varchar(200)", Storage = "_等车地址", UpdateCheck = UpdateCheck.Never)]
		public string 等车地址
		{
			get { return _等车地址; }
			set { _等车地址 = value; }
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
		private int _往救地点类型编码;
		/// <summary>
		/// 往救地点类型编码
		/// </summary>
		[Column(Name = "往救地点类型编码", DbType = "int", Storage = "_往救地点类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 往救地点类型编码
		{
			get { return _往救地点类型编码; }
			set { _往救地点类型编码 = value; }
		}
		private int _送往地点类型编码;
		/// <summary>
		/// 送往地点类型编码
		/// </summary>
		[Column(Name = "送往地点类型编码", DbType = "int", Storage = "_送往地点类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 送往地点类型编码
		{
			get { return _送往地点类型编码; }
			set { _送往地点类型编码 = value; }
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
		private string _分机;
		/// <summary>
		/// 分机
		/// </summary>
		[Column(Name = "分机", DbType = "varchar(14)", Storage = "_分机", UpdateCheck = UpdateCheck.Never)]
		public string 分机
		{
			get { return _分机; }
			set { _分机 = value; }
		}
		private string _患者姓名;
		/// <summary>
		/// 患者姓名
		/// </summary>
		[Column(Name = "患者姓名", DbType = "varchar(50)", Storage = "_患者姓名", UpdateCheck = UpdateCheck.Never)]
		public string 患者姓名
		{
			get { return _患者姓名; }
			set { _患者姓名 = value; }
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
		[Column(Name = "年龄", DbType = "varchar(20)", Storage = "_年龄", UpdateCheck = UpdateCheck.Never)]
		public string 年龄
		{
			get { return _年龄; }
			set { _年龄 = value; }
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
		private string _病种判断;
		/// <summary>
		/// 病种判断
		/// </summary>
		[Column(Name = "病种判断", DbType = "varchar(100)", Storage = "_病种判断", UpdateCheck = UpdateCheck.Never)]
		public string 病种判断
		{
			get { return _病种判断; }
			set { _病种判断 = value; }
		}
		private int _病情编码;
		/// <summary>
		/// 病情编码
		/// </summary>
		[Column(Name = "病情编码", DbType = "int", Storage = "_病情编码", UpdateCheck = UpdateCheck.Never)]
		public int 病情编码
		{
			get { return _病情编码; }
			set { _病情编码 = value; }
		}
		private bool _是否需要担架;
		/// <summary>
		/// 是否需要担架
		/// </summary>
		[Column(Name = "是否需要担架", DbType = "bit", Storage = "_是否需要担架", UpdateCheck = UpdateCheck.Never)]
		public bool 是否需要担架
		{
			get { return _是否需要担架; }
			set { _是否需要担架 = value; }
		}
		private int _患者人数;
		/// <summary>
		/// 患者人数
		/// </summary>
		[Column(Name = "患者人数", DbType = "int", Storage = "_患者人数", UpdateCheck = UpdateCheck.Never)]
		public int 患者人数
		{
			get { return _患者人数; }
			set { _患者人数 = value; }
		}
		private string _特殊要求;
		/// <summary>
		/// 特殊要求
		/// </summary>
		[Column(Name = "特殊要求", DbType = "varchar(50)", Storage = "_特殊要求", UpdateCheck = UpdateCheck.Never)]
		public string 特殊要求
		{
			get { return _特殊要求; }
			set { _特殊要求 = value; }
		}
		private bool _是否标注;
		/// <summary>
		/// 是否标注
		/// </summary>
		[Column(Name = "是否标注", DbType = "bit", Storage = "_是否标注", UpdateCheck = UpdateCheck.Never)]
		public bool 是否标注
		{
			get { return _是否标注; }
			set { _是否标注 = value; }
		}
		private double _X坐标;
		/// <summary>
		/// X坐标
		/// </summary>
		[Column(Name = "X坐标", DbType = "float", Storage = "_X坐标", UpdateCheck = UpdateCheck.Never)]
		public double X坐标
		{
			get { return _X坐标; }
			set { _X坐标 = value; }
		}
		private double _Y坐标;
		/// <summary>
		/// Y坐标
		/// </summary>
		[Column(Name = "Y坐标", DbType = "float", Storage = "_Y坐标", UpdateCheck = UpdateCheck.Never)]
		public double Y坐标
		{
			get { return _Y坐标; }
			set { _Y坐标 = value; }
		}
		private string _派车列表;
		/// <summary>
		/// 派车列表
		/// </summary>
		[Column(Name = "派车列表", DbType = "varchar(5000)", Storage = "_派车列表", UpdateCheck = UpdateCheck.Never)]
		public string 派车列表
		{
			get { return _派车列表; }
			set { _派车列表 = value; }
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
		private int _中心编码;
		/// <summary>
		/// 中心编码
		/// </summary>
		[Column(Name = "中心编码", DbType = "int", Storage = "_中心编码", UpdateCheck = UpdateCheck.Never)]
		public int 中心编码
		{
			get { return _中心编码; }
			set { _中心编码 = value; }
		}
		private string _MPDS备注;
		/// <summary>
		/// MPDS备注
		/// </summary>
		[Column(Name = "MPDS备注", DbType = "varchar(2000)", Storage = "_MPDS备注", UpdateCheck = UpdateCheck.Never)]
		public string MPDS备注
		{
			get { return _MPDS备注; }
			set { _MPDS备注 = value; }
		}
	}
}