using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TDictionaryType")]
	public class TDictionaryType
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
		private string _类型编码;
		/// <summary>
		/// 类型编码
		/// </summary>
		[Column(Name = "类型编码", DbType = "varchar(20)", Storage = "_类型编码", UpdateCheck = UpdateCheck.Never)]
		public string 类型编码
		{
			get { return _类型编码; }
			set { _类型编码 = value; }
		}
		private string _描述;
		/// <summary>
		/// 描述
		/// </summary>
		[Column(Name = "描述", DbType = "varchar(50)", Storage = "_描述", UpdateCheck = UpdateCheck.Never)]
		public string 描述
		{
			get { return _描述; }
			set { _描述 = value; }
		}
	}
}