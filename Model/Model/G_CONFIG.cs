using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "G_CONFIG")]
	public class G_CONFIG
	{
		private string _Key;
		/// <summary>
		/// Key
		/// </summary>
        [Column(IsPrimaryKey = true, Name = "Key", DbType = "nvarchar(100)", Storage = "_Key", UpdateCheck = UpdateCheck.Never)]
		public string Key
		{
			get { return _Key; }
			set { _Key = value; }
		}
        private string _Type;
        /// <summary>
        /// Key
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "Type", DbType = "nvarchar(100)", Storage = "_Type", UpdateCheck = UpdateCheck.Never)]
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
		private string _Value;
		/// <summary>
		/// Value
		/// </summary>
		[Column(Name = "Value", DbType = "nvarchar(510)", Storage = "_Value", UpdateCheck = UpdateCheck.Never)]
		public string Value
		{
			get { return _Value; }
			set { _Value = value; }
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