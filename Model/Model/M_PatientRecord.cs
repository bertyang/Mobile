using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
	[Table(Name = "M_PatientRecord")]
	public class M_PatientRecord
	{
		private string _TaskCode;
		/// <summary>
		/// TaskCode
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "TaskCode", DbType = "char(20)", Storage = "_TaskCode")]
		public string TaskCode
		{
			get { return _TaskCode; }
			set { _TaskCode = value; }
		}
		private int _PatientOrder;
		/// <summary>
		/// PatientOrder
		/// </summary>
		[Column(IsPrimaryKey = true, Name = "PatientOrder", DbType = "int", Storage = "_PatientOrder")]
		public int PatientOrder
		{
			get { return _PatientOrder; }
			set { _PatientOrder = value; }
		}
		private string _RecordID;
		/// <summary>
		/// RecordID
		/// </summary>
		[Column(Name = "RecordID", DbType = "varchar(30)", Storage = "_RecordID", UpdateCheck = UpdateCheck.Never)]
		public string RecordID
		{
			get { return _RecordID; }
			set { _RecordID = value; }
		}
		private bool _ThreeNonePatient;
		/// <summary>
		/// ThreeNonePatient
		/// </summary>
		[Column(Name = "ThreeNonePatient", DbType = "bit", Storage = "_ThreeNonePatient", UpdateCheck = UpdateCheck.Never)]
		public bool ThreeNonePatient
		{
			get { return _ThreeNonePatient; }
			set { _ThreeNonePatient = value; }
		}
		private string _Name;
		/// <summary>
		/// Name
		/// </summary>
		[Column(Name = "Name", DbType = "varchar(50)", Storage = "_Name", UpdateCheck = UpdateCheck.Never)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private string _Sex;
		/// <summary>
		/// Sex
		/// </summary>
		[Column(Name = "Sex", DbType = "varchar(10)", Storage = "_Sex", UpdateCheck = UpdateCheck.Never)]
		public string Sex
		{
			get { return _Sex; }
			set { _Sex = value; }
		}
		private string _Age;
		/// <summary>
		/// Age
		/// </summary>
		[Column(Name = "Age", DbType = "varchar(10)", Storage = "_Age", UpdateCheck = UpdateCheck.Never)]
		public string Age
		{
			get { return _Age; }
			set { _Age = value; }
		}
		private string _AgeType;
		/// <summary>
		/// AgeType
		/// </summary>
		[Column(Name = "AgeType", DbType = "varchar(10)", Storage = "_AgeType", UpdateCheck = UpdateCheck.Never)]
		public string AgeType
		{
			get { return _AgeType; }
			set { _AgeType = value; }
		}
		private string _Vocation;
		/// <summary>
		/// Vocation
		/// </summary>
		[Column(Name = "Vocation", DbType = "varchar(50)", Storage = "_Vocation", UpdateCheck = UpdateCheck.Never)]
		public string Vocation
		{
			get { return _Vocation; }
			set { _Vocation = value; }
		}
		private string _Nation;
		/// <summary>
		/// Nation
		/// </summary>
		[Column(Name = "Nation", DbType = "varchar(50)", Storage = "_Nation", UpdateCheck = UpdateCheck.Never)]
		public string Nation
		{
			get { return _Nation; }
			set { _Nation = value; }
		}
		private string _Nationality;
		/// <summary>
		/// Nationality
		/// </summary>
		[Column(Name = "Nationality", DbType = "varchar(50)", Storage = "_Nationality", UpdateCheck = UpdateCheck.Never)]
		public string Nationality
		{
			get { return _Nationality; }
			set { _Nationality = value; }
		}
		private string _TaskType;
		/// <summary>
		/// TaskType
		/// </summary>
		[Column(Name = "TaskType", DbType = "varchar(50)", Storage = "_TaskType", UpdateCheck = UpdateCheck.Never)]
		public string TaskType
		{
			get { return _TaskType; }
			set { _TaskType = value; }
		}
		private string _LocalAdd;
		/// <summary>
		/// LocalAdd
		/// </summary>
		[Column(Name = "LocalAdd", DbType = "varchar(200)", Storage = "_LocalAdd", UpdateCheck = UpdateCheck.Never)]
		public string LocalAdd
		{
			get { return _LocalAdd; }
			set { _LocalAdd = value; }
		}
		private int? _LocalAddType;
		/// <summary>
		/// LocalAddType
		/// </summary>
		[Column(Name = "LocalAddType", DbType = "int", Storage = "_LocalAddType", UpdateCheck = UpdateCheck.Never)]
		public int? LocalAddType
		{
			get { return _LocalAddType; }
			set { _LocalAddType = value; }
		}
		private string _SendAdd;
		/// <summary>
		/// SendAdd
		/// </summary>
		[Column(Name = "SendAdd", DbType = "varchar(200)", Storage = "_SendAdd", UpdateCheck = UpdateCheck.Never)]
		public string SendAdd
		{
			get { return _SendAdd; }
			set { _SendAdd = value; }
		}
		private int? _SendAddType;
		/// <summary>
		/// SendAddType
		/// </summary>
		[Column(Name = "SendAddType", DbType = "int", Storage = "_SendAddType", UpdateCheck = UpdateCheck.Never)]
		public int? SendAddType
		{
			get { return _SendAddType; }
			set { _SendAddType = value; }
		}
		private string _Contact;
		/// <summary>
		/// Contact
		/// </summary>
		[Column(Name = "Contact", DbType = "varchar(50)", Storage = "_Contact", UpdateCheck = UpdateCheck.Never)]
		public string Contact
		{
			get { return _Contact; }
			set { _Contact = value; }
		}
		private string _LinkPhone;
		/// <summary>
		/// LinkPhone
		/// </summary>
		[Column(Name = "LinkPhone", DbType = "varchar(50)", Storage = "_LinkPhone", UpdateCheck = UpdateCheck.Never)]
		public string LinkPhone
		{
			get { return _LinkPhone; }
			set { _LinkPhone = value; }
		}
		private string _Address;
		/// <summary>
		/// Address
		/// </summary>
		[Column(Name = "Address", DbType = "varchar(100)", Storage = "_Address", UpdateCheck = UpdateCheck.Never)]
		public string Address
		{
			get { return _Address; }
			set { _Address = value; }
		}
		private string _Station;
		/// <summary>
		/// Station
		/// </summary>
		[Column(Name = "Station", DbType = "varchar(50)", Storage = "_Station", UpdateCheck = UpdateCheck.Never)]
		public string Station
		{
			get { return _Station; }
			set { _Station = value; }
		}
		private string _Driver;
		/// <summary>
		/// Driver
		/// </summary>
		[Column(Name = "Driver", DbType = "varchar(50)", Storage = "_Driver", UpdateCheck = UpdateCheck.Never)]
		public string Driver
		{
			get { return _Driver; }
			set { _Driver = value; }
		}
		private string _StretcherBearersI;
		/// <summary>
		/// StretcherBearersI
		/// </summary>
		[Column(Name = "StretcherBearersI", DbType = "varchar(50)", Storage = "_StretcherBearersI", UpdateCheck = UpdateCheck.Never)]
		public string StretcherBearersI
		{
			get { return _StretcherBearersI; }
			set { _StretcherBearersI = value; }
		}
		private string _StretcherBearersII;
		/// <summary>
		/// StretcherBearersII
		/// </summary>
		[Column(Name = "StretcherBearersII", DbType = "varchar(50)", Storage = "_StretcherBearersII", UpdateCheck = UpdateCheck.Never)]
		public string StretcherBearersII
		{
			get { return _StretcherBearersII; }
			set { _StretcherBearersII = value; }
		}
		private string _Doctor;
		/// <summary>
		/// Doctor
		/// </summary>
		[Column(Name = "Doctor", DbType = "varchar(50)", Storage = "_Doctor", UpdateCheck = UpdateCheck.Never)]
		public string Doctor
		{
			get { return _Doctor; }
			set { _Doctor = value; }
		}
		private string _Nurse;
		/// <summary>
		/// Nurse
		/// </summary>
		[Column(Name = "Nurse", DbType = "varchar(50)", Storage = "_Nurse", UpdateCheck = UpdateCheck.Never)]
		public string Nurse
		{
			get { return _Nurse; }
			set { _Nurse = value; }
		}
		private string _FirstAider;
		/// <summary>
		/// FirstAider
		/// </summary>
		[Column(Name = "FirstAider", DbType = "varchar(50)", Storage = "_FirstAider", UpdateCheck = UpdateCheck.Never)]
		public string FirstAider
		{
			get { return _FirstAider; }
			set { _FirstAider = value; }
		}
		private DateTime? _DrivingTime;
		/// <summary>
		/// DrivingTime
		/// </summary>
		[Column(Name = "DrivingTime", DbType = "datetime", Storage = "_DrivingTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? DrivingTime
		{
			get { return _DrivingTime; }
			set { _DrivingTime = value; }
		}
		private DateTime? _ArrivedTime;
		/// <summary>
		/// ArrivedTime
		/// </summary>
		[Column(Name = "ArrivedTime", DbType = "datetime", Storage = "_ArrivedTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? ArrivedTime
		{
			get { return _ArrivedTime; }
			set { _ArrivedTime = value; }
		}
		private DateTime? _ArrivedPatientTime;
		/// <summary>
		/// ArrivedPatientTime
		/// </summary>
		[Column(Name = "ArrivedPatientTime", DbType = "datetime", Storage = "_ArrivedPatientTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? ArrivedPatientTime
		{
			get { return _ArrivedPatientTime; }
			set { _ArrivedPatientTime = value; }
		}
		private DateTime? _LeaveTime;
		/// <summary>
		/// LeaveTime
		/// </summary>
		[Column(Name = "LeaveTime", DbType = "datetime", Storage = "_LeaveTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? LeaveTime
		{
			get { return _LeaveTime; }
			set { _LeaveTime = value; }
		}
		private DateTime? _DeliveredTime;
		/// <summary>
		/// DeliveredTime
		/// </summary>
		[Column(Name = "DeliveredTime", DbType = "datetime", Storage = "_DeliveredTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? DeliveredTime
		{
			get { return _DeliveredTime; }
			set { _DeliveredTime = value; }
		}
		private string _InjuredReason;
		/// <summary>
		/// InjuredReason
		/// </summary>
		[Column(Name = "InjuredReason", DbType = "varchar(50)", Storage = "_InjuredReason", UpdateCheck = UpdateCheck.Never)]
		public string InjuredReason
		{
			get { return _InjuredReason; }
			set { _InjuredReason = value; }
		}
		private string _AccidentType;
		/// <summary>
		/// AccidentType
		/// </summary>
		[Column(Name = "AccidentType", DbType = "varchar(50)", Storage = "_AccidentType", UpdateCheck = UpdateCheck.Never)]
		public string AccidentType
		{
			get { return _AccidentType; }
			set { _AccidentType = value; }
		}
		private string _Complaints;
		/// <summary>
		/// Complaints
		/// </summary>
		[Column(Name = "Complaints", DbType = "varchar(800)", Storage = "_Complaints", UpdateCheck = UpdateCheck.Never)]
		public string Complaints
		{
			get { return _Complaints; }
			set { _Complaints = value; }
		}
		private string _HistoryOfPresentIllness;
		/// <summary>
		/// HistoryOfPresentIllness
		/// </summary>
		[Column(Name = "HistoryOfPresentIllness", DbType = "varchar(800)", Storage = "_HistoryOfPresentIllness", UpdateCheck = UpdateCheck.Never)]
		public string HistoryOfPresentIllness
		{
			get { return _HistoryOfPresentIllness; }
			set { _HistoryOfPresentIllness = value; }
		}
		private string _PastHistoryChoose;
		/// <summary>
		/// PastHistoryChoose
		/// </summary>
		[Column(Name = "PastHistoryChoose", DbType = "varchar(200)", Storage = "_PastHistoryChoose", UpdateCheck = UpdateCheck.Never)]
		public string PastHistoryChoose
		{
			get { return _PastHistoryChoose; }
			set { _PastHistoryChoose = value; }
		}
		private string _PastHistoryOther;
		/// <summary>
		/// PastHistoryOther
		/// </summary>
		[Column(Name = "PastHistoryOther", DbType = "varchar(200)", Storage = "_PastHistoryOther", UpdateCheck = UpdateCheck.Never)]
		public string PastHistoryOther
		{
			get { return _PastHistoryOther; }
			set { _PastHistoryOther = value; }
		}
		private string _AllergicHistoryChoose;
		/// <summary>
		/// AllergicHistoryChoose
		/// </summary>
		[Column(Name = "AllergicHistoryChoose", DbType = "varchar(200)", Storage = "_AllergicHistoryChoose", UpdateCheck = UpdateCheck.Never)]
		public string AllergicHistoryChoose
		{
			get { return _AllergicHistoryChoose; }
			set { _AllergicHistoryChoose = value; }
		}
		private string _AllergicHistoryOther;
		/// <summary>
		/// AllergicHistoryOther
		/// </summary>
		[Column(Name = "AllergicHistoryOther", DbType = "varchar(200)", Storage = "_AllergicHistoryOther", UpdateCheck = UpdateCheck.Never)]
		public string AllergicHistoryOther
		{
			get { return _AllergicHistoryOther; }
			set { _AllergicHistoryOther = value; }
		}
		private string _FirstDiagnoseName;
		/// <summary>
		/// FirstDiagnoseName
		/// </summary>
		[Column(Name = "FirstDiagnoseName", DbType = "varchar(100)", Storage = "_FirstDiagnoseName", UpdateCheck = UpdateCheck.Never)]
		public string FirstDiagnoseName
		{
			get { return _FirstDiagnoseName; }
			set { _FirstDiagnoseName = value; }
		}
		private string _FirstDiagnoseID;
		/// <summary>
		/// FirstDiagnoseID
		/// </summary>
		[Column(Name = "FirstDiagnoseID", DbType = "varchar(50)", Storage = "_FirstDiagnoseID", UpdateCheck = UpdateCheck.Never)]
		public string FirstDiagnoseID
		{
			get { return _FirstDiagnoseID; }
			set { _FirstDiagnoseID = value; }
		}
		private string _MPDSDiagnose;
		/// <summary>
		/// MPDSDiagnose
		/// </summary>
		[Column(Name = "MPDSDiagnose", DbType = "varchar(500)", Storage = "_MPDSDiagnose", UpdateCheck = UpdateCheck.Never)]
		public string MPDSDiagnose
		{
			get { return _MPDSDiagnose; }
			set { _MPDSDiagnose = value; }
		}
		private string _MPDSAccuracy;
		/// <summary>
		/// MPDSAccuracy
		/// </summary>
		[Column(Name = "MPDSAccuracy", DbType = "varchar(50)", Storage = "_MPDSAccuracy", UpdateCheck = UpdateCheck.Never)]
		public string MPDSAccuracy
		{
			get { return _MPDSAccuracy; }
			set { _MPDSAccuracy = value; }
		}
		private string _TentativeDiagnoseName;
		/// <summary>
		/// TentativeDiagnoseName
		/// </summary>
		[Column(Name = "TentativeDiagnoseName", DbType = "varchar(500)", Storage = "_TentativeDiagnoseName", UpdateCheck = UpdateCheck.Never)]
		public string TentativeDiagnoseName
		{
			get { return _TentativeDiagnoseName; }
			set { _TentativeDiagnoseName = value; }
		}
		private string _TentativeDiagnoseID;
		/// <summary>
		/// TentativeDiagnoseID
		/// </summary>
		[Column(Name = "TentativeDiagnoseID", DbType = "varchar(500)", Storage = "_TentativeDiagnoseID", UpdateCheck = UpdateCheck.Never)]
		public string TentativeDiagnoseID
		{
			get { return _TentativeDiagnoseID; }
			set { _TentativeDiagnoseID = value; }
		}
		private string _AuxiliaryDiagnose;
		/// <summary>
		/// AuxiliaryDiagnose
		/// </summary>
		[Column(Name = "AuxiliaryDiagnose", DbType = "varchar(200)", Storage = "_AuxiliaryDiagnose", UpdateCheck = UpdateCheck.Never)]
		public string AuxiliaryDiagnose
		{
			get { return _AuxiliaryDiagnose; }
			set { _AuxiliaryDiagnose = value; }
		}
		private string _IllnessSort;
		/// <summary>
		/// IllnessSort
		/// </summary>
		[Column(Name = "IllnessSort", DbType = "varchar(50)", Storage = "_IllnessSort", UpdateCheck = UpdateCheck.Never)]
		public string IllnessSort
		{
			get { return _IllnessSort; }
			set { _IllnessSort = value; }
		}
		private string _DeathType;
		/// <summary>
		/// DeathType
		/// </summary>
		[Column(Name = "DeathType", DbType = "varchar(50)", Storage = "_DeathType", UpdateCheck = UpdateCheck.Never)]
		public string DeathType
		{
			get { return _DeathType; }
			set { _DeathType = value; }
		}
		private string _IsEyewitness;
		/// <summary>
		/// IsEyewitness
		/// </summary>
		[Column(Name = "IsEyewitness", DbType = "varchar(50)", Storage = "_IsEyewitness", UpdateCheck = UpdateCheck.Never)]
		public string IsEyewitness
		{
			get { return _IsEyewitness; }
			set { _IsEyewitness = value; }
		}
		private string _FirstAideEffect;
		/// <summary>
		/// FirstAideEffect
		/// </summary>
		[Column(Name = "FirstAideEffect", DbType = "varchar(50)", Storage = "_FirstAideEffect", UpdateCheck = UpdateCheck.Never)]
		public string FirstAideEffect
		{
			get { return _FirstAideEffect; }
			set { _FirstAideEffect = value; }
		}
		private string _IllnessChange;
		/// <summary>
		/// IllnessChange
		/// </summary>
		[Column(Name = "IllnessChange", DbType = "varchar(50)", Storage = "_IllnessChange", UpdateCheck = UpdateCheck.Never)]
		public string IllnessChange
		{
			get { return _IllnessChange; }
			set { _IllnessChange = value; }
		}
		private string _VisitsType;
		/// <summary>
		/// VisitsType
		/// </summary>
		[Column(Name = "VisitsType", DbType = "varchar(100)", Storage = "_VisitsType", UpdateCheck = UpdateCheck.Never)]
		public string VisitsType
		{
			get { return _VisitsType; }
			set { _VisitsType = value; }
		}
		private string _Measure;
		/// <summary>
		/// Measure
		/// </summary>
		[Column(Name = "Measure", DbType = "varchar(100)", Storage = "_Measure", UpdateCheck = UpdateCheck.Never)]
		public string Measure
		{
			get { return _Measure; }
			set { _Measure = value; }
		}
		private string _IsPatientOK;
		/// <summary>
		/// IsPatientOK
		/// </summary>
		[Column(Name = "IsPatientOK", DbType = "varchar(50)", Storage = "_IsPatientOK", UpdateCheck = UpdateCheck.Never)]
		public string IsPatientOK
		{
			get { return _IsPatientOK; }
			set { _IsPatientOK = value; }
		}
		private string _IsMedicalOK;
		/// <summary>
		/// IsMedicalOK
		/// </summary>
		[Column(Name = "IsMedicalOK", DbType = "varchar(50)", Storage = "_IsMedicalOK", UpdateCheck = UpdateCheck.Never)]
		public string IsMedicalOK
		{
			get { return _IsMedicalOK; }
			set { _IsMedicalOK = value; }
		}
		private string _IsDiagOK;
		/// <summary>
		/// IsDiagOK
		/// </summary>
		[Column(Name = "IsDiagOK", DbType = "varchar(50)", Storage = "_IsDiagOK", UpdateCheck = UpdateCheck.Never)]
		public string IsDiagOK
		{
			get { return _IsDiagOK; }
			set { _IsDiagOK = value; }
		}
		private string _Remark;
		/// <summary>
		/// Remark
		/// </summary>
		[Column(Name = "Remark", DbType = "varchar(1000)", Storage = "_Remark", UpdateCheck = UpdateCheck.Never)]
		public string Remark
		{
			get { return _Remark; }
			set { _Remark = value; }
		}
		private int? _FillPersonCode;
		/// <summary>
		/// FillPersonCode
		/// </summary>
		[Column(Name = "FillPersonCode", DbType = "int", Storage = "_FillPersonCode", UpdateCheck = UpdateCheck.Never)]
		public int? FillPersonCode
		{
			get { return _FillPersonCode; }
			set { _FillPersonCode = value; }
		}
		private DateTime? _RecordCreateTime;
		/// <summary>
		/// RecordCreateTime
		/// </summary>
		[Column(Name = "RecordCreateTime", DbType = "datetime", Storage = "_RecordCreateTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? RecordCreateTime
		{
			get { return _RecordCreateTime; }
			set { _RecordCreateTime = value; }
		}
		private DateTime? _RecordCompleteTime;
		/// <summary>
		/// RecordCompleteTime
		/// </summary>
		[Column(Name = "RecordCompleteTime", DbType = "datetime", Storage = "_RecordCompleteTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? RecordCompleteTime
		{
			get { return _RecordCompleteTime; }
			set { _RecordCompleteTime = value; }
		}
		private bool? _IsFormComplete;
		/// <summary>
		/// IsFormComplete
		/// </summary>
		[Column(Name = "IsFormComplete", DbType = "bit", Storage = "_IsFormComplete", UpdateCheck = UpdateCheck.Never)]
		public bool? IsFormComplete
		{
			get { return _IsFormComplete; }
			set { _IsFormComplete = value; }
		}
		private bool? _IsSubmit;
		/// <summary>
		/// IsSubmit
		/// </summary>
		[Column(Name = "IsSubmit", DbType = "bit", Storage = "_IsSubmit", UpdateCheck = UpdateCheck.Never)]
		public bool? IsSubmit
		{
			get { return _IsSubmit; }
			set { _IsSubmit = value; }
		}
		private DateTime? _SubmitTime;
		/// <summary>
		/// SubmitTime
		/// </summary>
		[Column(Name = "SubmitTime", DbType = "datetime", Storage = "_SubmitTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? SubmitTime
		{
			get { return _SubmitTime; }
			set { _SubmitTime = value; }
		}
		private string _LastModifyPerson;
		/// <summary>
		/// LastModifyPerson
		/// </summary>
		[Column(Name = "LastModifyPerson", DbType = "varchar(50)", Storage = "_LastModifyPerson", UpdateCheck = UpdateCheck.Never)]
		public string LastModifyPerson
		{
			get { return _LastModifyPerson; }
			set { _LastModifyPerson = value; }
		}
		private DateTime? _LastModifyTime;
		/// <summary>
		/// LastModifyTime
		/// </summary>
		[Column(Name = "LastModifyTime", DbType = "datetime", Storage = "_LastModifyTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? LastModifyTime
		{
			get { return _LastModifyTime; }
			set { _LastModifyTime = value; }
		}
		private double? _GlasgowScore;
		/// <summary>
		/// GlasgowScore
		/// </summary>
		[Column(Name = "GlasgowScore", DbType = "float", Storage = "_GlasgowScore", UpdateCheck = UpdateCheck.Never)]
		public double? GlasgowScore
		{
			get { return _GlasgowScore; }
			set { _GlasgowScore = value; }
		}
		private double? _EyeScore;
		/// <summary>
		/// EyeScore
		/// </summary>
		[Column(Name = "EyeScore", DbType = "float", Storage = "_EyeScore", UpdateCheck = UpdateCheck.Never)]
		public double? EyeScore
		{
			get { return _EyeScore; }
			set { _EyeScore = value; }
		}
		private double? _VerbalScore;
		/// <summary>
		/// VerbalScore
		/// </summary>
		[Column(Name = "VerbalScore", DbType = "float", Storage = "_VerbalScore", UpdateCheck = UpdateCheck.Never)]
		public double? VerbalScore
		{
			get { return _VerbalScore; }
			set { _VerbalScore = value; }
		}
		private double? _MotorScore;
		/// <summary>
		/// MotorScore
		/// </summary>
		[Column(Name = "MotorScore", DbType = "float", Storage = "_MotorScore", UpdateCheck = UpdateCheck.Never)]
		public double? MotorScore
		{
			get { return _MotorScore; }
			set { _MotorScore = value; }
		}
		private string _EyeContent;
		/// <summary>
		/// EyeContent
		/// </summary>
		[Column(Name = "EyeContent", DbType = "varchar(50)", Storage = "_EyeContent", UpdateCheck = UpdateCheck.Never)]
		public string EyeContent
		{
			get { return _EyeContent; }
			set { _EyeContent = value; }
		}
		private string _VerbalContent;
		/// <summary>
		/// VerbalContent
		/// </summary>
		[Column(Name = "VerbalContent", DbType = "varchar(50)", Storage = "_VerbalContent", UpdateCheck = UpdateCheck.Never)]
		public string VerbalContent
		{
			get { return _VerbalContent; }
			set { _VerbalContent = value; }
		}
		private string _MotorContent;
		/// <summary>
		/// MotorContent
		/// </summary>
		[Column(Name = "MotorContent", DbType = "varchar(50)", Storage = "_MotorContent", UpdateCheck = UpdateCheck.Never)]
		public string MotorContent
		{
			get { return _MotorContent; }
			set { _MotorContent = value; }
		}
		private double? _ThroughScore;
		/// <summary>
		/// ThroughScore
		/// </summary>
		[Column(Name = "ThroughScore", DbType = "float", Storage = "_ThroughScore", UpdateCheck = UpdateCheck.Never)]
		public double? ThroughScore
		{
			get { return _ThroughScore; }
			set { _ThroughScore = value; }
		}
		private double? _BreatheScore;
		/// <summary>
		/// BreatheScore
		/// </summary>
		[Column(Name = "BreatheScore", DbType = "float", Storage = "_BreatheScore", UpdateCheck = UpdateCheck.Never)]
		public double? BreatheScore
		{
			get { return _BreatheScore; }
			set { _BreatheScore = value; }
		}
		private double? _PressureScore;
		/// <summary>
		/// PressureScore
		/// </summary>
		[Column(Name = "PressureScore", DbType = "float", Storage = "_PressureScore", UpdateCheck = UpdateCheck.Never)]
		public double? PressureScore
		{
			get { return _PressureScore; }
			set { _PressureScore = value; }
		}
		private double? _MindScore;
		/// <summary>
		/// MindScore
		/// </summary>
		[Column(Name = "MindScore", DbType = "float", Storage = "_MindScore", UpdateCheck = UpdateCheck.Never)]
		public double? MindScore
		{
			get { return _MindScore; }
			set { _MindScore = value; }
		}
		private double? _PulseRateScore;
		/// <summary>
		/// PulseRateScore
		/// </summary>
		[Column(Name = "PulseRateScore", DbType = "float", Storage = "_PulseRateScore", UpdateCheck = UpdateCheck.Never)]
		public double? PulseRateScore
		{
			get { return _PulseRateScore; }
			set { _PulseRateScore = value; }
		}
		private double? _PHIScore;
		/// <summary>
		/// PHIScore
		/// </summary>
		[Column(Name = "PHIScore", DbType = "float", Storage = "_PHIScore", UpdateCheck = UpdateCheck.Never)]
		public double? PHIScore
		{
			get { return _PHIScore; }
			set { _PHIScore = value; }
		}
		private string _Status;
		/// <summary>
		/// Status
		/// </summary>
		[Column(Name = "Status", DbType = "char(1)", Storage = "_Status", UpdateCheck = UpdateCheck.Never)]
		public string Status
		{
			get { return _Status; }
			set { _Status = value; }
		}
		private int? _WorkFlowNo;
		/// <summary>
		/// WorkFlowNo
		/// </summary>
		[Column(Name = "WorkFlowNo", DbType = "int", Storage = "_WorkFlowNo", UpdateCheck = UpdateCheck.Never)]
		public int? WorkFlowNo
		{
			get { return _WorkFlowNo; }
			set { _WorkFlowNo = value; }
		}
		private string _OutBillNo;
		/// <summary>
		/// OutBillNo
		/// </summary>
		[Column(Name = "OutBillNo", DbType = "varchar(20)", Storage = "_OutBillNo", UpdateCheck = UpdateCheck.Never)]
		public string OutBillNo
		{
			get { return _OutBillNo; }
			set { _OutBillNo = value; }
		}
		private bool? _IsPrint;
		/// <summary>
		/// IsPrint
		/// </summary>
		[Column(Name = "IsPrint", DbType = "bit", Storage = "_IsPrint", UpdateCheck = UpdateCheck.Never)]
		public bool? IsPrint
		{
			get { return _IsPrint; }
			set { _IsPrint = value; }
		}
		private DateTime? _TaskCompleteTime;
		/// <summary>
		/// TaskCompleteTime
		/// </summary>
		[Column(Name = "TaskCompleteTime", DbType = "datetime", Storage = "_TaskCompleteTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? TaskCompleteTime
		{
			get { return _TaskCompleteTime; }
			set { _TaskCompleteTime = value; }
		}
		private string _IsPenetratWound;
		/// <summary>
		/// IsPenetratWound
		/// </summary>
		[Column(Name = "IsPenetratWound", DbType = "varchar(50)", Storage = "_IsPenetratWound", UpdateCheck = UpdateCheck.Never)]
		public string IsPenetratWound
		{
			get { return _IsPenetratWound; }
			set { _IsPenetratWound = value; }
		}
		private string _PadType;
		/// <summary>
		/// PadType
		/// </summary>
		[Column(Name = "PadType", DbType = "varchar(20)", Storage = "_PadType", UpdateCheck = UpdateCheck.Never)]
		public string PadType
		{
			get { return _PadType; }
			set { _PadType = value; }
		}
		private string _PatientType;
		/// <summary>
		/// PatientType
		/// </summary>
		[Column(Name = "PatientType", DbType = "varchar(20)", Storage = "_PatientType", UpdateCheck = UpdateCheck.Never)]
		public string PatientType
		{
			get { return _PatientType; }
			set { _PatientType = value; }
		}
		private string _Provider;
		/// <summary>
		/// Provider
		/// </summary>
		[Column(Name = "Provider", DbType = "varchar(20)", Storage = "_Provider", UpdateCheck = UpdateCheck.Never)]
		public string Provider
		{
			get { return _Provider; }
			set { _Provider = value; }
		}
		private string _DHeight;
		/// <summary>
		/// DHeight
		/// </summary>
		[Column(Name = "DHeight", DbType = "varchar(20)", Storage = "_DHeight", UpdateCheck = UpdateCheck.Never)]
		public string DHeight
		{
			get { return _DHeight; }
			set { _DHeight = value; }
		}
		private string _TrafficAccident;
		/// <summary>
		/// TrafficAccident
		/// </summary>
		[Column(Name = "TrafficAccident", DbType = "varchar(50)", Storage = "_TrafficAccident", UpdateCheck = UpdateCheck.Never)]
		public string TrafficAccident
		{
			get { return _TrafficAccident; }
			set { _TrafficAccident = value; }
		}
		private string _AgeUnknown;
		/// <summary>
		/// AgeUnknown
		/// </summary>
		[Column(Name = "AgeUnknown", DbType = "varchar(20)", Storage = "_AgeUnknown", UpdateCheck = UpdateCheck.Never)]
		public string AgeUnknown
		{
			get { return _AgeUnknown; }
			set { _AgeUnknown = value; }
		}
		private string _Report;
		/// <summary>
		/// Report
		/// </summary>
		[Column(Name = "Report", DbType = "varchar(20)", Storage = "_Report", UpdateCheck = UpdateCheck.Never)]
		public string Report
		{
			get { return _Report; }
			set { _Report = value; }
		}
		private string _Instruction;
		/// <summary>
		/// Instruction
		/// </summary>
		[Column(Name = "Instruction", DbType = "varchar(50)", Storage = "_Instruction", UpdateCheck = UpdateCheck.Never)]
		public string Instruction
		{
			get { return _Instruction; }
			set { _Instruction = value; }
		}
		private string _GiveUpReason;
		/// <summary>
		/// GiveUpReason
		/// </summary>
		[Column(Name = "GiveUpReason", DbType = "varchar(20)", Storage = "_GiveUpReason", UpdateCheck = UpdateCheck.Never)]
		public string GiveUpReason
		{
			get { return _GiveUpReason; }
			set { _GiveUpReason = value; }
		}
		private string _IllChangeRecord;
		/// <summary>
		/// IllChangeRecord
		/// </summary>
		[Column(Name = "IllChangeRecord", DbType = "varchar(200)", Storage = "_IllChangeRecord", UpdateCheck = UpdateCheck.Never)]
		public string IllChangeRecord
		{
			get { return _IllChangeRecord; }
			set { _IllChangeRecord = value; }
		}
		private string _Mark;
		/// <summary>
		/// Mark
		/// </summary>
		[Column(Name = "Mark", DbType = "varchar(20)", Storage = "_Mark", UpdateCheck = UpdateCheck.Never)]
		public string Mark
		{
			get { return _Mark; }
			set { _Mark = value; }
		}
		private string _Symptom;
		/// <summary>
		/// Symptom
		/// </summary>
		[Column(Name = "Symptom", DbType = "varchar(200)", Storage = "_Symptom", UpdateCheck = UpdateCheck.Never)]
		public string Symptom
		{
			get { return _Symptom; }
			set { _Symptom = value; }
		}
		private string _ReportR;
		/// <summary>
		/// ReportR
		/// </summary>
		[Column(Name = "ReportR", DbType = "varchar(50)", Storage = "_ReportR", UpdateCheck = UpdateCheck.Never)]
		public string ReportR
		{
			get { return _ReportR; }
			set { _ReportR = value; }
		}
		private string _Eyewitness;
		/// <summary>
		/// Eyewitness
		/// </summary>
		[Column(Name = "Eyewitness", DbType = "varchar(50)", Storage = "_Eyewitness", UpdateCheck = UpdateCheck.Never)]
		public string Eyewitness
		{
			get { return _Eyewitness; }
			set { _Eyewitness = value; }
		}
		private DateTime? _PrintTime;
		/// <summary>
		/// PrintTime
		/// </summary>
		[Column(Name = "PrintTime", DbType = "datetime", Storage = "_PrintTime", UpdateCheck = UpdateCheck.Never)]
		public DateTime? PrintTime
		{
			get { return _PrintTime; }
			set { _PrintTime = value; }
		}
		private string _EditCount;
		/// <summary>
		/// EditCount
		/// </summary>
		[Column(Name = "EditCount", DbType = "varchar(20)", Storage = "_EditCount", UpdateCheck = UpdateCheck.Never)]
		public string EditCount
		{
			get { return _EditCount; }
			set { _EditCount = value; }
		}
	}
}