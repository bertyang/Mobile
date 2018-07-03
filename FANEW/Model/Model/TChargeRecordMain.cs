using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TChargeRecordMain")]
	public class TChargeRecordMain
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
		private string _发票号;
		/// <summary>
		/// 发票号
		/// </summary>
		[Column(Name = "发票号", DbType = "varchar(50)", Storage = "_发票号", UpdateCheck = UpdateCheck.Never)]
		public string 发票号
		{
			get { return _发票号; }
			set { _发票号 = value; }
		}
		private DateTime _收费时间;
		/// <summary>
		/// 收费时间
		/// </summary>
		[Column(Name = "收费时间", DbType = "datetime", Storage = "_收费时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime 收费时间
		{
			get { return _收费时间; }
			set { _收费时间 = value; }
		}
		private bool _是否欠费;
		/// <summary>
		/// 是否欠费
		/// </summary>
		[Column(Name = "是否欠费", DbType = "bit", Storage = "_是否欠费", UpdateCheck = UpdateCheck.Never)]
		public bool 是否欠费
		{
			get { return _是否欠费; }
			set { _是否欠费 = value; }
		}
		private double _应收总金额;
		/// <summary>
		/// 应收总金额
		/// </summary>
		[Column(Name = "应收总金额", DbType = "float", Storage = "_应收总金额", UpdateCheck = UpdateCheck.Never)]
		public double 应收总金额
		{
			get { return _应收总金额; }
			set { _应收总金额 = value; }
		}
		private double _实收总金额;
		/// <summary>
		/// 实收总金额
		/// </summary>
		[Column(Name = "实收总金额", DbType = "float", Storage = "_实收总金额", UpdateCheck = UpdateCheck.Never)]
		public double 实收总金额
		{
			get { return _实收总金额; }
			set { _实收总金额 = value; }
		}
		private double _公里数;
		/// <summary>
		/// 公里数
		/// </summary>
		[Column(Name = "公里数", DbType = "float", Storage = "_公里数", UpdateCheck = UpdateCheck.Never)]
		public double 公里数
		{
			get { return _公里数; }
			set { _公里数 = value; }
		}
		private int _操作来源编码;
		/// <summary>
		/// 操作来源编码
		/// </summary>
		[Column(Name = "操作来源编码", DbType = "int", Storage = "_操作来源编码", UpdateCheck = UpdateCheck.Never)]
		public int 操作来源编码
		{
			get { return _操作来源编码; }
			set { _操作来源编码 = value; }
		}
		private string _欠费原因;
		/// <summary>
		/// 欠费原因
		/// </summary>
		[Column(Name = "欠费原因", DbType = "varchar(20)", Storage = "_欠费原因", UpdateCheck = UpdateCheck.Never)]
		public string 欠费原因
		{
			get { return _欠费原因; }
			set { _欠费原因 = value; }
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
		private double? _出车公里数;
		/// <summary>
		/// 出车公里数
		/// </summary>
		[Column(Name = "出车公里数", DbType = "float", Storage = "_出车公里数", UpdateCheck = UpdateCheck.Never)]
		public double? 出车公里数
		{
			get { return _出车公里数; }
			set { _出车公里数 = value; }
		}
		private double? _收车公里数;
		/// <summary>
		/// 收车公里数
		/// </summary>
		[Column(Name = "收车公里数", DbType = "float", Storage = "_收车公里数", UpdateCheck = UpdateCheck.Never)]
		public double? 收车公里数
		{
			get { return _收车公里数; }
			set { _收车公里数 = value; }
		}
		private string _病历序号;
		/// <summary>
		/// 病历序号
		/// </summary>
		[Column(Name = "病历序号", DbType = "varchar(50)", Storage = "_病历序号", UpdateCheck = UpdateCheck.Never)]
		public string 病历序号
		{
			get { return _病历序号; }
			set { _病历序号 = value; }
		}
	}
}