using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_INST_FLOW")]
	public class F_INST_FLOW
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
		private int _FlowNo;
		/// <summary>
		/// FlowNo
		/// </summary>
		[Column(Name = "FlowNo", DbType = "int", Storage = "_FlowNo", UpdateCheck = UpdateCheck.Never)]
		public int FlowNo
		{
			get { return _FlowNo; }
			set { _FlowNo = value; }
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
		private int _ApplyerID;
		/// <summary>
		/// ApplyerID
		/// </summary>
		[Column(Name = "ApplyerID", DbType = "int", Storage = "_ApplyerID", UpdateCheck = UpdateCheck.Never)]
		public int ApplyerID
		{
			get { return _ApplyerID; }
			set { _ApplyerID = value; }
		}
		private int _FillerID;
		/// <summary>
		/// FillerID
		/// </summary>
		[Column(Name = "FillerID", DbType = "int", Storage = "_FillerID", UpdateCheck = UpdateCheck.Never)]
		public int FillerID
		{
			get { return _FillerID; }
			set { _FillerID = value; }
		}
		private string _Digest;
		/// <summary>
		/// Digest
		/// </summary>
		[Column(Name = "Digest", DbType = "nvarchar(510)", Storage = "_Digest", UpdateCheck = UpdateCheck.Never)]
		public string Digest
		{
			get { return _Digest; }
			set { _Digest = value; }
		}
		private string _PreviousApprover;
		/// <summary>
		/// PreviousApprover
		/// </summary>
		[Column(Name = "PreviousApprover", DbType = "nvarchar(100)", Storage = "_PreviousApprover", UpdateCheck = UpdateCheck.Never)]
		public string PreviousApprover
		{
			get { return _PreviousApprover; }
			set { _PreviousApprover = value; }
		}
	}
}