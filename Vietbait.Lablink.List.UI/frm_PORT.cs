using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using SubSonic.Sugar;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.List.Business;
namespace Vietbait.Lablink.List.UI
{
    public partial class frm_PORT : Form
    {
        #region Attributes
        private DataTable _tblRs232;
        private DataTable _tblTcpip;
        private UserAction paction;
        int _currentTcpipRowIndex = 0;
        private DataTable _dtTcpip;
        private DataTable _dtlRs232;
        #endregion

        #region Private Method
        /// <summary>
        /// Load thông tin Rs232
        /// </summary>
        /// 
        private void LoadRs232()
        {
            _dtlRs232 = MainBusiness.GetAllRs232Port();
            grdRs232.DataSource = _dtlRs232;
        }

        /// <summary>
        /// Load thông tin Tcpip
        ///  </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTcpip()
        {
            try
            {
                _dtTcpip = HaBusiness.GetAllTCPIP();
                grdTcpip.DataSource = _dtTcpip;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Form Envents
        public frm_PORT()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load dữ liệu thông tin RS232, TcpIp  trên grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PORT_Load(object sender, EventArgs e)
        {
            grdRs232.RowPostPaint += Utilities.UI.GridviewRowPostPaint;
            grdTcpip.RowPostPaint += Utilities.UI.GridviewRowPostPaint;
            LoadRs232();
            LoadTcpip();

        }
        
        private void frm_PORT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Dispose(true);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            LTcpip items;
            for (int i = 0; i < _dtTcpip.Rows.Count; i++)
            {
                DataRow dataRow = _dtTcpip.Rows[i];
                if (dataRow.RowState == DataRowState.Modified)
                {
                    items = new LTcpip(dataRow[LTcpip.Columns.Id]);
                    items.IPAddress = dataRow[LTcpip.Columns.IPAddress].ToString();
                    items.Description = dataRow[LTcpip.Columns.Description].ToString();
                    HaBusiness.UpdateTcpip(items);
                    MessageBox.Show("Bạn đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);

                }
                else if (dataRow.RowState == DataRowState.Added)
                {
                    items = new LTcpip();
                    items.IPAddress = dataRow[LTcpip.Columns.IPAddress].ToString();
                    items.Description = dataRow[LTcpip.Columns.Description].ToString();                
                    string id = HaBusiness.InsertTCPIP(items);
                    //check địa chỉ Ip nếu đã tồn tại
                    if (id == "-1")
                    {
                        MessageBox.Show("Tên cổng đã" + items.IPAddress.ToString()+ "đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        dataRow.Delete();
                        grdTcpip.Focus();

                    }
                        // ko tồn tại sẽ insert
                    else
                    {
                        dataRow[LTcpip.Columns.Id] = id;
                        MessageBox.Show("Bạn đã Thêm mới thành công", "Thông báo", MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                        //dataRow[LTcpip.Columns.Id] = id;
                      
                    }
                      }
            }
            _dtTcpip.AcceptChanges();
        }

        /// <summary>
        /// Xóa dữ liệu trên grdview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdTcpip_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridViewRow currentRow = grdTcpip.CurrentRow;
            if (currentRow != null)
            {

                if ((e.KeyCode == Keys.Delete) && (grdTcpip.CurrentRow != null))
                {
                    int pid = Convert.ToInt32(currentRow.Cells["colTcpId"].Value);
                    if (MessageBox.Show("Bạn có muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        HaBusiness.DeleteTcpip(pid);
                        grdTcpip.Rows.Remove(currentRow);
                        MessageBox.Show("Bạn đã xóa thành công", "Thông báo", MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Thêm mới và sửa dữ liệu trên Gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnSaveRs232_Click(object sender, EventArgs e)
        {
            LRS232 pItem;
            for (int i = 0; i < _dtlRs232.Rows.Count; i++)
            {
                DataRow dataRow = _dtlRs232.Rows[i];
                if (dataRow.RowState == DataRowState.Modified)
                {
                    pItem = new LRS232(dataRow[LRS232.Columns.Id]);
                    pItem.Name = dataRow[LRS232.Columns.Name].ToString();
                    pItem.Parity = Convert.ToInt32(dataRow[LRS232.Columns.Parity]);
                    pItem.Rts = Convert.ToInt32(dataRow[LRS232.Columns.Rts].ToString());
                    pItem.BaudRate = Convert.ToInt32(dataRow[LRS232.Columns.BaudRate].ToString());
                    pItem.DataBits = Convert.ToInt32(dataRow[LRS232.Columns.DataBits].ToString());
                    pItem.Dtr = Convert.ToInt32(dataRow[LRS232.Columns.Dtr].ToString());
                    pItem.StopBits = Convert.ToInt32(dataRow[LRS232.Columns.StopBits].ToString());
                    pItem.Description = dataRow[LRS232.Columns.Description].ToString();
                    MainBusiness.UpdateRs232(pItem);
                    MessageBox.Show("Bạn đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                }
                else if (dataRow.RowState == DataRowState.Added)
                {
                    pItem = new LRS232(dataRow[LRS232.Columns.Id]);
                    pItem.Name = dataRow[LRS232.Columns.Name].ToString();
                    pItem.Parity = Convert.ToInt32(dataRow[LRS232.Columns.Parity]);
                    pItem.Rts = Convert.ToInt32(dataRow[LRS232.Columns.Rts].ToString());
                    pItem.BaudRate = Convert.ToInt32(dataRow[LRS232.Columns.BaudRate].ToString());
                    pItem.DataBits = Convert.ToInt32(dataRow[LRS232.Columns.DataBits].ToString());
                    pItem.Dtr = Convert.ToInt32(dataRow[LRS232.Columns.Dtr].ToString());
                    pItem.StopBits = Convert.ToInt32(dataRow[LRS232.Columns.StopBits].ToString());
                    pItem.Description = dataRow[LRS232.Columns.Description].ToString();
                    string id = MainBusiness.InsertRs232(pItem);
                    if (id == "-1")
                    {
                        MessageBox.Show("Tên cổng"+pItem.Name.ToString()+" đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        dataRow.Delete();
}
                    else
                    {
                        dataRow[LRS232.Columns.Id] = id;
                        MessageBox.Show("Bạn đã thêm mới thành công", "Thông báo", MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    }

                }
            }
            _dtlRs232.AcceptChanges();

        }
        /// <summary>
        /// Xóa dữ liệu trên Grid
        /// sử dụng phím tắt delete trên bàn phím để xóa
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRs232_KeyUp(object sender, KeyEventArgs e)
        {
            LRS232 pItem;
            DataGridViewRow currentRow = grdRs232.CurrentRow;
            if ((e.KeyCode == Keys.Delete) && (grdRs232.CurrentRow != null))
            {
                int pid = Convert.ToInt32(currentRow.Cells["colRs232Id"].Value);
                if (
                    MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    MainBusiness.DeleteRs232(pid);
                    grdRs232.Rows.Remove(currentRow);
                    MessageBox.Show("Bạn đã xóa thành công", "Thông báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// Sự kiện bắt lỗi khi người dùng nhập ký tự chuỗi ==> Phải là số = True
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRs232_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if ((e.ColumnIndex == grdRs232.Columns["colBaudRate"].Index) ||
                (e.ColumnIndex == grdRs232.Columns["colDataBits"].Index) ||
                (e.ColumnIndex == grdRs232.Columns["colStopBits"].Index) ||
                (e.ColumnIndex == grdRs232.Columns["colParity"].Index) ||
                (e.ColumnIndex == grdRs232.Columns["colDTR"].Index) ||
                (e.ColumnIndex == grdRs232.Columns["colRTS"].Index))
            {
                string time = e.FormattedValue.ToString().Trim();
                if (!Validation.IsNumeric(time))
                {
                    MessageBox.Show("Bạn phải nhập ký tự là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        #endregion      


    }
}


