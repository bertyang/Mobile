using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TBlackList")]
	public class TBlackList
	{
		private string _电话号码;
		/// <summary>
		/// 电话号码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "电话号码", DbType = "varchar(15)", Storage = "_电话号码")]
		public string 电话号码
		{
			get { return _电话号码; }
			set { _电话号码 = value; }
		}
		private DateTime? _有效时间;
		/// <summary>
		/// 有效时间
		/// </summary>
		[Column(Name = "有效时间", DbType = "datetime", Storage = "_有效时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 有效时间
		{
			get { return _有效时间; }
			set { _有效时间 = value; }
		}
	}
}