using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.UI
{
    public partial class FrmDemo : Form
    {
        #region Attributes

        private const string StrCancel = @"Cancel";
        private const string StrExit = @"Exit";
        private UserAction _myUserAction;
        private DataTable _tblRs232;
        private DataTable _tblTCPIP;
        private UserAction paction;

        private DataTable _tblResult;
        //private IDataReader _dr;

        #endregion

        #region Contructor

        public FrmDemo()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Hàm dùng để set thuộc tính cho các control
        /// </summary>
        /// <param name="pMyUserAction"></param>
        private void SetControlStatus(UserAction pMyUserAction)
        {
            switch (pMyUserAction)
            {
                case UserAction.Insert:
                    grbParameters.Enabled = true;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    txtPortName.Focus();
                    txtPortName.SelectAll();
                    grdRS232.Enabled = false;
                    btnExit.Text = StrCancel;
                    btnAdd.Enabled = false;
                    txtId.Text = string.Empty;
                    break;

                case UserAction.Normal:
                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    grbParameters.Enabled = false;
                    btnExit.Text = StrExit;
                    grdRS232.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    break;

                case UserAction.Update:
                    grbParameters.Enabled = true;
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    txtPortName.Focus();
                    txtPortName.SelectAll();
                    grdRS232.Enabled = false;
                    btnExit.Text = StrCancel;
                    btnSave.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Hàm thực hiện lệnh 
        /// </summary>
        /// <param name="pMyUserAction">UserAction pMyUserAction</param>
        private void PerformAction(UserAction pMyUserAction)
        {
            try
            {
                LRS232 item;
                try
                {
                    item = new LRS232(Convert.ToInt32(txtId.Text));
                }
                catch (Exception)
                {
                    item = new LRS232();
                }
                item.Name = txtPortName.Text.Trim();
                item.BaudRate = Convert.ToInt32(txtBaudrate.Text);
                item.DataBits = Convert.ToInt32(txtDataBits.Text);
                item.StopBits = Convert.ToInt32(txtStopBits.Text);
                item.Parity = Convert.ToInt32(txtParity.Text);
                item.Dtr = Convert.ToInt32(txtDtr.Text);
                item.Rts = Convert.ToInt32(txtRts.Text);
                item.Description = txtDesc.Text.Trim();

                switch (pMyUserAction)
                {
                        //Thực hiện việc Insert
                    case UserAction.Insert:

                        MainBusiness.InsertRs232(item);
                        break;

                    case UserAction.Update:
                        MainBusiness.UpdateRs232(item);
                        LoadAllRs232Port();
                        break;

                    case UserAction.Delete:
                        MainBusiness.DeleteRs232(item.Id);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// List toàn bộ danh mục RS232 Port
        /// </summary>
        private void LoadAllRs232Port()
        {
            _tblRs232 = MainBusiness.GetAllRs232Port();

            grdRS232.DataSource = _tblRs232;
        }

        #endregion

        #region Form Event RS232

        private void frmDemo_Load(object sender, EventArgs e)
        {
            try
            {
                //_myUserAction = UserAction.Normal;
                LoadAllRs232Port();
                //SetControlStatus(_myUserAction);
                //paction = UserAction.Normal;
                //LoadAllTCPIPPORT();
                //SetControlTcpip(paction);
                //Utilities.UI.FillDataToCombobox(comboBox1, LRS232.Schema.Name, LRS232.Columns.Name,
                //                LRS232.Columns.Id, "--- Chọn ----", "-1");

                //Load Demo
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _myUserAction = UserAction.Insert;
            SetControlStatus(_myUserAction);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_myUserAction == UserAction.Normal)
            {
                Dispose(true);
            }
            else
            {
                _myUserAction = UserAction.Normal;
                SetControlStatus(_myUserAction);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PerformAction(_myUserAction);
                LoadAllRs232Port();
                _myUserAction = UserAction.Normal;
                SetControlStatus(_myUserAction);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _myUserAction = UserAction.Delete;
                PerformAction(_myUserAction);
                LoadAllRs232Port();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            _myUserAction = UserAction.Update;
            SetControlStatus(_myUserAction);
        }

        private void grdRS232_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow dr = grdRS232.CurrentRow;
            if (dr != null)
            {
                txtPortName.Text = dr.Cells[LRS232.Columns.Name].Value.ToString();
                txtBaudrate.Text = dr.Cells[LRS232.Columns.BaudRate].Value.ToString();
                txtDataBits.Text = dr.Cells[LRS232.Columns.DataBits].Value.ToString();
                txtStopBits.Text = dr.Cells[LRS232.Columns.StopBits].Value.ToString();
                txtParity.Text = dr.Cells[LRS232.Columns.Parity].Value.ToString();
                txtDtr.Text = dr.Cells[LRS232.Columns.Dtr].Value.ToString();
                txtRts.Text = dr.Cells[LRS232.Columns.Rts].Value.ToString();
                txtId.Text = dr.Cells[LRS232.Columns.Id].Value.ToString();
            }
        }

        #endregion

        #region private Method TCP/IP

        private void LoadAllTCPIPPORT()
        {
            _tblTCPIP = HaBusiness.GetAllTCPIP();
            grdTCP.DataSource = _tblTCPIP;
        }


        /// <summary>
        /// Xử lý các trạng thái control
        /// </summary>
        private void SetControlTcpip(UserAction status)
        {
            switch (status)
            {
                case UserAction.Insert:

                    grpParam.Enabled = true;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    txtAddresIP.Focus();
                    txtAddresIP.SelectAll();
                    grdTCP.Enabled = false;
                    btnThoat.Text = StrCancel;
                    btnThem.Enabled = false;
                    break;
                case UserAction.Normal:

                    btnThem.Enabled = true;
                    btnXoa.Enabled = true;
                    grpParam.Enabled = false;
                    btnLuu.Enabled = false;
                    btnSua.Enabled = true;
                    btnThoat.Text = StrExit;
                    break;
                case UserAction.Update:
                    grpParam.Enabled = true;
                    grdTCP.Enabled = true;
                    btnXoa.Enabled = false;
                    btnSua.Enabled = false;
                    btnThem.Enabled = false;
                    txtAddresIP.Focus();
                    txtAddresIP.SelectAll();
                    btnThoat.Text = StrCancel;
                    btnLuu.Enabled = true;

                    break;
            }
        }

        /// <summary>
        /// Hàm thực hiện lệnh Insert, Update, Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformmAction(UserAction status)
        {
            try
            {
                var items = new LTcpip();
                items.Id = Convert.ToInt32(txtPortID.Text);
                items.IPAddress = txtAddresIP.Text.Trim();
                items.Description = txtDes.Text.Trim();

                switch (status)
                {
                    case UserAction.Insert:
                        HaBusiness.InsertTCPIP(items);
                        break;
                    case UserAction.Update:
                        HaBusiness.UpdateTcpip(items);
                        break;
                    case UserAction.Delete:
                        HaBusiness.DeleteTcpip(items.Id);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Form Event TCP/IP

        private void btnThem_Click(object sender, EventArgs e)
        {
            paction = UserAction.Insert;
            SetControlTcpip(paction);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (paction == UserAction.Normal)
            {
                Dispose(true);
            }
            else
            {
                paction = UserAction.Normal;
                SetControlTcpip(paction);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            paction = UserAction.Update;
            SetControlTcpip(paction);
            //LoadAllTCPIPPORT();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                paction = UserAction.Delete;
                PerformmAction(paction);

                SetControlTcpip(paction);
                LoadAllTCPIPPORT();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        # endregion

        private void btnLuu_Click(object sender, EventArgs e)
        {
            PerformmAction(paction);
            LoadAllTCPIPPORT();
            paction = UserAction.Normal;
            SetControlTcpip(paction);
        }

        private void grdTCP_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = grdTCP.CurrentRow;

                if (dr != null)
                {
                    txtPortID.Text = dr.Cells[LTcpip.Columns.Id].Value.ToString();
                    txtAddresIP.Text = dr.Cells[LTcpip.Columns.IPAddress].Value.ToString();
                    txtDes.Text = dr.Cells[LTcpip.Columns.Description].Value.ToString();

                    ipAddressInput1.Value = dr.Cells[LTcpip.Columns.IPAddress].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void vScrollBarAdv1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void grdResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                //this method overrides the DataGridView's RowPostPaint event 
                //in order to automatically draw numbers on the row header cells
                //and to automatically adjust the width of the column containing
                //the row header cells so that it can accommodate the new row
                //numbers,

                //store a string representation of the row number in 'strRowNumber'
                string strRowNumber = (e.RowIndex + 1).ToString();

                //prepend leading zeros to the string if necessary to improve
                //appearance. For example, if there are ten rows in the grid,
                //row seven will be numbered as "07" instead of "7". Similarly, if 
                //there are 100 rows in the grid, row seven will be numbered as "007".
                while (strRowNumber.Length < grdResult.RowCount.ToString().Length) strRowNumber = "0" + strRowNumber;

                //determine the display size of the row number string using
                //the DataGridView's current font.
                SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);

                //adjust the width of the column that contains the row header cells 
                //if necessary
                if (grdResult.RowHeadersWidth < (int)(size.Width + 20)) grdResult.RowHeadersWidth = (int)(size.Width + 20);

                //this brush will be used to draw the row number string on the
                //row header cell using the system's current ControlText color
                Brush b = SystemBrushes.ControlText;

                //draw the row number string on the current row header cell using
                //the brush defined above and the DataGridView's default font
                e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();

            _tblResult = new SubSonic.Select().From(TResultDetail.Schema.Name).ExecuteDataSet().Tables[0];
            sw.Stop();

            MessageBox.Show(sw.ElapsedMilliseconds.ToString());

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            grdResult.DataSource = _tblResult;
            sw.Stop();

            MessageBox.Show(sw.ElapsedMilliseconds.ToString());

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
             IDataReader _dr;
            Stopwatch sw = Stopwatch.StartNew();
            _dr = new SubSonic.Select().From(TResultDetail.Schema.Name).ExecuteReader();
            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + ":::::" + _dr.RecordsAffected.ToString());

            //sw = Stopwatch.StartNew();
            //while (_dr.Read())
            //{
                
            //}
            //sw.Stop();

            sw = Stopwatch.StartNew();
            int count = new SubSonic.Select().From(TResultDetail.Schema.Name).GetRecordCount();
            sw.Stop();

            MessageBox.Show(string.Format("{0}:::::{1}", sw.ElapsedMilliseconds.ToString(), count.ToString()));

        }

        private void grdResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
    }
}