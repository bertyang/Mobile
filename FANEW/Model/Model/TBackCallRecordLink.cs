using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TBackCallRecordLink")]
	public class TBackCallRecordLink
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
		private DateTime? _创建时间;
		/// <summary>
		/// 创建时间
		/// </summary>
		[Column(Name = "创建时间", DbType = "datetime", Storage = "_创建时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 创建时间
		{
			get { return _创建时间; }
			set { _创建时间 = value; }
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
		private string _录音号;
		/// <summary>
		/// 录音号
		/// </summary>
		[Column(Name = "录音号", DbType = "varchar(50)", Storage = "_录音号", UpdateCheck = UpdateCheck.Never)]
		public string 录音号
		{
			get { return _录音号; }
			set { _录音号 = value; }
		}
	}
}