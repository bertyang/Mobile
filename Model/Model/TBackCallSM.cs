using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TBackCallSM")]
	public class TBackCallSM
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
		private string _手机号码;
		/// <summary>
		/// 手机号码
		/// </summary>
		[Column(Name = "手机号码", DbType = "varchar(14)", Storage = "_手机号码", UpdateCheck = UpdateCheck.Never)]
		public string 手机号码
		{
			get { return _手机号码; }
			set { _手机号码 = value; }
		}
		private string _发送内容;
		/// <summary>
		/// 发送内容
		/// </summary>
		[Column(Name = "发送内容", DbType = "varchar(1000)", Storage = "_发送内容", UpdateCheck = UpdateCheck.Never)]
		public string 发送内容
		{
			get { return _发送内容; }
			set { _发送内容 = value; }
		}
		private DateTime? _发送时间;
		/// <summary>
		/// 发送时间
		/// </summary>
		[Column(Name = "发送时间", DbType = "datetime", Storage = "_发送时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 发送时间
		{
			get { return _发送时间; }
			set { _发送时间 = value; }
		}
		private string _接收内容;
		/// <summary>
		/// 接收内容
		/// </summary>
		[Column(Name = "接收内容", DbType = "varchar(1000)", Storage = "_接收内容", UpdateCheck = UpdateCheck.Never)]
		public string 接收内容
		{
			get { return _接收内容; }
			set { _接收内容 = value; }
		}
		private DateTime? _接收时间;
		/// <summary>
		/// 接收时间
		/// </summary>
		[Column(Name = "接收时间", DbType = "datetime", Storage = "_接收时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 接收时间
		{
			get { return _接收时间; }
			set { _接收时间 = value; }
		}
	}
}