using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TTask")]
	public class TTask
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
		private string _事件编码;
		/// <summary>
		/// 事件编码
		/// </summary>
		[Column(Name = "事件编码", DbType = "char(16)", Storage = "_事件编码", UpdateCheck = UpdateCheck.Never)]
		public string 事件编码
		{
			get { return _事件编码; }
			set { _事件编码 = value; }
		}
		private int _受理序号;
		/// <summary>
		/// 受理序号
		/// </summary>
		[Column(Name = "受理序号", DbType = "int", Storage = "_受理序号", UpdateCheck = UpdateCheck.Never)]
		public int 受理序号
		{
			get { return _受理序号; }
			set { _受理序号 = value; }
		}
		private int _任务流水号;
		/// <summary>
		/// 任务流水号
		/// </summary>
		[Column(Name = "任务流水号", DbType = "int", Storage = "_任务流水号", UpdateCheck = UpdateCheck.Never)]
		public int 任务流水号
		{
			get { return _任务流水号; }
			set { _任务流水号 = value; }
		}
		private string _用户流水号;
		/// <summary>
		/// 用户流水号
		/// </summary>
		[Column(Name = "用户流水号", DbType = "varchar(20)", Storage = "_用户流水号", UpdateCheck = UpdateCheck.Never)]
		public string 用户流水号
		{
			get { return _用户流水号; }
			set { _用户流水号 = value; }
		}
		private string _责任调度人编码;
		/// <summary>
		/// 责任调度人编码
		/// </summary>
		[Column(Name = "责任调度人编码", DbType = "char(5)", Storage = "_责任调度人编码", UpdateCheck = UpdateCheck.Never)]
		public string 责任调度人编码
		{
			get { return _责任调度人编码; }
			set { _责任调度人编码 = value; }
		}
		private string _分站编码;
		/// <summary>
		/// 分站编码
		/// </summary>
		[Column(Name = "分站编码", DbType = "char(3)", Storage = "_分站编码", UpdateCheck = UpdateCheck.Never)]
		public string 分站编码
		{
			get { return _分站编码; }
			set { _分站编码 = value; }
		}
		private string _车辆编码;
		/// <summary>
		/// 车辆编码
		/// </summary>
		[Column(Name = "车辆编码", DbType = "char(5)", Storage = "_车辆编码", UpdateCheck = UpdateCheck.Never)]
		public string 车辆编码
		{
			get { return _车辆编码; }
			set { _车辆编码 = value; }
		}
		private bool _是否执行中;
		/// <summary>
		/// 是否执行中
		/// </summary>
		[Column(Name = "是否执行中", DbType = "bit", Storage = "_是否执行中", UpdateCheck = UpdateCheck.Never)]
		public bool 是否执行中
		{
			get { return _是否执行中; }
			set { _是否执行中 = value; }
		}
		private DateTime _生成任务时刻;
		/// <summary>
		/// 生成任务时刻
		/// </summary>
		[Column(Name = "生成任务时刻", DbType = "datetime", Storage = "_生成任务时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 生成任务时刻
		{
			get { return _生成任务时刻; }
			set { _生成任务时刻 = value; }
		}
		private DateTime? _接收命令时刻;
		/// <summary>
		/// 接收命令时刻
		/// </summary>
		[Column(Name = "接收命令时刻", DbType = "datetime", Storage = "_接收命令时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 接收命令时刻
		{
			get { return _接收命令时刻; }
			set { _接收命令时刻 = value; }
		}
		private DateTime? _出车时刻;
		/// <summary>
		/// 出车时刻
		/// </summary>
		[Column(Name = "出车时刻", DbType = "datetime", Storage = "_出车时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 出车时刻
		{
			get { return _出车时刻; }
			set { _出车时刻 = value; }
		}
		private DateTime? _到达现场时刻;
		/// <summary>
		/// 到达现场时刻
		/// </summary>
		[Column(Name = "到达现场时刻", DbType = "datetime", Storage = "_到达现场时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 到达现场时刻
		{
			get { return _到达现场时刻; }
			set { _到达现场时刻 = value; }
		}
		private DateTime? _离开现场时刻;
		/// <summary>
		/// 离开现场时刻
		/// </summary>
		[Column(Name = "离开现场时刻", DbType = "datetime", Storage = "_离开现场时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 离开现场时刻
		{
			get { return _离开现场时刻; }
			set { _离开现场时刻 = value; }
		}
		private DateTime? _到达医院时刻;
		/// <summary>
		/// 到达医院时刻
		/// </summary>
		[Column(Name = "到达医院时刻", DbType = "datetime", Storage = "_到达医院时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 到达医院时刻
		{
			get { return _到达医院时刻; }
			set { _到达医院时刻 = value; }
		}
		private DateTime? _完成时刻;
		/// <summary>
		/// 完成时刻
		/// </summary>
		[Column(Name = "完成时刻", DbType = "datetime", Storage = "_完成时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 完成时刻
		{
			get { return _完成时刻; }
			set { _完成时刻 = value; }
		}
		private DateTime? _返回站中时刻;
		/// <summary>
		/// 返回站中时刻
		/// </summary>
		[Column(Name = "返回站中时刻", DbType = "datetime", Storage = "_返回站中时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 返回站中时刻
		{
			get { return _返回站中时刻; }
			set { _返回站中时刻 = value; }
		}
		private double? _急救公里数;
		/// <summary>
		/// 急救公里数
		/// </summary>
		[Column(Name = "急救公里数", DbType = "float", Storage = "_急救公里数", UpdateCheck = UpdateCheck.Never)]
		public double? 急救公里数
		{
			get { return _急救公里数; }
			set { _急救公里数 = value; }
		}
		private double? _行驶公里数;
		/// <summary>
		/// 行驶公里数
		/// </summary>
		[Column(Name = "行驶公里数", DbType = "float", Storage = "_行驶公里数", UpdateCheck = UpdateCheck.Never)]
		public double? 行驶公里数
		{
			get { return _行驶公里数; }
			set { _行驶公里数 = value; }
		}
		private bool _是否正常结束;
		/// <summary>
		/// 是否正常结束
		/// </summary>
		[Column(Name = "是否正常结束", DbType = "bit", Storage = "_是否正常结束", UpdateCheck = UpdateCheck.Never)]
		public bool 是否正常结束
		{
			get { return _是否正常结束; }
			set { _是否正常结束 = value; }
		}
		private int _异常结束原因编码;
		/// <summary>
		/// 异常结束原因编码
		/// </summary>
		[Column(Name = "异常结束原因编码", DbType = "int", Storage = "_异常结束原因编码", UpdateCheck = UpdateCheck.Never)]
		public int 异常结束原因编码
		{
			get { return _异常结束原因编码; }
			set { _异常结束原因编码 = value; }
		}
		private int _实际救治人数;
		/// <summary>
		/// 实际救治人数
		/// </summary>
		[Column(Name = "实际救治人数", DbType = "int", Storage = "_实际救治人数", UpdateCheck = UpdateCheck.Never)]
		public int 实际救治人数
		{
			get { return _实际救治人数; }
			set { _实际救治人数 = value; }
		}
		private bool _是否站内出动;
		/// <summary>
		/// 是否站内出动
		/// </summary>
		[Column(Name = "是否站内出动", DbType = "bit", Storage = "_是否站内出动", UpdateCheck = UpdateCheck.Never)]
		public bool 是否站内出动
		{
			get { return _是否站内出动; }
			set { _是否站内出动 = value; }
		}
		private string _司机;
		/// <summary>
		/// 司机
		/// </summary>
		[Column(Name = "司机", DbType = "varchar(50)", Storage = "_司机", UpdateCheck = UpdateCheck.Never)]
		public string 司机
		{
			get { return _司机; }
			set { _司机 = value; }
		}
		private string _医生;
		/// <summary>
		/// 医生
		/// </summary>
		[Column(Name = "医生", DbType = "varchar(50)", Storage = "_医生", UpdateCheck = UpdateCheck.Never)]
		public string 医生
		{
			get { return _医生; }
			set { _医生 = value; }
		}
		private string _护士;
		/// <summary>
		/// 护士
		/// </summary>
		[Column(Name = "护士", DbType = "varchar(50)", Storage = "_护士", UpdateCheck = UpdateCheck.Never)]
		public string 护士
		{
			get { return _护士; }
			set { _护士 = value; }
		}
		private string _担架工;
		/// <summary>
		/// 担架工
		/// </summary>
		[Column(Name = "担架工", DbType = "varchar(50)", Storage = "_担架工", UpdateCheck = UpdateCheck.Never)]
		public string 担架工
		{
			get { return _担架工; }
			set { _担架工 = value; }
		}
		private string _抢救员;
		/// <summary>
		/// 抢救员
		/// </summary>
		[Column(Name = "抢救员", DbType = "varchar(50)", Storage = "_抢救员", UpdateCheck = UpdateCheck.Never)]
		public string 抢救员
		{
			get { return _抢救员; }
			set { _抢救员 = value; }
		}
		private string _改派前任务编码;
		/// <summary>
		/// 改派前任务编码
		/// </summary>
		[Column(Name = "改派前任务编码", DbType = "char(20)", Storage = "_改派前任务编码", UpdateCheck = UpdateCheck.Never)]
		public string 改派前任务编码
		{
			get { return _改派前任务编码; }
			set { _改派前任务编码 = value; }
		}
		private string _实际送往地点;
		/// <summary>
		/// 实际送往地点
		/// </summary>
		[Column(Name = "实际送往地点", DbType = "varchar(100)", Storage = "_实际送往地点", UpdateCheck = UpdateCheck.Never)]
		public string 实际送往地点
		{
			get { return _实际送往地点; }
			set { _实际送往地点 = value; }
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
	}
}