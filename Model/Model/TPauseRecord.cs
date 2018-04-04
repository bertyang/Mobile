using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPauseRecord")]
	public class TPauseRecord
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
		private string _车辆编码;
		/// <summary>
		/// 车辆编码
		/// </summary>
		[Column(Name = "车辆编码", DbType = "char(5)", Storage = "_车辆编码", UpdateCheck = UpdateCheck.Never)]
		public string 车辆编码
		{
			get { return _车辆编码; }
			set { _车辆编码 = value; }
		}
		private DateTime _暂停时刻;
		/// <summary>
		/// 暂停时刻
		/// </summary>
		[Column(Name = "暂停时刻", DbType = "datetime", Storage = "_暂停时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime 暂停时刻
		{
			get { return _暂停时刻; }
			set { _暂停时刻 = value; }
		}
		private DateTime? _恢复时刻;
		/// <summary>
		/// 恢复时刻
		/// </summary>
		[Column(Name = "恢复时刻", DbType = "datetime", Storage = "_恢复时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 恢复时刻
		{
			get { return _恢复时刻; }
			set { _恢复时刻 = value; }
		}
		private int _暂停原因编码;
		/// <summary>
		/// 暂停原因编码
		/// </summary>
		[Column(Name = "暂停原因编码", DbType = "int", Storage = "_暂停原因编码", UpdateCheck = UpdateCheck.Never)]
		public int 暂停原因编码
		{
			get { return _暂停原因编码; }
			set { _暂停原因编码 = value; }
		}
		private string _暂停操作人员编码;
		/// <summary>
		/// 暂停操作人员编码
		/// </summary>
		[Column(Name = "暂停操作人员编码", DbType = "char(5)", Storage = "_暂停操作人员编码", UpdateCheck = UpdateCheck.Never)]
		public string 暂停操作人员编码
		{
			get { return _暂停操作人员编码; }
			set { _暂停操作人员编码 = value; }
		}
		private string _恢复操作人员编码;
		/// <summary>
		/// 恢复操作人员编码
		/// </summary>
		[Column(Name = "恢复操作人员编码", DbType = "char(5)", Storage = "_恢复操作人员编码", UpdateCheck = UpdateCheck.Never)]
		public string 恢复操作人员编码
		{
			get { return _恢复操作人员编码; }
			set { _恢复操作人员编码 = value; }
		}
		private string _司机;
		/// <summary>
		/// 司机
		/// </summary>
		[Column(Name = "司机", DbType = "char(50)", Storage = "_司机", UpdateCheck = UpdateCheck.Never)]
		public string 司机
		{
			get { return _司机; }
			set { _司机 = value; }
		}
		private string _医生;
		/// <summary>
		/// 医生
		/// </summary>
		[Column(Name = "医生", DbType = "varchar(50)", Storage = "_医生", UpdateCheck = UpdateCheck.Never)]
		public string 医生
		{
			get { return _医生; }
			set { _医生 = value; }
		}
		private string _护士;
		/// <summary>
		/// 护士
		/// </summary>
		[Column(Name = "护士", DbType = "varchar(50)", Storage = "_护士", UpdateCheck = UpdateCheck.Never)]
		public string 护士
		{
			get { return _护士; }
			set { _护士 = value; }
		}
		private string _担架工;
		/// <summary>
		/// 担架工
		/// </summary>
		[Column(Name = "担架工", DbType = "varchar(50)", Storage = "_担架工", UpdateCheck = UpdateCheck.Never)]
		public string 担架工
		{
			get { return _担架工; }
			set { _担架工 = value; }
		}
		private string _抢救员;
		/// <summary>
		/// 抢救员
		/// </summary>
		[Column(Name = "抢救员", DbType = "varchar(50)", Storage = "_抢救员", UpdateCheck = UpdateCheck.Never)]
		public string 抢救员
		{
			get { return _抢救员; }
			set { _抢救员 = value; }
		}
		private string _备注;
		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "备注", DbType = "varchar(1000)", Storage = "_备注", UpdateCheck = UpdateCheck.Never)]
		public string 备注
		{
			get { return _备注; }
			set { _备注 = value; }
		}
	}
}