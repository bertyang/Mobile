using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TTelBook")]
	public class TTelBook
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
		private string _名称;
		/// <summary>
		/// 名称
		/// </summary>
		[Column(Name = "名称", DbType = "varchar(50)", Storage = "_名称", UpdateCheck = UpdateCheck.Never)]
		public string 名称
		{
			get { return _名称; }
			set { _名称 = value; }
		}
		private int _电话分类编码;
		/// <summary>
		/// 电话分类编码
		/// </summary>
		[Column(Name = "电话分类编码", DbType = "int", Storage = "_电话分类编码", UpdateCheck = UpdateCheck.Never)]
		public int 电话分类编码
		{
			get { return _电话分类编码; }
			set { _电话分类编码 = value; }
		}
		private string _联系电话一;
		/// <summary>
		/// 联系电话一
		/// </summary>
		[Column(Name = "联系电话一", DbType = "varchar(30)", Storage = "_联系电话一", UpdateCheck = UpdateCheck.Never)]
		public string 联系电话一
		{
			get { return _联系电话一; }
			set { _联系电话一 = value; }
		}
		private string _分机一;
		/// <summary>
		/// 分机一
		/// </summary>
		[Column(Name = "分机一", DbType = "varchar(15)", Storage = "_分机一", UpdateCheck = UpdateCheck.Never)]
		public string 分机一
		{
			get { return _分机一; }
			set { _分机一 = value; }
		}
		private string _联系电话二;
		/// <summary>
		/// 联系电话二
		/// </summary>
		[Column(Name = "联系电话二", DbType = "varchar(30)", Storage = "_联系电话二", UpdateCheck = UpdateCheck.Never)]
		public string 联系电话二
		{
			get { return _联系电话二; }
			set { _联系电话二 = value; }
		}
		private string _分机二;
		/// <summary>
		/// 分机二
		/// </summary>
		[Column(Name = "分机二", DbType = "varchar(15)", Storage = "_分机二", UpdateCheck = UpdateCheck.Never)]
		public string 分机二
		{
			get { return _分机二; }
			set { _分机二 = value; }
		}
		private string _备注;
		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "备注", DbType = "varchar(5000)", Storage = "_备注", UpdateCheck = UpdateCheck.Never)]
		public string 备注
		{
			get { return _备注; }
			set { _备注 = value; }
		}
		private int _顺序号;
		/// <summary>
		/// 顺序号
		/// </summary>
		[Column(Name = "顺序号", DbType = "int", Storage = "_顺序号", UpdateCheck = UpdateCheck.Never)]
		public int 顺序号
		{
			get { return _顺序号; }
			set { _顺序号 = value; }
		}
		private bool _是否有效;
		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "是否有效", DbType = "bit", Storage = "_是否有效", UpdateCheck = UpdateCheck.Never)]
		public bool 是否有效
		{
			get { return _是否有效; }
			set { _是否有效 = value; }
		}
	}
}