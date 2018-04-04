using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAlarmCall")]
	public class TAlarmCall
	{
		private DateTime _通话时刻;
		/// <summary>
		/// 通话时刻
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "通话时刻", DbType = "datetime", Storage = "_通话时刻")]
		public DateTime 通话时刻
		{
			get { return _通话时刻; }
			set { _通话时刻 = value; }
		}
		private DateTime? _振铃时刻;
		/// <summary>
		/// 振铃时刻
		/// </summary>
		[Column(Name = "振铃时刻", DbType = "datetime", Storage = "_振铃时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 振铃时刻
		{
			get { return _振铃时刻; }
			set { _振铃时刻 = value; }
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
		private string _台号;
		/// <summary>
		/// 台号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "台号", DbType = "char(2)", Storage = "_台号")]
		public string 台号
		{
			get { return _台号; }
			set { _台号 = value; }
		}
		private string _调度员编码;
		/// <summary>
		/// 调度员编码
		/// </summary>
		[Column(Name = "调度员编码", DbType = "char(5)", Storage = "_调度员编码", UpdateCheck = UpdateCheck.Never)]
		public string 调度员编码
		{
			get { return _调度员编码; }
			set { _调度员编码 = value; }
		}
		private int _通话类型编码;
		/// <summary>
		/// 通话类型编码
		/// </summary>
		[Column(Name = "通话类型编码", DbType = "int", Storage = "_通话类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 通话类型编码
		{
			get { return _通话类型编码; }
			set { _通话类型编码 = value; }
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
		private string _主叫号码;
		/// <summary>
		/// 主叫号码
		/// </summary>
		[Column(Name = "主叫号码", DbType = "varchar(14)", Storage = "_主叫号码", UpdateCheck = UpdateCheck.Never)]
		public string 主叫号码
		{
			get { return _主叫号码; }
			set { _主叫号码 = value; }
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
		private bool _是否呼出;
		/// <summary>
		/// 是否呼出
		/// </summary>
		[Column(Name = "是否呼出", DbType = "bit", Storage = "_是否呼出", UpdateCheck = UpdateCheck.Never)]
		public bool 是否呼出
		{
			get { return _是否呼出; }
			set { _是否呼出 = value; }
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