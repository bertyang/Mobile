using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_INST_ACTIVITY")]
	public class F_INST_ACTIVITY
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
		private int _FlowInstID;
		/// <summary>
		/// FlowInstID
		/// </summary>
		[Column(Name = "FlowInstID", DbType = "int", Storage = "_FlowInstID", UpdateCheck = UpdateCheck.Never)]
		public int FlowInstID
		{
			get { return _FlowInstID; }
			set { _FlowInstID = value; }
		}
		private int _ActivityID;
		/// <summary>
		/// ActivityID
		/// </summary>
		[Column(Name = "ActivityID", DbType = "int", Storage = "_ActivityID", UpdateCheck = UpdateCheck.Never)]
		public int ActivityID
		{
			get { return _ActivityID; }
			set { _ActivityID = value; }
		}
		private string _State;
		/// <summary>
		/// State
		/// </summary>
		[Column(Name = "State", DbType = "char(1)", Storage = "_State", UpdateCheck = UpdateCheck.Never)]
		public string State
		{
			get { return _State; }
			set { _State = value; }
		}
		private DateTime _BeginDate;
		/// <summary>
		/// BeginDate
		/// </summary>
		[Column(Name = "BeginDate", DbType = "datetime", Storage = "_BeginDate", UpdateCheck = UpdateCheck.Never)]
		public DateTime BeginDate
		{
			get { return _BeginDate; }
			set { _BeginDate = value; }
		}
		private DateTime? _EndDate;
		/// <summary>
		/// EndDate
		/// </summary>
		[Column(Name = "EndDate", DbType = "datetime", Storage = "_EndDate", UpdateCheck = UpdateCheck.Never)]
		public DateTime? EndDate
		{
			get { return _EndDate; }
			set { _EndDate = value; }
		}
	}
}