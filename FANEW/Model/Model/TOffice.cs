using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TOffice")]
	public class TOffice
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
		private int? _发送类型编码;
		/// <summary>
		/// 发送类型编码
		/// </summary>
		[Column(Name = "发送类型编码", DbType = "int", Storage = "_发送类型编码", UpdateCheck = UpdateCheck.Never)]
		public int? 发送类型编码
		{
			get { return _发送类型编码; }
			set { _发送类型编码 = value; }
		}
		private string _接收部门编码;
		/// <summary>
		/// 接收部门编码
		/// </summary>
		[Column(Name = "接收部门编码", DbType = "nvarchar(1000)", Storage = "_接收部门编码", UpdateCheck = UpdateCheck.Never)]
		public string 接收部门编码
		{
			get { return _接收部门编码; }
			set { _接收部门编码 = value; }
		}
		private string _接收分站编码;
		/// <summary>
		/// 接收分站编码
		/// </summary>
		[Column(Name = "接收分站编码", DbType = "nvarchar(1000)", Storage = "_接收分站编码", UpdateCheck = UpdateCheck.Never)]
		public string 接收分站编码
		{
			get { return _接收分站编码; }
			set { _接收分站编码 = value; }
		}
		private string _接收人编码;
		/// <summary>
		/// 接收人编码
		/// </summary>
		[Column(Name = "接收人编码", DbType = "nvarchar(1000)", Storage = "_接收人编码", UpdateCheck = UpdateCheck.Never)]
		public string 接收人编码
		{
			get { return _接收人编码; }
			set { _接收人编码 = value; }
		}
		private string _接收部门;
		/// <summary>
		/// 接收部门
		/// </summary>
		[Column(Name = "接收部门", DbType = "nvarchar(2000)", Storage = "_接收部门", UpdateCheck = UpdateCheck.Never)]
		public string 接收部门
		{
			get { return _接收部门; }
			set { _接收部门 = value; }
		}
		private string _接收分站;
		/// <summary>
		/// 接收分站
		/// </summary>
		[Column(Name = "接收分站", DbType = "nvarchar(2000)", Storage = "_接收分站", UpdateCheck = UpdateCheck.Never)]
		public string 接收分站
		{
			get { return _接收分站; }
			set { _接收分站 = value; }
		}
		private string _接收人;
		/// <summary>
		/// 接收人
		/// </summary>
		[Column(Name = "接收人", DbType = "nvarchar(2000)", Storage = "_接收人", UpdateCheck = UpdateCheck.Never)]
		public string 接收人
		{
			get { return _接收人; }
			set { _接收人 = value; }
		}
		private string _办公类型编码;
		/// <summary>
		/// 办公类型编码
		/// </summary>
		[Column(Name = "办公类型编码", DbType = "varchar(30)", Storage = "_办公类型编码", UpdateCheck = UpdateCheck.Never)]
		public string 办公类型编码
		{
			get { return _办公类型编码; }
			set { _办公类型编码 = value; }
		}
		private string _标题;
		/// <summary>
		/// 标题
		/// </summary>
		[Column(Name = "标题", DbType = "nvarchar(200)", Storage = "_标题", UpdateCheck = UpdateCheck.Never)]
		public string 标题
		{
			get { return _标题; }
			set { _标题 = value; }
		}
		private string _作者;
		/// <summary>
		/// 作者
		/// </summary>
		[Column(Name = "作者", DbType = "nvarchar(40)", Storage = "_作者", UpdateCheck = UpdateCheck.Never)]
		public string 作者
		{
			get { return _作者; }
			set { _作者 = value; }
		}
		private string _发送人编码;
		/// <summary>
		/// 发送人编码
		/// </summary>
		[Column(Name = "发送人编码", DbType = "char(5)", Storage = "_发送人编码", UpdateCheck = UpdateCheck.Never)]
		public string 发送人编码
		{
			get { return _发送人编码; }
			set { _发送人编码 = value; }
		}
		private DateTime _创建时间;
		/// <summary>
		/// 创建时间
		/// </summary>
		[Column(Name = "创建时间", DbType = "datetime", Storage = "_创建时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime 创建时间
		{
			get { return _创建时间; }
			set { _创建时间 = value; }
		}
		private string _内容;
		/// <summary>
		/// 内容
		/// </summary>
		[Column(Name = "内容", DbType = "nvarchar(5000)", Storage = "_内容", UpdateCheck = UpdateCheck.Never)]
		public string 内容
		{
			get { return _内容; }
			set { _内容 = value; }
		}
		private string _备用1;
		/// <summary>
		/// 备用1
		/// </summary>
		[Column(Name = "备用1", DbType = "nvarchar(200)", Storage = "_备用1", UpdateCheck = UpdateCheck.Never)]
		public string 备用1
		{
			get { return _备用1; }
			set { _备用1 = value; }
		}
		private string _备用2;
		/// <summary>
		/// 备用2
		/// </summary>
		[Column(Name = "备用2", DbType = "nvarchar(200)", Storage = "_备用2", UpdateCheck = UpdateCheck.Never)]
		public string 备用2
		{
			get { return _备用2; }
			set { _备用2 = value; }
		}
		private string _备用3;
		/// <summary>
		/// 备用3
		/// </summary>
		[Column(Name = "备用3", DbType = "nvarchar(200)", Storage = "_备用3", UpdateCheck = UpdateCheck.Never)]
		public string 备用3
		{
			get { return _备用3; }
			set { _备用3 = value; }
		}
	}
}