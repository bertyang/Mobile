using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TOfficeAttachment")]
	public class TOfficeAttachment
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
		private string _原附件名;
		/// <summary>
		/// 原附件名
		/// </summary>
		[Column(Name = "原附件名", DbType = "nvarchar(100)", Storage = "_原附件名", UpdateCheck = UpdateCheck.Never)]
		public string 原附件名
		{
			get { return _原附件名; }
			set { _原附件名 = value; }
		}
		private string _编码附件名;
		/// <summary>
		/// 编码附件名
		/// </summary>
		[Column(Name = "编码附件名", DbType = "nvarchar(200)", Storage = "_编码附件名", UpdateCheck = UpdateCheck.Never)]
		public string 编码附件名
		{
			get { return _编码附件名; }
			set { _编码附件名 = value; }
		}
		private double? _文件大小;
		/// <summary>
		/// 文件大小
		/// </summary>
		[Column(Name = "文件大小", DbType = "float", Storage = "_文件大小", UpdateCheck = UpdateCheck.Never)]
		public double? 文件大小
		{
			get { return _文件大小; }
			set { _文件大小 = value; }
		}
		private string _附件路径;
		/// <summary>
		/// 附件路径
		/// </summary>
		[Column(Name = "附件路径", DbType = "nvarchar(400)", Storage = "_附件路径", UpdateCheck = UpdateCheck.Never)]
		public string 附件路径
		{
			get { return _附件路径; }
			set { _附件路径 = value; }
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
	}
}