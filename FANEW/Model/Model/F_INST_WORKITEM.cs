using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_INST_WORKITEM")]
	public class F_INST_WORKITEM
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
		private int _Assignerid;
		/// <summary>
		/// Assignerid
		/// </summary>
		[Column(Name = "Assignerid", DbType = "int", Storage = "_Assignerid", UpdateCheck = UpdateCheck.Never)]
		public int Assignerid
		{
			get { return _Assignerid; }
			set { _Assignerid = value; }
		}
		private int _Assigneeid;
		/// <summary>
		/// Assigneeid
		/// </summary>
		[Column(Name = "Assigneeid", DbType = "int", Storage = "_Assigneeid", UpdateCheck = UpdateCheck.Never)]
		public int Assigneeid
		{
			get { return _Assigneeid; }
			set { _Assigneeid = value; }
		}
		private int? _Actorid;
		/// <summary>
		/// Actorid
		/// </summary>
		[Column(Name = "Actorid", DbType = "int", Storage = "_Actorid", UpdateCheck = UpdateCheck.Never)]
		public int? Actorid
		{
			get { return _Actorid; }
			set { _Actorid = value; }
		}
		private string _Role;
		/// <summary>
		/// Role
		/// </summary>
		[Column(Name = "Role", DbType = "nvarchar(100)", Storage = "_Role", UpdateCheck = UpdateCheck.Never)]
		public string Role
		{
			get { return _Role; }
			set { _Role = value; }
		}
		private string _AppType;
		/// <summary>
		/// AppType
		/// </summary>
		[Column(Name = "AppType", DbType = "char(1)", Storage = "_AppType", UpdateCheck = UpdateCheck.Never)]
		public string AppType
		{
			get { return _AppType; }
			set { _AppType = value; }
		}
		private string _AssignType;
		/// <summary>
		/// AssignType
		/// </summary>
		[Column(Name = "AssignType", DbType = "char(1)", Storage = "_AssignType", UpdateCheck = UpdateCheck.Never)]
		public string AssignType
		{
			get { return _AssignType; }
			set { _AssignType = value; }
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
		private string _AppValue;
		/// <summary>
		/// AppValue
		/// </summary>
		[Column(Name = "AppValue", DbType = "char(1)", Storage = "_AppValue", UpdateCheck = UpdateCheck.Never)]
		public string AppValue
		{
			get { return _AppValue; }
			set { _AppValue = value; }
		}
		private string _AppRemark;
		/// <summary>
		/// AppRemark
		/// </summary>
		[Column(Name = "AppRemark", DbType = "nvarchar(510)", Storage = "_AppRemark", UpdateCheck = UpdateCheck.Never)]
		public string AppRemark
		{
			get { return _AppRemark; }
			set { _AppRemark = value; }
		}
		private string _Esignature;
		/// <summary>
		/// Esignature
		/// </summary>
		[Column(Name = "Esignature", DbType = "nvarchar(200)", Storage = "_Esignature", UpdateCheck = UpdateCheck.Never)]
		public string Esignature
		{
			get { return _Esignature; }
			set { _Esignature = value; }
		}
		private string _Cert;
		/// <summary>
		/// Cert
		/// </summary>
		[Column(Name = "Cert", DbType = "nvarchar(100)", Storage = "_Cert", UpdateCheck = UpdateCheck.Never)]
		public string Cert
		{
			get { return _Cert; }
			set { _Cert = value; }
		}
	}
}