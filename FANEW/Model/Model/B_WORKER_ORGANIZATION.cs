using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
    [Table(Name = "B_WORKER_ORGANIZATION")]
    public class B_WORKER_ORGANIZATION
    {
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
        private string _Remark;
        /// <summary>
        /// Remark
        /// </summary>
        [Column(Name = "Remark", DbType = "varchar(100) NULL", Storage = "_Remark", UpdateCheck = UpdateCheck.Never)]
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private int _ParentID;
        /// <summary>
        /// ParentID
        /// </summary>
        [Column(Name = "ParentID", DbType = "int NOT NULL", Storage = "_ParentID", UpdateCheck = UpdateCheck.Never)]
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        private int _PostID;
        /// <summary>
        /// PostID
        /// </summary>
        [Column(Name = "PostID", DbType = "int NOT NULL", Storage = "_PostID", UpdateCheck = UpdateCheck.Never)]
        public int PostID
        {
            get { return _PostID; }
            set { _PostID = value; }
        }
        private int _Type;
        /// <summary>
        /// Type
        /// </summary>
        [Column(Name = "Type", DbType = "int NOT NULL", Storage = "_Type", UpdateCheck = UpdateCheck.Never)]
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
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
    }
}