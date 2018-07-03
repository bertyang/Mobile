using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
    public enum OrgType
    {
        Root = 0,
        Center = 1,
        Branch = 2,
        Station = 3,
    }

    [Table(Name = "B_ORGANIZATION")]
    public class B_ORGANIZATION
    {
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
        private int _ManagerID;
        /// <summary>
        /// ManagerID
        /// </summary>
        [Column(Name = "ManagerID", DbType = "int NOT NULL", Storage = "_ManagerID", UpdateCheck = UpdateCheck.Never)]
        public int ManagerID
        {
            get { return _ManagerID; }
            set { _ManagerID = value; }
        }
        private string _Name;
        /// <summary>
        /// Name
        /// </summary>
        [Column(Name = "Name", DbType = "varchar(40) NOT NULL", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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
        private string _编码;
        /// <summary>
        /// 编码
        /// </summary>
        [Column(Name = "编码", DbType = "varchar(3) NOT NULL", Storage = "_编码", UpdateCheck = UpdateCheck.Never)]
        public string 编码
        {
            get { return _编码; }
            set { _编码 = value; }
        }
        private int _Sort;
        /// <summary>
        /// Sort
        /// </summary>
        [Column(Name = "Sort", DbType = "int NOT NULL", Storage = "_Sort", UpdateCheck = UpdateCheck.Never)]
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
    }
}