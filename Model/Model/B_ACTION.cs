using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "B_ACTION")]
	public class B_ACTION
	{
		private int _ID;
		/// <summary>
		/// ID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "ID", DbType = "int", Storage = "_ID")]
		public int ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		private string _Url;
		/// <summary>
		/// Url
		/// </summary>
		[Column(Name = "Url", DbType = "varchar(200)", Storage = "_Url", UpdateCheck = UpdateCheck.Never)]
		public string Url
		{
			get { return _Url; }
			set { _Url = value; }
		}
		private string _Remark;
		/// <summary>
		/// Remark
		/// </summary>
		[Column(Name = "Remark", DbType = "varchar(50)", Storage = "_Remark", UpdateCheck = UpdateCheck.Never)]
		public string Remark
		{
			get { return _Remark; }
			set { _Remark = value; }
		}
		private int? _ParentID;
		/// <summary>
		/// ParentID
		/// </summary>
		[Column(Name = "ParentID", DbType = "int", Storage = "_ParentID", UpdateCheck = UpdateCheck.Never)]
		public int? ParentID
		{
			get { return _ParentID; }
			set { _ParentID = value; }
		}
		private string _Icon;
		/// <summary>
		/// Icon
		/// </summary>
		[Column(Name = "Icon", DbType = "varchar(50)", Storage = "_Icon", UpdateCheck = UpdateCheck.Never)]
		public string Icon
		{
			get { return _Icon; }
			set { _Icon = value; }
		}
		private string _IsActive;
		/// <summary>
		/// IsActive
		/// </summary>
		[Column(Name = "IsActive", DbType = "char(1)", Storage = "_IsActive", UpdateCheck = UpdateCheck.Never)]
		public string IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}
		private int? _OrderID;
		/// <summary>
		/// OrderID
		/// </summary>
		[Column(Name = "OrderID", DbType = "int", Storage = "_OrderID", UpdateCheck = UpdateCheck.Never)]
		public int? OrderID
		{
			get { return _OrderID; }
			set { _OrderID = value; }
		}
	}
}