using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_PARTICIPANT")]
	public class F_PARTICIPANT
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
		private int _ActivityID;
		/// <summary>
		/// ActivityID
		/// </summary>
		[Column(Name = "ActivityID", DbType = "int", Storage = "_ActivityID", UpdateCheck = UpdateCheck.Never)]
		public int ActivityID
		{
			get { return _ActivityID; }
			set { _ActivityID = value; }
		}
	}
}