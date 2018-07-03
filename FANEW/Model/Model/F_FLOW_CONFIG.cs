using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "F_FLOW_CONFIG")]
	public class F_FLOW_CONFIG
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
		private string _ItemName;
		/// <summary>
		/// ItemName
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "ItemName", DbType = "nvarchar(100)", Storage = "_ItemName")]
		public string ItemName
		{
			get { return _ItemName; }
			set { _ItemName = value; }
		}
		private string _ItemValue;
		/// <summary>
		/// ItemValue
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "ItemValue", DbType = "nvarchar(510)", Storage = "_ItemValue")]
		public string ItemValue
		{
			get { return _ItemValue; }
			set { _ItemValue = value; }
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
	}
}