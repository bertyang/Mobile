using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_PARTICIPANT_ORG")]
	public class F_PARTICIPANT_ORG
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
		private int _WorkerID;
		/// <summary>
		/// WorkerID
		/// </summary>
		[Column(Name = "WorkerID", DbType = "int", Storage = "_WorkerID", UpdateCheck = UpdateCheck.Never)]
		public int WorkerID
		{
			get { return _WorkerID; }
			set { _WorkerID = value; }
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