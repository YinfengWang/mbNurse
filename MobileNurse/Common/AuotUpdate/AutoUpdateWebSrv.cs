using AutoUpdate.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace AutoUpdate.AuotUpdate
{
    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "2.0.50727.1433"), DebuggerStepThrough, WebServiceBinding(Name = "AutoUpdateWebSrvSoap", Namespace = "http://tempuri.org/")]
    public class AutoUpdateWebSrv : SoapHttpClientProtocol
    {
        private SendOrPostCallback CheckUpdatedOperationCompleted;
        private SendOrPostCallback GetServerFileListOperationCompleted;
        private SendOrPostCallback GetServerFileOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;

        public event CheckUpdatedCompletedEventHandler CheckUpdatedCompleted;

        public event GetServerFileCompletedEventHandler GetServerFileCompleted;

        public event GetServerFileListCompletedEventHandler GetServerFileListCompleted;

        public AutoUpdateWebSrv()
        {
            this.Url = Settings.Default.AutoUpdate_AuotUpdate_AutoUpdateWebSrv;
            if (this.IsLocalFileSystemWebService(this.Url))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        [SoapDocumentMethod("http://tempuri.org/CheckUpdated", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public bool CheckUpdated(string appCode, DataSet ds)
        {
            return (bool)base.Invoke("CheckUpdated", new object[] { appCode, ds })[0];
        }

        public void CheckUpdatedAsync(string appCode, DataSet ds)
        {
            this.CheckUpdatedAsync(appCode, ds, null);
        }

        public void CheckUpdatedAsync(string appCode, DataSet ds, object userState)
        {
            if (this.CheckUpdatedOperationCompleted == null)
            {
                this.CheckUpdatedOperationCompleted = new SendOrPostCallback(this.OnCheckUpdatedOperationCompleted);
            }
            base.InvokeAsync("CheckUpdated", new object[] { appCode, ds }, this.CheckUpdatedOperationCompleted, userState);
        }

        [return: XmlElement(DataType = "base64Binary")]
        [SoapDocumentMethod("http://tempuri.org/GetServerFile", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public byte[] GetServerFile(string appCode, string fileName)
        {
            return (byte[])base.Invoke("GetServerFile", new object[] { appCode, fileName })[0];
        }

        public void GetServerFileAsync(string appCode, string fileName)
        {
            this.GetServerFileAsync(appCode, fileName, null);
        }

        public void GetServerFileAsync(string appCode, string fileName, object userState)
        {
            if (this.GetServerFileOperationCompleted == null)
            {
                this.GetServerFileOperationCompleted = new SendOrPostCallback(this.OnGetServerFileOperationCompleted);
            }
            base.InvokeAsync("GetServerFile", new object[] { appCode, fileName }, this.GetServerFileOperationCompleted, userState);
        }

        [SoapDocumentMethod("http://tempuri.org/GetServerFileList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public DataSet GetServerFileList(string appCode)
        {
            return (DataSet)base.Invoke("GetServerFileList", new object[] { appCode })[0];
        }

        public void GetServerFileListAsync(string appCode)
        {
            this.GetServerFileListAsync(appCode, null);
        }

        public void GetServerFileListAsync(string appCode, object userState)
        {
            if (this.GetServerFileListOperationCompleted == null)
            {
                this.GetServerFileListOperationCompleted = new SendOrPostCallback(this.OnGetServerFileListOperationCompleted);
            }
            base.InvokeAsync("GetServerFileList", new object[] { appCode }, this.GetServerFileListOperationCompleted, userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if ((url == null) || (url == string.Empty))
            {
                return false;
            }
            Uri uri = new Uri(url);
            return ((uri.Port >= 1024) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0));
        }

        private void OnCheckUpdatedOperationCompleted(object arg)
        {
            if (this.CheckUpdatedCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs)arg;
                this.CheckUpdatedCompleted(this, new CheckUpdatedCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetServerFileListOperationCompleted(object arg)
        {
            if (this.GetServerFileListCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs)arg;
                this.GetServerFileListCompleted(this, new GetServerFileListCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetServerFileOperationCompleted(object arg)
        {
            if (this.GetServerFileCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs)arg;
                this.GetServerFileCompleted(this, new GetServerFileCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        public string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if (!((!this.IsLocalFileSystemWebService(base.Url) || this.useDefaultCredentialsSetExplicitly) || this.IsLocalFileSystemWebService(value)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

