using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommPrinter
{
    public class DataGridViewExport
    {
        private DataGridView _dataGridView;

        public DataGridViewExport(DataGridView dataGridView)
        {
            this._dataGridView = dataGridView;
        }

        public void Export()
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "Execl files (*.xls)|*.xls",
                FilterIndex = 0,
                RestoreDirectory = true,
                CreatePrompt = true,
                Title = "保存为Excel文件"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = dialog.OpenFile();
                StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding(0));
                string str = "";
                try
                {
                    for (int i = 0; i < this._dataGridView.ColumnCount; i++)
                    {
                        if (this._dataGridView.Columns[i].Visible)
                        {
                            if (i > 0)
                            {
                                str = str + "\t";
                            }
                            str = str + this._dataGridView.Columns[i].HeaderText;
                        }
                    }
                    writer.WriteLine(str);
                    for (int j = 0; j < this._dataGridView.Rows.Count; j++)
                    {
                        string str2 = "";
                        for (int k = 0; k < this._dataGridView.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                str2 = str2 + "\t";
                            }
                            if (this._dataGridView.Rows[j].Cells[k].Value == null)
                            {
                                str2 = str2 ?? "";
                            }
                            else
                            {
                                str2 = str2 + this._dataGridView.Rows[j].Cells[k].Value.ToString().Replace("\r\n", "").Trim();
                            }
                        }
                        writer.WriteLine(str2);
                    }
                    writer.Close();
                    stream.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
                finally
                {
                    writer.Close();
                    stream.Close();
                }
            }
        }
    }
}

