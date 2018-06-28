using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TCenter")]
	public class TCenter
	{
		private int _编码;
		/// <summary>
		/// 编码
		/// </summary>
        [Column(IsPrimaryKey = true, Name = "编码", DbType = "int", Storage = "_编码", UpdateCheck = UpdateCheck.Never)]
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
		private string _拼音头;
		/// <summary>
		/// 拼音头
		/// </summary>
		[Column(Name = "拼音头", DbType = "varchar(10)", Storage = "_拼音头", UpdateCheck = UpdateCheck.Never)]
		public string 拼音头
		{
			get { return _拼音头; }
			set { _拼音头 = value; }
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
		private string _IP地址;
		/// <summary>
		/// IP地址
		/// </summary>
		[Column(Name = "IP地址", DbType = "varchar(100)", Storage = "_IP地址", UpdateCheck = UpdateCheck.Never)]
		public string IP地址
		{
			get { return _IP地址; }
			set { _IP地址 = value; }
		}
		private string _CTI服务IP地址;
		/// <summary>
		/// CTI服务IP地址
		/// </summary>
		[Column(Name = "CTI服务IP地址", DbType = "varchar(15)", Storage = "_CTI服务IP地址", UpdateCheck = UpdateCheck.Never)]
		public string CTI服务IP地址
		{
			get { return _CTI服务IP地址; }
			set { _CTI服务IP地址 = value; }
		}
		private string _电话号码;
		/// <summary>
		/// 电话号码
		/// </summary>
		[Column(Name = "电话号码", DbType = "varchar(100)", Storage = "_电话号码", UpdateCheck = UpdateCheck.Never)]
		public string 电话号码
		{
			get { return _电话号码; }
			set { _电话号码 = value; }
		}
		private int _当前任务流水号;
		/// <summary>
		/// 当前任务流水号
		/// </summary>
		[Column(Name = "当前任务流水号", DbType = "int", Storage = "_当前任务流水号", UpdateCheck = UpdateCheck.Never)]
		public int 当前任务流水号
		{
			get { return _当前任务流水号; }
			set { _当前任务流水号 = value; }
		}
		private int _所属调度中心编码;
		/// <summary>
		/// 所属调度中心编码
		/// </summary>
		[Column(Name = "所属调度中心编码", DbType = "int", Storage = "_所属调度中心编码", UpdateCheck = UpdateCheck.Never)]
		public int 所属调度中心编码
		{
			get { return _所属调度中心编码; }
			set { _所属调度中心编码 = value; }
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
        private bool _是否发送短信;
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(Name = "是否发送短信", DbType = "bit", Storage = "_是否发送短信", UpdateCheck = UpdateCheck.Never)]
        public bool 是否发送短信
        {
            get { return _是否发送短信; }
            set { _是否发送短信 = value; }
        }
	}
}