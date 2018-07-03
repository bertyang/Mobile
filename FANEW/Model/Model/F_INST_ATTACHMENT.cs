using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_INST_ATTACHMENT")]
	public class F_INST_ATTACHMENT
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
		private string _OriginalName;
		/// <summary>
		/// OriginalName
		/// </summary>
		[Column(Name = "OriginalName", DbType = "nvarchar(200)", Storage = "_OriginalName", UpdateCheck = UpdateCheck.Never)]
		public string OriginalName
		{
			get { return _OriginalName; }
			set { _OriginalName = value; }
		}
		private string _CodingName;
		/// <summary>
		/// CodingName
		/// </summary>
		[Column(Name = "CodingName", DbType = "nvarchar(200)", Storage = "_CodingName", UpdateCheck = UpdateCheck.Never)]
		public string CodingName
		{
			get { return _CodingName; }
			set { _CodingName = value; }
		}
		private double _Size;
		/// <summary>
		/// Size
		/// </summary>
		[Column(Name = "Size", DbType = "float", Storage = "_Size", UpdateCheck = UpdateCheck.Never)]
		public double Size
		{
			get { return _Size; }
			set { _Size = value; }
		}
	}
}