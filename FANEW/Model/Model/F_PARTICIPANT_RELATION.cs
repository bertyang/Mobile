using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_PARTICIPANT_RELATION")]
	public class F_PARTICIPANT_RELATION
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
		private string _Relation;
		/// <summary>
		/// Relation
		/// </summary>
		[Column(Name = "Relation", DbType = "nvarchar(60)", Storage = "_Relation", UpdateCheck = UpdateCheck.Never)]
		public string Relation
		{
			get { return _Relation; }
			set { _Relation = value; }
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