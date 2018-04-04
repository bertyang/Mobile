using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TTelLog")]
	public class TTelLog
	{
		private Guid _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "uniqueidentifier", Storage = "_编码")]
		public Guid 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
		private int _记录类型编码;
		/// <summary>
		/// 记录类型编码
		/// </summary>
		[Column(Name = "记录类型编码", DbType = "int", Storage = "_记录类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 记录类型编码
		{
			get { return _记录类型编码; }
			set { _记录类型编码 = value; }
		}
		private DateTime _产生时刻;
		/// <summary>
		/// 产生时刻
		/// </summary>
		[Column(Name = "产生时刻", DbType = "datetime", Storage = "_产生时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 产生时刻
		{
			get { return _产生时刻; }
			set { _产生时刻 = value; }
		}
		private string _对方电话;
		/// <summary>
		/// 对方电话
		/// </summary>
		[Column(Name = "对方电话", DbType = "varchar(20)", Storage = "_对方电话", UpdateCheck = UpdateCheck.Never)]
		public string 对方电话
		{
			get { return _对方电话; }
			set { _对方电话 = value; }
		}
		private DateTime? _呼入时刻;
		/// <summary>
		/// 呼入时刻
		/// </summary>
		[Column(Name = "呼入时刻", DbType = "datetime", Storage = "_呼入时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 呼入时刻
		{
			get { return _呼入时刻; }
			set { _呼入时刻 = value; }
		}
		private DateTime? _排队时刻;
		/// <summary>
		/// 排队时刻
		/// </summary>
		[Column(Name = "排队时刻", DbType = "datetime", Storage = "_排队时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 排队时刻
		{
			get { return _排队时刻; }
			set { _排队时刻 = value; }
		}
		private DateTime? _震铃时刻;
		/// <summary>
		/// 震铃时刻
		/// </summary>
		[Column(Name = "震铃时刻", DbType = "datetime", Storage = "_震铃时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 震铃时刻
		{
			get { return _震铃时刻; }
			set { _震铃时刻 = value; }
		}
		private DateTime? _通话时刻;
		/// <summary>
		/// 通话时刻
		/// </summary>
		[Column(Name = "通话时刻", DbType = "datetime", Storage = "_通话时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 通话时刻
		{
			get { return _通话时刻; }
			set { _通话时刻 = value; }
		}
		private DateTime? _中间操作时刻;
		/// <summary>
		/// 中间操作时刻
		/// </summary>
		[Column(Name = "中间操作时刻", DbType = "datetime", Storage = "_中间操作时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 中间操作时刻
		{
			get { return _中间操作时刻; }
			set { _中间操作时刻 = value; }
		}
		private string _中间操作号码;
		/// <summary>
		/// 中间操作号码
		/// </summary>
		[Column(Name = "中间操作号码", DbType = "varchar(20)", Storage = "_中间操作号码", UpdateCheck = UpdateCheck.Never)]
		public string 中间操作号码
		{
			get { return _中间操作号码; }
			set { _中间操作号码 = value; }
		}
		private string _台号;
		/// <summary>
		/// 台号
		/// </summary>
		[Column(Name = "台号", DbType = "varchar(5)", Storage = "_台号", UpdateCheck = UpdateCheck.Never)]
		public string 台号
		{
			get { return _台号; }
			set { _台号 = value; }
		}
		private string _调度员工号;
		/// <summary>
		/// 调度员工号
		/// </summary>
		[Column(Name = "调度员工号", DbType = "varchar(10)", Storage = "_调度员工号", UpdateCheck = UpdateCheck.Never)]
		public string 调度员工号
		{
			get { return _调度员工号; }
			set { _调度员工号 = value; }
		}
		private string _录音号;
		/// <summary>
		/// 录音号
		/// </summary>
		[Column(Name = "录音号", DbType = "varchar(100)", Storage = "_录音号", UpdateCheck = UpdateCheck.Never)]
		public string 录音号
		{
			get { return _录音号; }
			set { _录音号 = value; }
		}
		private DateTime? _结束时刻;
		/// <summary>
		/// 结束时刻
		/// </summary>
		[Column(Name = "结束时刻", DbType = "datetime", Storage = "_结束时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 结束时刻
		{
			get { return _结束时刻; }
			set { _结束时刻 = value; }
		}
		private int? _操作说明编码;
		/// <summary>
		/// 操作说明编码
		/// </summary>
		[Column(Name = "操作说明编码", DbType = "int", Storage = "_操作说明编码", UpdateCheck = UpdateCheck.Never)]
		public int? 操作说明编码
		{
			get { return _操作说明编码; }
			set { _操作说明编码 = value; }
		}
		private int? _结果编码;
		/// <summary>
		/// 结果编码
		/// </summary>
		[Column(Name = "结果编码", DbType = "int", Storage = "_结果编码", UpdateCheck = UpdateCheck.Never)]
		public int? 结果编码
		{
			get { return _结果编码; }
			set { _结果编码 = value; }
		}
		private int? _中心编码;
		/// <summary>
		/// 中心编码
		/// </summary>
		[Column(Name = "中心编码", DbType = "int", Storage = "_中心编码", UpdateCheck = UpdateCheck.Never)]
		public int? 中心编码
		{
			get { return _中心编码; }
			set { _中心编码 = value; }
		}
	}
}