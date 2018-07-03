using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "G_DATA")]
	public class G_DATA
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
		private string _Type;
		/// <summary>
		/// Type
		/// </summary>
		[Column(Name = "Type", DbType = "nvarchar(100)", Storage = "_Type", UpdateCheck = UpdateCheck.Never)]
		public string Type
		{
			get { return _Type; }
			set { _Type = value; }
		}
		private string _Name;
		/// <summary>
		/// Name
		/// </summary>
		[Column(Name = "Name", DbType = "nvarchar(100)", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private string _Value;
		/// <summary>
		/// Value
		/// </summary>
		[Column(Name = "Value", DbType = "nvarchar(100)", Storage = "_Value", UpdateCheck = UpdateCheck.Never)]
		public string Value
		{
			get { return _Value; }
			set { _Value = value; }
		}
		private int _Sequence;
		/// <summary>
		/// Sequence
		/// </summary>
		[Column(Name = "Sequence", DbType = "int", Storage = "_Sequence", UpdateCheck = UpdateCheck.Never)]
		public int Sequence
		{
			get { return _Sequence; }
			set { _Sequence = value; }
		}
		private string _Remark;
		/// <summary>
		/// Remark
		/// </summary>
		[Column(Name = "Remark", DbType = "nvarchar(100)", Storage = "_Remark", UpdateCheck = UpdateCheck.Never)]
		public string Remark
		{
			get { return _Remark; }
			set { _Remark = value; }
		}
	}
}