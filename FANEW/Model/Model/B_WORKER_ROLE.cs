using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
    [Table(Name = "B_WORKER_ROLE")]
    public class B_WORKER_ROLE
    {
        private string _EmpNo;
        /// <summary>
        /// EmpNo
        /// </summary>
        [Column(Name = "EmpNo", DbType = "varchar(10) NOT NULL", Storage = "_EmpNo", UpdateCheck = UpdateCheck.Never)]
        public string EmpNo
        {
            get { return _EmpNo; }
            set { _EmpNo = value; }
        }
        private int _WorkerID;
        /// <summary>
        /// WorkerID
        /// </summary>
        [Column(Name = "WorkerID", DbType = "int NOT NULL", Storage = "_WorkerID", UpdateCheck = UpdateCheck.Never)]
        public int WorkerID
        {
            get { return _WorkerID; }
            set { _WorkerID = value; }
        }
        private int _RoleID;
        /// <summary>
        /// RoleID
        /// </summary>
        [Column(Name = "RoleID", DbType = "int NOT NULL", Storage = "_RoleID", UpdateCheck = UpdateCheck.Never)]
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        private int _OrgID;
        /// <summary>
        /// OrgID
        /// </summary>
        [Column(Name = "OrgID", DbType = "int NOT NULL", Storage = "_OrgID", UpdateCheck = UpdateCheck.Never)]
        public int OrgID
        {
            get { return _OrgID; }
            set { _OrgID = value; }
        }
        private int _ID;
        /// <summary>
        /// ID
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "ID", DbType = "int", Storage = "_ID", UpdateCheck = UpdateCheck.Never)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _TPerson±àÂë;
        /// <summary>
        /// TPerson±àÂë
        /// </summary>
        [Column(Name = "TPerson±àÂë", DbType = "varchar(5) NOT NULL", Storage = "_TPerson±àÂë", UpdateCheck = UpdateCheck.Never)]
        public string TPerson±àÂë
        {
            get { return _TPerson±àÂë; }
            set { _TPerson±àÂë = value; }
        }
    }
}