using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPatientRecordWsjc")]
	public class TPatientRecordWsjc
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
		private string _外伤部位;
		/// <summary>
		/// 外伤部位
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "外伤部位", DbType = "nvarchar(20)", Storage = "_外伤部位")]
		public string 外伤部位
		{
			get { return _外伤部位; }
			set { _外伤部位 = value; }
		}
		private string _外伤类型;
		/// <summary>
		/// 外伤类型
		/// </summary>
		[Column(Name = "外伤类型", DbType = "nvarchar(20)", Storage = "_外伤类型", UpdateCheck = UpdateCheck.Never)]
		public string 外伤类型
		{
			get { return _外伤类型; }
			set { _外伤类型 = value; }
		}
		private string _局部伤情;
		/// <summary>
		/// 局部伤情
		/// </summary>
		[Column(Name = "局部伤情", DbType = "nvarchar(400)", Storage = "_局部伤情", UpdateCheck = UpdateCheck.Never)]
		public string 局部伤情
		{
			get { return _局部伤情; }
			set { _局部伤情 = value; }
		}
	}
}