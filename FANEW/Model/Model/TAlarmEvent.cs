using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAlarmEvent")]
	public class TAlarmEvent
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
		private string _首次呼救电话;
		/// <summary>
		/// 首次呼救电话
		/// </summary>
		[Column(Name = "首次呼救电话", DbType = "varchar(14)", Storage = "_首次呼救电话", UpdateCheck = UpdateCheck.Never)]
		public string 首次呼救电话
		{
			get { return _首次呼救电话; }
			set { _首次呼救电话 = value; }
		}
		private string _事件名称;
		/// <summary>
		/// 事件名称
		/// </summary>
		[Column(Name = "事件名称", DbType = "varchar(200)", Storage = "_事件名称", UpdateCheck = UpdateCheck.Never)]
		public string 事件名称
		{
			get { return _事件名称; }
			set { _事件名称 = value; }
		}
		private int _事件来源编码;
		/// <summary>
		/// 事件来源编码
		/// </summary>
		[Column(Name = "事件来源编码", DbType = "int", Storage = "_事件来源编码", UpdateCheck = UpdateCheck.Never)]
		public int 事件来源编码
		{
			get { return _事件来源编码; }
			set { _事件来源编码 = value; }
		}
		private int _事件类型编码;
		/// <summary>
		/// 事件类型编码
		/// </summary>
		[Column(Name = "事件类型编码", DbType = "int", Storage = "_事件类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 事件类型编码
		{
			get { return _事件类型编码; }
			set { _事件类型编码 = value; }
		}
		private int _事故类型编码;
		/// <summary>
		/// 事故类型编码
		/// </summary>
		[Column(Name = "事故类型编码", DbType = "int", Storage = "_事故类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 事故类型编码
		{
			get { return _事故类型编码; }
			set { _事故类型编码 = value; }
		}
		private int _事故等级编码;
		/// <summary>
		/// 事故等级编码
		/// </summary>
		[Column(Name = "事故等级编码", DbType = "int", Storage = "_事故等级编码", UpdateCheck = UpdateCheck.Never)]
		public int 事故等级编码
		{
			get { return _事故等级编码; }
			set { _事故等级编码 = value; }
		}
		private bool _是否测试;
		/// <summary>
		/// 是否测试
		/// </summary>
		[Column(Name = "是否测试", DbType = "bit", Storage = "_是否测试", UpdateCheck = UpdateCheck.Never)]
		public bool 是否测试
		{
			get { return _是否测试; }
			set { _是否测试 = value; }
		}
		private int _受理次数;
		/// <summary>
		/// 受理次数
		/// </summary>
		[Column(Name = "受理次数", DbType = "int", Storage = "_受理次数", UpdateCheck = UpdateCheck.Never)]
		public int 受理次数
		{
			get { return _受理次数; }
			set { _受理次数 = value; }
		}
		private int _撤消受理数;
		/// <summary>
		/// 撤消受理数
		/// </summary>
		[Column(Name = "撤消受理数", DbType = "int", Storage = "_撤消受理数", UpdateCheck = UpdateCheck.Never)]
		public int 撤消受理数
		{
			get { return _撤消受理数; }
			set { _撤消受理数 = value; }
		}
		private int _执行任务总数;
		/// <summary>
		/// 执行任务总数
		/// </summary>
		[Column(Name = "执行任务总数", DbType = "int", Storage = "_执行任务总数", UpdateCheck = UpdateCheck.Never)]
		public int 执行任务总数
		{
			get { return _执行任务总数; }
			set { _执行任务总数 = value; }
		}
		private int _当前执行任务数;
		/// <summary>
		/// 当前执行任务数
		/// </summary>
		[Column(Name = "当前执行任务数", DbType = "int", Storage = "_当前执行任务数", UpdateCheck = UpdateCheck.Never)]
		public int 当前执行任务数
		{
			get { return _当前执行任务数; }
			set { _当前执行任务数 = value; }
		}
		private int _中止任务数;
		/// <summary>
		/// 中止任务数
		/// </summary>
		[Column(Name = "中止任务数", DbType = "int", Storage = "_中止任务数", UpdateCheck = UpdateCheck.Never)]
		public int 中止任务数
		{
			get { return _中止任务数; }
			set { _中止任务数 = value; }
		}
		private string _首次调度员编码;
		/// <summary>
		/// 首次调度员编码
		/// </summary>
		[Column(Name = "首次调度员编码", DbType = "char(5)", Storage = "_首次调度员编码", UpdateCheck = UpdateCheck.Never)]
		public string 首次调度员编码
		{
			get { return _首次调度员编码; }
			set { _首次调度员编码 = value; }
		}
		private DateTime _首次受理时刻;
		/// <summary>
		/// 首次受理时刻
		/// </summary>
		[Column(Name = "首次受理时刻", DbType = "datetime", Storage = "_首次受理时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 首次受理时刻
		{
			get { return _首次受理时刻; }
			set { _首次受理时刻 = value; }
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
		private DateTime? _预约时刻;
		/// <summary>
		/// 预约时刻
		/// </summary>
		[Column(Name = "预约时刻", DbType = "datetime", Storage = "_预约时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 预约时刻
		{
			get { return _预约时刻; }
			set { _预约时刻 = value; }
		}
		private DateTime? _首次派车时刻;
		/// <summary>
		/// 首次派车时刻
		/// </summary>
		[Column(Name = "首次派车时刻", DbType = "datetime", Storage = "_首次派车时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 首次派车时刻
		{
			get { return _首次派车时刻; }
			set { _首次派车时刻 = value; }
		}
		private bool _是否挂起;
		/// <summary>
		/// 是否挂起
		/// </summary>
		[Column(Name = "是否挂起", DbType = "bit", Storage = "_是否挂起", UpdateCheck = UpdateCheck.Never)]
		public bool 是否挂起
		{
			get { return _是否挂起; }
			set { _是否挂起 = value; }
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
		private string _区域;
		/// <summary>
		/// 区域
		/// </summary>
		[Column(Name = "区域", DbType = "varchar(100)", Storage = "_区域", UpdateCheck = UpdateCheck.Never)]
		public string 区域
		{
			get { return _区域; }
			set { _区域 = value; }
		}
		private string _占用台号;
		/// <summary>
		/// 占用台号
		/// </summary>
		[Column(Name = "占用台号", DbType = "char(2)", Storage = "_占用台号", UpdateCheck = UpdateCheck.Never)]
		public string 占用台号
		{
			get { return _占用台号; }
			set { _占用台号 = value; }
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
		private bool _表单完成标志;
		/// <summary>
		/// 表单完成标志
		/// </summary>
		[Column(Name = "表单完成标志", DbType = "bit", Storage = "_表单完成标志", UpdateCheck = UpdateCheck.Never)]
		public bool 表单完成标志
		{
			get { return _表单完成标志; }
			set { _表单完成标志 = value; }
		}
		private string _所属事故编码;
		/// <summary>
		/// 所属事故编码
		/// </summary>
		[Column(Name = "所属事故编码", DbType = "char(16)", Storage = "_所属事故编码", UpdateCheck = UpdateCheck.Never)]
		public string 所属事故编码
		{
			get { return _所属事故编码; }
			set { _所属事故编码 = value; }
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
		private bool _是否落单;
		/// <summary>
		/// 是否落单
		/// </summary>
		[Column(Name = "是否落单", DbType = "bit", Storage = "_是否落单", UpdateCheck = UpdateCheck.Never)]
		public bool 是否落单
		{
			get { return _是否落单; }
			set { _是否落单 = value; }
		}
	}
}