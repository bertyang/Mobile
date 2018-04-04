using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TOfficeSend")]
	public class TOfficeSend
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
		private int? _办公编码;
		/// <summary>
		/// 办公编码
		/// </summary>
		[Column(Name = "办公编码", DbType = "int", Storage = "_办公编码", UpdateCheck = UpdateCheck.Never)]
		public int? 办公编码
		{
			get { return _办公编码; }
			set { _办公编码 = value; }
		}
		private int? _发送类型编码;
		/// <summary>
		/// 发送类型编码
		/// </summary>
		[Column(Name = "发送类型编码", DbType = "int", Storage = "_发送类型编码", UpdateCheck = UpdateCheck.Never)]
		public int? 发送类型编码
		{
			get { return _发送类型编码; }
			set { _发送类型编码 = value; }
		}
		private string _目的地编码;
		/// <summary>
		/// 目的地编码
		/// </summary>
		[Column(Name = "目的地编码", DbType = "nvarchar(100)", Storage = "_目的地编码", UpdateCheck = UpdateCheck.Never)]
		public string 目的地编码
		{
			get { return _目的地编码; }
			set { _目的地编码 = value; }
		}
	}
}