﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.8742
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.CompactFramework.Design.Data 2.0.50727.8742 版自动生成。
// 
namespace HISPlus.DataWebSrv {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DataWebSrvSoap", Namespace="http://tempuri.org/")]
    public partial class DataWebSrv : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public DataWebSrv() {
            this.Url = "http://localhost/MobileWebSrv/DataWebSrv.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPdaDbTableList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetPdaDbTableList(System.DateTime dtLastUpdate) {
            object[] results = this.Invoke("GetPdaDbTableList", new object[] {
                        dtLastUpdate});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetPdaDbTableList(System.DateTime dtLastUpdate, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPdaDbTableList", new object[] {
                        dtLastUpdate}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetPdaDbTableList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetDataChanged", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetDataChanged(string tableName, string lastDownDBTS, string filter) {
            object[] results = this.Invoke("GetDataChanged", new object[] {
                        tableName,
                        lastDownDBTS,
                        filter});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDataChanged(string tableName, string lastDownDBTS, string filter, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDataChanged", new object[] {
                        tableName,
                        lastDownDBTS,
                        filter}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetDataChanged(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetDataChanged2", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetDataChanged2(string tableName, string lastDownDBTS, string filter) {
            object[] results = this.Invoke("GetDataChanged2", new object[] {
                        tableName,
                        lastDownDBTS,
                        filter});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDataChanged2(string tableName, string lastDownDBTS, string filter, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDataChanged2", new object[] {
                        tableName,
                        lastDownDBTS,
                        filter}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetDataChanged2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ApplyDataChange", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.DateTime ApplyDataChange(System.Data.DataSet dsChanged, string tableName) {
            object[] results = this.Invoke("ApplyDataChange", new object[] {
                        dsChanged,
                        tableName});
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginApplyDataChange(System.Data.DataSet dsChanged, string tableName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ApplyDataChange", new object[] {
                        dsChanged,
                        tableName}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.DateTime EndApplyDataChange(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ApplyDataChange2", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.DateTime ApplyDataChange2() {
            object[] results = this.Invoke("ApplyDataChange2", new object[0]);
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginApplyDataChange2(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ApplyDataChange2", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.DateTime EndApplyDataChange2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOrders", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetOrders(string patientId, string visitId) {
            object[] results = this.Invoke("GetOrders", new object[] {
                        patientId,
                        visitId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetOrders(string patientId, string visitId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetOrders", new object[] {
                        patientId,
                        visitId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetOrders(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getDeptCodeCount", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet getDeptCodeCount(string deptCode) {
            object[] results = this.Invoke("getDeptCodeCount", new object[] {
                        deptCode});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetDeptCodeCount(string deptCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getDeptCodeCount", new object[] {
                        deptCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndgetDeptCodeCount(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOrdersExecute", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetOrdersExecute(string patientId, string visitId) {
            object[] results = this.Invoke("GetOrdersExecute", new object[] {
                        patientId,
                        visitId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetOrdersExecute(string patientId, string visitId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetOrdersExecute", new object[] {
                        patientId,
                        visitId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetOrdersExecute(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOrdersExecuteTime", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetOrdersExecuteTime(string patientId, string visitId, string start, string stop) {
            object[] results = this.Invoke("GetOrdersExecuteTime", new object[] {
                        patientId,
                        visitId,
                        start,
                        stop});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetOrdersExecuteTime(string patientId, string visitId, string start, string stop, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetOrdersExecuteTime", new object[] {
                        patientId,
                        visitId,
                        start,
                        stop}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetOrdersExecuteTime(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SaveData(System.Data.DataSet ds) {
            object[] results = this.Invoke("SaveData", new object[] {
                        ds});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSaveData(System.Data.DataSet ds, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SaveData", new object[] {
                        ds}, callback, asyncState);
        }
        
        /// <remarks/>
        public bool EndSaveData(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetXunShis", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetXunShis(string patientId, string visitId, string wardCode) {
            object[] results = this.Invoke("GetXunShis", new object[] {
                        patientId,
                        visitId,
                        wardCode});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetXunShis(string patientId, string visitId, string wardCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetXunShis", new object[] {
                        patientId,
                        visitId,
                        wardCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetXunShis(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPdaClientTimeout", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetPdaClientTimeout() {
            object[] results = this.Invoke("GetPdaClientTimeout", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetPdaClientTimeout(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPdaClientTimeout", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetPdaClientTimeout(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveSpeciment", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SaveSpeciment(System.Data.DataSet dsChanged) {
            object[] results = this.Invoke("SaveSpeciment", new object[] {
                        dsChanged});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSaveSpeciment(System.Data.DataSet dsChanged, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SaveSpeciment", new object[] {
                        dsChanged}, callback, asyncState);
        }
        
        /// <remarks/>
        public bool EndSaveSpeciment(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }
    }
}