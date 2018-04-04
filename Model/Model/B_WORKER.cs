using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
    [Table(Name = "B_WORKER")]
    public class B_WORKER
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
        private string _LoginName;
        /// <summary>
        /// LoginName
        /// </summary>
        [Column(Name = "LoginName", DbType = "nvarchar(50) NOT NULL", Storage = "_LoginName", UpdateCheck = UpdateCheck.Never)]
        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
        private string _Name;
        /// <summary>
        /// Name
        /// </summary>
        [Column(Name = "Name", DbType = "nvarchar(50) NOT NULL", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Sex;
        /// <summary>
        /// Sex
        /// </summary>
        [Column(Name = "Sex", DbType = "nvarchar(20) NULL", Storage = "_Sex", UpdateCheck = UpdateCheck.Never)]
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        private DateTime? _EntryDate;
        /// <summary>
        /// EntryDate
        /// </summary>
        [Column(Name = "EntryDate", DbType = "datetime NULL", Storage = "_EntryDate", UpdateCheck = UpdateCheck.Never)]
        public DateTime? EntryDate
        {
            get { return _EntryDate; }
            set { _EntryDate = value; }
        }
        private string _PassWord;
        /// <summary>
        /// PassWord
        /// </summary>
        [Column(Name = "PassWord", DbType = "nvarchar(100) NOT NULL", Storage = "_PassWord", UpdateCheck = UpdateCheck.Never)]
        public string PassWord
        {
            get { return _PassWord; }
            set { _PassWord = value; }
        }
        private string _TitleTechnicalID;
        /// <summary>
        /// TitleTechnical
        /// </summary>
        [Column(Name = "TitleTechnicalID", DbType = "nvarchar(100) NULL", Storage = "_TitleTechnicalID", UpdateCheck = UpdateCheck.Never)]
        public string TitleTechnicalID
        {
            get { return _TitleTechnicalID; }
            set { _TitleTechnicalID = value; }
        }
        private string _IsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        [Column(Name = "IsActive", DbType = "nvarchar(1) NOT NULL", Storage = "_IsActive", UpdateCheck = UpdateCheck.Never)]
        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private string _IsQuota;
        /// <summary>
        /// IsQuota
        /// </summary>
        [Column(Name = "IsQuota", DbType = "nvarchar(100) NULL", Storage = "_IsQuota", UpdateCheck = UpdateCheck.Never)]
        public string IsQuota
        {
            get { return _IsQuota; }
            set { _IsQuota = value; }
        }
        private string _Mobile;
        /// <summary>
        /// Mobile
        /// </summary>
        [Column(Name = "Mobile", DbType = "nvarchar(30) NULL", Storage = "_Mobile", UpdateCheck = UpdateCheck.Never)]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        private string _IsAllowInternetAccess;
        /// <summary>
        /// IsAllowInternetAccess
        /// </summary>
        [Column(Name = "IsAllowInternetAccess", DbType = "char(1) NULL", Storage = "_IsAllowInternetAccess", UpdateCheck = UpdateCheck.Never)]
        public string IsAllowInternetAccess
        {
            get { return _IsAllowInternetAccess; }
            set { _IsAllowInternetAccess = value; }
        }
        private string _Tel;
        /// <summary>
        /// Tel
        /// </summary>
        [Column(Name = "Tel", DbType = "nvarchar(30) NULL", Storage = "_Tel", UpdateCheck = UpdateCheck.Never)]
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        private string _JobLevel;
        /// <summary>
        /// JobLevel
        /// </summary>
        [Column(Name = "JobLevel", DbType = "nvarchar(100) NULL", Storage = "_JobLevel", UpdateCheck = UpdateCheck.Never)]
        public string JobLevel
        {
            get { return _JobLevel; }
            set { _JobLevel = value; }
        }
    }
}