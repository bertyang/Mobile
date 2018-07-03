using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPatientRecordMeasure")]
	public class TPatientRecordMeasure
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
		private int _序号;
		/// <summary>
		/// 序号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "序号", DbType = "int", Storage = "_序号")]
		public int 序号
		{
			get { return _序号; }
			set { _序号 = value; }
		}
		private int _救治措施编码;
		/// <summary>
		/// 救治措施编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "救治措施编码", DbType = "int", Storage = "_救治措施编码")]
		public int 救治措施编码
		{
			get { return _救治措施编码; }
			set { _救治措施编码 = value; }
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
	}
}