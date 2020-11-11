namespace AutoUpdate.AuotUpdate
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "2.0.50727.1433"), DebuggerStepThrough]
    public class GetServerFileCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal GetServerFileCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public byte[] Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (byte[]) this.results[0];
            }
        }
    }
}

