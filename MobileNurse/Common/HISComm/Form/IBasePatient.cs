namespace HISPlus
{
    /// <summary>
    /// 包含病人列表操作的父类接口。   
    /// </summary>
    public interface IBasePatient
    {
        /// <summary>
        /// 病人选择项变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Patient_SelectionChanged(object sender, PatientEventArgs e);

        void Patient_ListRefresh(object sender, PatientEventArgs e);
    }
}
