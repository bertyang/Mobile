using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TAccidentPatient")]
	public class TAccidentPatient
	{
		private string _事故编码;
		/// <summary>
		/// 事故编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "事故编码", DbType = "varchar(16)", Storage = "_事故编码")]
		public string 事故编码
		{
			get { return _事故编码; }
			set { _事故编码 = value; }
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
		private string _性别;
		/// <summary>
		/// 性别
		/// </summary>
		[Column(Name = "性别", DbType = "varchar(10)", Storage = "_性别", UpdateCheck = UpdateCheck.Never)]
		public string 性别
		{
			get { return _性别; }
			set { _性别 = value; }
		}
		private int? _年龄;
		/// <summary>
		/// 年龄
		/// </summary>
		[Column(Name = "年龄", DbType = "int", Storage = "_年龄", UpdateCheck = UpdateCheck.Never)]
		public int? 年龄
		{
			get { return _年龄; }
			set { _年龄 = value; }
		}
		private string _年龄单位;
		/// <summary>
		/// 年龄单位
		/// </summary>
		[Column(Name = "年龄单位", DbType = "varchar(10)", Storage = "_年龄单位", UpdateCheck = UpdateCheck.Never)]
		public string 年龄单位
		{
			get { return _年龄单位; }
			set { _年龄单位 = value; }
		}
		private string _职业;
		/// <summary>
		/// 职业
		/// </summary>
		[Column(Name = "职业", DbType = "varchar(50)", Storage = "_职业", UpdateCheck = UpdateCheck.Never)]
		public string 职业
		{
			get { return _职业; }
			set { _职业 = value; }
		}
		private string _单位;
		/// <summary>
		/// 单位
		/// </summary>
		[Column(Name = "单位", DbType = "varchar(100)", Storage = "_单位", UpdateCheck = UpdateCheck.Never)]
		public string 单位
		{
			get { return _单位; }
			set { _单位 = value; }
		}
		private string _住址;
		/// <summary>
		/// 住址
		/// </summary>
		[Column(Name = "住址", DbType = "varchar(100)", Storage = "_住址", UpdateCheck = UpdateCheck.Never)]
		public string 住址
		{
			get { return _住址; }
			set { _住址 = value; }
		}
		private string _初步诊断;
		/// <summary>
		/// 初步诊断
		/// </summary>
		[Column(Name = "初步诊断", DbType = "varchar(200)", Storage = "_初步诊断", UpdateCheck = UpdateCheck.Never)]
		public string 初步诊断
		{
			get { return _初步诊断; }
			set { _初步诊断 = value; }
		}
		private int? _病情编码;
		/// <summary>
		/// 病情编码
		/// </summary>
		[Column(Name = "病情编码", DbType = "int", Storage = "_病情编码", UpdateCheck = UpdateCheck.Never)]
		public int? 病情编码
		{
			get { return _病情编码; }
			set { _病情编码 = value; }
		}
		private string _收治医院;
		/// <summary>
		/// 收治医院
		/// </summary>
		[Column(Name = "收治医院", DbType = "varchar(100)", Storage = "_收治医院", UpdateCheck = UpdateCheck.Never)]
		public string 收治医院
		{
			get { return _收治医院; }
			set { _收治医院 = value; }
		}
		private string _治疗方式;
		/// <summary>
		/// 治疗方式
		/// </summary>
		[Column(Name = "治疗方式", DbType = "varchar(200)", Storage = "_治疗方式", UpdateCheck = UpdateCheck.Never)]
		public string 治疗方式
		{
			get { return _治疗方式; }
			set { _治疗方式 = value; }
		}
		private string _转归;
		/// <summary>
		/// 转归
		/// </summary>
		[Column(Name = "转归", DbType = "varchar(50)", Storage = "_转归", UpdateCheck = UpdateCheck.Never)]
		public string 转归
		{
			get { return _转归; }
			set { _转归 = value; }
		}
	}
}