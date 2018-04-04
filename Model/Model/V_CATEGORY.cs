using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "V_CATEGORY")]
	public class V_CATEGORY
	{
		private string _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(Name = "编码", DbType = "nvarchar(100)", Storage = "_编码", CanBeNull=false)]
		public string 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
		private string _名称;
		/// <summary>
		/// 名称
		/// </summary>
		[Column(Name = "名称", DbType = "nvarchar(100)", Storage = "_名称", CanBeNull=false)]
		public string 名称
		{
			get { return _名称; }
			set { _名称 = value; }
		}
		private int _顺序号;
		/// <summary>
		/// 顺序号
		/// </summary>
		[Column(Name = "顺序号", DbType = "int", Storage = "_顺序号", CanBeNull=false)]
		public int 顺序号
		{
			get { return _顺序号; }
			set { _顺序号 = value; }
		}
		private int _是否有效;
		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "是否有效", DbType = "int", Storage = "_是否有效", CanBeNull=false)]
		public int 是否有效
		{
			get { return _是否有效; }
			set { _是否有效 = value; }
		}
	}
}