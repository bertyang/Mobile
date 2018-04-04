using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TBackCall")]
	public class TBackCall
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
		private int? _调度;
		/// <summary>
		/// 调度
		/// </summary>
		[Column(Name = "调度", DbType = "int", Storage = "_调度", UpdateCheck = UpdateCheck.Never)]
		public int? 调度
		{
			get { return _调度; }
			set { _调度 = value; }
		}
		private int? _医生;
		/// <summary>
		/// 医生
		/// </summary>
		[Column(Name = "医生", DbType = "int", Storage = "_医生", UpdateCheck = UpdateCheck.Never)]
		public int? 医生
		{
			get { return _医生; }
			set { _医生 = value; }
		}
		private int? _护士;
		/// <summary>
		/// 护士
		/// </summary>
		[Column(Name = "护士", DbType = "int", Storage = "_护士", UpdateCheck = UpdateCheck.Never)]
		public int? 护士
		{
			get { return _护士; }
			set { _护士 = value; }
		}
		private int? _司机;
		/// <summary>
		/// 司机
		/// </summary>
		[Column(Name = "司机", DbType = "int", Storage = "_司机", UpdateCheck = UpdateCheck.Never)]
		public int? 司机
		{
			get { return _司机; }
			set { _司机 = value; }
		}
		private int? _担架;
		/// <summary>
		/// 担架
		/// </summary>
		[Column(Name = "担架", DbType = "int", Storage = "_担架", UpdateCheck = UpdateCheck.Never)]
		public int? 担架
		{
			get { return _担架; }
			set { _担架 = value; }
		}
		private int? _车组;
		/// <summary>
		/// 车组
		/// </summary>
		[Column(Name = "车组", DbType = "int", Storage = "_车组", UpdateCheck = UpdateCheck.Never)]
		public int? 车组
		{
			get { return _车组; }
			set { _车组 = value; }
		}
		private int? _车况;
		/// <summary>
		/// 车况
		/// </summary>
		[Column(Name = "车况", DbType = "int", Storage = "_车况", UpdateCheck = UpdateCheck.Never)]
		public int? 车况
		{
			get { return _车况; }
			set { _车况 = value; }
		}
		private int? _转运;
		/// <summary>
		/// 转运
		/// </summary>
		[Column(Name = "转运", DbType = "int", Storage = "_转运", UpdateCheck = UpdateCheck.Never)]
		public int? 转运
		{
			get { return _转运; }
			set { _转运 = value; }
		}
		private int? _速度;
		/// <summary>
		/// 速度
		/// </summary>
		[Column(Name = "速度", DbType = "int", Storage = "_速度", UpdateCheck = UpdateCheck.Never)]
		public int? 速度
		{
			get { return _速度; }
			set { _速度 = value; }
		}
		private int? _调度服务;
		/// <summary>
		/// 调度服务
		/// </summary>
		[Column(Name = "调度服务", DbType = "int", Storage = "_调度服务", UpdateCheck = UpdateCheck.Never)]
		public int? 调度服务
		{
			get { return _调度服务; }
			set { _调度服务 = value; }
		}
		private int? _医德医风;
		/// <summary>
		/// 医德医风
		/// </summary>
		[Column(Name = "医德医风", DbType = "int", Storage = "_医德医风", UpdateCheck = UpdateCheck.Never)]
		public int? 医德医风
		{
			get { return _医德医风; }
			set { _医德医风 = value; }
		}
		private int? _医护人员;
		/// <summary>
		/// 医护人员
		/// </summary>
		[Column(Name = "医护人员", DbType = "int", Storage = "_医护人员", UpdateCheck = UpdateCheck.Never)]
		public int? 医护人员
		{
			get { return _医护人员; }
			set { _医护人员 = value; }
		}
		private int? _价格;
		/// <summary>
		/// 价格
		/// </summary>
		[Column(Name = "价格", DbType = "int", Storage = "_价格", UpdateCheck = UpdateCheck.Never)]
		public int? 价格
		{
			get { return _价格; }
			set { _价格 = value; }
		}
		private int? _明细;
		/// <summary>
		/// 明细
		/// </summary>
		[Column(Name = "明细", DbType = "int", Storage = "_明细", UpdateCheck = UpdateCheck.Never)]
		public int? 明细
		{
			get { return _明细; }
			set { _明细 = value; }
		}
		private int? _发票;
		/// <summary>
		/// 发票
		/// </summary>
		[Column(Name = "发票", DbType = "int", Storage = "_发票", UpdateCheck = UpdateCheck.Never)]
		public int? 发票
		{
			get { return _发票; }
			set { _发票 = value; }
		}
		private int? _收费;
		/// <summary>
		/// 收费
		/// </summary>
		[Column(Name = "收费", DbType = "int", Storage = "_收费", UpdateCheck = UpdateCheck.Never)]
		public int? 收费
		{
			get { return _收费; }
			set { _收费 = value; }
		}
		private int? _统一;
		/// <summary>
		/// 统一
		/// </summary>
		[Column(Name = "统一", DbType = "int", Storage = "_统一", UpdateCheck = UpdateCheck.Never)]
		public int? 统一
		{
			get { return _统一; }
			set { _统一 = value; }
		}
		private string _备注;
		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "备注", DbType = "varchar(200)", Storage = "_备注", UpdateCheck = UpdateCheck.Never)]
		public string 备注
		{
			get { return _备注; }
			set { _备注 = value; }
		}
		private string _回访结果;
		/// <summary>
		/// 回访结果
		/// </summary>
		[Column(Name = "回访结果", DbType = "varchar(20)", Storage = "_回访结果", UpdateCheck = UpdateCheck.Never)]
		public string 回访结果
		{
			get { return _回访结果; }
			set { _回访结果 = value; }
		}
		private string _是否有效;
		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "是否有效", DbType = "varchar(20)", Storage = "_是否有效", UpdateCheck = UpdateCheck.Never)]
		public string 是否有效
		{
			get { return _是否有效; }
			set { _是否有效 = value; }
		}
		private string _是否要求回馈;
		/// <summary>
		/// 是否要求回馈
		/// </summary>
		[Column(Name = "是否要求回馈", DbType = "varchar(20)", Storage = "_是否要求回馈", UpdateCheck = UpdateCheck.Never)]
		public string 是否要求回馈
		{
			get { return _是否要求回馈; }
			set { _是否要求回馈 = value; }
		}
		private DateTime? _回访保存时间;
		/// <summary>
		/// 回访保存时间
		/// </summary>
		[Column(Name = "回访保存时间", DbType = "datetime", Storage = "_回访保存时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 回访保存时间
		{
			get { return _回访保存时间; }
			set { _回访保存时间 = value; }
		}
		private string _回馈;
		/// <summary>
		/// 回馈
		/// </summary>
		[Column(Name = "回馈", DbType = "varchar(200)", Storage = "_回馈", UpdateCheck = UpdateCheck.Never)]
		public string 回馈
		{
			get { return _回馈; }
			set { _回馈 = value; }
		}
		private string _回访人;
		/// <summary>
		/// 回访人
		/// </summary>
		[Column(Name = "回访人", DbType = "varchar(50)", Storage = "_回访人", UpdateCheck = UpdateCheck.Never)]
		public string 回访人
		{
			get { return _回访人; }
			set { _回访人 = value; }
		}
		private string _被回访人;
		/// <summary>
		/// 被回访人
		/// </summary>
		[Column(Name = "被回访人", DbType = "varchar(50)", Storage = "_被回访人", UpdateCheck = UpdateCheck.Never)]
		public string 被回访人
		{
			get { return _被回访人; }
			set { _被回访人 = value; }
		}
	}
}