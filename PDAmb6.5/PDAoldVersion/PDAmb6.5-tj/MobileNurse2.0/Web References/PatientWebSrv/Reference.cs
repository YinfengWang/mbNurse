﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.8669
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.CompactFramework.Design.Data 2.0.50727.8669 版自动生成。
// 
namespace HISPlus.PatientWebSrv {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="PatientWebSrvSoap", Namespace="http://tempuri.org/")]
    public partial class PatientWebSrv : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public PatientWebSrv() {
            this.Url = "http://localhost/MobileWebSrv/PatientWebSrv.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPatientInfo_Filter", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetPatientInfo_Filter(string deptCode, string filterName) {
            object[] results = this.Invoke("GetPatientInfo_Filter", new object[] {
                        deptCode,
                        filterName});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetPatientInfo_Filter(string deptCode, string filterName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPatientInfo_Filter", new object[] {
                        deptCode,
                        filterName}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetPatientInfo_Filter(System.IAsyncResult asyncResult) {
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
    }
}
