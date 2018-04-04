using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAmbulanceStateTime")]
	public class TAmbulanceStateTime
	{
		private int _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "int", Storage = "_编码")]
		public int 编码
		{
			get { return _编码; }
			set { _编码 = value; }
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
		private int _车辆状态编码;
		/// <summary>
		/// 车辆状态编码
		/// </summary>
		[Column(Name = "车辆状态编码", DbType = "int", Storage = "_车辆状态编码", UpdateCheck = UpdateCheck.Never)]
		public int 车辆状态编码
		{
			get { return _车辆状态编码; }
			set { _车辆状态编码 = value; }
		}
		private DateTime _时刻值;
		/// <summary>
		/// 时刻值
		/// </summary>
		[Column(Name = "时刻值", DbType = "datetime", Storage = "_时刻值", UpdateCheck = UpdateCheck.Never)]
		public DateTime 时刻值
		{
			get { return _时刻值; }
			set { _时刻值 = value; }
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
		private string _操作员编码;
		/// <summary>
		/// 操作员编码
		/// </summary>
		[Column(Name = "操作员编码", DbType = "char(5)", Storage = "_操作员编码", UpdateCheck = UpdateCheck.Never)]
		public string 操作员编码
		{
			get { return _操作员编码; }
			set { _操作员编码 = value; }
		}
		private bool _车辆是否在线;
		/// <summary>
		/// 车辆是否在线
		/// </summary>
		[Column(Name = "车辆是否在线", DbType = "bit", Storage = "_车辆是否在线", UpdateCheck = UpdateCheck.Never)]
		public bool 车辆是否在线
		{
			get { return _车辆是否在线; }
			set { _车辆是否在线 = value; }
		}
	}
}