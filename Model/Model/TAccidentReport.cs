using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAccidentReport")]
	public class TAccidentReport
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
		private string _事故编码;
		/// <summary>
		/// 事故编码
		/// </summary>
		[Column(Name = "事故编码", DbType = "varchar(16)", Storage = "_事故编码", UpdateCheck = UpdateCheck.Never)]
		public string 事故编码
		{
			get { return _事故编码; }
			set { _事故编码 = value; }
		}
		private DateTime? _时刻;
		/// <summary>
		/// 时刻
		/// </summary>
		[Column(Name = "时刻", DbType = "datetime", Storage = "_时刻", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 时刻
		{
			get { return _时刻; }
			set { _时刻 = value; }
		}
		private string _类别;
		/// <summary>
		/// 类别
		/// </summary>
		[Column(Name = "类别", DbType = "varchar(20)", Storage = "_类别", UpdateCheck = UpdateCheck.Never)]
		public string 类别
		{
			get { return _类别; }
			set { _类别 = value; }
		}
		private string _姓名;
		/// <summary>
		/// 姓名
		/// </summary>
		[Column(Name = "姓名", DbType = "varchar(50)", Storage = "_姓名", UpdateCheck = UpdateCheck.Never)]
		public string 姓名
		{
			get { return _姓名; }
			set { _姓名 = value; }
		}
		private int _死亡人数;
		/// <summary>
		/// 死亡人数
		/// </summary>
		[Column(Name = "死亡人数", DbType = "int", Storage = "_死亡人数", UpdateCheck = UpdateCheck.Never)]
		public int 死亡人数
		{
			get { return _死亡人数; }
			set { _死亡人数 = value; }
		}
		private int? _死亡男性人数;
		/// <summary>
		/// 死亡男性人数
		/// </summary>
		[Column(Name = "死亡男性人数", DbType = "int", Storage = "_死亡男性人数", UpdateCheck = UpdateCheck.Never)]
		public int? 死亡男性人数
		{
			get { return _死亡男性人数; }
			set { _死亡男性人数 = value; }
		}
		private int? _死亡女性人数;
		/// <summary>
		/// 死亡女性人数
		/// </summary>
		[Column(Name = "死亡女性人数", DbType = "int", Storage = "_死亡女性人数", UpdateCheck = UpdateCheck.Never)]
		public int? 死亡女性人数
		{
			get { return _死亡女性人数; }
			set { _死亡女性人数 = value; }
		}
		private int _受伤人数;
		/// <summary>
		/// 受伤人数
		/// </summary>
		[Column(Name = "受伤人数", DbType = "int", Storage = "_受伤人数", UpdateCheck = UpdateCheck.Never)]
		public int 受伤人数
		{
			get { return _受伤人数; }
			set { _受伤人数 = value; }
		}
		private int? _受伤男性人数;
		/// <summary>
		/// 受伤男性人数
		/// </summary>
		[Column(Name = "受伤男性人数", DbType = "int", Storage = "_受伤男性人数", UpdateCheck = UpdateCheck.Never)]
		public int? 受伤男性人数
		{
			get { return _受伤男性人数; }
			set { _受伤男性人数 = value; }
		}
		private int? _受伤女性人数;
		/// <summary>
		/// 受伤女性人数
		/// </summary>
		[Column(Name = "受伤女性人数", DbType = "int", Storage = "_受伤女性人数", UpdateCheck = UpdateCheck.Never)]
		public int? 受伤女性人数
		{
			get { return _受伤女性人数; }
			set { _受伤女性人数 = value; }
		}
		private int _重伤人数;
		/// <summary>
		/// 重伤人数
		/// </summary>
		[Column(Name = "重伤人数", DbType = "int", Storage = "_重伤人数", UpdateCheck = UpdateCheck.Never)]
		public int 重伤人数
		{
			get { return _重伤人数; }
			set { _重伤人数 = value; }
		}
		private int _轻伤人数;
		/// <summary>
		/// 轻伤人数
		/// </summary>
		[Column(Name = "轻伤人数", DbType = "int", Storage = "_轻伤人数", UpdateCheck = UpdateCheck.Never)]
		public int 轻伤人数
		{
			get { return _轻伤人数; }
			set { _轻伤人数 = value; }
		}
		private string _备用字段1;
		/// <summary>
		/// 备用字段1
		/// </summary>
		[Column(Name = "备用字段1", DbType = "varchar(1000)", Storage = "_备用字段1", UpdateCheck = UpdateCheck.Never)]
		public string 备用字段1
		{
			get { return _备用字段1; }
			set { _备用字段1 = value; }
		}
		private string _备用字段2;
		/// <summary>
		/// 备用字段2
		/// </summary>
		[Column(Name = "备用字段2", DbType = "varchar(1000)", Storage = "_备用字段2", UpdateCheck = UpdateCheck.Never)]
		public string 备用字段2
		{
			get { return _备用字段2; }
			set { _备用字段2 = value; }
		}
		private string _备用字段3;
		/// <summary>
		/// 备用字段3
		/// </summary>
		[Column(Name = "备用字段3", DbType = "varchar(1000)", Storage = "_备用字段3", UpdateCheck = UpdateCheck.Never)]
		public string 备用字段3
		{
			get { return _备用字段3; }
			set { _备用字段3 = value; }
		}
		private string _备用字段4;
		/// <summary>
		/// 备用字段4
		/// </summary>
		[Column(Name = "备用字段4", DbType = "varchar(1000)", Storage = "_备用字段4", UpdateCheck = UpdateCheck.Never)]
		public string 备用字段4
		{
			get { return _备用字段4; }
			set { _备用字段4 = value; }
		}
	}
}