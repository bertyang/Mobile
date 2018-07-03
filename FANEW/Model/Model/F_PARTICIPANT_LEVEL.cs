using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_PARTICIPANT_LEVEL")]
	public class F_PARTICIPANT_LEVEL
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
		private int _Level;
		/// <summary>
		/// Level
		/// </summary>
		[Column(Name = "Level", DbType = "int", Storage = "_Level", UpdateCheck = UpdateCheck.Never)]
		public int Level
		{
			get { return _Level; }
			set { _Level = value; }
		}
		private string _IsAbsolute;
		/// <summary>
		/// IsAbsolute
		/// </summary>
		[Column(Name = "IsAbsolute", DbType = "char(1)", Storage = "_IsAbsolute", UpdateCheck = UpdateCheck.Never)]
		public string IsAbsolute
		{
			get { return _IsAbsolute; }
			set { _IsAbsolute = value; }
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