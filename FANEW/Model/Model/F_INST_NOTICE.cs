using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_INST_NOTICE")]
	public class F_INST_NOTICE
	{
		private int _FlowID;
		/// <summary>
		/// FlowID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "FlowID", DbType = "int", Storage = "_FlowID")]
		public int FlowID
		{
			get { return _FlowID; }
			set { _FlowID = value; }
		}
		private int _FlowNo;
		/// <summary>
		/// FlowNo
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "FlowNo", DbType = "int", Storage = "_FlowNo")]
		public int FlowNo
		{
			get { return _FlowNo; }
			set { _FlowNo = value; }
		}
		private int _WorkerID;
		/// <summary>
		/// WorkerID
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "WorkerID", DbType = "int", Storage = "_WorkerID")]
		public int WorkerID
		{
			get { return _WorkerID; }
			set { _WorkerID = value; }
		}
        private bool _ReadFlag;
        /// <summary>
        /// ReadFlag
        /// </summary>
        [Column(Name = "ReadFlag", DbType = "bit NOT NULL", Storage = "_ReadFlag", UpdateCheck = UpdateCheck.Never)]
        public bool ReadFlag
        {
            get { return _ReadFlag; }
            set { _ReadFlag = value; }
        }
	}
}