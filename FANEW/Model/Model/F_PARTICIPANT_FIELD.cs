using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_PARTICIPANT_FIELD")]
	public class F_PARTICIPANT_FIELD
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
		private int _ParticipantID;
		/// <summary>
		/// ParticipantID
		/// </summary>
		[Column(Name = "ParticipantID", DbType = "int", Storage = "_ParticipantID", UpdateCheck = UpdateCheck.Never)]
		public int ParticipantID
		{
			get { return _ParticipantID; }
			set { _ParticipantID = value; }
		}
	}
}