using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPerson")]
	public class TPerson
	{
		private string _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "char(5)", Storage = "_编码")]
		public string 编码
		{
			get { return _编码; }
			set { _编码 = value; }
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
		private string _单位编码;
		/// <summary>
		/// 单位编码
		/// </summary>
		[Column(Name = "单位编码", DbType = "varchar(50)", Storage = "_单位编码", UpdateCheck = UpdateCheck.Never)]
		public string 单位编码
		{
			get { return _单位编码; }
			set { _单位编码 = value; }
		}
		private string _通话号码;
		/// <summary>
		/// 通话号码
		/// </summary>
		[Column(Name = "通话号码", DbType = "varchar(50)", Storage = "_通话号码", UpdateCheck = UpdateCheck.Never)]
		public string 通话号码
		{
			get { return _通话号码; }
			set { _通话号码 = value; }
		}
		private string _短信号码;
		/// <summary>
		/// 短信号码
		/// </summary>
		[Column(Name = "短信号码", DbType = "varchar(50)", Storage = "_短信号码", UpdateCheck = UpdateCheck.Never)]
		public string 短信号码
		{
			get { return _短信号码; }
			set { _短信号码 = value; }
		}
		private int _类型编码;
		/// <summary>
		/// 类型编码
		/// </summary>
		[Column(Name = "类型编码", DbType = "int", Storage = "_类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 类型编码
		{
			get { return _类型编码; }
			set { _类型编码 = value; }
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
		private string _工号;
		/// <summary>
		/// 工号
		/// </summary>
		[Column(Name = "工号", DbType = "varchar(10)", Storage = "_工号", UpdateCheck = UpdateCheck.Never)]
		public string 工号
		{
			get { return _工号; }
			set { _工号 = value; }
		}
		private string _口令;
		/// <summary>
		/// 口令
		/// </summary>
		[Column(Name = "口令", DbType = "varchar(50)", Storage = "_口令", UpdateCheck = UpdateCheck.Never)]
		public string 口令
		{
			get { return _口令; }
			set { _口令 = value; }
		}
		private string _绑定车辆编码;
		/// <summary>
		/// 绑定车辆编码
		/// </summary>
		[Column(Name = "绑定车辆编码", DbType = "char(5)", Storage = "_绑定车辆编码", UpdateCheck = UpdateCheck.Never)]
		public string 绑定车辆编码
		{
			get { return _绑定车辆编码; }
			set { _绑定车辆编码 = value; }
		}
		private DateTime _上次操作时间;
		/// <summary>
		/// 上次操作时间
		/// </summary>
		[Column(Name = "上次操作时间", DbType = "datetime", Storage = "_上次操作时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime 上次操作时间
		{
			get { return _上次操作时间; }
			set { _上次操作时间 = value; }
		}
		private string _登录台号;
		/// <summary>
		/// 登录台号
		/// </summary>
		[Column(Name = "登录台号", DbType = "char(4)", Storage = "_登录台号", UpdateCheck = UpdateCheck.Never)]
		public string 登录台号
		{
			get { return _登录台号; }
			set { _登录台号 = value; }
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