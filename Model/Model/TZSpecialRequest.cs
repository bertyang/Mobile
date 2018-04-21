using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TZSpecialRequest")]
	public class TZSpecialRequest
	{
		private int _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(Name = "编码", DbType = "int", Storage = "_编码", CanBeNull=false)]
		public int 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
		private string _名称;
		/// <summary>
		/// 名称
		/// </summary>
		[Column(Name = "名称", DbType = "varchar(50)", Storage = "_名称", CanBeNull=false)]
		public string 名称
		{
			get { return _名称; }
			set { _名称 = value; }
		}
		private int _上级编码;
		/// <summary>
		/// 上级编码
		/// </summary>
		[Column(Name = "上级编码", DbType = "int", Storage = "_上级编码", CanBeNull=false)]
		public int 上级编码
		{
			get { return _上级编码; }
			set { _上级编码 = value; }
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
		private bool _是否有效;
		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "是否有效", DbType = "bit", Storage = "_是否有效", CanBeNull=false)]
		public bool 是否有效
		{
			get { return _是否有效; }
			set { _是否有效 = value; }
		}
	}
}