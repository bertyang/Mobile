using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Anchor.FA.Model
{
    public class MainDataContext : DataContext
    {
        #region Model
        #region Table
        public Table<B_ACTION> B_ACTION;
        public Table<B_ORGANIZATION> B_ORGANIZATION;
        //public Table<B_POST> B_POST;
        public Table<B_Range> B_Range;
        public Table<B_LOGIN_LOG> B_LOGIN_LOG;
        public Table<B_Remind> B_Remind;
        public Table<B_ROLE> B_ROLE;
        public Table<B_ROLE_ACTION> B_ROLE_ACTION;
        public Table<B_ROLE_Range> B_ROLE_Range;
        public Table<B_WORKER> B_WORKER;
        public Table<B_WORKER_ORGANIZATION> B_WORKER_ORGANIZATION;
        public Table<B_WORKER_ROLE> B_WORKER_ROLE;
        public Table<B_TELTYPE_CUSTOM> B_TELTYPE_CUSTOM;
        public Table<E_Mail> E_Mail;
        public Table<E_Mail_Attachment> E_Mail_Attachment;
        public Table<E_Mail_Worker> E_Mail_Worker;
        public Table<F_ACTIVITY> F_ACTIVITY;
        public Table<F_CATALOG> F_CATALOG;
        public Table<F_CONDITION> F_CONDITION;
        public Table<F_FLOW> F_FLOW;
        public Table<F_FLOW_CONFIG> F_FLOW_CONFIG;
        public Table<F_INST_ACTIVITY> F_INST_ACTIVITY;
        public Table<F_INST_ATTACHMENT> F_INST_ATTACHMENT;
        public Table<F_INST_FLOW> F_INST_FLOW;
        public Table<F_INST_NOTICE> F_INST_NOTICE;
        public Table<F_INST_TRANSATION> F_INST_TRANSATION;
        public Table<F_INST_WORKITEM> F_INST_WORKITEM;
        public Table<F_PARTICIPANT> F_PARTICIPANT;
        public Table<F_PARTICIPANT_FIELD> F_PARTICIPANT_FIELD;
        public Table<F_PARTICIPANT_LEVEL> F_PARTICIPANT_LEVEL;
        public Table<F_PARTICIPANT_ORG> F_PARTICIPANT_ORG;
        public Table<F_PARTICIPANT_POST> F_PARTICIPANT_POST;
        public Table<F_PARTICIPANT_RELATION> F_PARTICIPANT_RELATION;
        public Table<F_RETURN_CONFIG> F_RETURN_CONFIG;
        public Table<F_TRANSITION> F_TRANSITION;
        public Table<G_CONFIG> G_CONFIG;
        public Table<G_DATA> G_DATA;
        public Table<G_KEYVALUE> G_KEYVALUE;
        public Table<M_PatientRecord> M_PatientRecord;
        public Table<TChargeItem> TChargeItem;
        public Table<TChargeRecordDetail> TChargeRecordDetail;
        public Table<TChargeRecordMain> TChargeRecordMain;
        public Table<TCureMeasure> TCureMeasure;
        public Table<TCureRule> TCureRule;
        public Table<TDanger> TDanger;
        public Table<TDictionary> TDictionary;
        public Table<TDictionaryType> TDictionaryType;
        public Table<TOffice> TOffice;
        public Table<TOfficeAttachment> TOfficeAttachment;
        public Table<TOfficeReceive> TOfficeReceive;
        public Table<TOfficeSend> TOfficeSend;
        public Table<TPatientRecord> TPatientRecord;
        public Table<TPatientRecordDiagnosis> TPatientRecordDiagnosis;
        public Table<TPatientRecordMeasure> TPatientRecordMeasure;
        public Table<TPatientRecordTgjc> TPatientRecordTgjc;
        public Table<TPatientRecordWsjc> TPatientRecordWsjc;
        public Table<TPoison> TPoison;
        public Table<TWeiGui> TWeiGui;
        public Table<TZICD> TZICD;
        public Table<TZCenterType> TZCenterType;
        public Table<TZStationType> TZStationType;
        #endregion
        #region View
        public Table<TAcceptEvent> TAcceptEvent;
        public Table<TAccidentPatient> TAccidentPatient;
        public Table<TAccidentReport> TAccidentReport;
        public Table<TAlarmCall> TAlarmCall;
        public Table<TAlarmCallOther> TAlarmCallOther;
        public Table<TAlarmEvent> TAlarmEvent;
        public Table<TAmbulance> TAmbulance;
        public Table<TAmbulancePersonSign> TAmbulancePersonSign;
        public Table<TAmbulanceStateTime> TAmbulanceStateTime;
        public Table<TBackCall> TBackCall;
        public Table<TBackCallAudit> TBackCallAudit;
        public Table<TBackCallRecordLink> TBackCallRecordLink;
        public Table<TBackCallSM> TBackCallSM;
        public Table<TBlackList> TBlackList;
        public Table<TCenter> TCenter;
        public Table<TDesk> TDesk;
        public Table<THospitalInfo> THospitalInfo;
        public Table<TModifyRecord> TModifyRecord;
        public Table<TNotice> TNotice;
        public Table<TNoticeLog> TNoticeLog;
        public Table<TOperatorSign> TOperatorSign;
        public Table<TOperatorSignNew> TOperatorSignNew;
        public Table<TParameterAcceptInfo> TParameterAcceptInfo;
        public Table<TPauseRecord> TPauseRecord;
        public Table<TPerson> TPerson;
        public Table<TRole> TRole;
        public Table<TStation> TStation;
        public Table<TTask> TTask;
        public Table<TTaskPersonLink> TTaskPersonLink;
        public Table<TTelBook> TTelBook;
        public Table<TTelLog> TTelLog;
        public Table<TTelQueue> TTelQueue;
        public Table<TZAcceptEventType> TZAcceptEventType;
        public Table<TZAccidentLevel> TZAccidentLevel;
        public Table<TZAccidentType> TZAccidentType;
        public Table<TZAge> TZAge;
        public Table<TZAlarmCallType> TZAlarmCallType;
        public Table<TZAlarmEventOrigin> TZAlarmEventOrigin;
        public Table<TZAlarmEventType> TZAlarmEventType;
        public Table<TZAlarmReason> TZAlarmReason;
        public Table<TZAmbulanceGroup> TZAmbulanceGroup;
        public Table<TZAmbulanceLevel> TZAmbulanceLevel;
        public Table<TZAmbulanceState> TZAmbulanceState;
        public Table<TZAmbulanceType> TZAmbulanceType;
        public Table<TZArea> TZArea;
        public Table<TZCallType> TZCallType;
        public Table<TZDeskType> TZDeskType;
        public Table<TZDropReason> TZDropReason;
        public Table<TZFolk> TZFolk;
        public Table<TZHangUpReason> TZHangUpReason;
        public Table<TZIllState> TZIllState;
        public Table<TZLocalAddrType> TZLocalAddrType;
        public Table<TZNational> TZNational;
        public Table<TZNoticeType> TZNoticeType;
        public Table<TZOperationOrigin> TZOperationOrigin;
        public Table<TZPauseReason> TZPauseReason;
        public Table<TZRejectReason> TZRejectReason;
        public Table<TZSendAddrType> TZSendAddrType;
        public Table<TZTaskAbendReason> TZTaskAbendReason;
        public Table<TZTelLogOperator> TZTelLogOperator;
        public Table<TZTelLogRecordType> TZTelLogRecordType;
        public Table<TZTelLogResult> TZTelLogResult;
        public Table<TZTelType> TZTelType;

        public Table<TZModifyRecordType> TZModifyRecordType;
        public Table<TZCommandAspect> TZCommandAspect;
        public Table<TZBranch> TZBranch;
        public Table<TStationMsg> TStationMsg;
        public Table<TZStationMsgType> TZStationMsgType;


        public Table<TZSpecialRequest> TZSpecialRequest;
        public Table<TZBackupOne> TZBackupOne;
        public Table<TZBackupTwo> TZBackupTwo;

        #endregion
        #endregion


        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        public MainDataContext() :
            base(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, mappingSource)
        {
        }
        public MainDataContext(string connection) : base(connection, mappingSource) { }
        public MainDataContext(IDbConnection con) : base(con, mappingSource) { }
    }
}
