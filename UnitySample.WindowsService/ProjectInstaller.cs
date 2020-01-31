using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace UnitySample.TestService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : BaseWindowsServiceInstaller
    {
        public ProjectInstaller() : base(
                description: "Test service to check memory leak",
                displayName: "TestService",
                serviceName: "Test-Service",
                dependsOn: new string[] {})
        {
            base.InitializeComponent();
        }
    }

    public class BaseWindowsServiceInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BaseWindowsServiceInstaller()
        {

        }

        public BaseWindowsServiceInstaller(string description, string displayName, string serviceName, string[] dependsOn)
        {
            this.Description = description;
            this.DisplayName = displayName;
            this.ServiceName = serviceName;
            this.DependsOn = dependsOn;
        }

        public string ServiceName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string[] DependsOn { get; set; }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this._serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this._serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller
            // 
            this._serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this._serviceProcessInstaller.Password = null;
            this._serviceProcessInstaller.Username = null;
            // 
            // serviceInstaller
            // 
            this._serviceInstaller.Description = Description;
            this._serviceInstaller.DisplayName = DisplayName;
            this._serviceInstaller.ServiceName = ServiceName;
            this._serviceInstaller.ServicesDependedOn = DependsOn;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this._serviceProcessInstaller,
            this._serviceInstaller});

        }

        #endregion

        /// <summary>
        /// Overridden method - gets called while installing service
        /// </summary>
        /// <param name="stateSaver"></param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            RetrieveServiceName();
            base.Install(stateSaver);
        }

        /// <summary>
        /// Overridden method - gets called while uninstalling service
        /// </summary>
        /// <param name="savedState"></param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            RetrieveServiceName();
            base.Uninstall(savedState);
        }

        private void RetrieveServiceName()
        {
            var serviceName = Context.Parameters["servicename"];
            if (!string.IsNullOrEmpty(serviceName))
            {
                this._serviceInstaller.ServiceName = serviceName;
                this._serviceInstaller.DisplayName = serviceName;
            }
        }

        private System.ServiceProcess.ServiceProcessInstaller _serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller _serviceInstaller;
    }
}
