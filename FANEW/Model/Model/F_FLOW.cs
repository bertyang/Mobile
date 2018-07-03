using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_FLOW")]
	public class F_FLOW
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
		private string _Name;
		/// <summary>
		/// Name
		/// </summary>
		[Column(Name = "Name", DbType = "nvarchar(200)", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private DateTime _CreateDate;
		/// <summary>
		/// CreateDate
		/// </summary>
		[Column(Name = "CreateDate", DbType = "datetime", Storage = "_CreateDate", UpdateCheck = UpdateCheck.Never)]
		public DateTime CreateDate
		{
			get { return _CreateDate; }
			set { _CreateDate = value; }
		}
		private DateTime _ModifyDate;
		/// <summary>
		/// ModifyDate
		/// </summary>
		[Column(Name = "ModifyDate", DbType = "datetime", Storage = "_ModifyDate", UpdateCheck = UpdateCheck.Never)]
		public DateTime ModifyDate
		{
			get { return _ModifyDate; }
			set { _ModifyDate = value; }
		}
		private int _CatalogID;
		/// <summary>
		/// CatalogID
		/// </summary>
		[Column(Name = "CatalogID", DbType = "int", Storage = "_CatalogID", UpdateCheck = UpdateCheck.Never)]
		public int CatalogID
		{
			get { return _CatalogID; }
			set { _CatalogID = value; }
		}
		private string _Description;
		/// <summary>
		/// Description
		/// </summary>
		[Column(Name = "Description", DbType = "nvarchar(510)", Storage = "_Description", UpdateCheck = UpdateCheck.Never)]
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
		private string _Url;
		/// <summary>
		/// Url
		/// </summary>
		[Column(Name = "Url", DbType = "nvarchar(510)", Storage = "_Url", UpdateCheck = UpdateCheck.Never)]
		public string Url
		{
			get { return _Url; }
			set { _Url = value; }
		}
		private string _IsInner;
		/// <summary>
		/// IsInner
		/// </summary>
		[Column(Name = "IsInner", DbType = "char(1)", Storage = "_IsInner", UpdateCheck = UpdateCheck.Never)]
		public string IsInner
		{
			get { return _IsInner; }
			set { _IsInner = value; }
		}
		private int _LayoutType;
		/// <summary>
		/// LayoutType
		/// </summary>
		[Column(Name = "LayoutType", DbType = "int", Storage = "_LayoutType", UpdateCheck = UpdateCheck.Never)]
		public int LayoutType
		{
			get { return _LayoutType; }
			set { _LayoutType = value; }
		}
		private string _Active;
		/// <summary>
		/// Active
		/// </summary>
		[Column(Name = "Active", DbType = "char(1)", Storage = "_Active", UpdateCheck = UpdateCheck.Never)]
		public string Active
		{
			get { return _Active; }
			set { _Active = value; }
		}
	}
}