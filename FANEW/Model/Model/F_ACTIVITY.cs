using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_ACTIVITY")]
	public class F_ACTIVITY
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
		private string _Name;
		/// <summary>
		/// Name
		/// </summary>
		[Column(Name = "Name", DbType = "nvarchar(200)", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
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
		private string _Type;
		/// <summary>
		/// Type
		/// </summary>
		[Column(Name = "Type", DbType = "nvarchar(20)", Storage = "_Type", UpdateCheck = UpdateCheck.Never)]
		public string Type
		{
			get { return _Type; }
			set { _Type = value; }
		}
		private string _JoinType;
		/// <summary>
		/// JoinType
		/// </summary>
		[Column(Name = "JoinType", DbType = "nvarchar(20)", Storage = "_JoinType", UpdateCheck = UpdateCheck.Never)]
		public string JoinType
		{
			get { return _JoinType; }
			set { _JoinType = value; }
		}
		private string _SplitType;
		/// <summary>
		/// SplitType
		/// </summary>
		[Column(Name = "SplitType", DbType = "nvarchar(20)", Storage = "_SplitType", UpdateCheck = UpdateCheck.Never)]
		public string SplitType
		{
			get { return _SplitType; }
			set { _SplitType = value; }
		}
		private string _AutoByPass;
		/// <summary>
		/// AutoByPass
		/// </summary>
		[Column(Name = "AutoByPass", DbType = "nvarchar(20)", Storage = "_AutoByPass", UpdateCheck = UpdateCheck.Never)]
		public string AutoByPass
		{
			get { return _AutoByPass; }
			set { _AutoByPass = value; }
		}
		private string _CompleteType;
		/// <summary>
		/// CompleteType
		/// </summary>
		[Column(Name = "CompleteType", DbType = "nvarchar(20)", Storage = "_CompleteType", UpdateCheck = UpdateCheck.Never)]
		public string CompleteType
		{
			get { return _CompleteType; }
			set { _CompleteType = value; }
		}
		private string _IsSignature;
		/// <summary>
		/// IsSignature
		/// </summary>
		[Column(Name = "IsSignature", DbType = "char(1)", Storage = "_IsSignature", UpdateCheck = UpdateCheck.Never)]
		public string IsSignature
		{
			get { return _IsSignature; }
			set { _IsSignature = value; }
		}
		private string _Description;
		/// <summary>
		/// Description
		/// </summary>
		[Column(Name = "Description", DbType = "nvarchar(510)", Storage = "_Description", UpdateCheck = UpdateCheck.Never)]
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
		private string _ReturnType;
		/// <summary>
		/// ReturnType
		/// </summary>
		[Column(Name = "ReturnType", DbType = "nvarchar(20)", Storage = "_ReturnType", UpdateCheck = UpdateCheck.Never)]
		public string ReturnType
		{
			get { return _ReturnType; }
			set { _ReturnType = value; }
		}
		private string _Url;
		/// <summary>
		/// Url
		/// </summary>
		[Column(Name = "Url", DbType = "nvarchar(300)", Storage = "_Url", UpdateCheck = UpdateCheck.Never)]
		public string Url
		{
			get { return _Url; }
			set { _Url = value; }
		}
		private string _IsSMS;
		/// <summary>
		/// IsSMS
		/// </summary>
		[Column(Name = "IsSMS", DbType = "char(1)", Storage = "_IsSMS", UpdateCheck = UpdateCheck.Never)]
		public string IsSMS
		{
			get { return _IsSMS; }
			set { _IsSMS = value; }
		}
	}
}