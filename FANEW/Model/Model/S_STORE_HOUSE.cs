using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "S_STORE_HOUSE")]
	public class S_STORE_HOUSE
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
		private int _OrgID;
		/// <summary>
		/// OrgID
		/// </summary>
		[Column(Name = "OrgID", DbType = "int", Storage = "_OrgID", UpdateCheck = UpdateCheck.Never)]
		public int OrgID
		{
			get { return _OrgID; }
			set { _OrgID = value; }
		}
		private int? _ManagerID;
		/// <summary>
		/// ManagerID
		/// </summary>
		[Column(Name = "ManagerID", DbType = "int", Storage = "_ManagerID", UpdateCheck = UpdateCheck.Never)]
		public int? ManagerID
		{
			get { return _ManagerID; }
			set { _ManagerID = value; }
		}
		private int? _Sort;
		/// <summary>
		/// Sort
		/// </summary>
		[Column(Name = "Sort", DbType = "int", Storage = "_Sort", UpdateCheck = UpdateCheck.Never)]
		public int? Sort
		{
			get { return _Sort; }
			set { _Sort = value; }
		}
		private int? _Type;
		/// <summary>
		/// Type
		/// </summary>
		[Column(Name = "Type", DbType = "int", Storage = "_Type", UpdateCheck = UpdateCheck.Never)]
		public int? Type
		{
			get { return _Type; }
			set { _Type = value; }
		}
	}
}