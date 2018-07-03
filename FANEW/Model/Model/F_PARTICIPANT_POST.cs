using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_PARTICIPANT_POST")]
	public class F_PARTICIPANT_POST
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
		private int _PostID;
		/// <summary>
		/// PostID
		/// </summary>
		[Column(Name = "PostID", DbType = "int", Storage = "_PostID", UpdateCheck = UpdateCheck.Never)]
		public int PostID
		{
			get { return _PostID; }
			set { _PostID = value; }
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