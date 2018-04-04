using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAmbulancePersonSign")]
	public class TAmbulancePersonSign
	{
		private string _人员编码;
		/// <summary>
		/// 人员编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "人员编码", DbType = "char(5)", Storage = "_人员编码")]
		public string 人员编码
		{
			get { return _人员编码; }
			set { _人员编码 = value; }
		}
		private DateTime _操作时刻;
		/// <summary>
		/// 操作时刻
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "操作时刻", DbType = "datetime", Storage = "_操作时刻")]
		public DateTime 操作时刻
		{
			get { return _操作时刻; }
			set { _操作时刻 = value; }
		}
		private DateTime _记录时刻;
		/// <summary>
		/// 记录时刻
		/// </summary>
		[Column(Name = "记录时刻", DbType = "datetime", Storage = "_记录时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 记录时刻
		{
			get { return _记录时刻; }
			set { _记录时刻 = value; }
		}
		private int _人员类型编码;
		/// <summary>
		/// 人员类型编码
		/// </summary>
		[Column(Name = "人员类型编码", DbType = "int", Storage = "_人员类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 人员类型编码
		{
			get { return _人员类型编码; }
			set { _人员类型编码 = value; }
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
		private string _操作人编码;
		/// <summary>
		/// 操作人编码
		/// </summary>
		[Column(Name = "操作人编码", DbType = "char(5)", Storage = "_操作人编码", UpdateCheck = UpdateCheck.Never)]
		public string 操作人编码
		{
			get { return _操作人编码; }
			set { _操作人编码 = value; }
		}
		private bool _是否上班操作;
		/// <summary>
		/// 是否上班操作
		/// </summary>
		[Column(Name = "是否上班操作", DbType = "bit", Storage = "_是否上班操作", UpdateCheck = UpdateCheck.Never)]
		public bool 是否上班操作
		{
			get { return _是否上班操作; }
			set { _是否上班操作 = value; }
		}
		private int _操作来源编码;
		/// <summary>
		/// 操作来源编码
		/// </summary>
		[Column(Name = "操作来源编码", DbType = "int", Storage = "_操作来源编码", UpdateCheck = UpdateCheck.Never)]
		public int 操作来源编码
		{
			get { return _操作来源编码; }
			set { _操作来源编码 = value; }
		}
		private DateTime _上次操作时刻;
		/// <summary>
		/// 上次操作时刻
		/// </summary>
		[Column(Name = "上次操作时刻", DbType = "datetime", Storage = "_上次操作时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 上次操作时刻
		{
			get { return _上次操作时刻; }
			set { _上次操作时刻 = value; }
		}
	}
}