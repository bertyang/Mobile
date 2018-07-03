using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TWeiGui")]
	public class TWeiGui
	{
		private string _急救站编号;
		/// <summary>
		/// 急救站编号
		/// </summary>
		[Column(Name = "急救站编号", DbType = "nchar(20)", Storage = "_急救站编号", UpdateCheck = UpdateCheck.Never)]
		public string 急救站编号
		{
			get { return _急救站编号; }
			set { _急救站编号 = value; }
		}
		private string _急救站;
		/// <summary>
		/// 急救站
		/// </summary>
		[Column(Name = "急救站", DbType = "varchar(50)", Storage = "_急救站", UpdateCheck = UpdateCheck.Never)]
		public string 急救站
		{
			get { return _急救站; }
			set { _急救站 = value; }
		}
		private string _违规车号;
		/// <summary>
		/// 违规车号
		/// </summary>
		[Column(Name = "违规车号", DbType = "varchar(50)", Storage = "_违规车号", UpdateCheck = UpdateCheck.Never)]
		public string 违规车号
		{
			get { return _违规车号; }
			set { _违规车号 = value; }
		}
		private string _违规类型;
		/// <summary>
		/// 违规类型
		/// </summary>
		[Column(Name = "违规类型", DbType = "varchar(50)", Storage = "_违规类型", UpdateCheck = UpdateCheck.Never)]
		public string 违规类型
		{
			get { return _违规类型; }
			set { _违规类型 = value; }
		}
		private string _备注;
		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "备注", DbType = "varchar(5000)", Storage = "_备注", UpdateCheck = UpdateCheck.Never)]
		public string 备注
		{
			get { return _备注; }
			set { _备注 = value; }
		}
		private DateTime? _违规时间;
		/// <summary>
		/// 违规时间
		/// </summary>
		[Column(Name = "违规时间", DbType = "datetime", Storage = "_违规时间", UpdateCheck = UpdateCheck.Never)]
		public DateTime? 违规时间
		{
			get { return _违规时间; }
			set { _违规时间 = value; }
		}
	}
}