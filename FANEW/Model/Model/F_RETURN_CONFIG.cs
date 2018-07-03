using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_RETURN_CONFIG")]
	public class F_RETURN_CONFIG
	{
		private int _FromActivityID;
		/// <summary>
		/// FromActivityID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "FromActivityID", DbType = "int", Storage = "_FromActivityID")]
		public int FromActivityID
		{
			get { return _FromActivityID; }
			set { _FromActivityID = value; }
		}
		private int _ToActivityID;
		/// <summary>
		/// ToActivityID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "ToActivityID", DbType = "int", Storage = "_ToActivityID")]
		public int ToActivityID
		{
			get { return _ToActivityID; }
			set { _ToActivityID = value; }
		}
		private int _FlowID;
		/// <summary>
		/// FlowID
		/// </summary>
		[Column(Name = "FlowID", DbType = "int", Storage = "_FlowID", UpdateCheck = UpdateCheck.Never)]
		public int FlowID
		{
			get { return _FlowID; }
			set { _FlowID = value; }
		}
	}
}