using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "G_KEYVALUE")]
	public class G_KEYVALUE
	{
		private string _KeyName;
		/// <summary>
		/// KeyName
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "KeyName", DbType = "varchar(50)", Storage = "_KeyName")]
		public string KeyName
		{
			get { return _KeyName; }
			set { _KeyName = value; }
		}
		private string _KeyAdditional;
		/// <summary>
		/// KeyAdditional
		/// </summary>
		[Column(Name = "KeyAdditional", DbType = "char(8)", Storage = "_KeyAdditional", UpdateCheck = UpdateCheck.Never)]
		public string KeyAdditional
		{
			get { return _KeyAdditional; }
			set { _KeyAdditional = value; }
		}
		private int _KeyValue;
		/// <summary>
		/// KeyValue
		/// </summary>
		[Column(Name = "KeyValue", DbType = "int", Storage = "_KeyValue", UpdateCheck = UpdateCheck.Never)]
		public int KeyValue
		{
			get { return _KeyValue; }
			set { _KeyValue = value; }
		}
	}
}