using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TParameterAcceptInfo")]
	public class TParameterAcceptInfo
	{
		private string _控件名称;
		/// <summary>
		/// 控件名称
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "控件名称", DbType = "varchar(100)", Storage = "_控件名称")]
		public string 控件名称
		{
			get { return _控件名称; }
			set { _控件名称 = value; }
		}
		private int _LEFT;
		/// <summary>
		/// LEFT
		/// </summary>
		[Column(Name = "LEFT", DbType = "int", Storage = "_LEFT", UpdateCheck = UpdateCheck.Never)]
		public int LEFT
		{
			get { return _LEFT; }
			set { _LEFT = value; }
		}
		private int _TOP;
		/// <summary>
		/// TOP
		/// </summary>
		[Column(Name = "TOP", DbType = "int", Storage = "_TOP", UpdateCheck = UpdateCheck.Never)]
		public int TOP
		{
			get { return _TOP; }
			set { _TOP = value; }
		}
		private int _WIDTH;
		/// <summary>
		/// WIDTH
		/// </summary>
		[Column(Name = "WIDTH", DbType = "int", Storage = "_WIDTH", UpdateCheck = UpdateCheck.Never)]
		public int WIDTH
		{
			get { return _WIDTH; }
			set { _WIDTH = value; }
		}
		private int _TabIndex;
		/// <summary>
		/// TabIndex
		/// </summary>
		[Column(Name = "TabIndex", DbType = "int", Storage = "_TabIndex", UpdateCheck = UpdateCheck.Never)]
		public int TabIndex
		{
			get { return _TabIndex; }
			set { _TabIndex = value; }
		}
		private string _默认值;
		/// <summary>
		/// 默认值
		/// </summary>
		[Column(Name = "默认值", DbType = "varchar(100)", Storage = "_默认值", UpdateCheck = UpdateCheck.Never)]
		public string 默认值
		{
			get { return _默认值; }
			set { _默认值 = value; }
		}
		private int _颜色;
		/// <summary>
		/// 颜色
		/// </summary>
		[Column(Name = "颜色", DbType = "int", Storage = "_颜色", UpdateCheck = UpdateCheck.Never)]
		public int 颜色
		{
			get { return _颜色; }
			set { _颜色 = value; }
		}
		private bool _是否必填;
		/// <summary>
		/// 是否必填
		/// </summary>
		[Column(Name = "是否必填", DbType = "bit", Storage = "_是否必填", UpdateCheck = UpdateCheck.Never)]
		public bool 是否必填
		{
			get { return _是否必填; }
			set { _是否必填 = value; }
		}
		private bool _是否可见;
		/// <summary>
		/// 是否可见
		/// </summary>
		[Column(Name = "是否可见", DbType = "bit", Storage = "_是否可见", UpdateCheck = UpdateCheck.Never)]
		public bool 是否可见
		{
			get { return _是否可见; }
			set { _是否可见 = value; }
		}
	}
}