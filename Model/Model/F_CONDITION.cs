using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_CONDITION")]
	public class F_CONDITION
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
		private string _Sql;
		/// <summary>
		/// Sql
		/// </summary>
		[Column(Name = "Sql", DbType = "nvarchar(510)", Storage = "_Sql", UpdateCheck = UpdateCheck.Never)]
		public string Sql
		{
			get { return _Sql; }
			set { _Sql = value; }
		}
		private string _Operator;
		/// <summary>
		/// Operator
		/// </summary>
		[Column(Name = "Operator", DbType = "nvarchar(100)", Storage = "_Operator", UpdateCheck = UpdateCheck.Never)]
		public string Operator
		{
			get { return _Operator; }
			set { _Operator = value; }
		}
		private string _Value;
		/// <summary>
		/// Value
		/// </summary>
		[Column(Name = "Value", DbType = "nvarchar(100)", Storage = "_Value", UpdateCheck = UpdateCheck.Never)]
		public string Value
		{
			get { return _Value; }
			set { _Value = value; }
		}
		private string _Type;
		/// <summary>
		/// Type
		/// </summary>
		[Column(Name = "Type", DbType = "nvarchar(100)", Storage = "_Type", UpdateCheck = UpdateCheck.Never)]
		public string Type
		{
			get { return _Type; }
			set { _Type = value; }
		}
		private DateTime _CreateTime;
		/// <summary>
		/// CreateTime
		/// </summary>
		[Column(Name = "CreateTime", DbType = "datetime", Storage = "_CreateTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime CreateTime
		{
			get { return _CreateTime; }
			set { _CreateTime = value; }
		}
		private int _TransationID;
		/// <summary>
		/// TransationID
		/// </summary>
		[Column(Name = "TransationID", DbType = "int", Storage = "_TransationID", UpdateCheck = UpdateCheck.Never)]
		public int TransationID
		{
			get { return _TransationID; }
			set { _TransationID = value; }
		}
	}
}