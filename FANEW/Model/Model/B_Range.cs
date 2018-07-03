using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
    [Table(Name = "B_Range")]
    [Serializable] //必须添加序列化特性
	public class B_Range
	{
		private int _ActionId;
		/// <summary>
		/// ActionId
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "ActionId", DbType = "int", Storage = "_ActionId")]
		public int ActionId
		{
			get { return _ActionId; }
			set { _ActionId = value; }
		}
		private string _Range;
		/// <summary>
		/// Range
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "Range", DbType = "varchar(20)", Storage = "_Range")]
		public string Range
		{
			get { return _Range; }
			set { _Range = value; }
		}
		private string _RangeReamrk;
		/// <summary>
		/// RangeReamrk
		/// </summary>
		[Column(Name = "RangeReamrk", DbType = "varchar(50)", Storage = "_RangeReamrk", UpdateCheck = UpdateCheck.Never)]
		public string RangeReamrk
		{
			get { return _RangeReamrk; }
			set { _RangeReamrk = value; }
		}
		private string _GroupRange;
		/// <summary>
		/// GroupRange
		/// </summary>
		[Column(Name = "GroupRange", DbType = "varchar(20)", Storage = "_GroupRange", UpdateCheck = UpdateCheck.Never)]
		public string GroupRange
		{
			get { return _GroupRange; }
			set { _GroupRange = value; }
		}
		private string _GroupReamrk;
		/// <summary>
		/// GroupReamrk
		/// </summary>
		[Column(Name = "GroupReamrk", DbType = "varchar(50)", Storage = "_GroupReamrk", UpdateCheck = UpdateCheck.Never)]
		public string GroupReamrk
		{
			get { return _GroupReamrk; }
			set { _GroupReamrk = value; }
		}
		private int? _Weight;
		/// <summary>
		/// Weight
		/// </summary>
		[Column(Name = "Weight", DbType = "int", Storage = "_Weight", UpdateCheck = UpdateCheck.Never)]
		public int? Weight
		{
			get { return _Weight; }
			set { _Weight = value; }
		}
		private bool? _IsActive;
		/// <summary>
		/// IsActive
		/// </summary>
		[Column(Name = "IsActive", DbType = "bit", Storage = "_IsActive", UpdateCheck = UpdateCheck.Never)]
		public bool? IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}
		private int? _OrderID;
		/// <summary>
		/// OrderID
		/// </summary>
		[Column(Name = "OrderID", DbType = "int", Storage = "_OrderID", UpdateCheck = UpdateCheck.Never)]
		public int? OrderID
		{
			get { return _OrderID; }
			set { _OrderID = value; }
		}
	}
}