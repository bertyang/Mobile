using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_INST_TRANSATION")]
	public class F_INST_TRANSATION
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
		private int _ActivityInstID;
		/// <summary>
		/// ActivityInstID
		/// </summary>
		[Column(Name = "ActivityInstID", DbType = "int", Storage = "_ActivityInstID", UpdateCheck = UpdateCheck.Never)]
		public int ActivityInstID
		{
			get { return _ActivityInstID; }
			set { _ActivityInstID = value; }
		}
		private int _TransationID;
		/// <summary>
		/// TransationID
		/// </summary>
		[Column(Name = "TransationID", DbType = "int", Storage = "_TransationID", UpdateCheck = UpdateCheck.Never)]
		public int TransationID
		{
			get { return _TransationID; }
			set { _TransationID = value; }
		}
		private string _TransationValue;
		/// <summary>
		/// TransationValue
		/// </summary>
		[Column(Name = "TransationValue", DbType = "nvarchar(10)", Storage = "_TransationValue", UpdateCheck = UpdateCheck.Never)]
		public string TransationValue
		{
			get { return _TransationValue; }
			set { _TransationValue = value; }
		}
		private DateTime _PassTime;
		/// <summary>
		/// PassTime
		/// </summary>
		[Column(Name = "PassTime", DbType = "datetime", Storage = "_PassTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime PassTime
		{
			get { return _PassTime; }
			set { _PassTime = value; }
		}
	}
}