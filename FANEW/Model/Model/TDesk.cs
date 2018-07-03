using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TDesk")]
	public class TDesk
	{
		private string _台号;
		/// <summary>
		/// 台号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "台号", DbType = "char(2)", Storage = "_台号")]
		public string 台号
		{
			get { return _台号; }
			set { _台号 = value; }
		}
		private string _机器名称;
		/// <summary>
		/// 机器名称
		/// </summary>
		[Column(Name = "机器名称", DbType = "varchar(20)", Storage = "_机器名称", UpdateCheck = UpdateCheck.Never)]
		public string 机器名称
		{
			get { return _机器名称; }
			set { _机器名称 = value; }
		}
		private string _显示名称;
		/// <summary>
		/// 显示名称
		/// </summary>
		[Column(Name = "显示名称", DbType = "varchar(20)", Storage = "_显示名称", UpdateCheck = UpdateCheck.Never)]
		public string 显示名称
		{
			get { return _显示名称; }
			set { _显示名称 = value; }
		}
		private string _分机号码;
		/// <summary>
		/// 分机号码
		/// </summary>
		[Column(Name = "分机号码", DbType = "varchar(10)", Storage = "_分机号码", UpdateCheck = UpdateCheck.Never)]
		public string 分机号码
		{
			get { return _分机号码; }
			set { _分机号码 = value; }
		}
		private string _ACD台号;
		/// <summary>
		/// ACD台号
		/// </summary>
		[Column(Name = "ACD台号", DbType = "varchar(6)", Storage = "_ACD台号", UpdateCheck = UpdateCheck.Never)]
		public string ACD台号
		{
			get { return _ACD台号; }
			set { _ACD台号 = value; }
		}
		private string _调度员编码;
		/// <summary>
		/// 调度员编码
		/// </summary>
		[Column(Name = "调度员编码", DbType = "char(5)", Storage = "_调度员编码", UpdateCheck = UpdateCheck.Never)]
		public string 调度员编码
		{
			get { return _调度员编码; }
			set { _调度员编码 = value; }
		}
		private int _受理台类型编码;
		/// <summary>
		/// 受理台类型编码
		/// </summary>
		[Column(Name = "受理台类型编码", DbType = "int", Storage = "_受理台类型编码", UpdateCheck = UpdateCheck.Never)]
		public int 受理台类型编码
		{
			get { return _受理台类型编码; }
			set { _受理台类型编码 = value; }
		}
		private string _设备编码;
		/// <summary>
		/// 设备编码
		/// </summary>
		[Column(Name = "设备编码", DbType = "varchar(10)", Storage = "_设备编码", UpdateCheck = UpdateCheck.Never)]
		public string 设备编码
		{
			get { return _设备编码; }
			set { _设备编码 = value; }
		}
		private string _TN码;
		/// <summary>
		/// TN码
		/// </summary>
		[Column(Name = "TN码", DbType = "varchar(6)", Storage = "_TN码", UpdateCheck = UpdateCheck.Never)]
		public string TN码
		{
			get { return _TN码; }
			set { _TN码 = value; }
		}
		private int? _设备标志;
		/// <summary>
		/// 设备标志
		/// </summary>
		[Column(Name = "设备标志", DbType = "int", Storage = "_设备标志", UpdateCheck = UpdateCheck.Never)]
		public int? 设备标志
		{
			get { return _设备标志; }
			set { _设备标志 = value; }
		}
		private int? _ACD组位置标识;
		/// <summary>
		/// ACD组位置标识
		/// </summary>
		[Column(Name = "ACD组位置标识", DbType = "int", Storage = "_ACD组位置标识", UpdateCheck = UpdateCheck.Never)]
		public int? ACD组位置标识
		{
			get { return _ACD组位置标识; }
			set { _ACD组位置标识 = value; }
		}
		private int? _ACD组优先级;
		/// <summary>
		/// ACD组优先级
		/// </summary>
		[Column(Name = "ACD组优先级", DbType = "int", Storage = "_ACD组优先级", UpdateCheck = UpdateCheck.Never)]
		public int? ACD组优先级
		{
			get { return _ACD组优先级; }
			set { _ACD组优先级 = value; }
		}
		private bool _是否监控;
		/// <summary>
		/// 是否监控
		/// </summary>
		[Column(Name = "是否监控", DbType = "bit", Storage = "_是否监控", UpdateCheck = UpdateCheck.Never)]
		public bool 是否监控
		{
			get { return _是否监控; }
			set { _是否监控 = value; }
		}
		private int? _整型字段;
		/// <summary>
		/// 整型字段
		/// </summary>
		[Column(Name = "整型字段", DbType = "int", Storage = "_整型字段", UpdateCheck = UpdateCheck.Never)]
		public int? 整型字段
		{
			get { return _整型字段; }
			set { _整型字段 = value; }
		}
		private string _字符字段;
		/// <summary>
		/// 字符字段
		/// </summary>
		[Column(Name = "字符字段", DbType = "varchar(20)", Storage = "_字符字段", UpdateCheck = UpdateCheck.Never)]
		public string 字符字段
		{
			get { return _字符字段; }
			set { _字符字段 = value; }
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
		private bool? _是否有效;
		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "是否有效", DbType = "bit", Storage = "_是否有效", UpdateCheck = UpdateCheck.Never)]
		public bool? 是否有效
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