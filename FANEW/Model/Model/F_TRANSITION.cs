using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_TRANSITION")]
	public class F_TRANSITION
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
		private string _ConditionJoin;
		/// <summary>
		/// ConditionJoin
		/// </summary>
		[Column(Name = "ConditionJoin", DbType = "nvarchar(100)", Storage = "_ConditionJoin", UpdateCheck = UpdateCheck.Never)]
		public string ConditionJoin
		{
			get { return _ConditionJoin; }
			set { _ConditionJoin = value; }
		}
		private int _StartActivtyID;
		/// <summary>
		/// StartActivtyID
		/// </summary>
		[Column(Name = "StartActivtyID", DbType = "int", Storage = "_StartActivtyID", UpdateCheck = UpdateCheck.Never)]
		public int StartActivtyID
		{
			get { return _StartActivtyID; }
			set { _StartActivtyID = value; }
		}
		private int _EndActivityID;
		/// <summary>
		/// EndActivityID
		/// </summary>
		[Column(Name = "EndActivityID", DbType = "int", Storage = "_EndActivityID", UpdateCheck = UpdateCheck.Never)]
		public int EndActivityID
		{
			get { return _EndActivityID; }
			set { _EndActivityID = value; }
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
	}
}