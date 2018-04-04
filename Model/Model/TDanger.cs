using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TDanger")]
	public class TDanger
	{
		private string _序号;
		/// <summary>
		/// 序号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "序号", DbType = "varchar(4)", Storage = "_序号")]
		public string 序号
		{
			get { return _序号; }
			set { _序号 = value; }
		}
		private string _中文名称;
		/// <summary>
		/// 中文名称
		/// </summary>
		[Column(Name = "中文名称", DbType = "varchar(30)", Storage = "_中文名称", UpdateCheck = UpdateCheck.Never)]
		public string 中文名称
		{
			get { return _中文名称; }
			set { _中文名称 = value; }
		}
		private string _英文名称;
		/// <summary>
		/// 英文名称
		/// </summary>
		[Column(Name = "英文名称", DbType = "varchar(40)", Storage = "_英文名称", UpdateCheck = UpdateCheck.Never)]
		public string 英文名称
		{
			get { return _英文名称; }
			set { _英文名称 = value; }
		}
		private string _Cas;
		/// <summary>
		/// Cas
		/// </summary>
		[Column(Name = "Cas", DbType = "varchar(12)", Storage = "_Cas", UpdateCheck = UpdateCheck.Never)]
		public string Cas
		{
			get { return _Cas; }
			set { _Cas = value; }
		}
		private string _Rtecs;
		/// <summary>
		/// Rtecs
		/// </summary>
		[Column(Name = "Rtecs", DbType = "varchar(16)", Storage = "_Rtecs", UpdateCheck = UpdateCheck.Never)]
		public string Rtecs
		{
			get { return _Rtecs; }
			set { _Rtecs = value; }
		}
		private string _Un;
		/// <summary>
		/// Un
		/// </summary>
		[Column(Name = "Un", DbType = "varchar(12)", Storage = "_Un", UpdateCheck = UpdateCheck.Never)]
		public string Un
		{
			get { return _Un; }
			set { _Un = value; }
		}
		private string _危险号;
		/// <summary>
		/// 危险号
		/// </summary>
		[Column(Name = "危险号", DbType = "varchar(14)", Storage = "_危险号", UpdateCheck = UpdateCheck.Never)]
		public string 危险号
		{
			get { return _危险号; }
			set { _危险号 = value; }
		}
		private string _分子式;
		/// <summary>
		/// 分子式
		/// </summary>
		[Column(Name = "分子式", DbType = "varchar(40)", Storage = "_分子式", UpdateCheck = UpdateCheck.Never)]
		public string 分子式
		{
			get { return _分子式; }
			set { _分子式 = value; }
		}
		private string _性状;
		/// <summary>
		/// 性状
		/// </summary>
		[Column(Name = "性状", DbType = "text(16)", Storage = "_性状", UpdateCheck = UpdateCheck.Never)]
		public string 性状
		{
			get { return _性状; }
			set { _性状 = value; }
		}
		private string _空气中的允许极限及测定;
		/// <summary>
		/// 空气中的允许极限及测定
		/// </summary>
		[Column(Name = "空气中的允许极限及测定", DbType = "text(16)", Storage = "_空气中的允许极限及测定", UpdateCheck = UpdateCheck.Never)]
		public string 空气中的允许极限及测定
		{
			get { return _空气中的允许极限及测定; }
			set { _空气中的允许极限及测定 = value; }
		}
		private string _水中的允许极限及测定;
		/// <summary>
		/// 水中的允许极限及测定
		/// </summary>
		[Column(Name = "水中的允许极限及测定", DbType = "text(16)", Storage = "_水中的允许极限及测定", UpdateCheck = UpdateCheck.Never)]
		public string 水中的允许极限及测定
		{
			get { return _水中的允许极限及测定; }
			set { _水中的允许极限及测定 = value; }
		}
		private string _禁忌;
		/// <summary>
		/// 禁忌
		/// </summary>
		[Column(Name = "禁忌", DbType = "text(16)", Storage = "_禁忌", UpdateCheck = UpdateCheck.Never)]
		public string 禁忌
		{
			get { return _禁忌; }
			set { _禁忌 = value; }
		}
		private string _同义词;
		/// <summary>
		/// 同义词
		/// </summary>
		[Column(Name = "同义词", DbType = "text(16)", Storage = "_同义词", UpdateCheck = UpdateCheck.Never)]
		public string 同义词
		{
			get { return _同义词; }
			set { _同义词 = value; }
		}
		private string _危险性;
		/// <summary>
		/// 危险性
		/// </summary>
		[Column(Name = "危险性", DbType = "text(16)", Storage = "_危险性", UpdateCheck = UpdateCheck.Never)]
		public string 危险性
		{
			get { return _危险性; }
			set { _危险性 = value; }
		}
		private string _急救措施;
		/// <summary>
		/// 急救措施
		/// </summary>
		[Column(Name = "急救措施", DbType = "text(16)", Storage = "_急救措施", UpdateCheck = UpdateCheck.Never)]
		public string 急救措施
		{
			get { return _急救措施; }
			set { _急救措施 = value; }
		}
		private string _防护措施;
		/// <summary>
		/// 防护措施
		/// </summary>
		[Column(Name = "防护措施", DbType = "text(16)", Storage = "_防护措施", UpdateCheck = UpdateCheck.Never)]
		public string 防护措施
		{
			get { return _防护措施; }
			set { _防护措施 = value; }
		}
		private string _储存;
		/// <summary>
		/// 储存
		/// </summary>
		[Column(Name = "储存", DbType = "text(16)", Storage = "_储存", UpdateCheck = UpdateCheck.Never)]
		public string 储存
		{
			get { return _储存; }
			set { _储存 = value; }
		}
		private string _泄露处理;
		/// <summary>
		/// 泄露处理
		/// </summary>
		[Column(Name = "泄露处理", DbType = "text(16)", Storage = "_泄露处理", UpdateCheck = UpdateCheck.Never)]
		public string 泄露处理
		{
			get { return _泄露处理; }
			set { _泄露处理 = value; }
		}
		private string _运输要求;
		/// <summary>
		/// 运输要求
		/// </summary>
		[Column(Name = "运输要求", DbType = "text(16)", Storage = "_运输要求", UpdateCheck = UpdateCheck.Never)]
		public string 运输要求
		{
			get { return _运输要求; }
			set { _运输要求 = value; }
		}
		private string _附注;
		/// <summary>
		/// 附注
		/// </summary>
		[Column(Name = "附注", DbType = "text(16)", Storage = "_附注", UpdateCheck = UpdateCheck.Never)]
		public string 附注
		{
			get { return _附注; }
			set { _附注 = value; }
		}
		private string _常用标志;
		/// <summary>
		/// 常用标志
		/// </summary>
		[Column(Name = "常用标志", DbType = "varchar(6)", Storage = "_常用标志", UpdateCheck = UpdateCheck.Never)]
		public string 常用标志
		{
			get { return _常用标志; }
			set { _常用标志 = value; }
		}
	}
}