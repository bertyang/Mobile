using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "TPatientRecordTgjc")]
	public class TPatientRecordTgjc
	{
		private string _任务编码;
		/// <summary>
		/// 任务编码
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "任务编码", DbType = "char(20)", Storage = "_任务编码")]
		public string 任务编码
		{
			get { return _任务编码; }
			set { _任务编码 = value; }
		}
		private int _序号;
		/// <summary>
		/// 序号
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "序号", DbType = "int", Storage = "_序号")]
		public int 序号
		{
			get { return _序号; }
			set { _序号 = value; }
		}
		private string _是否体查;
		/// <summary>
		/// 是否体查
		/// </summary>
		[Column(Name = "是否体查", DbType = "nvarchar(20)", Storage = "_是否体查", UpdateCheck = UpdateCheck.Never)]
		public string 是否体查
		{
			get { return _是否体查; }
			set { _是否体查 = value; }
		}
		private string _未体查原因;
		/// <summary>
		/// 未体查原因
		/// </summary>
		[Column(Name = "未体查原因", DbType = "nvarchar(100)", Storage = "_未体查原因", UpdateCheck = UpdateCheck.Never)]
		public string 未体查原因
		{
			get { return _未体查原因; }
			set { _未体查原因 = value; }
		}
		private string _BP1;
		/// <summary>
		/// BP1
		/// </summary>
		[Column(Name = "BP1", DbType = "varchar(10)", Storage = "_BP1", UpdateCheck = UpdateCheck.Never)]
		public string BP1
		{
			get { return _BP1; }
			set { _BP1 = value; }
		}
		private string _BP2;
		/// <summary>
		/// BP2
		/// </summary>
		[Column(Name = "BP2", DbType = "varchar(10)", Storage = "_BP2", UpdateCheck = UpdateCheck.Never)]
		public string BP2
		{
			get { return _BP2; }
			set { _BP2 = value; }
		}
		private string _P;
		/// <summary>
		/// P
		/// </summary>
		[Column(Name = "P", DbType = "varchar(10)", Storage = "_P", UpdateCheck = UpdateCheck.Never)]
		public string P
		{
			get { return _P; }
			set { _P = value; }
		}
		private string _R;
		/// <summary>
		/// R
		/// </summary>
		[Column(Name = "R", DbType = "varchar(10)", Storage = "_R", UpdateCheck = UpdateCheck.Never)]
		public string R
		{
			get { return _R; }
			set { _R = value; }
		}
		private string _HR;
		/// <summary>
		/// HR
		/// </summary>
		[Column(Name = "HR", DbType = "varchar(10)", Storage = "_HR", UpdateCheck = UpdateCheck.Never)]
		public string HR
		{
			get { return _HR; }
			set { _HR = value; }
		}
		private string _T;
		/// <summary>
		/// T
		/// </summary>
		[Column(Name = "T", DbType = "varchar(10)", Storage = "_T", UpdateCheck = UpdateCheck.Never)]
		public string T
		{
			get { return _T; }
			set { _T = value; }
		}
		private string _一般情况;
		/// <summary>
		/// 一般情况
		/// </summary>
		[Column(Name = "一般情况", DbType = "nvarchar(20)", Storage = "_一般情况", UpdateCheck = UpdateCheck.Never)]
		public string 一般情况
		{
			get { return _一般情况; }
			set { _一般情况 = value; }
		}
		private string _体位;
		/// <summary>
		/// 体位
		/// </summary>
		[Column(Name = "体位", DbType = "nvarchar(40)", Storage = "_体位", UpdateCheck = UpdateCheck.Never)]
		public string 体位
		{
			get { return _体位; }
			set { _体位 = value; }
		}
		private string _神志;
		/// <summary>
		/// 神志
		/// </summary>
		[Column(Name = "神志", DbType = "nvarchar(40)", Storage = "_神志", UpdateCheck = UpdateCheck.Never)]
		public string 神志
		{
			get { return _神志; }
			set { _神志 = value; }
		}
		private string _皮肤颜色;
		/// <summary>
		/// 皮肤颜色
		/// </summary>
		[Column(Name = "皮肤颜色", DbType = "nvarchar(20)", Storage = "_皮肤颜色", UpdateCheck = UpdateCheck.Never)]
		public string 皮肤颜色
		{
			get { return _皮肤颜色; }
			set { _皮肤颜色 = value; }
		}
		private string _皮肤其他;
		/// <summary>
		/// 皮肤其他
		/// </summary>
		[Column(Name = "皮肤其他", DbType = "nvarchar(100)", Storage = "_皮肤其他", UpdateCheck = UpdateCheck.Never)]
		public string 皮肤其他
		{
			get { return _皮肤其他; }
			set { _皮肤其他 = value; }
		}
		private string _瞳孔左;
		/// <summary>
		/// 瞳孔左
		/// </summary>
		[Column(Name = "瞳孔左", DbType = "varchar(10)", Storage = "_瞳孔左", UpdateCheck = UpdateCheck.Never)]
		public string 瞳孔左
		{
			get { return _瞳孔左; }
			set { _瞳孔左 = value; }
		}
		private string _瞳孔右;
		/// <summary>
		/// 瞳孔右
		/// </summary>
		[Column(Name = "瞳孔右", DbType = "varchar(10)", Storage = "_瞳孔右", UpdateCheck = UpdateCheck.Never)]
		public string 瞳孔右
		{
			get { return _瞳孔右; }
			set { _瞳孔右 = value; }
		}
		private string _对光反射;
		/// <summary>
		/// 对光反射
		/// </summary>
		[Column(Name = "对光反射", DbType = "nvarchar(20)", Storage = "_对光反射", UpdateCheck = UpdateCheck.Never)]
		public string 对光反射
		{
			get { return _对光反射; }
			set { _对光反射 = value; }
		}
		private string _头部其他;
		/// <summary>
		/// 头部其他
		/// </summary>
		[Column(Name = "头部其他", DbType = "nvarchar(40)", Storage = "_头部其他", UpdateCheck = UpdateCheck.Never)]
		public string 头部其他
		{
			get { return _头部其他; }
			set { _头部其他 = value; }
		}
		private string _颈抵抗;
		/// <summary>
		/// 颈抵抗
		/// </summary>
		[Column(Name = "颈抵抗", DbType = "nvarchar(20)", Storage = "_颈抵抗", UpdateCheck = UpdateCheck.Never)]
		public string 颈抵抗
		{
			get { return _颈抵抗; }
			set { _颈抵抗 = value; }
		}
		private string _颈静脉怒张;
		/// <summary>
		/// 颈静脉怒张
		/// </summary>
		[Column(Name = "颈静脉怒张", DbType = "nvarchar(20)", Storage = "_颈静脉怒张", UpdateCheck = UpdateCheck.Never)]
		public string 颈静脉怒张
		{
			get { return _颈静脉怒张; }
			set { _颈静脉怒张 = value; }
		}
		private string _颈动脉搏动;
		/// <summary>
		/// 颈动脉搏动
		/// </summary>
		[Column(Name = "颈动脉搏动", DbType = "nvarchar(20)", Storage = "_颈动脉搏动", UpdateCheck = UpdateCheck.Never)]
		public string 颈动脉搏动
		{
			get { return _颈动脉搏动; }
			set { _颈动脉搏动 = value; }
		}
		private string _呼吸停止;
		/// <summary>
		/// 呼吸停止
		/// </summary>
		[Column(Name = "呼吸停止", DbType = "nvarchar(20)", Storage = "_呼吸停止", UpdateCheck = UpdateCheck.Never)]
		public string 呼吸停止
		{
			get { return _呼吸停止; }
			set { _呼吸停止 = value; }
		}
		private string _左肺呼吸音;
		/// <summary>
		/// 左肺呼吸音
		/// </summary>
		[Column(Name = "左肺呼吸音", DbType = "nvarchar(20)", Storage = "_左肺呼吸音", UpdateCheck = UpdateCheck.Never)]
		public string 左肺呼吸音
		{
			get { return _左肺呼吸音; }
			set { _左肺呼吸音 = value; }
		}
		private string _左肺湿罗音;
		/// <summary>
		/// 左肺湿罗音
		/// </summary>
		[Column(Name = "左肺湿罗音", DbType = "nvarchar(20)", Storage = "_左肺湿罗音", UpdateCheck = UpdateCheck.Never)]
		public string 左肺湿罗音
		{
			get { return _左肺湿罗音; }
			set { _左肺湿罗音 = value; }
		}
		private string _左肺干罗音;
		/// <summary>
		/// 左肺干罗音
		/// </summary>
		[Column(Name = "左肺干罗音", DbType = "nvarchar(20)", Storage = "_左肺干罗音", UpdateCheck = UpdateCheck.Never)]
		public string 左肺干罗音
		{
			get { return _左肺干罗音; }
			set { _左肺干罗音 = value; }
		}
		private string _右肺呼吸音;
		/// <summary>
		/// 右肺呼吸音
		/// </summary>
		[Column(Name = "右肺呼吸音", DbType = "nvarchar(20)", Storage = "_右肺呼吸音", UpdateCheck = UpdateCheck.Never)]
		public string 右肺呼吸音
		{
			get { return _右肺呼吸音; }
			set { _右肺呼吸音 = value; }
		}
		private string _右肺湿罗音;
		/// <summary>
		/// 右肺湿罗音
		/// </summary>
		[Column(Name = "右肺湿罗音", DbType = "nvarchar(20)", Storage = "_右肺湿罗音", UpdateCheck = UpdateCheck.Never)]
		public string 右肺湿罗音
		{
			get { return _右肺湿罗音; }
			set { _右肺湿罗音 = value; }
		}
		private string _右肺干罗音;
		/// <summary>
		/// 右肺干罗音
		/// </summary>
		[Column(Name = "右肺干罗音", DbType = "nvarchar(20)", Storage = "_右肺干罗音", UpdateCheck = UpdateCheck.Never)]
		public string 右肺干罗音
		{
			get { return _右肺干罗音; }
			set { _右肺干罗音 = value; }
		}
		private string _心率;
		/// <summary>
		/// 心率
		/// </summary>
		[Column(Name = "心率", DbType = "varchar(10)", Storage = "_心率", UpdateCheck = UpdateCheck.Never)]
		public string 心率
		{
			get { return _心率; }
			set { _心率 = value; }
		}
		private string _心律;
		/// <summary>
		/// 心律
		/// </summary>
		[Column(Name = "心律", DbType = "nvarchar(20)", Storage = "_心律", UpdateCheck = UpdateCheck.Never)]
		public string 心律
		{
			get { return _心律; }
			set { _心律 = value; }
		}
		private string _心音;
		/// <summary>
		/// 心音
		/// </summary>
		[Column(Name = "心音", DbType = "nvarchar(20)", Storage = "_心音", UpdateCheck = UpdateCheck.Never)]
		public string 心音
		{
			get { return _心音; }
			set { _心音 = value; }
		}
		private string _杂音;
		/// <summary>
		/// 杂音
		/// </summary>
		[Column(Name = "杂音", DbType = "nvarchar(20)", Storage = "_杂音", UpdateCheck = UpdateCheck.Never)]
		public string 杂音
		{
			get { return _杂音; }
			set { _杂音 = value; }
		}
		private string _杂音部位;
		/// <summary>
		/// 杂音部位
		/// </summary>
		[Column(Name = "杂音部位", DbType = "nvarchar(40)", Storage = "_杂音部位", UpdateCheck = UpdateCheck.Never)]
		public string 杂音部位
		{
			get { return _杂音部位; }
			set { _杂音部位 = value; }
		}
		private string _腹部;
		/// <summary>
		/// 腹部
		/// </summary>
		[Column(Name = "腹部", DbType = "nvarchar(20)", Storage = "_腹部", UpdateCheck = UpdateCheck.Never)]
		public string 腹部
		{
			get { return _腹部; }
			set { _腹部 = value; }
		}
		private string _腹部压痛;
		/// <summary>
		/// 腹部压痛
		/// </summary>
		[Column(Name = "腹部压痛", DbType = "nvarchar(20)", Storage = "_腹部压痛", UpdateCheck = UpdateCheck.Never)]
		public string 腹部压痛
		{
			get { return _腹部压痛; }
			set { _腹部压痛 = value; }
		}
		private string _压痛部位;
		/// <summary>
		/// 压痛部位
		/// </summary>
		[Column(Name = "压痛部位", DbType = "nvarchar(40)", Storage = "_压痛部位", UpdateCheck = UpdateCheck.Never)]
		public string 压痛部位
		{
			get { return _压痛部位; }
			set { _压痛部位 = value; }
		}
		private string _腹部反跳痛;
		/// <summary>
		/// 腹部反跳痛
		/// </summary>
		[Column(Name = "腹部反跳痛", DbType = "nvarchar(20)", Storage = "_腹部反跳痛", UpdateCheck = UpdateCheck.Never)]
		public string 腹部反跳痛
		{
			get { return _腹部反跳痛; }
			set { _腹部反跳痛 = value; }
		}
		private string _肝脏;
		/// <summary>
		/// 肝脏
		/// </summary>
		[Column(Name = "肝脏", DbType = "nvarchar(20)", Storage = "_肝脏", UpdateCheck = UpdateCheck.Never)]
		public string 肝脏
		{
			get { return _肝脏; }
			set { _肝脏 = value; }
		}
		private string _脾脏;
		/// <summary>
		/// 脾脏
		/// </summary>
		[Column(Name = "脾脏", DbType = "nvarchar(20)", Storage = "_脾脏", UpdateCheck = UpdateCheck.Never)]
		public string 脾脏
		{
			get { return _脾脏; }
			set { _脾脏 = value; }
		}
		private string _肠鸣音;
		/// <summary>
		/// 肠鸣音
		/// </summary>
		[Column(Name = "肠鸣音", DbType = "nvarchar(20)", Storage = "_肠鸣音", UpdateCheck = UpdateCheck.Never)]
		public string 肠鸣音
		{
			get { return _肠鸣音; }
			set { _肠鸣音 = value; }
		}
		private string _腹部其他;
		/// <summary>
		/// 腹部其他
		/// </summary>
		[Column(Name = "腹部其他", DbType = "nvarchar(100)", Storage = "_腹部其他", UpdateCheck = UpdateCheck.Never)]
		public string 腹部其他
		{
			get { return _腹部其他; }
			set { _腹部其他 = value; }
		}
		private string _脊柱;
		/// <summary>
		/// 脊柱
		/// </summary>
		[Column(Name = "脊柱", DbType = "nvarchar(100)", Storage = "_脊柱", UpdateCheck = UpdateCheck.Never)]
		public string 脊柱
		{
			get { return _脊柱; }
			set { _脊柱 = value; }
		}
		private string _四肢肌力;
		/// <summary>
		/// 四肢肌力
		/// </summary>
		[Column(Name = "四肢肌力", DbType = "nvarchar(20)", Storage = "_四肢肌力", UpdateCheck = UpdateCheck.Never)]
		public string 四肢肌力
		{
			get { return _四肢肌力; }
			set { _四肢肌力 = value; }
		}
		private string _减弱部位;
		/// <summary>
		/// 减弱部位
		/// </summary>
		[Column(Name = "减弱部位", DbType = "nvarchar(40)", Storage = "_减弱部位", UpdateCheck = UpdateCheck.Never)]
		public string 减弱部位
		{
			get { return _减弱部位; }
			set { _减弱部位 = value; }
		}
		private string _下肢水肿;
		/// <summary>
		/// 下肢水肿
		/// </summary>
		[Column(Name = "下肢水肿", DbType = "nvarchar(20)", Storage = "_下肢水肿", UpdateCheck = UpdateCheck.Never)]
		public string 下肢水肿
		{
			get { return _下肢水肿; }
			set { _下肢水肿 = value; }
		}
		private string _四肢其他;
		/// <summary>
		/// 四肢其他
		/// </summary>
		[Column(Name = "四肢其他", DbType = "nvarchar(100)", Storage = "_四肢其他", UpdateCheck = UpdateCheck.Never)]
		public string 四肢其他
		{
			get { return _四肢其他; }
			set { _四肢其他 = value; }
		}
		private string _生理反射;
		/// <summary>
		/// 生理反射
		/// </summary>
		[Column(Name = "生理反射", DbType = "nvarchar(20)", Storage = "_生理反射", UpdateCheck = UpdateCheck.Never)]
		public string 生理反射
		{
			get { return _生理反射; }
			set { _生理反射 = value; }
		}
		private string _巴宾斯基征;
		/// <summary>
		/// 巴宾斯基征
		/// </summary>
		[Column(Name = "巴宾斯基征", DbType = "nvarchar(20)", Storage = "_巴宾斯基征", UpdateCheck = UpdateCheck.Never)]
		public string 巴宾斯基征
		{
			get { return _巴宾斯基征; }
			set { _巴宾斯基征 = value; }
		}
		private string _巴宾斯基征位置;
		/// <summary>
		/// 巴宾斯基征位置
		/// </summary>
		[Column(Name = "巴宾斯基征位置", DbType = "nvarchar(20)", Storage = "_巴宾斯基征位置", UpdateCheck = UpdateCheck.Never)]
		public string 巴宾斯基征位置
		{
			get { return _巴宾斯基征位置; }
			set { _巴宾斯基征位置 = value; }
		}
		private string _神经系统其他;
		/// <summary>
		/// 神经系统其他
		/// </summary>
		[Column(Name = "神经系统其他", DbType = "nvarchar(100)", Storage = "_神经系统其他", UpdateCheck = UpdateCheck.Never)]
		public string 神经系统其他
		{
			get { return _神经系统其他; }
			set { _神经系统其他 = value; }
		}
		private string _体格检查其他;
		/// <summary>
		/// 体格检查其他
		/// </summary>
		[Column(Name = "体格检查其他", DbType = "nvarchar(100)", Storage = "_体格检查其他", UpdateCheck = UpdateCheck.Never)]
		public string 体格检查其他
		{
			get { return _体格检查其他; }
			set { _体格检查其他 = value; }
		}
	}
}