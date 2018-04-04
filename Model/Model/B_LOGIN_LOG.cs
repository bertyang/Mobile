using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "B_LOGIN_LOG")]
	public class B_LOGIN_LOG
	{
		private string _Name;
		/// <summary>
		/// Name
		/// </summary>
		[Column(Name = "Name", DbType = "nvarchar(100)", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private string _IP;
		/// <summary>
		/// IP
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "IP", DbType = "nvarchar(100)", Storage = "_IP")]
		public string IP
		{
			get { return _IP; }
			set { _IP = value; }
		}
		private DateTime _LoginTime;
		/// <summary>
		/// LoginTime
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "LoginTime", DbType = "datetime", Storage = "_LoginTime")]
		public DateTime LoginTime
		{
			get { return _LoginTime; }
			set { _LoginTime = value; }
		}
	}
}