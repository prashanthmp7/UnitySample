using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitySample.Common.Consts
{
    public static class Consts
    {
        /// <summary>
        /// Gets the guid of the functionDomainObjectGuid 
        /// </summary>
        public static Guid FunctionDomainObjectGuid = Guid.Parse("DD36C94E-3F35-42D0-BDC6-F1408211E8AB");

        /// <summary>
        /// Gets the guid of the labelDomainObjectGuid 
        /// </summary>
        public static Guid LabelDomainObjectGuid = Guid.Parse("E21442C4-9BAC-4DFF-9150-36151C47B484");

        /// <summary>
        /// Gets the guid of the admin
        /// </summary>
        public static Guid AdminGuid = Guid.Parse("310357DE-D49D-4FD6-B954-656C88D40BBD");

        /// <summary>
        /// Gets the guid of the guest user
        /// </summary>
        public static Guid GuestGuid = Guid.Parse("EB65B39C-B439-4A35-BD73-B17737775460");

        /// <summary>
        /// Gets the guid of the coordinator user
        /// </summary>
        public static Guid CoordinatorGuid = Guid.Parse("dc6e5460-34f3-4efa-a0c8-750e859eded3");

        /// <summary>
        /// Gets the guid of the Applicator user
        /// </summary>
        public static Guid ApplicatorGuid = Guid.Parse("0bdd2b99-6399-4579-98c2-263b1645489c");

        /// <summary>
        /// Gets the guid of the Applicator user
        /// </summary>
        public static Guid GroupUserGuid = Guid.Parse("3ee10192-4f5c-44d5-9447-b89e164343b6");

        /// <summary>
        /// The akv change dictionary
        /// </summary>
        public static string AkvChangeDictionary = "220d7979-853a-4b72-b867-fd41e2a43071";

        /// <summary>
        /// The sync scope
        /// </summary>
        public static string DpeSyncScope = "DPESyncScope";

        public static string EditUserView = "43087401-378B-4E3B-AA5B-44BBEC66B8C5";

        public static string ImportedSoftware = "017ACC2C-A6FE-4EFB-8E9B-0D831BDD5BDE";

        public static string CreateUser = "2F5A0E71-FE66-4821-931C-3D0F5C9BC660";

        /// <summary>
        /// The sync scope
        /// </summary>
        public static string OplSyncScope = "OPLSyncScope";

        /// <summary>
        /// The sync filter
        /// </summary>
        public static string OplSyncFilterTemplate = "OPLFilterTemplate";

        /// <summary>
        /// The opl filtered scope
        /// </summary>
        public static string OplSyncFilteredScopeFormat = "{0}_Scope";


        /// <summary>
        /// The pin board title for akv
        /// </summary>
        public static string PinAKVTitle = "PinAKVTitle";


        /// <summary>
        /// The pin board title for release 
        /// </summary>
        public static string PinReleaseTitle = "PinReleaseTitle";


        /// <summary>
        /// The synchronize prefix
        /// </summary>
        public static string SyncPrefix = "sync";

        /// <summary>
        /// The synchronize prefix
        /// </summary>
        public static string SyncOneWayPrefix = "syncOneWay";

        /// <summary>
        /// The scope for one way sync
        /// </summary>
        public static string ScopeSyncOneWay = "ScopeSyncOneWay";

        /// <summary>
        /// The release tree function cluster column
        /// </summary>
        public static string ReleaseTreeFunctionClusterColumn = "ReleaseTreeFunctionClusterColumn";

        /// <summary>
        /// The release tree function column
        /// </summary>
        public static string ReleaseTreeFunctionColumn = "ReleaseTreeFunctionColumn";

        /// <summary>
        /// The release tree label column
        /// </summary>
        public static string ReleaseTreeLabelColumn1 = "ReleaseTreeLabelColumn1";

        /// <summary>
        /// The release tree label column2
        /// </summary>
        public static string ReleaseTreeLabelColumn2 = "ReleaseTreeLabelColumn2";

        /// <summary>
        /// The release tree label cluster column
        /// </summary>
        public static string ReleaseTreeLabelClusterColumn = "ReleaseTreeLabelClusterColumn";

        /// <summary>
        /// HIer werden die Urls eingetragen
        /// </summary>
        public static class Urls
        {
            /// <summary>
            /// The endpoint configuration name
            /// </summary>
            public const string EndpointConfigurationName = "*";
        }

        /// <summary>
        /// Gets or sets the on behalf user.
        /// </summary>
        /// <value>
        /// The on behalf user.
        /// </value>
        public static string OnBehalfUser { get; set; }

        /// <summary>
        /// Gets or sets the start up page.
        /// </summary>
        /// <value>
        /// The start up page.
        /// </value>
        public static string StartUpPage { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public static Guid UserId { get; set; }

        /// <summary>
        /// The service security context
        /// </summary>
        public static string ServiceSecurityContext = "ServiceSecurityContext";

        /// <summary>
        /// The Rolechange Duration
        /// </summary>
        public static string Rolechange = "Rolechange";

        /// <summary>
        /// The Neutralparametrization Duration
        /// </summary>
        public static string Neutralparametrization = "Neutralparametrization";

        /// <summary>
        /// The tile refresh interval
        /// </summary>
        public static string TileRefreshInterval = "TileRefreshIntervalInSecond";

        /// <summary>
        /// The new release valid days for the AKV
        /// </summary>
        public static string NewReleaseAKVValidDays = "NewReleaseAKVValidDays";

        /// <summary>
        /// The new successorSoftware valid Days
        /// </summary>
        public static string NewSuccessorSoftwareValidDays = "NewSuccessorSoftwareValidDays";

        /// <summary>
        /// The cancelLabel workflow valid days
        /// </summary>
        public static string CancelLabelWorkflowValidDays = "CancelLabelWorkflowValidDays";

        /// <summary>
        /// The TicketChanged valid days
        /// </summary>
        public static string TicketChangedValidDays = "TicketChangedValidDays";

        /// <summary>
        /// The akv memory cache expiration time
        /// </summary>
        public static string AKVMemoryCacheExpirationInMinutes = "AKVMemoryCacheExpirationInMinutes";


        /// <summary>
        /// The ReactivateUser request deadline
        /// </summary>
        public static string ReactivateUserDeadlineInDays = "ReactivateUserDeadlineInDays";

        /// <summary>
        /// The RequestUserAccount deadline
        /// </summary>
        public static string RequestUserAccountDeadlineInDays = "RequestUserAccountDeadlineInDays";

        /// <summary>
        /// The maximum cache item count
        /// </summary>
        public static string MaxCacheItemCount = "MaxCacheItemCount";

        /// <summary>
        /// The synchronisation interval in minutes
        /// </summary>
        public static string SynchronisationIntervalMinutes = "SynchronisationIntervalMinutes";

        /// <summary>
        /// DateTime when the DPE database was originally created
        /// </summary>
        public static string DatabaseCreationDate = "DatabaseCreationDate";

        ///<summary>
        ///Selected software project from software list view.
        /// </summary>
        public static string SelectedSoftwareProject = "SelectedSoftwareProject";

        /// <summary>
        /// The NoLabel 
        /// </summary>
        public static string NoLabel = "<Kein Label enthalten>";

        /// <summary>
        /// The NoLabelDisplay 
        /// </summary>
        public static string NoLabelDisplay = string.Format("<{0}>", NoLabel);

        /// <summary>
        /// Konstanten für Module
        /// </summary>
        public static class Modules
        {
            /// <summary>
            /// The software management module
            /// </summary>
            public const string SoftwareManagementModule = "SoftwareManagementModule";

            /// <summary>
            /// The resource module
            /// </summary>
            public const string ResourceModule = "ResourceModule";

            /// <summary>
            /// The open item module
            /// </summary>
            public const string OpenItemModule = "OpenItemModule";

            /// <summary>
            /// The user module
            /// </summary>
            public const string UserModule = "UserModule";

            /// <summary>
            /// The master data module
            /// </summary>
            public const string MasterDataModule = "MasterDataModule";

            /// <summary>
            /// The settings data module
            /// </summary>
            public const string SettingsDataModule = "SettingsDataModule";

            /// <summary>
            /// The AKV module
            /// </summary>
            public const string AKVModule = "AKVModule";

            /// <summary>
            /// The Workflow module
            /// </summary>
            public const string WorkflowModule = "WorkflowModule";

            /// <summary>
            /// The Ticket module
            /// </summary>
            public const string TicketModule = "TicketModule";

            /// <summary>
            /// The Pinboard module
            /// </summary>
            public const string PinboardModule = "PinboardModule";

            /// <summary>
            /// The QGroup module
            /// </summary>
            public const string QgroupModule = "QgroupModule";
        }

        /// <summary>
        /// Konstanten für die Namen der Views
        /// </summary>
        public class ViewNames
        {
            /// <summary>
            /// The resource list
            /// </summary>
            public static string ResourceList = "ResourceList";

            /// <summary>
            /// The open item list
            /// </summary>
            public static string OpenItemList = "OpenItemList";

            /// <summary>
            /// The user list
            /// </summary>
            public static string ListUser = "ListUser";

            /// <summary>
            /// The create user view
            /// </summary>
            public const string CreateUserView = "CreateUserView";

            /// <summary>
            /// The role list
            /// </summary>
            public static string ListRole = "ListRole";

            /// <summary>
            /// The project view
            /// </summary>
            public static string Project = "Project";

            /// <summary>
            /// The start view
            /// </summary>
            public static string StartView = "StartView";

            /// <summary>
            /// The criteria view
            /// </summary>
            public static string ListCriteria = "CriteriaList";

            /// <summary>
            /// The labelcluster view
            /// </summary>
            public static string ListLabelCluster = "LabelClusterList";

            /// <summary>
            /// Gets or sets the create gewerk user view.
            /// </summary>
            /// <value>
            /// The create gewerk user view.
            /// </value>
            public static string CreateGewerkUserView = "CreateGewerkUserView";

            /// <summary>
            /// The create resource
            /// </summary>
            public static string CreateResource = "CreateResource";

            /// <summary>
            /// The user settings
            /// </summary>
            public static string UserSettings = "Profile";

            /// <summary>
            /// The akv view
            /// </summary>
            public static string AKVView = "AKVView";

            /// <summary>
            /// The akv approval
            /// </summary>
            public static string AKVApproval = "AKVApprovalView";

            /// <summary>
            /// The active directory tool
            /// </summary>
            public static string ActiveDirectoryTool = "ActiveDirectoryTool";

            /// <summary>
            /// The function cluster view
            /// </summary>
            public static string ListFunctionCluster = "FunctionClusterList";

            /// <summary>
            /// The import software
            /// </summary>
            public static string ImportSoftware = "ImportSoftware";

            /// <summary>
            /// The software list
            /// </summary>
            public static string SoftwareList = "SoftwareList";

            /// <summary>
            /// The software view
            /// </summary>
            public static string SoftwareView = "SoftwareView";

            /// <summary>
            /// The software view
            /// </summary>
            public static string SoftwareComparison = "SoftwareComparison";

            /// <summary>
            /// The akv wizard view
            /// </summary>
            public static string AKVWizardView = "AKVWizardView";

            /// <summary>
            /// The clone akv wizard view
            /// </summary>
            public static string CloneAKVWizardView = "CloneAKVWizardView";

            /// <summary>
            /// The creta edit view
            /// </summary>
            public static string CretaEditView = "CretaEditView";

            /// <summary>
            /// The dd man edit view
            /// </summary>
            public static string DDManEditView = "DDManEdit";

            /// <summary>
            /// The information board view
            /// </summary>
            public static string InformationBoardView = "NIL";

            /// <summary>
            /// The admin release schedule view
            /// </summary>
            public static string AdminReleaseScheduleView = "AdminReleaseScheduleView";

            /// <summary>
            /// The akv selection view.
            /// </summary>
            public static string AKVSelection = "AKVSelection";

            /// <summary>
            /// The akv instance selection view.
            /// </summary>
            public static string AKVList = "AKVList";

            /// <summary>
            /// The approval selection
            /// </summary>
            public static string ApprovalSelection = "ApprovalSelection";

            /// <summary>
            /// The Software Release Selection.
            /// </summary>
            public static string SWReleaseSelection = "SWReleaseSelection";

            /// <summary>
            /// Gets or sets <see cref="TicketList"/>
            /// </summary>
            public static string TicketList = "TicketList";

            /// <summary>
            /// The create ticket
            /// </summary>
            public static string CreateTicketView = "CreateTicketView";

            /// <summary>
            /// The AKV Labels Edit View
            /// </summary>
            public static string AKVLabelsEditView = "AKVLabelsEditView";

            /// <summary>
            /// The AKV Functions Edit View
            /// </summary>
            public static string AKVFunctionsEditView = "AKVFunctionsEditView";

            /// <summary>
            /// The User Import Map View
            /// </summary>
            public static string UserImportMapView = "UserImportMapView";

            /// <summary>
            /// The MasterData list
            /// </summary>
            public static string MasterDataList = "MasterDataList";

            /// <summary>
            /// The create MasterData
            /// </summary>
            public static string CreateMasterData = "CreateMasterData";

            /// <summary>
            /// The Pinboard view
            /// </summary>
            public static string PinboardView = "PinboardView";

            /// <summary>
            /// The ChangeLogHistory View
            /// </summary>
            public static string ChangeLogHistoryView = "ChangeLogHistoryView";

            /// <summary>
            /// The Schedule Item Edit View
            /// </summary>
            public static string ScheduleItemEditView = "ScheduleItemEditView";

            /// <summary>
            /// The Q-Gruppe dashboard View
            /// </summary>
            public static string QgruppeDashboard = "QgroupDashboard";
        }


        /// <summary>
        /// Stringkonstanten für Regions
        /// </summary>
        public static class Regions
        {
            /// <summary>
            /// The main region
            /// </summary>
            public const string MainRegion = "MainWindow";
        }

        /// <summary>
        /// States for the Entities
        /// </summary>
        public enum EntityState
        {
            /// <summary>
            /// The created state
            /// </summary>
            Created,

            /// <summary>
            /// The loaded state
            /// </summary>
            Unchanged,

            /// <summary>
            /// The modified state
            /// </summary>
            Modified,

            /// <summary>
            /// The deleted state
            /// </summary>
            Deleted
        }

        /// <summary>
        /// Enum for Faultexception typ
        /// </summary>
        public enum FaultExceptionEnum
        {
            /// <summary>
            /// The default
            /// </summary>
            Default = 0,

            /// <summary>
            /// The unknow user
            /// </summary>
            UnknownUser,

            /// <summary>
            /// The deactived user
            /// </summary>
            DeactivedUser,

            /// <summary>
            /// Error initialiazing client
            /// </summary>
            InitializationError
        }

        public enum OnlineMode
        {
            /// <summary>
            /// The online
            /// </summary>
            Online,

            /// <summary>
            /// The offline
            /// </summary>
            Offline,
            /// <summary>
            /// The none
            /// </summary>
            None
        }
    }
}
