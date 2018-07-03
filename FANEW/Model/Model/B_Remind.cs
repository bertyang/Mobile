using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "B_Remind")]
	public class B_Remind
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
		private DateTime _提醒时间;
		/// <summary>
		/// 提醒时间
		/// </summary>
		[Column(Name = "提醒时间", DbType = "datetime", Storage = "_提醒时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime 提醒时间
		{
			get { return _提醒时间; }
			set { _提醒时间 = value; }
		}
		private string _发送对象;
		/// <summary>
		/// 发送对象
		/// </summary>
		[Column(Name = "发送对象", DbType = "text(16)", Storage = "_发送对象", UpdateCheck = UpdateCheck.Never)]
		public string 发送对象
		{
			get { return _发送对象; }
			set { _发送对象 = value; }
		}
		private string _是否发送;
		/// <summary>
		/// 是否发送
		/// </summary>
		[Column(Name = "是否发送", DbType = "char(1)", Storage = "_是否发送", UpdateCheck = UpdateCheck.Never)]
		public string 是否发送
		{
			get { return _是否发送; }
			set { _是否发送 = value; }
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
	}
}