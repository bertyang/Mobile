using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TCureRule")]
	public class TCureRule
	{
		private string _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "varchar(4)", Storage = "_编码")]
		public string 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
		private string _疾病名称;
		/// <summary>
		/// 疾病名称
		/// </summary>
		[Column(Name = "疾病名称", DbType = "varchar(30)", Storage = "_疾病名称", UpdateCheck = UpdateCheck.Never)]
		public string 疾病名称
		{
			get { return _疾病名称; }
			set { _疾病名称 = value; }
		}
		private string _病症描述;
		/// <summary>
		/// 病症描述
		/// </summary>
		[Column(Name = "病症描述", DbType = "text(16)", Storage = "_病症描述", UpdateCheck = UpdateCheck.Never)]
		public string 病症描述
		{
			get { return _病症描述; }
			set { _病症描述 = value; }
		}
		private string _诊断要点;
		/// <summary>
		/// 诊断要点
		/// </summary>
		[Column(Name = "诊断要点", DbType = "text(16)", Storage = "_诊断要点", UpdateCheck = UpdateCheck.Never)]
		public string 诊断要点
		{
			get { return _诊断要点; }
			set { _诊断要点 = value; }
		}
		private string _即刻处理;
		/// <summary>
		/// 即刻处理
		/// </summary>
		[Column(Name = "即刻处理", DbType = "text(16)", Storage = "_即刻处理", UpdateCheck = UpdateCheck.Never)]
		public string 即刻处理
		{
			get { return _即刻处理; }
			set { _即刻处理 = value; }
		}
		private string _转运条件;
		/// <summary>
		/// 转运条件
		/// </summary>
		[Column(Name = "转运条件", DbType = "text(16)", Storage = "_转运条件", UpdateCheck = UpdateCheck.Never)]
		public string 转运条件
		{
			get { return _转运条件; }
			set { _转运条件 = value; }
		}
		private int? _使用频率;
		/// <summary>
		/// 使用频率
		/// </summary>
		[Column(Name = "使用频率", DbType = "int", Storage = "_使用频率", UpdateCheck = UpdateCheck.Never)]
		public int? 使用频率
		{
			get { return _使用频率; }
			set { _使用频率 = value; }
		}
	}
}