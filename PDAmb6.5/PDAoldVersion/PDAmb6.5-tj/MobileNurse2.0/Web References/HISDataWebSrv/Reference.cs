﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5456
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.CompactFramework.Design.Data 2.0.50727.5456 版自动生成。
// 
namespace HISPlus.HISDataWebSrv {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="HISDataWebSrvSoap", Namespace="http://tempuri.org/")]
    public partial class HISDataWebSrv : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public HISDataWebSrv() {
            this.Url = "http://localhost/MobileWebSrv/HISDataWebSrv.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetData(string sql, string tableName) {
            object[] results = this.Invoke("GetData", new object[] {
                        sql,
                        tableName});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetData(string sql, string tableName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetData", new object[] {
                        sql,
                        tableName}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetData(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetExamList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetExamList(string patientId, string visitId) {
            object[] results = this.Invoke("GetExamList", new object[] {
                        patientId,
                        visitId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetExamList(string patientId, string visitId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetExamList", new object[] {
                        patientId,
                        visitId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetExamList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetExamResult", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetExamResult(string examNo) {
            object[] results = this.Invoke("GetExamResult", new object[] {
                        examNo});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetExamResult(string examNo, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetExamResult", new object[] {
                        examNo}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetExamResult(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLabTestList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetLabTestList(string patientId, string visitId) {
            object[] results = this.Invoke("GetLabTestList", new object[] {
                        patientId,
                        visitId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetLabTestList(string patientId, string visitId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetLabTestList", new object[] {
                        patientId,
                        visitId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetLabTestList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLabTestResult", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetLabTestResult(string testNo) {
            object[] results = this.Invoke("GetLabTestResult", new object[] {
                        testNo});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetLabTestResult(string testNo, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetLabTestResult", new object[] {
                        testNo}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetLabTestResult(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetSpeciment", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetSpeciment(string patientId, string visitId) {
            object[] results = this.Invoke("GetSpeciment", new object[] {
                        patientId,
                        visitId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetSpeciment(string patientId, string visitId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetSpeciment", new object[] {
                        patientId,
                        visitId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetSpeciment(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
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
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChkNewOrder", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ChkNewOrder(string deptCode, string preChkDate) {
            object[] results = this.Invoke("ChkNewOrder", new object[] {
                        deptCode,
                        preChkDate});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginChkNewOrder(string deptCode, string preChkDate, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ChkNewOrder", new object[] {
                        deptCode,
                        preChkDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndChkNewOrder(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChkNewStopOrder", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int ChkNewStopOrder(string deptCode) {
            object[] results = this.Invoke("ChkNewStopOrder", new object[] {
                        deptCode});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginChkNewStopOrder(string deptCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ChkNewStopOrder", new object[] {
                        deptCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndChkNewStopOrder(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPatientBedLabel", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetPatientBedLabel(string deptCode) {
            object[] results = this.Invoke("GetPatientBedLabel", new object[] {
                        deptCode});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetPatientBedLabel(string deptCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPatientBedLabel", new object[] {
                        deptCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetPatientBedLabel(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOrderRemindList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetOrderRemindList(string deptCode) {
            object[] results = this.Invoke("GetOrderRemindList", new object[] {
                        deptCode});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetOrderRemindList(string deptCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetOrderRemindList", new object[] {
                        deptCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetOrderRemindList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOrderRemindInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetOrderRemindInfo(string deptCode, string bedLabel) {
            object[] results = this.Invoke("GetOrderRemindInfo", new object[] {
                        deptCode,
                        bedLabel});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetOrderRemindInfo(string deptCode, string bedLabel, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetOrderRemindInfo", new object[] {
                        deptCode,
                        bedLabel}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetOrderRemindInfo(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOrderRemindPatientList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetOrderRemindPatientList(string deptCode) {
            object[] results = this.Invoke("GetOrderRemindPatientList", new object[] {
                        deptCode});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetOrderRemindPatientList(string deptCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetOrderRemindPatientList", new object[] {
                        deptCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetOrderRemindPatientList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
    }
}
