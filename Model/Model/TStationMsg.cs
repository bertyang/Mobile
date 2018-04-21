using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TStationMsg")]
	public class TStationMsg
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
		private string _事件编码;
		/// <summary>
		/// 事件编码
		/// </summary>
		[Column(Name = "事件编码", DbType = "char(16) NULL", Storage = "_事件编码", UpdateCheck = UpdateCheck.Never)]
		public string 事件编码
		{
			get { return _事件编码; }
			set { _事件编码 = value; }
		}
		private DateTime _时间;
		/// <summary>
		/// 时间
		/// </summary>
		[Column(Name = "时间", DbType = "datetime NOT NULL", Storage = "_时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime 时间
		{
			get { return _时间; }
			set { _时间 = value; }
		}
		private string _分站编码;
		/// <summary>
		/// 分站编码
		/// </summary>
		[Column(Name = "分站编码", DbType = "char(3) NOT NULL", Storage = "_分站编码", UpdateCheck = UpdateCheck.Never)]
		public string 分站编码
		{
			get { return _分站编码; }
			set { _分站编码 = value; }
		}
		private string _人员编码;
		/// <summary>
		/// 人员编码
		/// </summary>
		[Column(Name = "人员编码", DbType = "char(5) NULL", Storage = "_人员编码", UpdateCheck = UpdateCheck.Never)]
		public string 人员编码
		{
			get { return _人员编码; }
			set { _人员编码 = value; }
		}
		private int _类型;
		/// <summary>
		/// 类型
		/// </summary>
		[Column(Name = "类型", DbType = "int NOT NULL", Storage = "_类型", UpdateCheck = UpdateCheck.Never)]
		public int 类型
		{
			get { return _类型; }
			set { _类型 = value; }
		}
	}
}