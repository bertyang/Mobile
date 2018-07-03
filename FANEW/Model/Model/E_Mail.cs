using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "E_Mail")]
	public class E_Mail
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
		private string _Title;
		/// <summary>
		/// Title
		/// </summary>
		[Column(Name = "Title", DbType = "nvarchar(510)", Storage = "_Title", UpdateCheck = UpdateCheck.Never)]
		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}
		private string _Body;
		/// <summary>
		/// Body
		/// </summary>
		[Column(Name = "Body", DbType = "text(16)", Storage = "_Body", UpdateCheck = UpdateCheck.Never)]
		public string Body
		{
			get { return _Body; }
			set { _Body = value; }
		}
		private string _From;
		/// <summary>
		/// From
		/// </summary>
		[Column(Name = "From", DbType = "nvarchar(100)", Storage = "_From", UpdateCheck = UpdateCheck.Never)]
		public string From
		{
			get { return _From; }
			set { _From = value; }
		}
		private string _To;
		/// <summary>
		/// To
		/// </summary>
		[Column(Name = "To", DbType = "text(16)", Storage = "_To", UpdateCheck = UpdateCheck.Never)]
		public string To
		{
			get { return _To; }
			set { _To = value; }
		}
		private string _CC;
		/// <summary>
		/// CC
		/// </summary>
		[Column(Name = "CC", DbType = "text(16)", Storage = "_CC", UpdateCheck = UpdateCheck.Never)]
		public string CC
		{
			get { return _CC; }
			set { _CC = value; }
		}
		private string _SC;
		/// <summary>
		/// SC
		/// </summary>
		[Column(Name = "SC", DbType = "text(16)", Storage = "_SC", UpdateCheck = UpdateCheck.Never)]
		public string SC
		{
			get { return _SC; }
			set { _SC = value; }
		}
		private DateTime _SendDate;
		/// <summary>
		/// SendDate
		/// </summary>
		[Column(Name = "SendDate", DbType = "datetime", Storage = "_SendDate", UpdateCheck = UpdateCheck.Never)]
		public DateTime SendDate
		{
			get { return _SendDate; }
			set { _SendDate = value; }
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
	}
}