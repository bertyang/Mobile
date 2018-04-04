using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TTaskPersonLink")]
	public class TTaskPersonLink
	{
		private string _任务编码;
		/// <summary>
		/// 任务编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "任务编码", DbType = "char(20)", Storage = "_任务编码")]
		public string 任务编码
		{
			get { return _任务编码; }
			set { _任务编码 = value; }
		}
		private string _人员编码;
		/// <summary>
		/// 人员编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "人员编码", DbType = "char(5)", Storage = "_人员编码")]
		public string 人员编码
		{
			get { return _人员编码; }
			set { _人员编码 = value; }
		}
		private string _姓名;
		/// <summary>
		/// 姓名
		/// </summary>
		[Column(Name = "姓名", DbType = "varchar(20)", Storage = "_姓名", UpdateCheck = UpdateCheck.Never)]
		public string 姓名
		{
			get { return _姓名; }
			set { _姓名 = value; }
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
		private string _分站编码;
		/// <summary>
		/// 分站编码
		/// </summary>
		[Column(Name = "分站编码", DbType = "char(3)", Storage = "_分站编码", UpdateCheck = UpdateCheck.Never)]
		public string 分站编码
		{
			get { return _分站编码; }
			set { _分站编码 = value; }
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