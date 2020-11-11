using System;
using System.ComponentModel;
using System.Threading;

namespace DXApplication2
{
    partial class MdiFrm
    {
        private BackgroundWorker _backgroundWorker;//后台线程

        private BackgroundWorkerEventArgs _eventArgs;//异常参数
        private string _inforMessage;

        public void RunBackground()
        {
            barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            repositoryItemProgressBar1.ShowTitle = true;
            _eventArgs = new BackgroundWorkerEventArgs();
            _backgroundWorker =
                new BackgroundWorker { WorkerReportsProgress = true };
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
        }

        //显示进度
        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { barEditItem1.EditValue = e.ProgressPercentage; }));
            }
            else
            {
                barEditItem1.EditValue = e.ProgressPercentage;
            }

            if (e.ProgressPercentage == 100)
                Thread.Sleep(2000);
        }

        //操作进行完毕后关闭进度条窗体
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (barEditItem1.Visibility != DevExpress.XtraBars.BarItemVisibility.Never)
            {
                barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bar3.Visible = false;
            }
            if (this.BackgroundWorkerCompleted != null)
            {
                this.BackgroundWorkerCompleted(null, _eventArgs);
            }
        }

        //后台执行的操作
        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (BackgroundWork != null)
            {
                try
                {
                    BackgroundWork(this.ReportPercent);
                }
                catch (Exception ex)
                {
                    _eventArgs.BackGroundException = ex;
                }
            }
        }

        #region 公共方法、属性、事件

        /// <summary>
        /// 设置进度条显示的提示信息
        /// </summary>
        public string MessageInfo
        {
            set { _inforMessage = value; }
        }

        /// <summary>
        /// <para>后台执行的操作,参数为一个参数为int型的委托；
        /// 在客户端要执行的后台方法中可以使用Action&lt;int&gt;预报完成进度,如：
        /// <example>
        /// <code>
        /// PercentProcessOperator o = new PercentProcessOperator();
        /// o.BackgroundWork = this.DoWord;
        /// 
        /// private void DoWork(Action&lt;int&gt; Report)
        /// {
        ///     Report(0);
        ///     //do soming;
        ///     Report(10);
        ///     //do soming;
        ///     Report(50);
        ///     //do soming
        ///     Report(100);
        /// }
        /// </code>
        /// </example></para>
        /// </summary>
        public Action<Action<int>> BackgroundWork { private get; set; }

        /// <summary>
        /// 后台任务执行完毕后事件
        /// </summary>
        public event EventHandler<BackgroundWorkerEventArgs> BackgroundWorkerCompleted;

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Start()
        {
            _backgroundWorker.RunWorkerAsync();
            bar3.Visible = true;
        }

        #endregion

        //报告完成百分比
        public void ReportPercent(int i)
        {
            if (i >= 0 && i <= 100)
            {
                _backgroundWorker.ReportProgress(i);
            }
        }

    }

    public class BackgroundWorkerEventArgs : EventArgs
    {
        /// <summary>
        /// 后台程序运行时抛出的异常
        /// </summary>
        public Exception BackGroundException { private get; set; }
    }
}
