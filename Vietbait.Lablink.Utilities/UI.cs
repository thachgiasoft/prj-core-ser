using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using SubSonic;

namespace Vietbait.Lablink.Utilities
{
    public static class UI
    {
        #region Common

        public static void SetControlProperty(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyDelegate(SetControlProperty),
                               new[] {control, propertyName, propertyValue});
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control,
                                               new[] {propertyValue});
            }
        }

        private delegate void SetControlPropertyDelegate(Control control, string propertyName, object propertyValue);

        #endregion

        #region Combobox Utilities

        /// <summary>
        /// Load dữ liệu từ CSDL vào Combobox
        /// </summary>
        /// <param name="comboBox">Combobox</param>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="displayMember">Trường DL dùng để hiển thị</param>
        /// <param name="valueMember">Trường DL dùng làm value</param>
        public static void FillDataToCombobox(ComboBox comboBox, string tableName, string displayMember,
                                              string valueMember)
        {
            try
            {
                DataTable table = new Select(displayMember, valueMember).From(tableName).ExecuteDataSet().Tables[0];
                FillComboboxFromDataTable(comboBox, table, displayMember, valueMember);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load dữ liệu từ CSDL vào Combobox
        /// </summary>
        /// <param name="comboBox">Combobox</param>
        /// <param name="data"></param>
        /// <param name="displayMember">Trường DL dùng để hiển thị</param>
        /// <param name="valueMember">Trường DL dùng làm value</param>
        public static void FillDataToCombobox(ComboBox comboBox, DataTable data, string displayMember,
                                              string valueMember)
        {
            try
            {
                FillComboboxFromDataTable(comboBox, data, displayMember, valueMember);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load dữ liệu từ CSDL vào Combobox
        /// </summary>
        /// <param name="comboBox">Combobox</param>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="displayMember">Trường DL dùng để hiển thị</param>
        /// <param name="valueMember">Trường DL dùng làm value</param>
        /// <param name="selectionString">Chuỗi lựa chọn ở dòng đầu tiên</param>
        /// <param name="selectionValue">Value lựa chọn</param>
        public static void FillDataToCombobox(ComboBox comboBox, string tableName, string displayMember,
                                              string valueMember, string selectionString, string selectionValue)
        {
            try
            {
                DataTable table = new Select(displayMember, valueMember).From(tableName).ExecuteDataSet().Tables[0];
                DataRow dr = table.NewRow();
                dr[displayMember] = selectionString;
                dr[valueMember] = selectionValue;
                table.Rows.InsertAt(dr, 0);
                FillComboboxFromDataTable(comboBox, table, displayMember, valueMember);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Load DL vào combobox từ Datatable
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="table"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        private static void FillComboboxFromDataTable(ComboBox comboBox, DataTable table, string displayMember,
                                                      string valueMember)
        {
            try
            {
                comboBox.DataSource = table;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void GridviewRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridView gridViewX;
                gridViewX = sender as DataGridView;

                string strRowNumber = (e.RowIndex + 1).ToString();

                if (gridViewX != null)
                {
                    while (strRowNumber.Length < gridViewX.RowCount.ToString().Length)
                        strRowNumber = "0" + strRowNumber;

                    SizeF size = e.Graphics.MeasureString(strRowNumber, gridViewX.Font);

                    if (gridViewX.RowHeadersWidth < (int) (size.Width + 20))
                        gridViewX.RowHeadersWidth = (int) (size.Width + 20);
                    Brush b = SystemBrushes.ControlText;
                    e.Graphics.DrawString(strRowNumber, gridViewX.Font, b, e.RowBounds.Location.X + 15,
                                          e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height)/2));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hàm Tạo ra một cột dạng Text trong DataGridView
        /// </summary>
        /// <param name="name">Tên cột</param>
        /// <param name="header">Header Text của cột</param>
        /// <param name="dataProperty">Tên Trường dữ liệu trong Data Source</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn CreateGridTextColumn(string name, string header, string dataProperty)
        {
            try
            {
                var col = new DataGridViewTextBoxColumn();
                col.Name = name;
                col.HeaderText = header;
                col.DataPropertyName = dataProperty;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                return col;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataGridViewTextBoxColumn CreateGridTextColumn(string name, string header, string dataProperty,
                                                                     int? width)
        {
            try
            {
                DataGridViewTextBoxColumn col = CreateGridTextColumn(name, header, dataProperty);
                if (width.HasValue)
                {
                    col.Width = (int) width;
                }
                else
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                return col;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataGridViewTextBoxColumn CreateGridTextColumn(string name, string header, string dataProperty,
                                                                     int? width, bool? readOnly)
        {
            try
            {
                DataGridViewTextBoxColumn col = CreateGridTextColumn(name, header, dataProperty, width);
                col.ReadOnly = readOnly == null ? false : (bool) readOnly;
                return col;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataGridViewTextBoxColumn CreateGridTextColumn(string name, string header, string dataProperty,
                                                                     int? width, bool? readOnly, bool? visible)
        {
            try
            {
                DataGridViewTextBoxColumn col = CreateGridTextColumn(name, header, dataProperty, width, readOnly);
                col.Visible = visible == null || (bool) visible;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                return col;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy dữ liệu dạng string trên cell của Gridview
        /// Nếu có lỗi trong quá trình lấy dữ liệu trả về ""
        /// </summary>
        /// <param name="cell">Ô cần lấy dữ liệu</param>
        /// <returns>Giá trị dạng string của ô</returns>
        public static string GetCellValue(DataGridViewCell cell)
        {
            string result = "";
            try
            {
                result = cell.Value.ToString();
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }
    }
}