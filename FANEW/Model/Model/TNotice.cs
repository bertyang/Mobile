using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TNotice")]
	public class TNotice
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
		private string _内容;
		/// <summary>
		/// 内容
		/// </summary>
		[Column(Name = "内容", DbType = "varchar(5000)", Storage = "_内容", UpdateCheck = UpdateCheck.Never)]
		public string 内容
		{
			get { return _内容; }
			set { _内容 = value; }
		}
		private DateTime _发送时刻;
		/// <summary>
		/// 发送时刻
		/// </summary>
		[Column(Name = "发送时刻", DbType = "datetime", Storage = "_发送时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 发送时刻
		{
			get { return _发送时刻; }
			set { _发送时刻 = value; }
		}
		private string _操作员编码;
		/// <summary>
		/// 操作员编码
		/// </summary>
		[Column(Name = "操作员编码", DbType = "char(5)", Storage = "_操作员编码", UpdateCheck = UpdateCheck.Never)]
		public string 操作员编码
		{
			get { return _操作员编码; }
			set { _操作员编码 = value; }
		}
		private int? _类型编码;
		/// <summary>
		/// 类型编码
		/// </summary>
		[Column(Name = "类型编码", DbType = "int", Storage = "_类型编码", UpdateCheck = UpdateCheck.Never)]
		public int? 类型编码
		{
			get { return _类型编码; }
			set { _类型编码 = value; }
		}
	}
}