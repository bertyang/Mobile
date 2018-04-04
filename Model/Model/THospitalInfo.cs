using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "THospitalInfo")]
	public class THospitalInfo
	{
		private string _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "char(3)", Storage = "_编码")]
		public string 编码
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
		private int _等级编码;
		/// <summary>
		/// 等级编码
		/// </summary>
		[Column(Name = "等级编码", DbType = "int", Storage = "_等级编码", UpdateCheck = UpdateCheck.Never)]
		public int 等级编码
		{
			get { return _等级编码; }
			set { _等级编码 = value; }
		}
		private string _拼音头;
		/// <summary>
		/// 拼音头
		/// </summary>
		[Column(Name = "拼音头", DbType = "varchar(20)", Storage = "_拼音头", UpdateCheck = UpdateCheck.Never)]
		public string 拼音头
		{
			get { return _拼音头; }
			set { _拼音头 = value; }
		}
		private string _地址;
		/// <summary>
		/// 地址
		/// </summary>
		[Column(Name = "地址", DbType = "varchar(100)", Storage = "_地址", UpdateCheck = UpdateCheck.Never)]
		public string 地址
		{
			get { return _地址; }
			set { _地址 = value; }
		}
		private string _联系电话;
		/// <summary>
		/// 联系电话
		/// </summary>
		[Column(Name = "联系电话", DbType = "varchar(14)", Storage = "_联系电话", UpdateCheck = UpdateCheck.Never)]
		public string 联系电话
		{
			get { return _联系电话; }
			set { _联系电话 = value; }
		}
		private string _联系信息;
		/// <summary>
		/// 联系信息
		/// </summary>
		[Column(Name = "联系信息", DbType = "varchar(20)", Storage = "_联系信息", UpdateCheck = UpdateCheck.Never)]
		public string 联系信息
		{
			get { return _联系信息; }
			set { _联系信息 = value; }
		}
		private string _基本情况;
		/// <summary>
		/// 基本情况
		/// </summary>
		[Column(Name = "基本情况", DbType = "varchar(1000)", Storage = "_基本情况", UpdateCheck = UpdateCheck.Never)]
		public string 基本情况
		{
			get { return _基本情况; }
			set { _基本情况 = value; }
		}
		private string _通信标识;
		/// <summary>
		/// 通信标识
		/// </summary>
		[Column(Name = "通信标识", DbType = "varchar(50)", Storage = "_通信标识", UpdateCheck = UpdateCheck.Never)]
		public string 通信标识
		{
			get { return _通信标识; }
			set { _通信标识 = value; }
		}
		private bool _是否标注;
		/// <summary>
		/// 是否标注
		/// </summary>
		[Column(Name = "是否标注", DbType = "bit", Storage = "_是否标注", UpdateCheck = UpdateCheck.Never)]
		public bool 是否标注
		{
			get { return _是否标注; }
			set { _是否标注 = value; }
		}
		private double? _X坐标;
		/// <summary>
		/// X坐标
		/// </summary>
		[Column(Name = "X坐标", DbType = "float", Storage = "_X坐标", UpdateCheck = UpdateCheck.Never)]
		public double? X坐标
		{
			get { return _X坐标; }
			set { _X坐标 = value; }
		}
		private double? _Y坐标;
		/// <summary>
		/// Y坐标
		/// </summary>
		[Column(Name = "Y坐标", DbType = "float", Storage = "_Y坐标", UpdateCheck = UpdateCheck.Never)]
		public double? Y坐标
		{
			get { return _Y坐标; }
			set { _Y坐标 = value; }
		}
		private bool _是否在线;
		/// <summary>
		/// 是否在线
		/// </summary>
		[Column(Name = "是否在线", DbType = "bit", Storage = "_是否在线", UpdateCheck = UpdateCheck.Never)]
		public bool 是否在线
		{
			get { return _是否在线; }
			set { _是否在线 = value; }
		}
		private int _顺序号;
		/// <summary>
		/// 顺序号
		/// </summary>
		[Column(Name = "顺序号", DbType = "int", Storage = "_顺序号", UpdateCheck = UpdateCheck.Never)]
		public int 顺序号
		{
			get { return _顺序号; }
			set { _顺序号 = value; }
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
		private string _分站编码;
		/// <summary>
		/// 分站编码
		/// </summary>
		[Column(Name = "分站编码", DbType = "char(3)", Storage = "_分站编码", UpdateCheck = UpdateCheck.Never)]
		public string 分站编码
		{
			get { return _分站编码; }
			set { _分站编码 = value; }
		}
	}
}