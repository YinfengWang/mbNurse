namespace AutoUpdate.AuotUpdate
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;

    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "2.0.50727.1433"), DebuggerStepThrough]
    public class GetServerFileListCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal GetServerFileListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DataSet Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (DataSet) this.results[0];
            }
        }
    }
}

