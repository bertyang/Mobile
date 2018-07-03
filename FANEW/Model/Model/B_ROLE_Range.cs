using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "B_ROLE_Range")]
	public class B_ROLE_Range
	{
		private int _RoleID;
		/// <summary>
		/// RoleID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "RoleID", DbType = "int", Storage = "_RoleID")]
		public int RoleID
		{
			get { return _RoleID; }
			set { _RoleID = value; }
		}
		private int _ActionID;
		/// <summary>
		/// ActionID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "ActionID", DbType = "int", Storage = "_ActionID")]
		public int ActionID
		{
			get { return _ActionID; }
			set { _ActionID = value; }
		}
		private string _Range;
		/// <summary>
		/// Range
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "Range", DbType = "varchar(20)", Storage = "_Range")]
		public string Range
		{
			get { return _Range; }
			set { _Range = value; }
		}
	}
}