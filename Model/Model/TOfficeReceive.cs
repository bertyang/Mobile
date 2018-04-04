using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TOfficeReceive")]
	public class TOfficeReceive
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
		private int _办公编码;
		/// <summary>
		/// 办公编码
		/// </summary>
		[Column(Name = "办公编码", DbType = "int", Storage = "_办公编码", UpdateCheck = UpdateCheck.Never)]
		public int 办公编码
		{
			get { return _办公编码; }
			set { _办公编码 = value; }
		}
		private string _接收人编码;
		/// <summary>
		/// 接收人编码
		/// </summary>
		[Column(Name = "接收人编码", DbType = "char(5)", Storage = "_接收人编码", UpdateCheck = UpdateCheck.Never)]
		public string 接收人编码
		{
			get { return _接收人编码; }
			set { _接收人编码 = value; }
		}
		private bool _是否已阅;
		/// <summary>
		/// 是否已阅
		/// </summary>
		[Column(Name = "是否已阅", DbType = "bit", Storage = "_是否已阅", UpdateCheck = UpdateCheck.Never)]
		public bool 是否已阅
		{
			get { return _是否已阅; }
			set { _是否已阅 = value; }
		}
		private DateTime? _查阅时间;
		/// <summary>
		/// 查阅时间
		/// </summary>
		[Column(Name = "查阅时间", DbType = "datetime", Storage = "_查阅时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 查阅时间
		{
			get { return _查阅时间; }
			set { _查阅时间 = value; }
		}
	}
}