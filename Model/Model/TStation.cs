using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TStation")]
	public class TStation
	{
		private string _编码;
		/// <summary>
		/// 编码
		/// </summary>
        [Column(IsPrimaryKey = true, Name = "编码", DbType = "char(3)", Storage = "_编码", UpdateCheck = UpdateCheck.Never)]
		public string 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
		private string _名称;
		/// <summary>
		/// 名称
		/// </summary>
		[Column(Name = "名称", DbType = "varchar(30)", Storage = "_名称", UpdateCheck = UpdateCheck.Never)]
		public string 名称
		{
			get { return _名称; }
			set { _名称 = value; }
		}
		private int _类型编码;
		/// <summary>
		/// 类型编码
		/// </summary>
		[Column(Name = "类型编码", DbType = "int", Storage = "_类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 类型编码
		{
			get { return _类型编码; }
			set { _类型编码 = value; }
		}
		private string _IP地址;
		/// <summary>
		/// IP地址
		/// </summary>
		[Column(Name = "IP地址", DbType = "varchar(50)", Storage = "_IP地址", UpdateCheck = UpdateCheck.Never)]
		public string IP地址
		{
			get { return _IP地址; }
			set { _IP地址 = value; }
		}
		private string _电话号码;
		/// <summary>
		/// 电话号码
		/// </summary>
		[Column(Name = "电话号码", DbType = "varchar(14)", Storage = "_电话号码", UpdateCheck = UpdateCheck.Never)]
		public string 电话号码
		{
			get { return _电话号码; }
			set { _电话号码 = value; }
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
		private bool _是否调度;
		/// <summary>
		/// 是否调度
		/// </summary>
		[Column(Name = "是否调度", DbType = "bit", Storage = "_是否调度", UpdateCheck = UpdateCheck.Never)]
		public bool 是否调度
		{
			get { return _是否调度; }
			set { _是否调度 = value; }
		}
		private string _所属区域;
		/// <summary>
		/// 所属区域
		/// </summary>
		[Column(Name = "所属区域", DbType = "varchar(100)", Storage = "_所属区域", UpdateCheck = UpdateCheck.Never)]
		public string 所属区域
		{
			get { return _所属区域; }
			set { _所属区域 = value; }
		}
		private bool _是否传送GPS;
		/// <summary>
		/// 是否传送GPS
		/// </summary>
		[Column(Name = "是否传送GPS", DbType = "bit", Storage = "_是否传送GPS", UpdateCheck = UpdateCheck.Never)]
		public bool 是否传送GPS
		{
			get { return _是否传送GPS; }
			set { _是否传送GPS = value; }
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
		private double _X坐标;
		/// <summary>
		/// X坐标
		/// </summary>
		[Column(Name = "X坐标", DbType = "float", Storage = "_X坐标", UpdateCheck = UpdateCheck.Never)]
		public double X坐标
		{
			get { return _X坐标; }
			set { _X坐标 = value; }
		}
		private double _Y坐标;
		/// <summary>
		/// Y坐标
		/// </summary>
		[Column(Name = "Y坐标", DbType = "float", Storage = "_Y坐标", UpdateCheck = UpdateCheck.Never)]
		public double Y坐标
		{
			get { return _Y坐标; }
			set { _Y坐标 = value; }
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
		private int _中心编码;
		/// <summary>
		/// 中心编码
		/// </summary>
		[Column(Name = "中心编码", DbType = "int", Storage = "_中心编码", UpdateCheck = UpdateCheck.Never)]
		public int 中心编码
		{
			get { return _中心编码; }
			set { _中心编码 = value; }
		}
	}
}