using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPoison")]
	public class TPoison
	{
		private string _序号;
		/// <summary>
		/// 序号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "序号", DbType = "varchar(8)", Storage = "_序号")]
		public string 序号
		{
			get { return _序号; }
			set { _序号 = value; }
		}
		private string _类别;
		/// <summary>
		/// 类别
		/// </summary>
		[Column(Name = "类别", DbType = "varchar(1)", Storage = "_类别", UpdateCheck = UpdateCheck.Never)]
		public string 类别
		{
			get { return _类别; }
			set { _类别 = value; }
		}
		private string _父类;
		/// <summary>
		/// 父类
		/// </summary>
		[Column(Name = "父类", DbType = "varchar(8)", Storage = "_父类", UpdateCheck = UpdateCheck.Never)]
		public string 父类
		{
			get { return _父类; }
			set { _父类 = value; }
		}
		private string _中文名称;
		/// <summary>
		/// 中文名称
		/// </summary>
		[Column(Name = "中文名称", DbType = "varchar(40)", Storage = "_中文名称", UpdateCheck = UpdateCheck.Never)]
		public string 中文名称
		{
			get { return _中文名称; }
			set { _中文名称 = value; }
		}
		private string _英文名称;
		/// <summary>
		/// 英文名称
		/// </summary>
		[Column(Name = "英文名称", DbType = "varchar(40)", Storage = "_英文名称", UpdateCheck = UpdateCheck.Never)]
		public string 英文名称
		{
			get { return _英文名称; }
			set { _英文名称 = value; }
		}
		private string _别名;
		/// <summary>
		/// 别名
		/// </summary>
		[Column(Name = "别名", DbType = "varchar(100)", Storage = "_别名", UpdateCheck = UpdateCheck.Never)]
		public string 别名
		{
			get { return _别名; }
			set { _别名 = value; }
		}
		private string _性质;
		/// <summary>
		/// 性质
		/// </summary>
		[Column(Name = "性质", DbType = "text(16)", Storage = "_性质", UpdateCheck = UpdateCheck.Never)]
		public string 性质
		{
			get { return _性质; }
			set { _性质 = value; }
		}
		private string _毒性;
		/// <summary>
		/// 毒性
		/// </summary>
		[Column(Name = "毒性", DbType = "text(16)", Storage = "_毒性", UpdateCheck = UpdateCheck.Never)]
		public string 毒性
		{
			get { return _毒性; }
			set { _毒性 = value; }
		}
		private string _特点;
		/// <summary>
		/// 特点
		/// </summary>
		[Column(Name = "特点", DbType = "text(16)", Storage = "_特点", UpdateCheck = UpdateCheck.Never)]
		public string 特点
		{
			get { return _特点; }
			set { _特点 = value; }
		}
		private string _毒理作用;
		/// <summary>
		/// 毒理作用
		/// </summary>
		[Column(Name = "毒理作用", DbType = "text(16)", Storage = "_毒理作用", UpdateCheck = UpdateCheck.Never)]
		public string 毒理作用
		{
			get { return _毒理作用; }
			set { _毒理作用 = value; }
		}
		private string _中毒表现;
		/// <summary>
		/// 中毒表现
		/// </summary>
		[Column(Name = "中毒表现", DbType = "text(16)", Storage = "_中毒表现", UpdateCheck = UpdateCheck.Never)]
		public string 中毒表现
		{
			get { return _中毒表现; }
			set { _中毒表现 = value; }
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
		private string _救治要点;
		/// <summary>
		/// 救治要点
		/// </summary>
		[Column(Name = "救治要点", DbType = "text(16)", Storage = "_救治要点", UpdateCheck = UpdateCheck.Never)]
		public string 救治要点
		{
			get { return _救治要点; }
			set { _救治要点 = value; }
		}
		private string _毒理;
		/// <summary>
		/// 毒理
		/// </summary>
		[Column(Name = "毒理", DbType = "text(16)", Storage = "_毒理", UpdateCheck = UpdateCheck.Never)]
		public string 毒理
		{
			get { return _毒理; }
			set { _毒理 = value; }
		}
		private string _药动学;
		/// <summary>
		/// 药动学
		/// </summary>
		[Column(Name = "药动学", DbType = "text(16)", Storage = "_药动学", UpdateCheck = UpdateCheck.Never)]
		public string 药动学
		{
			get { return _药动学; }
			set { _药动学 = value; }
		}
		private string _实验室检查;
		/// <summary>
		/// 实验室检查
		/// </summary>
		[Column(Name = "实验室检查", DbType = "text(16)", Storage = "_实验室检查", UpdateCheck = UpdateCheck.Never)]
		public string 实验室检查
		{
			get { return _实验室检查; }
			set { _实验室检查 = value; }
		}
		private string _药理;
		/// <summary>
		/// 药理
		/// </summary>
		[Column(Name = "药理", DbType = "text(16)", Storage = "_药理", UpdateCheck = UpdateCheck.Never)]
		public string 药理
		{
			get { return _药理; }
			set { _药理 = value; }
		}
		private string _病原学;
		/// <summary>
		/// 病原学
		/// </summary>
		[Column(Name = "病原学", DbType = "text(16)", Storage = "_病原学", UpdateCheck = UpdateCheck.Never)]
		public string 病原学
		{
			get { return _病原学; }
			set { _病原学 = value; }
		}
		private string _流行病学;
		/// <summary>
		/// 流行病学
		/// </summary>
		[Column(Name = "流行病学", DbType = "text(16)", Storage = "_流行病学", UpdateCheck = UpdateCheck.Never)]
		public string 流行病学
		{
			get { return _流行病学; }
			set { _流行病学 = value; }
		}
		private string _备注;
		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "备注", DbType = "text(16)", Storage = "_备注", UpdateCheck = UpdateCheck.Never)]
		public string 备注
		{
			get { return _备注; }
			set { _备注 = value; }
		}
	}
}