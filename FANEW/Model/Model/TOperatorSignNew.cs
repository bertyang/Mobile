using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TOperatorSignNew")]
	public class TOperatorSignNew
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
		private string _人员编码;
		/// <summary>
		/// 人员编码
		/// </summary>
		[Column(Name = "人员编码", DbType = "char(5)", Storage = "_人员编码", UpdateCheck = UpdateCheck.Never)]
		public string 人员编码
		{
			get { return _人员编码; }
			set { _人员编码 = value; }
		}
		private string _人员姓名;
		/// <summary>
		/// 人员姓名
		/// </summary>
		[Column(Name = "人员姓名", DbType = "varchar(20)", Storage = "_人员姓名", UpdateCheck = UpdateCheck.Never)]
		public string 人员姓名
		{
			get { return _人员姓名; }
			set { _人员姓名 = value; }
		}
		private int _人员类型编码;
		/// <summary>
		/// 人员类型编码
		/// </summary>
		[Column(Name = "人员类型编码", DbType = "int", Storage = "_人员类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 人员类型编码
		{
			get { return _人员类型编码; }
			set { _人员类型编码 = value; }
		}
		private DateTime _开始时刻;
		/// <summary>
		/// 开始时刻
		/// </summary>
		[Column(Name = "开始时刻", DbType = "datetime", Storage = "_开始时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 开始时刻
		{
			get { return _开始时刻; }
			set { _开始时刻 = value; }
		}
		private string _开始类型;
		/// <summary>
		/// 开始类型
		/// </summary>
		[Column(Name = "开始类型", DbType = "varchar(20)", Storage = "_开始类型", UpdateCheck = UpdateCheck.Never)]
		public string 开始类型
		{
			get { return _开始类型; }
			set { _开始类型 = value; }
		}
		private DateTime? _结束时刻;
		/// <summary>
		/// 结束时刻
		/// </summary>
		[Column(Name = "结束时刻", DbType = "datetime", Storage = "_结束时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 结束时刻
		{
			get { return _结束时刻; }
			set { _结束时刻 = value; }
		}
		private string _结束类型;
		/// <summary>
		/// 结束类型
		/// </summary>
		[Column(Name = "结束类型", DbType = "varchar(20)", Storage = "_结束类型", UpdateCheck = UpdateCheck.Never)]
		public string 结束类型
		{
			get { return _结束类型; }
			set { _结束类型 = value; }
		}
		private string _台号;
		/// <summary>
		/// 台号
		/// </summary>
		[Column(Name = "台号", DbType = "char(2)", Storage = "_台号", UpdateCheck = UpdateCheck.Never)]
		public string 台号
		{
			get { return _台号; }
			set { _台号 = value; }
		}
	}
}