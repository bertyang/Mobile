using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TModifyRecord")]
	public class TModifyRecord
	{
		private Guid _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "uniqueidentifier", Storage = "_编码")]
		public Guid 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
		private int _修改类型编码;
		/// <summary>
		/// 修改类型编码
		/// </summary>
		[Column(Name = "修改类型编码", DbType = "int", Storage = "_修改类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 修改类型编码
		{
			get { return _修改类型编码; }
			set { _修改类型编码 = value; }
		}
		private string _事件编码;
		/// <summary>
		/// 事件编码
		/// </summary>
		[Column(Name = "事件编码", DbType = "char(16)", Storage = "_事件编码", UpdateCheck = UpdateCheck.Never)]
		public string 事件编码
		{
			get { return _事件编码; }
			set { _事件编码 = value; }
		}
		private int? _受理序号;
		/// <summary>
		/// 受理序号
		/// </summary>
		[Column(Name = "受理序号", DbType = "int", Storage = "_受理序号", UpdateCheck = UpdateCheck.Never)]
		public int? 受理序号
		{
			get { return _受理序号; }
			set { _受理序号 = value; }
		}
		private string _任务编码;
		/// <summary>
		/// 任务编码
		/// </summary>
		[Column(Name = "任务编码", DbType = "char(20)", Storage = "_任务编码", UpdateCheck = UpdateCheck.Never)]
		public string 任务编码
		{
			get { return _任务编码; }
			set { _任务编码 = value; }
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
		private DateTime _产生时刻;
		/// <summary>
		/// 产生时刻
		/// </summary>
		[Column(Name = "产生时刻", DbType = "datetime", Storage = "_产生时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 产生时刻
		{
			get { return _产生时刻; }
			set { _产生时刻 = value; }
		}
		private string _修改前内容;
		/// <summary>
		/// 修改前内容
		/// </summary>
		[Column(Name = "修改前内容", DbType = "varchar(1000)", Storage = "_修改前内容", UpdateCheck = UpdateCheck.Never)]
		public string 修改前内容
		{
			get { return _修改前内容; }
			set { _修改前内容 = value; }
		}
		private string _修改后内容;
		/// <summary>
		/// 修改后内容
		/// </summary>
		[Column(Name = "修改后内容", DbType = "varchar(1000)", Storage = "_修改后内容", UpdateCheck = UpdateCheck.Never)]
		public string 修改后内容
		{
			get { return _修改后内容; }
			set { _修改后内容 = value; }
		}
	}
}