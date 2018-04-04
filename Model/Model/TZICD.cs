using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TZICD")]
	public class TZICD
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
		private string _名称;
		/// <summary>
		/// 名称
		/// </summary>
		[Column(Name = "名称", DbType = "varchar(50)", Storage = "_名称", UpdateCheck = UpdateCheck.Never)]
		public string 名称
		{
			get { return _名称; }
			set { _名称 = value; }
		}
		private int? _顺序号;
		/// <summary>
		/// 顺序号
		/// </summary>
		[Column(Name = "顺序号", DbType = "int", Storage = "_顺序号", UpdateCheck = UpdateCheck.Never)]
		public int? 顺序号
		{
			get { return _顺序号; }
			set { _顺序号 = value; }
		}
		private int? _上级编码;
		/// <summary>
		/// 上级编码
		/// </summary>
		[Column(Name = "上级编码", DbType = "int", Storage = "_上级编码", UpdateCheck = UpdateCheck.Never)]
		public int? 上级编码
		{
			get { return _上级编码; }
			set { _上级编码 = value; }
		}
		private string _ICD10编码;
		/// <summary>
		/// ICD10编码
		/// </summary>
		[Column(Name = "ICD10编码", DbType = "varchar(50)", Storage = "_ICD10编码", UpdateCheck = UpdateCheck.Never)]
		public string ICD10编码
		{
			get { return _ICD10编码; }
			set { _ICD10编码 = value; }
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
		private string _字头;
		/// <summary>
		/// 字头
		/// </summary>
		[Column(Name = "字头", DbType = "varchar(50)", Storage = "_字头", UpdateCheck = UpdateCheck.Never)]
		public string 字头
		{
			get { return _字头; }
			set { _字头 = value; }
		}
		private int? _一级目录编码;
		/// <summary>
		/// 一级目录编码
		/// </summary>
		[Column(Name = "一级目录编码", DbType = "int", Storage = "_一级目录编码", UpdateCheck = UpdateCheck.Never)]
		public int? 一级目录编码
		{
			get { return _一级目录编码; }
			set { _一级目录编码 = value; }
		}
		private int? _二级目录编码;
		/// <summary>
		/// 二级目录编码
		/// </summary>
		[Column(Name = "二级目录编码", DbType = "int", Storage = "_二级目录编码", UpdateCheck = UpdateCheck.Never)]
		public int? 二级目录编码
		{
			get { return _二级目录编码; }
			set { _二级目录编码 = value; }
		}
		private int? _三级目录编码;
		/// <summary>
		/// 三级目录编码
		/// </summary>
		[Column(Name = "三级目录编码", DbType = "int", Storage = "_三级目录编码", UpdateCheck = UpdateCheck.Never)]
		public int? 三级目录编码
		{
			get { return _三级目录编码; }
			set { _三级目录编码 = value; }
		}
		private int? _四级目录编码;
		/// <summary>
		/// 四级目录编码
		/// </summary>
		[Column(Name = "四级目录编码", DbType = "int", Storage = "_四级目录编码", UpdateCheck = UpdateCheck.Never)]
		public int? 四级目录编码
		{
			get { return _四级目录编码; }
			set { _四级目录编码 = value; }
		}
	}
}