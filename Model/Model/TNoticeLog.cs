using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TNoticeLog")]
	public class TNoticeLog
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
		private int _通知单编码;
		/// <summary>
		/// 通知单编码
		/// </summary>
		[Column(Name = "通知单编码", DbType = "int", Storage = "_通知单编码", UpdateCheck = UpdateCheck.Never)]
		public int 通知单编码
		{
			get { return _通知单编码; }
			set { _通知单编码 = value; }
		}
		private string _目的地编码;
		/// <summary>
		/// 目的地编码
		/// </summary>
		[Column(Name = "目的地编码", DbType = "varchar(20)", Storage = "_目的地编码", UpdateCheck = UpdateCheck.Never)]
		public string 目的地编码
		{
			get { return _目的地编码; }
			set { _目的地编码 = value; }
		}
		private string _目的地名称;
		/// <summary>
		/// 目的地名称
		/// </summary>
		[Column(Name = "目的地名称", DbType = "varchar(50)", Storage = "_目的地名称", UpdateCheck = UpdateCheck.Never)]
		public string 目的地名称
		{
			get { return _目的地名称; }
			set { _目的地名称 = value; }
		}
	}
}