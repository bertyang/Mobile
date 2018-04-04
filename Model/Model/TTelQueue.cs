using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TTelQueue")]
	public class TTelQueue
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
		private DateTime _排队时刻;
		/// <summary>
		/// 排队时刻
		/// </summary>
		[Column(Name = "排队时刻", DbType = "datetime", Storage = "_排队时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 排队时刻
		{
			get { return _排队时刻; }
			set { _排队时刻 = value; }
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
		private string _ACD号码;
		/// <summary>
		/// ACD号码
		/// </summary>
		[Column(Name = "ACD号码", DbType = "varchar(6)", Storage = "_ACD号码", UpdateCheck = UpdateCheck.Never)]
		public string ACD号码
		{
			get { return _ACD号码; }
			set { _ACD号码 = value; }
		}
		private int? _排队数量;
		/// <summary>
		/// 排队数量
		/// </summary>
		[Column(Name = "排队数量", DbType = "int", Storage = "_排队数量", UpdateCheck = UpdateCheck.Never)]
		public int? 排队数量
		{
			get { return _排队数量; }
			set { _排队数量 = value; }
		}
		private string _排队电话;
		/// <summary>
		/// 排队电话
		/// </summary>
		[Column(Name = "排队电话", DbType = "varchar(20)", Storage = "_排队电话", UpdateCheck = UpdateCheck.Never)]
		public string 排队电话
		{
			get { return _排队电话; }
			set { _排队电话 = value; }
		}
		private int? _排队结果编码;
		/// <summary>
		/// 排队结果编码
		/// </summary>
		[Column(Name = "排队结果编码", DbType = "int", Storage = "_排队结果编码", UpdateCheck = UpdateCheck.Never)]
		public int? 排队结果编码
		{
			get { return _排队结果编码; }
			set { _排队结果编码 = value; }
		}
	}
}