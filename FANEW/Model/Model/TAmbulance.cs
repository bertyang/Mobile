using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAmbulance")]
	public class TAmbulance
	{
		private string _车辆编码;
		/// <summary>
		/// 车辆编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "车辆编码", DbType = "char(5)", Storage = "_车辆编码")]
		public string 车辆编码
		{
			get { return _车辆编码; }
			set { _车辆编码 = value; }
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
		private int _车辆类型编码;
		/// <summary>
		/// 车辆类型编码
		/// </summary>
		[Column(Name = "车辆类型编码", DbType = "int", Storage = "_车辆类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 车辆类型编码
		{
			get { return _车辆类型编码; }
			set { _车辆类型编码 = value; }
		}
		private string _车牌号码;
		/// <summary>
		/// 车牌号码
		/// </summary>
		[Column(Name = "车牌号码", DbType = "varchar(20)", Storage = "_车牌号码", UpdateCheck = UpdateCheck.Never)]
		public string 车牌号码
		{
			get { return _车牌号码; }
			set { _车牌号码 = value; }
		}
		private string _实际标识;
		/// <summary>
		/// 实际标识
		/// </summary>
		[Column(Name = "实际标识", DbType = "varchar(20)", Storage = "_实际标识", UpdateCheck = UpdateCheck.Never)]
		public string 实际标识
		{
			get { return _实际标识; }
			set { _实际标识 = value; }
		}
		private int _命令单发送去向编码;
		/// <summary>
		/// 命令单发送去向编码
		/// </summary>
		[Column(Name = "命令单发送去向编码", DbType = "int", Storage = "_命令单发送去向编码", UpdateCheck = UpdateCheck.Never)]
		public int 命令单发送去向编码
		{
			get { return _命令单发送去向编码; }
			set { _命令单发送去向编码 = value; }
		}
		private string _任务编码;
		/// <summary>
		/// 任务编码
		/// </summary>
		[Column(Name = "任务编码", DbType = "char(20)", Storage = "_任务编码", UpdateCheck = UpdateCheck.Never)]
		public string 任务编码
		{
			get { return _任务编码; }
			set { _任务编码 = value; }
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
		private int _工作状态编码;
		/// <summary>
		/// 工作状态编码
		/// </summary>
		[Column(Name = "工作状态编码", DbType = "int", Storage = "_工作状态编码", UpdateCheck = UpdateCheck.Never)]
		public int 工作状态编码
		{
			get { return _工作状态编码; }
			set { _工作状态编码 = value; }
		}
		private int _当班执行任务数;
		/// <summary>
		/// 当班执行任务数
		/// </summary>
		[Column(Name = "当班执行任务数", DbType = "int", Storage = "_当班执行任务数", UpdateCheck = UpdateCheck.Never)]
		public int 当班执行任务数
		{
			get { return _当班执行任务数; }
			set { _当班执行任务数 = value; }
		}
		private string _司机;
		/// <summary>
		/// 司机
		/// </summary>
		[Column(Name = "司机", DbType = "varchar(30)", Storage = "_司机", UpdateCheck = UpdateCheck.Never)]
		public string 司机
		{
			get { return _司机; }
			set { _司机 = value; }
		}
		private string _医生;
		/// <summary>
		/// 医生
		/// </summary>
		[Column(Name = "医生", DbType = "varchar(30)", Storage = "_医生", UpdateCheck = UpdateCheck.Never)]
		public string 医生
		{
			get { return _医生; }
			set { _医生 = value; }
		}
		private string _护士;
		/// <summary>
		/// 护士
		/// </summary>
		[Column(Name = "护士", DbType = "varchar(30)", Storage = "_护士", UpdateCheck = UpdateCheck.Never)]
		public string 护士
		{
			get { return _护士; }
			set { _护士 = value; }
		}
		private string _担架工;
		/// <summary>
		/// 担架工
		/// </summary>
		[Column(Name = "担架工", DbType = "varchar(30)", Storage = "_担架工", UpdateCheck = UpdateCheck.Never)]
		public string 担架工
		{
			get { return _担架工; }
			set { _担架工 = value; }
		}
		private string _抢救员;
		/// <summary>
		/// 抢救员
		/// </summary>
		[Column(Name = "抢救员", DbType = "varchar(30)", Storage = "_抢救员", UpdateCheck = UpdateCheck.Never)]
		public string 抢救员
		{
			get { return _抢救员; }
			set { _抢救员 = value; }
		}
		private DateTime? _按键时刻;
		/// <summary>
		/// 按键时刻
		/// </summary>
		[Column(Name = "按键时刻", DbType = "datetime", Storage = "_按键时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 按键时刻
		{
			get { return _按键时刻; }
			set { _按键时刻 = value; }
		}
		private string _随车电话;
		/// <summary>
		/// 随车电话
		/// </summary>
		[Column(Name = "随车电话", DbType = "varchar(14)", Storage = "_随车电话", UpdateCheck = UpdateCheck.Never)]
		public string 随车电话
		{
			get { return _随车电话; }
			set { _随车电话 = value; }
		}
		private int? _分组编码;
		/// <summary>
		/// 分组编码
		/// </summary>
		[Column(Name = "分组编码", DbType = "int", Storage = "_分组编码", UpdateCheck = UpdateCheck.Never)]
		public int? 分组编码
		{
			get { return _分组编码; }
			set { _分组编码 = value; }
		}
		private int _车辆等级编码;
		/// <summary>
		/// 车辆等级编码
		/// </summary>
		[Column(Name = "车辆等级编码", DbType = "int", Storage = "_车辆等级编码", UpdateCheck = UpdateCheck.Never)]
		public int 车辆等级编码
		{
			get { return _车辆等级编码; }
			set { _车辆等级编码 = value; }
		}
		private bool _是否在线;
		/// <summary>
		/// 是否在线
		/// </summary>
		[Column(Name = "是否在线", DbType = "bit", Storage = "_是否在线", UpdateCheck = UpdateCheck.Never)]
		public bool 是否在线
		{
			get { return _是否在线; }
			set { _是否在线 = value; }
		}
		private bool _是否有效;
		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "是否有效", DbType = "bit", Storage = "_是否有效", UpdateCheck = UpdateCheck.Never)]
		public bool 是否有效
		{
			get { return _是否有效; }
			set { _是否有效 = value; }
		}
		private int _顺序号;
		/// <summary>
		/// 顺序号
		/// </summary>
		[Column(Name = "顺序号", DbType = "int", Storage = "_顺序号", UpdateCheck = UpdateCheck.Never)]
		public int 顺序号
		{
			get { return _顺序号; }
			set { _顺序号 = value; }
		}
	}
}