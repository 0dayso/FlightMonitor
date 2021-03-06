﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3662
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3662.
// 
#pragma warning disable 1591

namespace AirSoft.FlightMonitor.FlightMonitorClient.FlightService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetStationFlightsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlightsByPositionOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetLastGuaranteeChangeRecordsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetChangeRecordsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetLastWatchChangeRecordsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::AirSoft.FlightMonitor.FlightMonitorClient.Properties.Settings.Default.AirSoft_FlightMonitor_FlightMonitorClient_FlightService_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event GetStationFlightsCompletedEventHandler GetStationFlightsCompleted;
        
        /// <remarks/>
        public event GetFlightsByPositionCompletedEventHandler GetFlightsByPositionCompleted;
        
        /// <remarks/>
        public event GetLastGuaranteeChangeRecordsCompletedEventHandler GetLastGuaranteeChangeRecordsCompleted;
        
        /// <remarks/>
        public event GetChangeRecordsCompletedEventHandler GetChangeRecordsCompleted;
        
        /// <remarks/>
        public event GetLastWatchChangeRecordsCompletedEventHandler GetLastWatchChangeRecordsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetStationFlights", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetStationFlights(DateTimeBM dateTimeBM, StationBM stationBM) {
            object[] results = this.Invoke("GetStationFlights", new object[] {
                        dateTimeBM,
                        stationBM});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetStationFlightsAsync(DateTimeBM dateTimeBM, StationBM stationBM) {
            this.GetStationFlightsAsync(dateTimeBM, stationBM, null);
        }
        
        /// <remarks/>
        public void GetStationFlightsAsync(DateTimeBM dateTimeBM, StationBM stationBM, object userState) {
            if ((this.GetStationFlightsOperationCompleted == null)) {
                this.GetStationFlightsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetStationFlightsOperationCompleted);
            }
            this.InvokeAsync("GetStationFlights", new object[] {
                        dateTimeBM,
                        stationBM}, this.GetStationFlightsOperationCompleted, userState);
        }
        
        private void OnGetStationFlightsOperationCompleted(object arg) {
            if ((this.GetStationFlightsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetStationFlightsCompleted(this, new GetStationFlightsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFlightsByPosition", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM) {
            object[] results = this.Invoke("GetFlightsByPosition", new object[] {
                        dateTimeBM,
                        positionNameBM});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetFlightsByPositionAsync(DateTimeBM dateTimeBM, PositionNameBM positionNameBM) {
            this.GetFlightsByPositionAsync(dateTimeBM, positionNameBM, null);
        }
        
        /// <remarks/>
        public void GetFlightsByPositionAsync(DateTimeBM dateTimeBM, PositionNameBM positionNameBM, object userState) {
            if ((this.GetFlightsByPositionOperationCompleted == null)) {
                this.GetFlightsByPositionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlightsByPositionOperationCompleted);
            }
            this.InvokeAsync("GetFlightsByPosition", new object[] {
                        dateTimeBM,
                        positionNameBM}, this.GetFlightsByPositionOperationCompleted, userState);
        }
        
        private void OnGetFlightsByPositionOperationCompleted(object arg) {
            if ((this.GetFlightsByPositionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlightsByPositionCompleted(this, new GetFlightsByPositionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLastGuaranteeChangeRecords", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM) {
            object[] results = this.Invoke("GetLastGuaranteeChangeRecords", new object[] {
                        iLastRecordNo,
                        dateTimeBM,
                        stationBM});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetLastGuaranteeChangeRecordsAsync(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM) {
            this.GetLastGuaranteeChangeRecordsAsync(iLastRecordNo, dateTimeBM, stationBM, null);
        }
        
        /// <remarks/>
        public void GetLastGuaranteeChangeRecordsAsync(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, object userState) {
            if ((this.GetLastGuaranteeChangeRecordsOperationCompleted == null)) {
                this.GetLastGuaranteeChangeRecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLastGuaranteeChangeRecordsOperationCompleted);
            }
            this.InvokeAsync("GetLastGuaranteeChangeRecords", new object[] {
                        iLastRecordNo,
                        dateTimeBM,
                        stationBM}, this.GetLastGuaranteeChangeRecordsOperationCompleted, userState);
        }
        
        private void OnGetLastGuaranteeChangeRecordsOperationCompleted(object arg) {
            if ((this.GetLastGuaranteeChangeRecordsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLastGuaranteeChangeRecordsCompleted(this, new GetLastGuaranteeChangeRecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetChangeRecords", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetChangeRecords(DateTimeBM dateTimeBM, StationBM stationBM) {
            object[] results = this.Invoke("GetChangeRecords", new object[] {
                        dateTimeBM,
                        stationBM});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetChangeRecordsAsync(DateTimeBM dateTimeBM, StationBM stationBM) {
            this.GetChangeRecordsAsync(dateTimeBM, stationBM, null);
        }
        
        /// <remarks/>
        public void GetChangeRecordsAsync(DateTimeBM dateTimeBM, StationBM stationBM, object userState) {
            if ((this.GetChangeRecordsOperationCompleted == null)) {
                this.GetChangeRecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetChangeRecordsOperationCompleted);
            }
            this.InvokeAsync("GetChangeRecords", new object[] {
                        dateTimeBM,
                        stationBM}, this.GetChangeRecordsOperationCompleted, userState);
        }
        
        private void OnGetChangeRecordsOperationCompleted(object arg) {
            if ((this.GetChangeRecordsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetChangeRecordsCompleted(this, new GetChangeRecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLastWatchChangeRecords", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM) {
            object[] results = this.Invoke("GetLastWatchChangeRecords", new object[] {
                        iLastRecordNo,
                        dateTimeBM,
                        positionNameBM});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetLastWatchChangeRecordsAsync(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM) {
            this.GetLastWatchChangeRecordsAsync(iLastRecordNo, dateTimeBM, positionNameBM, null);
        }
        
        /// <remarks/>
        public void GetLastWatchChangeRecordsAsync(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM, object userState) {
            if ((this.GetLastWatchChangeRecordsOperationCompleted == null)) {
                this.GetLastWatchChangeRecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLastWatchChangeRecordsOperationCompleted);
            }
            this.InvokeAsync("GetLastWatchChangeRecords", new object[] {
                        iLastRecordNo,
                        dateTimeBM,
                        positionNameBM}, this.GetLastWatchChangeRecordsOperationCompleted, userState);
        }
        
        private void OnGetLastWatchChangeRecordsOperationCompleted(object arg) {
            if ((this.GetLastWatchChangeRecordsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLastWatchChangeRecordsCompleted(this, new GetLastWatchChangeRecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3662")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class DateTimeBM {
        
        private string startDateTimeField;
        
        private string endDateTimeField;
        
        /// <remarks/>
        public string StartDateTime {
            get {
                return this.startDateTimeField;
            }
            set {
                this.startDateTimeField = value;
            }
        }
        
        /// <remarks/>
        public string EndDateTime {
            get {
                return this.endDateTimeField;
            }
            set {
                this.endDateTimeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3662")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class PositionNameBM {
        
        private int positionIDField;
        
        private string positionNameField;
        
        /// <remarks/>
        public int PositionID {
            get {
                return this.positionIDField;
            }
            set {
                this.positionIDField = value;
            }
        }
        
        /// <remarks/>
        public string PositionName {
            get {
                return this.positionNameField;
            }
            set {
                this.positionNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3662")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class StationBM {
        
        private int stationInforIdField;
        
        private string threeCodeField;
        
        private string stationNameField;
        
        private string airportNameField;
        
        private string commanderOfficeNameField;
        
        private string stationSignInFlagField;
        
        private int dayLineField;
        
        private int delayTimeLineField;
        
        private int joinTimeLineField;
        
        private int disconnectTimeLineField;
        
        /// <remarks/>
        public int StationInforId {
            get {
                return this.stationInforIdField;
            }
            set {
                this.stationInforIdField = value;
            }
        }
        
        /// <remarks/>
        public string ThreeCode {
            get {
                return this.threeCodeField;
            }
            set {
                this.threeCodeField = value;
            }
        }
        
        /// <remarks/>
        public string StationName {
            get {
                return this.stationNameField;
            }
            set {
                this.stationNameField = value;
            }
        }
        
        /// <remarks/>
        public string AirportName {
            get {
                return this.airportNameField;
            }
            set {
                this.airportNameField = value;
            }
        }
        
        /// <remarks/>
        public string CommanderOfficeName {
            get {
                return this.commanderOfficeNameField;
            }
            set {
                this.commanderOfficeNameField = value;
            }
        }
        
        /// <remarks/>
        public string StationSignInFlag {
            get {
                return this.stationSignInFlagField;
            }
            set {
                this.stationSignInFlagField = value;
            }
        }
        
        /// <remarks/>
        public int DayLine {
            get {
                return this.dayLineField;
            }
            set {
                this.dayLineField = value;
            }
        }
        
        /// <remarks/>
        public int DelayTimeLine {
            get {
                return this.delayTimeLineField;
            }
            set {
                this.delayTimeLineField = value;
            }
        }
        
        /// <remarks/>
        public int JoinTimeLine {
            get {
                return this.joinTimeLineField;
            }
            set {
                this.joinTimeLineField = value;
            }
        }
        
        /// <remarks/>
        public int DisconnectTimeLine {
            get {
                return this.disconnectTimeLineField;
            }
            set {
                this.disconnectTimeLineField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetStationFlightsCompletedEventHandler(object sender, GetStationFlightsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetStationFlightsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetStationFlightsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetFlightsByPositionCompletedEventHandler(object sender, GetFlightsByPositionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlightsByPositionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlightsByPositionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetLastGuaranteeChangeRecordsCompletedEventHandler(object sender, GetLastGuaranteeChangeRecordsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLastGuaranteeChangeRecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetLastGuaranteeChangeRecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetChangeRecordsCompletedEventHandler(object sender, GetChangeRecordsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetChangeRecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetChangeRecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetLastWatchChangeRecordsCompletedEventHandler(object sender, GetLastWatchChangeRecordsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLastWatchChangeRecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetLastWatchChangeRecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591