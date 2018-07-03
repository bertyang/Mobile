using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "B_TELTYPE_CUSTOM")]
	public class B_TELTYPE_CUSTOM
	{
		private int _编码;
		/// <summary>
		/// 编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "编码", DbType = "int", Storage = "_编码")]
		public int 编码
		{
			get { return _编码; }
			set { _编码 = value; }
		}
        private int _归属人ID;
		/// <summary>
		/// OwnerID
		/// </summary>
        [Column(Name = "归属人ID", DbType = "int", Storage = "_归属人ID", UpdateCheck = UpdateCheck.Never)]
        public int 归属人ID
		{
            get { return _归属人ID; }
            set { _归属人ID = value; }
		}
	}
}