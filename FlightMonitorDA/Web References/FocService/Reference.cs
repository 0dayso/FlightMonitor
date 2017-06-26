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

namespace AirSoft.FlightMonitor.FlightMonitorDA.FocService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="FleetWatchSoap", Namespace="http://tempuri.org/")]
    public partial class FleetWatch : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetCrewLegsInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetLegsInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback MatchDateTimeOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertGateOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlightNumOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlightDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetStationScheduleOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMaintanceInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlightInfoByMCCDelayOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVIPInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback ClearHainan_crew_legsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FleetWatch() {
            this.Url = global::AirSoft.FlightMonitor.FlightMonitorDA.Properties.Settings.Default.AirSoft_FlightMonitor_FlightMonitorDA_FocService_FleetWatch;
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
        public event GetCrewLegsInfoCompletedEventHandler GetCrewLegsInfoCompleted;
        
        /// <remarks/>
        public event GetLegsInfoCompletedEventHandler GetLegsInfoCompleted;
        
        /// <remarks/>
        public event MatchDateTimeCompletedEventHandler MatchDateTimeCompleted;
        
        /// <remarks/>
        public event InsertGateCompletedEventHandler InsertGateCompleted;
        
        /// <remarks/>
        public event InsertMessageCompletedEventHandler InsertMessageCompleted;
        
        /// <remarks/>
        public event GetFlightNumCompletedEventHandler GetFlightNumCompleted;
        
        /// <remarks/>
        public event GetFlightDataCompletedEventHandler GetFlightDataCompleted;
        
        /// <remarks/>
        public event GetStationScheduleCompletedEventHandler GetStationScheduleCompleted;
        
        /// <remarks/>
        public event GetMaintanceInfoCompletedEventHandler GetMaintanceInfoCompleted;
        
        /// <remarks/>
        public event GetFlightInfoByMCCDelayCompletedEventHandler GetFlightInfoByMCCDelayCompleted;
        
        /// <remarks/>
        public event GetVIPInfoCompletedEventHandler GetVIPInfoCompleted;
        
        /// <remarks/>
        public event ClearHainan_crew_legsCompletedEventHandler ClearHainan_crew_legsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCrewLegsInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void GetCrewLegsInfo() {
            this.Invoke("GetCrewLegsInfo", new object[0]);
        }
        
        /// <remarks/>
        public void GetCrewLegsInfoAsync() {
            this.GetCrewLegsInfoAsync(null);
        }
        
        /// <remarks/>
        public void GetCrewLegsInfoAsync(object userState) {
            if ((this.GetCrewLegsInfoOperationCompleted == null)) {
                this.GetCrewLegsInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCrewLegsInfoOperationCompleted);
            }
            this.InvokeAsync("GetCrewLegsInfo", new object[0], this.GetCrewLegsInfoOperationCompleted, userState);
        }
        
        private void OnGetCrewLegsInfoOperationCompleted(object arg) {
            if ((this.GetCrewLegsInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCrewLegsInfoCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLegsInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetLegsInfo(string FltId, string STDStart, string STDEnd, string DepStn, string ArrStn, string LongReg, int iDataSet) {
            object[] results = this.Invoke("GetLegsInfo", new object[] {
                        FltId,
                        STDStart,
                        STDEnd,
                        DepStn,
                        ArrStn,
                        LongReg,
                        iDataSet});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetLegsInfoAsync(string FltId, string STDStart, string STDEnd, string DepStn, string ArrStn, string LongReg, int iDataSet) {
            this.GetLegsInfoAsync(FltId, STDStart, STDEnd, DepStn, ArrStn, LongReg, iDataSet, null);
        }
        
        /// <remarks/>
        public void GetLegsInfoAsync(string FltId, string STDStart, string STDEnd, string DepStn, string ArrStn, string LongReg, int iDataSet, object userState) {
            if ((this.GetLegsInfoOperationCompleted == null)) {
                this.GetLegsInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLegsInfoOperationCompleted);
            }
            this.InvokeAsync("GetLegsInfo", new object[] {
                        FltId,
                        STDStart,
                        STDEnd,
                        DepStn,
                        ArrStn,
                        LongReg,
                        iDataSet}, this.GetLegsInfoOperationCompleted, userState);
        }
        
        private void OnGetLegsInfoOperationCompleted(object arg) {
            if ((this.GetLegsInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLegsInfoCompleted(this, new GetLegsInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/MatchDateTime", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet MatchDateTime(string PlanDate, string FlyNo) {
            object[] results = this.Invoke("MatchDateTime", new object[] {
                        PlanDate,
                        FlyNo});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void MatchDateTimeAsync(string PlanDate, string FlyNo) {
            this.MatchDateTimeAsync(PlanDate, FlyNo, null);
        }
        
        /// <remarks/>
        public void MatchDateTimeAsync(string PlanDate, string FlyNo, object userState) {
            if ((this.MatchDateTimeOperationCompleted == null)) {
                this.MatchDateTimeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnMatchDateTimeOperationCompleted);
            }
            this.InvokeAsync("MatchDateTime", new object[] {
                        PlanDate,
                        FlyNo}, this.MatchDateTimeOperationCompleted, userState);
        }
        
        private void OnMatchDateTimeOperationCompleted(object arg) {
            if ((this.MatchDateTimeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.MatchDateTimeCompleted(this, new MatchDateTimeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertGate", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int InsertGate(string msgText) {
            object[] results = this.Invoke("InsertGate", new object[] {
                        msgText});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void InsertGateAsync(string msgText) {
            this.InsertGateAsync(msgText, null);
        }
        
        /// <remarks/>
        public void InsertGateAsync(string msgText, object userState) {
            if ((this.InsertGateOperationCompleted == null)) {
                this.InsertGateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertGateOperationCompleted);
            }
            this.InvokeAsync("InsertGate", new object[] {
                        msgText}, this.InsertGateOperationCompleted, userState);
        }
        
        private void OnInsertGateOperationCompleted(object arg) {
            if ((this.InsertGateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertGateCompleted(this, new InsertGateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int InsertMessage(string tstAmp, string origin, string msgType, string msgText) {
            object[] results = this.Invoke("InsertMessage", new object[] {
                        tstAmp,
                        origin,
                        msgType,
                        msgText});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void InsertMessageAsync(string tstAmp, string origin, string msgType, string msgText) {
            this.InsertMessageAsync(tstAmp, origin, msgType, msgText, null);
        }
        
        /// <remarks/>
        public void InsertMessageAsync(string tstAmp, string origin, string msgType, string msgText, object userState) {
            if ((this.InsertMessageOperationCompleted == null)) {
                this.InsertMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertMessageOperationCompleted);
            }
            this.InvokeAsync("InsertMessage", new object[] {
                        tstAmp,
                        origin,
                        msgType,
                        msgText}, this.InsertMessageOperationCompleted, userState);
        }
        
        private void OnInsertMessageOperationCompleted(object arg) {
            if ((this.InsertMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertMessageCompleted(this, new InsertMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFlightNum", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetFlightNum(string STDStart, string STDEnd, string Station) {
            object[] results = this.Invoke("GetFlightNum", new object[] {
                        STDStart,
                        STDEnd,
                        Station});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlightNumAsync(string STDStart, string STDEnd, string Station) {
            this.GetFlightNumAsync(STDStart, STDEnd, Station, null);
        }
        
        /// <remarks/>
        public void GetFlightNumAsync(string STDStart, string STDEnd, string Station, object userState) {
            if ((this.GetFlightNumOperationCompleted == null)) {
                this.GetFlightNumOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlightNumOperationCompleted);
            }
            this.InvokeAsync("GetFlightNum", new object[] {
                        STDStart,
                        STDEnd,
                        Station}, this.GetFlightNumOperationCompleted, userState);
        }
        
        private void OnGetFlightNumOperationCompleted(object arg) {
            if ((this.GetFlightNumCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlightNumCompleted(this, new GetFlightNumCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFlightData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetFlightData(string STDStart, string STDEnd, string Station) {
            object[] results = this.Invoke("GetFlightData", new object[] {
                        STDStart,
                        STDEnd,
                        Station});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlightDataAsync(string STDStart, string STDEnd, string Station) {
            this.GetFlightDataAsync(STDStart, STDEnd, Station, null);
        }
        
        /// <remarks/>
        public void GetFlightDataAsync(string STDStart, string STDEnd, string Station, object userState) {
            if ((this.GetFlightDataOperationCompleted == null)) {
                this.GetFlightDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlightDataOperationCompleted);
            }
            this.InvokeAsync("GetFlightData", new object[] {
                        STDStart,
                        STDEnd,
                        Station}, this.GetFlightDataOperationCompleted, userState);
        }
        
        private void OnGetFlightDataOperationCompleted(object arg) {
            if ((this.GetFlightDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlightDataCompleted(this, new GetFlightDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetStationSchedule", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetStationSchedule(string startDate, string endDate, string station) {
            object[] results = this.Invoke("GetStationSchedule", new object[] {
                        startDate,
                        endDate,
                        station});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetStationScheduleAsync(string startDate, string endDate, string station) {
            this.GetStationScheduleAsync(startDate, endDate, station, null);
        }
        
        /// <remarks/>
        public void GetStationScheduleAsync(string startDate, string endDate, string station, object userState) {
            if ((this.GetStationScheduleOperationCompleted == null)) {
                this.GetStationScheduleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetStationScheduleOperationCompleted);
            }
            this.InvokeAsync("GetStationSchedule", new object[] {
                        startDate,
                        endDate,
                        station}, this.GetStationScheduleOperationCompleted, userState);
        }
        
        private void OnGetStationScheduleOperationCompleted(object arg) {
            if ((this.GetStationScheduleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetStationScheduleCompleted(this, new GetStationScheduleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetMaintanceInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetMaintanceInfo(string Id, string LongReg, string Station, string TStartDate) {
            object[] results = this.Invoke("GetMaintanceInfo", new object[] {
                        Id,
                        LongReg,
                        Station,
                        TStartDate});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetMaintanceInfoAsync(string Id, string LongReg, string Station, string TStartDate) {
            this.GetMaintanceInfoAsync(Id, LongReg, Station, TStartDate, null);
        }
        
        /// <remarks/>
        public void GetMaintanceInfoAsync(string Id, string LongReg, string Station, string TStartDate, object userState) {
            if ((this.GetMaintanceInfoOperationCompleted == null)) {
                this.GetMaintanceInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMaintanceInfoOperationCompleted);
            }
            this.InvokeAsync("GetMaintanceInfo", new object[] {
                        Id,
                        LongReg,
                        Station,
                        TStartDate}, this.GetMaintanceInfoOperationCompleted, userState);
        }
        
        private void OnGetMaintanceInfoOperationCompleted(object arg) {
            if ((this.GetMaintanceInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMaintanceInfoCompleted(this, new GetMaintanceInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFlightInfoByMCCDelay", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetFlightInfoByMCCDelay(string LongReg, string Station, string FlightDate) {
            object[] results = this.Invoke("GetFlightInfoByMCCDelay", new object[] {
                        LongReg,
                        Station,
                        FlightDate});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlightInfoByMCCDelayAsync(string LongReg, string Station, string FlightDate) {
            this.GetFlightInfoByMCCDelayAsync(LongReg, Station, FlightDate, null);
        }
        
        /// <remarks/>
        public void GetFlightInfoByMCCDelayAsync(string LongReg, string Station, string FlightDate, object userState) {
            if ((this.GetFlightInfoByMCCDelayOperationCompleted == null)) {
                this.GetFlightInfoByMCCDelayOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlightInfoByMCCDelayOperationCompleted);
            }
            this.InvokeAsync("GetFlightInfoByMCCDelay", new object[] {
                        LongReg,
                        Station,
                        FlightDate}, this.GetFlightInfoByMCCDelayOperationCompleted, userState);
        }
        
        private void OnGetFlightInfoByMCCDelayOperationCompleted(object arg) {
            if ((this.GetFlightInfoByMCCDelayCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlightInfoByMCCDelayCompleted(this, new GetFlightInfoByMCCDelayCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetVIPInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetVIPInfo(string strStartDate) {
            object[] results = this.Invoke("GetVIPInfo", new object[] {
                        strStartDate});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetVIPInfoAsync(string strStartDate) {
            this.GetVIPInfoAsync(strStartDate, null);
        }
        
        /// <remarks/>
        public void GetVIPInfoAsync(string strStartDate, object userState) {
            if ((this.GetVIPInfoOperationCompleted == null)) {
                this.GetVIPInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVIPInfoOperationCompleted);
            }
            this.InvokeAsync("GetVIPInfo", new object[] {
                        strStartDate}, this.GetVIPInfoOperationCompleted, userState);
        }
        
        private void OnGetVIPInfoOperationCompleted(object arg) {
            if ((this.GetVIPInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVIPInfoCompleted(this, new GetVIPInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ClearHainan_crew_legs", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ClearHainan_crew_legs() {
            this.Invoke("ClearHainan_crew_legs", new object[0]);
        }
        
        /// <remarks/>
        public void ClearHainan_crew_legsAsync() {
            this.ClearHainan_crew_legsAsync(null);
        }
        
        /// <remarks/>
        public void ClearHainan_crew_legsAsync(object userState) {
            if ((this.ClearHainan_crew_legsOperationCompleted == null)) {
                this.ClearHainan_crew_legsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnClearHainan_crew_legsOperationCompleted);
            }
            this.InvokeAsync("ClearHainan_crew_legs", new object[0], this.ClearHainan_crew_legsOperationCompleted, userState);
        }
        
        private void OnClearHainan_crew_legsOperationCompleted(object arg) {
            if ((this.ClearHainan_crew_legsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ClearHainan_crew_legsCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetCrewLegsInfoCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetLegsInfoCompletedEventHandler(object sender, GetLegsInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLegsInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetLegsInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void MatchDateTimeCompletedEventHandler(object sender, MatchDateTimeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MatchDateTimeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal MatchDateTimeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void InsertGateCompletedEventHandler(object sender, InsertGateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InsertGateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InsertGateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void InsertMessageCompletedEventHandler(object sender, InsertMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InsertMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InsertMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetFlightNumCompletedEventHandler(object sender, GetFlightNumCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlightNumCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlightNumCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetFlightDataCompletedEventHandler(object sender, GetFlightDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlightDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlightDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetStationScheduleCompletedEventHandler(object sender, GetStationScheduleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetStationScheduleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetStationScheduleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetMaintanceInfoCompletedEventHandler(object sender, GetMaintanceInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMaintanceInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMaintanceInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetFlightInfoByMCCDelayCompletedEventHandler(object sender, GetFlightInfoByMCCDelayCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlightInfoByMCCDelayCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlightInfoByMCCDelayCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetVIPInfoCompletedEventHandler(object sender, GetVIPInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVIPInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVIPInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void ClearHainan_crew_legsCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591