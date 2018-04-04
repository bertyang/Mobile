using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "E_Mail_Worker")]
	public class E_Mail_Worker
	{
		private int _MailId;
		/// <summary>
		/// MailId
		/// </summary>
		[Column(Name = "MailId", DbType = "int", Storage = "_MailId", UpdateCheck = UpdateCheck.Never)]
		public int MailId
		{
			get { return _MailId; }
			set { _MailId = value; }
		}
		private int _WorkerId;
		/// <summary>
		/// WorkerId
		/// </summary>
		[Column(Name = "WorkerId", DbType = "int", Storage = "_WorkerId", UpdateCheck = UpdateCheck.Never)]
		public int WorkerId
		{
			get { return _WorkerId; }
			set { _WorkerId = value; }
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
		private string _ReadFlag;
		/// <summary>
		/// ReadFlag
		/// </summary>
		[Column(Name = "ReadFlag", DbType = "char(1)", Storage = "_ReadFlag", UpdateCheck = UpdateCheck.Never)]
		public string ReadFlag
		{
			get { return _ReadFlag; }
			set { _ReadFlag = value; }
		}
		private int? _FolderID;
		/// <summary>
		/// FolderID
		/// </summary>
		[Column(Name = "FolderID", DbType = "int", Storage = "_FolderID", UpdateCheck = UpdateCheck.Never)]
		public int? FolderID
		{
			get { return _FolderID; }
			set { _FolderID = value; }
		}
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
	}
}