using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TChargeRecordDetail")]
	public class TChargeRecordDetail
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
		private int _收费项编码;
		/// <summary>
		/// 收费项编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "收费项编码", DbType = "int", Storage = "_收费项编码")]
		public int 收费项编码
		{
			get { return _收费项编码; }
			set { _收费项编码 = value; }
		}
		private string _收费项名称;
		/// <summary>
		/// 收费项名称
		/// </summary>
		[Column(Name = "收费项名称", DbType = "varchar(50)", Storage = "_收费项名称", UpdateCheck = UpdateCheck.Never)]
		public string 收费项名称
		{
			get { return _收费项名称; }
			set { _收费项名称 = value; }
		}
		private double _收费金额;
		/// <summary>
		/// 收费金额
		/// </summary>
		[Column(Name = "收费金额", DbType = "float", Storage = "_收费金额", UpdateCheck = UpdateCheck.Never)]
		public double 收费金额
		{
			get { return _收费金额; }
			set { _收费金额 = value; }
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
	}
}