
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.List.UI.Properties;
using Vietbait.Lablink.Model;
using VietBaIT.CommonLibrary;
using System.Collections.Generic;

namespace Vietbait.Lablink.List.UI

{
    public partial class FrmGeneralListV2 : Form
    {
        #region Attributes

        private string MsgDelete = "Bạn đã xóa thành công";
        private string MsgEror = "Có lỗi xảy ra khi thêm dữ liệu";
        private string MsgExits = "Đã tồn tại";
        private string MsgNew = "Bạn đã thêm mới thành công";
        private string MsgUpdate = "Bạn đã cập nhật thành công";
        private DataTable _dtDataConTrol;
        private DataTable _dtTcpip;
        private DataTable _dtlRs232;
        //private DataTable _ChangeStatus = new DataTable();
        private string _rowFilter = "1=1";
        private bool b_StatusClick;
        private DataTable m_dtDataList = new DataTable();
        private int v_Device_Id = -1;

        #endregion

        public FrmGeneralListV2()
        {
            InitializeComponent();
        }

        #region Events

        //Kiểm tra khi thao tác dữ liệu
        private void grdAll_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (grdAll.Tag == btnTestType.Tag)
            {
                MessageBox.Show(@"Thông tin kiểu số. Mời nhập lại !", "Thông báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else if (grdAll.Tag == btnRS232.Tag)
            {
                MessageBox.Show(@"Thông tin kiểu số. Mời nhập lại !", "Thông báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else if (grdAll.Tag == btnCodeToCtrlEquip.Tag)
            {
                MessageBox.Show(@"Thông tin kiểu số. Mời nhập lại !", "Thông báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void grdDatacontrol_SelectionChanged(object sender, EventArgs e)
        {
            //if (b_StatusClick)
            //{
            //    if (grdDatacontrol.CurrentRow != null)
            //    {
            //        v_Device_Id =
            //            Utility.Int32Dbnull(
            //                Utility.GetValueFromGridColumn(grdDatacontrol, "Device_ID", grdDatacontrol.CurrentRow.Index),
            //                -1);
            //        _rowFilter = "-1=" + v_Device_Id + " or Device_ID=" + v_Device_Id;
            //    }
            //    else
            //    {
            //        _rowFilter = "1=2";
            //    }
            //    m_dtDataList.DefaultView.RowFilter = _rowFilter;
            //    m_dtDataList.AcceptChanges();
            //}

            if (grdDatacontrol.CurrentRow != null)
            {
                m_dtDataList =
                    CodeToCtrlEquip.GetDataControlForDevice(
                        grdDatacontrol.CurrentRow.Cells[DDeviceList.Columns.DeviceId].Value.ToString());
                grdAll.DataSource = m_dtDataList;
                SetAttributesGridDataControl();
            }
        }

        private void FrmGeneralList_Load(object sender, EventArgs e)
        {
            grdAll.RowPostPaint += Utilities.UI.GridviewRowPostPaint;
            grdDatacontrol.RowPostPaint += Utilities.UI.GridviewRowPostPaint;
        }

        private void FrmGeneralList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
            }
        }

        //Xóa thông tin từ database
        private void grdAll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            string SelectedTag = GetSelectedTag();
            if (grdAll.Tag == btnEquip.Tag)
            {
                DeleteDevice();
            }
            else if (grdAll.Tag == btnTestType.Tag)
            {
                DeleteTestType();
            }
            else if (grdAll.Tag == btnManufacturer.Tag)
            {
                DeleteManuafacture();
            }
            else if (grdAll.Tag == btnTCPIP.Tag)
            {
                DeleteTcpIp();
            }
            else if (grdAll.Tag == btnRS232.Tag)
            {
                DeleteRs232();
            }
            else if (grdAll.Tag == btnCodeToCtrlEquip.Tag)
            {
                DeleteDataControl();
            }
        }

        private void btnDeviceLibrary_Click(object sender, EventArgs e)
        {
            var ofn = new OpenFileDialog();
            ofn.Filter = @"Dll Files (*.dll)|*.dll|All Files (*.*)|*.*";
            ofn.Title = @"Open Dll File";
            //ofn.ShowDialog();
            if (ofn.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (true)
                    {
                        //txtDeviceName.Text = ofn.FileName;
                        txtDeviceLibPath.Text = ofn.FileNames[0];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdAll.CurrentRow != null)
                {
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceConnectorType].Value = rdR232.Checked;
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.ConnectorID].Value = cboPort.SelectedValue;
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.Protocol].Value = rdOne.Checked;
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.Valid].Value = rdUsed.Checked;
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.ManufactureId].Value = cboManufacturer.SelectedValue;
                    grdAll.CurrentRow.Cells["Manufacture_Name"].Value = cboManufacturer.Text;
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.TestTypeId].Value = cboTest.SelectedValue;
                    grdAll.CurrentRow.Cells[TTestTypeList.Columns.TestTypeName].Value = cboTest.Text;

                    if (File.Exists(txtDeviceLibPath.Text))
                    {
                        var fi = new FileInfo(txtDeviceLibPath.Text);
                        grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceLibrary].Value = fi.Name;
                    }
                    else grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceLibrary].Value = "";


                    grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceNameSpace].Value = cboDeviceNameSpace.Text;
                    grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceClass].Value = cboDeviceClass.Text;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtDeviceLib_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txtDeviceLibPath.Text))
                {
                    string strProperties = "Properties";
                    Assembly assembly = Assembly.LoadFrom(txtDeviceLibPath.Text);


                    List<string> allNameSpace = (from s in assembly.GetTypes()
                                                 where s.Namespace.IndexOf(strProperties) < 0
                                                 select s.Namespace).Distinct().ToList();
                    if (allNameSpace.Count > 0)
                    {
                        cboDeviceNameSpace.DataSource = allNameSpace;
                        cboDeviceNameSpace.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cboDeviceNameSpace_SelectedIndexChanged(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.LoadFrom(txtDeviceLibPath.Text);

            List<string> allClass = (from s in assembly.GetTypes()
                                     where s.Namespace.Equals(cboDeviceNameSpace.Text)
                                     select s.Name).ToList();

            if (allClass.Count > 0)
            {
                cboDeviceClass.DataSource = allClass;
                cboDeviceClass.SelectedIndex = 0;
            }
        }

        private void rdR232_CheckedChanged(object sender, EventArgs e)
        {
            //Load Data Port
            if (rdR232.Checked)
            {
                DataTable dtRs232 = PortListBusiness.GetRs232NameAndIdList();
                cboPort.DataSource = dtRs232.DefaultView;
                cboPort.DisplayMember = LRS232.Columns.Name;
                cboPort.ValueMember = LRS232.Columns.Id;
            }
            else if (rdTCPIP.Checked)
            {
                DataTable dtTcpip = PortListBusiness.GetTcpipNameAndIdList();
                cboPort.DataSource = dtTcpip.DefaultView;
                cboPort.DisplayMember = LTcpip.Columns.IPAddress;
                cboPort.ValueMember = LTcpip.Columns.Id;
            }
        }

        private void grdAll_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdAll.Tag == btnEquip.Tag)
                {
                    cboTest.SelectedIndex =
                        cboTest.FindStringExact(
                            grdAll.CurrentRow.Cells[TTestTypeList.Columns.TestTypeName].Value.ToString());
                    cboManufacturer.SelectedIndex =
                        cboManufacturer.FindStringExact(grdAll.CurrentRow.Cells["Manufacture_Name"].Value.ToString());
                    rdR232.Checked =
                        grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceConnectorType].Value.ToString() == "1";
                    rdTCPIP.Checked =
                        grdAll.CurrentRow.Cells[DDeviceList.Columns.DeviceConnectorType].Value.ToString() == "0";

                    //cboPort.SelectedIndex = cboPort.f;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Set Value for pItem from Datarow

        //Set Device Value from Datarow
        private void SetValueFromRow(DDeviceList pItem, DataRow dr)
        {
            pItem.DeviceType = null;
            pItem.TestTypeId = Utility.Int16Dbnull(dr[DDeviceList.Columns.TestTypeId]);
            pItem.DeviceName = dr[DDeviceList.Columns.DeviceName].ToString();
            pItem.ManufactureId = Utility.Int16Dbnull(dr[DDeviceList.Columns.ManufactureId]);
            pItem.Valid = string.IsNullOrEmpty(dr[DDeviceList.Columns.Valid].ToString()) ? false : true;
            pItem.Description = dr[DDeviceList.Columns.Description].ToString();
            pItem.Protocol = string.IsNullOrEmpty(dr[DDeviceList.Columns.Protocol].ToString()) ? false : true;
            pItem.DeviceLibrary = dr[DDeviceList.Columns.DeviceLibrary].ToString();
            pItem.DeviceNameSpace = dr[DDeviceList.Columns.DeviceNameSpace].ToString();
            pItem.DeviceClass = dr[DDeviceList.Columns.DeviceClass].ToString();
            pItem.ComputerName = dr[DDeviceList.Columns.ComputerName].ToString();
            pItem.DeviceConnectorType = Utility.ByteDbnull(dr[DDeviceList.Columns.DeviceConnectorType]);
            pItem.ConnectorID = Utility.ByteDbnull(dr[DDeviceList.Columns.ConnectorID]);
        }

        //Set DataControl Value from Datarow
        private void SetValueFromRow(DDataControl pItem, DataRow dr)
        {
            pItem.DeviceId =
                Utility.Int32Dbnull(grdDatacontrol.CurrentRow.Cells[DDeviceList.Columns.DeviceId].Value.ToString());
            pItem.DataTypeId = Utility.Int32Dbnull(dr[DDataControl.Columns.DataTypeId]);
            pItem.DataSequence = Utility.Int32Dbnull(dr[DDataControl.Columns.DataSequence]);
            pItem.ControlType = string.IsNullOrEmpty(dr[DDataControl.Columns.ControlType].ToString()) ? false : true;
            pItem.DataName = dr[DDataControl.Columns.DataName].ToString();
            pItem.AliasName = dr[DDataControl.Columns.AliasName].ToString();
            pItem.MeasureUnit = dr[DDataControl.Columns.MeasureUnit].ToString();
            pItem.DataPoint = Utility.Int16Dbnull(dr[DDataControl.Columns.DataPoint], 1);
            pItem.NormalLevel = dr[DDataControl.Columns.NormalLevel].ToString();
            pItem.NormalLevelW = dr[DDataControl.Columns.NormalLevelW].ToString();
            pItem.DataView = string.IsNullOrEmpty(dr[DDataControl.Columns.DataView].ToString()) ? false : true;
            pItem.DataPrint = string.IsNullOrEmpty(dr[DDataControl.Columns.DataPrint].ToString()) ? false : true;
            pItem.Description = dr[DDataControl.Columns.Description].ToString();
            pItem.DataType = "";
        }

        #endregion

        #region UpdateDataToDB

        // Update bảng thiết bị
        private void UpdateDevice()
        {
            try
            {
                foreach (DataRow dr in m_dtDataList.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        var pItem = new DDeviceList();
                        SetValueFromRow(pItem, dr);
                        dr[DDeviceList.Columns.DeviceId] = DevicesListBusiness.InsertDevice(pItem);
                        ;
                    }
                    else
                    {
                        var pItem = new DDeviceList(Convert.ToInt16(dr[DDeviceList.Columns.DeviceId]));
                        SetValueFromRow(pItem, dr);
                        DevicesListBusiness.UpdateDevice(pItem);
                    }
                }
                SetTextForWarning(MsgUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update bản mã điều khiển thiết bị - Data Control
        public void UpdateDataControl()
        {
            try
            {
                var m_dtDataList = grdAll.DataSource as DataTable;
                foreach (DataRow dr in m_dtDataList.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        var pItem = new DDataControl();
                        SetValueFromRow(pItem, dr);
                        dr[DDataControl.Columns.DataControlId] = CodeToCtrlEquip.InsertDataControl(pItem);
                        dr[DDataControl.Columns.DeviceId] =
                            grdDatacontrol.CurrentRow.Cells[DDeviceList.Columns.DeviceId].Value.ToString();
                        SetTextForWarning(MsgNew);
                    }
                    else
                    {
                        var pItem = new DDataControl(Convert.ToInt16(dr[DDataControl.Columns.DataControlId]));
                        SetValueFromRow(pItem, dr);
                        CodeToCtrlEquip.UpdateDataControl(pItem);
                    }
                }
                SetTextForWarning(MsgUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_dtDataList.AcceptChanges();
            }
        }

        // xử lý update and Insert data TcpIp
        public void UpdateTcpIp()
        {
            LTcpip items;
            string saddresIp = "";
            int addresIp = 0;
            var _dtTcpip = grdAll.DataSource as DataTable;
            for (int i = 0; i < _dtTcpip.Rows.Count; i++)
            {
                DataRow dataRow = _dtTcpip.Rows[i];
                object c = dataRow[LTcpip.Columns.Id];
                if (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Added)
                {
                    saddresIp = dataRow[LTcpip.Columns.IPAddress].ToString();
                    if (saddresIp == "")
                    {
                        SetTextForWarning(MsgEror);
                        continue;
                    }
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    items = new LTcpip(dataRow[LTcpip.Columns.Id]);
                    items.IPAddress = dataRow[LTcpip.Columns.IPAddress].ToString();
                    items.Description = dataRow[LTcpip.Columns.Description].ToString();
                    if (!InValidata(items)) return;
                    string id = HaBusiness.UpdateTcpip1(items);
                    dataRow[LTcpip.Columns.Id] = id;
                    SetTextForWarning(MsgUpdate);
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
                        MessageBox.Show("Tên cổng " + items.IPAddress + " đã tồn tại", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        dataRow.Delete();
                    }
                        // ko tồn tại sẽ insert
                    else
                    {
                        dataRow[LTcpip.Columns.Id] = id;
                        SetTextForWarning(MsgNew);
                    }
                }
            }
            _dtTcpip.AcceptChanges();
        }

        //kiểm tra dữ liệu nhập vào LTcpip
        private bool InValidata(LTcpip items)
        {
            if (!HaBusiness.InValiTCP(items))
            {
                // SetTextForWarning(MsgExits);
                MessageBox.Show("Đã tồn tại " + items.IPAddress + " Mời bạn nhập lại", "Thông báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private bool InValidata(LRS232 items)
        {
            if (!MainBusiness.InValiRs232(items))
            {
                MessageBox.Show("Đã tồn tại " + items.Name + " Mời bạn nhập lại", "Thông báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        // xử lý update and Insert data Rs232
        public void UpdateRs232()
        {
            LRS232 pItem;
            string Nameport = "", spatity = "", srts = "", sbaudrate = "", sdatabit = "", sdtr = "", sstopbit = "";
            int patity = 0, rts = 0, baudrate = 0, databit = 0, dtr = 0, stopbit = 0;
            ;
            var _dtlRs232 = grdAll.DataSource as DataTable;
            for (int i = 0; i < _dtlRs232.Rows.Count; i++)
            {
                DataRow dataRow = _dtlRs232.Rows[i];
                object c = dataRow[LRS232.Columns.Id];

                if (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Added)
                {
                    pItem = new LRS232(dataRow[LRS232.Columns.Id]);
                    spatity = dataRow[LRS232.Columns.Parity].ToString();
                    Nameport = dataRow[LRS232.Columns.Name].ToString();
                    srts = dataRow[LRS232.Columns.Rts].ToString();
                    sbaudrate = dataRow[LRS232.Columns.BaudRate].ToString();
                    sdatabit = dataRow[LRS232.Columns.DataBits].ToString();
                    sdtr = dataRow[LRS232.Columns.Dtr].ToString();
                    sstopbit = dataRow[LRS232.Columns.StopBits].ToString();
                    pItem.Description = dataRow[LRS232.Columns.Description].ToString();
                    if (spatity == "" || srts == "" || sbaudrate == "" || sstopbit == "" || sdatabit == "" || sdtr == "")
                    {
                        patity = -1;
                        rts = -1;
                        baudrate = -1;
                        databit = -1;
                        dtr = -1;
                        stopbit = -1;
                    }
                    else
                    {
                        Nameport = dataRow[LRS232.Columns.Name].ToString();
                        patity = Convert.ToInt32(dataRow[LRS232.Columns.Parity]);
                        rts = Convert.ToInt32(dataRow[LRS232.Columns.Rts]);
                        baudrate = Convert.ToInt32(dataRow[LRS232.Columns.BaudRate]);
                        databit = Convert.ToInt32(dataRow[LRS232.Columns.DataBits]);
                        dtr = Convert.ToInt32(dataRow[LRS232.Columns.Dtr]);
                        stopbit = Convert.ToInt32(dataRow[LRS232.Columns.StopBits]);
                        pItem.Description = dataRow[LRS232.Columns.Description].ToString();
                    }

                    if (Nameport == "" || patity == -1 || rts == -1 || baudrate == -1 || databit == -1 || stopbit == -1)
                    {
                        SetTextForWarning(MsgEror);
                        continue;
                    }
                }
                if (dataRow.RowState == DataRowState.Modified)
                {
                    pItem = new LRS232(dataRow[LRS232.Columns.Id]);
                    pItem.Name = Nameport;
                    pItem.Parity = patity;
                    pItem.Rts = rts;
                    pItem.DataBits = databit;
                    pItem.BaudRate = baudrate;
                    pItem.Dtr = dtr;
                    pItem.StopBits = stopbit;
                    pItem.Description = dataRow[LRS232.Columns.Description].ToString();
                    if (!InValidata(pItem)) return;
                    string id = MainBusiness.UpdateRs232(pItem);
                    dataRow[LRS232.Columns.Id] = id;
                    SetTextForWarning(MsgUpdate);
                    //MainBusiness.UpdateRs232(pItem);
                    //SetTextForWarning(MsgUpdate);
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
                        MessageBox.Show("Tên cổng " + pItem.Name + " đã tồn tại ", "Thông báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        dataRow.Delete();
                    }
                    else
                    {
                        dataRow[LRS232.Columns.Id] = id;
                        SetTextForWarning(MsgNew);
                    }
                }
            }
            _dtlRs232.AcceptChanges();
        }

        //Update and Insert data in LDepartment
        public void UpdateDepartment()
        {
            LDepartment pitems;
            string Name = "";
            var dataTable = grdAll.DataSource as DataTable;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                object c = dr[LDepartment.Columns.SName];
                //Kiểm tra trạng thái của dòng:
                if ((dr.RowState == DataRowState.Modified) || (dr.RowState == DataRowState.Added))
                {
                    Name = dr[LDepartment.Columns.SName].ToString();

                    if ((Name == ""))
                    {
                        SetTextForWarning(MsgEror);
                        continue;
                    }
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    pitems = new LDepartment(dr[LDepartment.Columns.Id]);
                    pitems.SName = Name;
                    pitems.SDesc = dr[LDepartment.Columns.SDesc].ToString();
                    DepartmentBusiness.UpdateDepartment(pitems);
                    SetTextForWarning(MsgUpdate);
                }
                else if (dr.RowState == DataRowState.Added)
                {
                    pitems = new LDepartment();
                    pitems.SName = Name;
                    pitems.SDesc = dr[LDepartment.Columns.SDesc].ToString();
                    dr[LDepartment.Columns.Id] = DepartmentBusiness.InsertDepartment(pitems);
                    SetTextForWarning(MsgNew);
                }
            }
            dataTable.AcceptChanges();
        }

        /// Xử lý thêm mới  và update cho bảng Manuafacture
        private void UpdateManuafacture()
        {
            LManufacture pitems;
            string sName = "", sDesc = "";
            var dataTable = grdAll.DataSource as DataTable;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                object c = dr[LManufacture.Columns.SName];
                //Kiểm tra trạng thái của dòng:
                if ((dr.RowState == DataRowState.Modified) || (dr.RowState == DataRowState.Added))
                {
                    sName = dr[LManufacture.Columns.SName].ToString();
                    // sDesc = dr[LManufacture.Columns.SDesc].ToString();
                    if ((sName == ""))
                    {
                        warningBox1.Text = MsgEror;
                        continue;
                    }
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    pitems = new LManufacture(dr[LManufacture.Columns.Id]);
                    pitems.SName = sName;
                    pitems.SDesc = sDesc;
                    ManufactureBusiness.UpdateManuafacture(pitems);
                    warningBox1.Text = MsgUpdate;
                }
                else if (dr.RowState == DataRowState.Added)
                {
                    pitems = new LManufacture();
                    pitems.SName = sName;
                    pitems.SDesc = sDesc;
                    dr[LManufacture.Columns.Id] = ManufactureBusiness.InsertManufacture(pitems);
                    warningBox1.Text = MsgNew;
                }
            }
            dataTable.AcceptChanges();
        }

        /// Xử lý thêm mới và update cho bảng Doctor
        public void UpdateDoctor()
        {
            LUser pitems;
            string Username = "", RoleId = "";
            var dataTable = grdAll.DataSource as DataTable;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                object c = dr[LUser.Columns.UserId];
                //Kiểm tra trạng thái của dòng:
                if ((dr.RowState == DataRowState.Modified) || (dr.RowState == DataRowState.Added))
                {
                    Username = dr[LUser.Columns.UserName].ToString();
                    RoleId = dr[LUser.Columns.RoleId].ToString();
                    if ((Username == "") || (RoleId == ""))
                    {
                        SetTextForWarning(MsgEror);
                        continue;
                    }
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    pitems = new LUser(dr[LUser.Columns.UserId]);
                    pitems.UserName = Username;
                    pitems.RoleId = Convert.ToInt32(RoleId);
                    DoctorBusiness.UpdateDoctor(pitems);
                    SetTextForWarning(MsgUpdate);
                }
                else if (dr.RowState == DataRowState.Added)
                {
                    pitems = new LUser();
                    pitems.UserName = Username;
                    pitems.RoleId = Convert.ToInt32(RoleId);
                    dr[LUser.Columns.UserId] = DoctorBusiness.InsertDoctor(pitems);
                    SetTextForWarning(MsgNew);
                }
            }
            dataTable.AcceptChanges();
        }

        /// Xử lý thêm và sửa cho bảng Testypelist
        private void UpdateTestType()
        {
            TTestTypeList pItem;
            string TestName = "", sStt = "", sinchitiet = "", sgia = "";
            int Stt = 0, inchitiet = 0, gia = 0;

            var dataTable = grdAll.DataSource as DataTable;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                //Kiểm tra trạng thái của dòng:
                object c = dr[TTestTypeList.Columns.TestTypeId];
                //Thêm mới
                if ((dr.RowState == DataRowState.Modified) || (dr.RowState == DataRowState.Added))
                {
                    //pItem = new TTestTypeList();
                    TestName = dr[TTestTypeList.Columns.TestTypeName].ToString();
                    sStt = dr[TTestTypeList.Columns.IntOrder].ToString();
                    sinchitiet = dr[TTestTypeList.Columns.PrintDetail].ToString();
                    sgia = dr[TTestTypeList.Columns.Abbreviation].ToString();
                    //Stt = Convert.ToInt32(dr[TTestTypeList.Columns.IntOrder]);
                    //inchitiet = Convert.ToInt32(dr[TTestTypeList.Columns.IntOrder]);
                    if ((TestName == "") || (sStt == "") || (sinchitiet == "") || (sgia == ""))
                    {
                        SetTextForWarning(MsgEror);
                        continue;
                    }
                }

                if (dr.RowState == DataRowState.Modified)
                {
                    pItem = new TTestTypeList(dr[TTestTypeList.Columns.TestTypeId]);
                    pItem.TestTypeName = dr[TTestTypeList.Columns.TestTypeName].ToString();
                    pItem.Note = dr[TTestTypeList.Columns.Note].ToString();
                    pItem.IntOrder = Convert.ToInt16(dr[TTestTypeList.Columns.IntOrder]);
                    pItem.PrintDetail = Convert.ToInt16(dr[TTestTypeList.Columns.PrintDetail]);
                    pItem.Abbreviation = (dr[TTestTypeList.Columns.Abbreviation].ToString());
                    TestTypeListBusiness.UpdateTestTypeList(pItem);
                    SetTextForWarning(MsgUpdate);
                }
                else if (dr.RowState == DataRowState.Added)
                {
                    // pItem = new TTestTypeList(dr[TTestTypeList.Columns.TestTypeId]);
                    pItem = new TTestTypeList();
                    pItem.TestTypeName = dr[TTestTypeList.Columns.TestTypeName].ToString();
                    pItem.Note = dr[TTestTypeList.Columns.Note].ToString();
                    pItem.IntOrder = Convert.ToInt16(dr[TTestTypeList.Columns.IntOrder]);
                    pItem.PrintDetail = Convert.ToInt16(dr[TTestTypeList.Columns.PrintDetail]);
                    pItem.Abbreviation = (dr[TTestTypeList.Columns.Abbreviation].ToString());
                    TestTypeListBusiness.InsertTestTypeList(pItem);
                    SetTextForWarning(MsgNew);
                    // string id = TestTypeListBusiness.InsertTestTypeList(pItem);
                    //if (id == "-1")
                    //{
                    //    SetTextForWarning(MsgEror);
                    //    dr.Delete();
                    //}
                    //else
                    //{
                    //    dr[TTestTypeList.Columns.TestTypeId] = id;
                    //    SetTextForWarning(MsgNew);
                    //}
                }
            }
            dataTable.AcceptChanges();
        }

        #endregion

        #region DeleteDataFromDB

        //Xử lý xóa dữ liệu trên bảng TestTypeList
        private void DeleteDevice()
        {
            try
            {
                DataGridViewRow currentRow = grdDatacontrol.CurrentRow;

                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[DDeviceList.Columns.DeviceId]);
                    if (pid != "")
                    {
                        if (
                            MessageBox.Show("Bạn có muốn xóa thiết bị này không? ", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            DevicesListBusiness.DeleteDevice(Convert.ToInt32(pid));
                            grdDatacontrol.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                (grdDatacontrol.DataSource as DataTable).AcceptChanges();
            }
        }

        //Xử lý xóa dữ liệu trên bảng DataContol
        private void DeleteDataControl()
        {
            try
            {
                DataGridViewRow currentRow = grdAll.CurrentRow;
                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[DDataControl.Columns.DataControlId]);
                    if (pid != "")
                    {
                        if (
                            MessageBox.Show("Bạn có muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CodeToCtrlEquip.DeleteDataControl(Convert.ToInt32(pid));
                            grdAll.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        //Xử lý xóa dữ liệu trên bảng TestTypeList
        private void DeleteTestType()
        {
            try
            {
                DataGridViewRow currentRow = grdAll.CurrentRow;

                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[TTestTypeList.Columns.TestTypeId]);
                    //int pid = Convert.ToInt32(currentRow.Cells[TTestTypeList.Columns.TestTypeId].Value);
                    if (pid != "")
                    {
                        if (
                            MessageBox.Show("Bạn có muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            TestTypeListBusiness.DelteTestTypeList(Convert.ToInt32(pid));
                            grdAll.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        //Xử lý xóa dữ liệu trên bảng Manuafacture
        private void DeleteManuafacture()
        {
            try
            {
                DataGridViewRow currentRow = grdAll.CurrentRow;

                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[LManufacture.Columns.Id]);
                    if (pid != "")
                    {
                        // int pid = Convert.ToInt32(.Value);
                        if (
                            MessageBox.Show("Bạn có muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            ManufactureBusiness.DeleteManuafacture(Convert.ToInt32(pid));
                            grdAll.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ko thể xóa dữ liệu");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ko thể xóa dòng");
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        ////Xử lý xóa dữ liệu trên bảng Doctor
        private void DeleteDocTor()
        {
            try
            {
                DataGridViewRow currentRow = grdAll.CurrentRow;

                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[LUser.Columns.UserId]);
                    if (pid != "")
                    {
                        // int pid = Convert.ToInt32(.Value);
                        if (
                            MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            DoctorBusiness.DeleteDoctor(Convert.ToInt32(pid));
                            grdAll.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                    else
                    {
                        SetTextForWarning(MsgEror);
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextForWarning(MsgEror);
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        //Xóa dữ liệu trên bảng LDepartment
        private void DeleteDepartment()
        {
            try
            {
                DataGridViewRow currentRow = grdAll.CurrentRow;

                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[LDepartment.Columns.Id]);
                    if (pid != "")
                    {
                        if (
                            MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            DepartmentBusiness.DeleteDepartment(Convert.ToInt32(pid));
                            grdAll.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                    else
                    {
                        SetTextForWarning(MsgEror);
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextForWarning(MsgEror);
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        //Xóa dữ liệu trên bảng TcpIp
        private void DeleteTcpIp()
        {
            try
            {
                DataGridViewRow currentRow = grdAll.CurrentRow;
                if (currentRow != null)
                {
                    string pid = Utilities.UI.GetCellValue(currentRow.Cells[LTcpip.Columns.Id]);
                    if (pid != "")
                    {
                        if (
                            MessageBox.Show("Bạn có muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            HaBusiness.DeleteTcpip(Convert.ToInt32(pid));
                            grdAll.Rows.Remove(currentRow);
                            SetTextForWarning(MsgDelete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        //Xóa dữ liệu trên bảng Rs232
        private void DeleteRs232()
        {
            try
            {
                LRS232 pItem;
                DataGridViewRow currentRow = grdAll.CurrentRow;

                string pid = Utilities.UI.GetCellValue(currentRow.Cells[LRS232.Columns.Id]);
                if (pid != "")
                {
                    if (
                        MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) ==
                        DialogResult.Yes)
                    {
                        MainBusiness.DeleteRs232(Convert.ToInt32(pid));
                        grdAll.Rows.Remove(currentRow);
                        SetTextForWarning(MsgDelete);
                    }
                    else
                    {
                        SetTextForWarning(MsgEror);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                (grdAll.DataSource as DataTable).AcceptChanges();
            }
        }

        #endregion

        #region Main Buttons Click Event

        // Xử lý tạo grid Device
        private void btnEquip_Click(object sender, EventArgs e)
        {
            b_StatusClick = true;
            btnAdd.Enabled = true;


            grdAll.DataSource = null;
            //Xóa sạch dữ liệu
            grdAll.Rows.Clear();
            grdAll.Columns.Clear();
            grdAll.Tag = btnEquip.Tag;

            m_dtDataList = DevicesListBusiness.GetAllDevicesList();
            //m_dtDataList.Columns.Add("colChangeStatus", typeof (byte), "0");


            grdAll.DataSource = m_dtDataList;
            //grdAll.Columns["colChangeStatus"].DisplayIndex = 0;

            //Load Data cho cboTest
            DataTable dtTestType = TestTypeListBusiness.GetTestTypeNameAndIdList();
            cboTest.DataSource = dtTestType.DefaultView;
            cboTest.DisplayMember = TTestTypeList.Columns.TestTypeName;
            cboTest.ValueMember = TTestTypeList.Columns.TestTypeId;

            //Load Data cho cboManufacturer
            DataTable dtManufacture = ManufactureBusiness.GetManufacturerNameAndIdList();
            cboManufacturer.DataSource = dtManufacture.DefaultView;
            cboManufacturer.DisplayMember = LManufacture.Columns.SName;
            cboManufacturer.ValueMember = LManufacture.Columns.Id;

            rdR232_CheckedChanged(rdR232, new EventArgs());
            SetAttributesGridDevice();
        }

        // Xử lý tạo grid DataControl
        private void btnCodeToCtrlEquip_Click(object sender, EventArgs e)
        {
            try
            {
                b_StatusClick = true;
                grdAll.DataSource = null;
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnCodeToCtrlEquip.Tag;

                _dtDataConTrol = DevicesListBusiness.GetDeviceNameAndIdList();
                DataRow dr = _dtDataConTrol.NewRow();
                // dr[DDeviceList.Columns.DeviceId] = -1;
                //string strTatca = "Tất cả";
                //dr[DDeviceList.Columns.DeviceName] = strTatca;
                //_dtDataConTrol.Rows.InsertAt(dr, 0);
                grdDatacontrol.DataSource = _dtDataConTrol;

                //Thêm cột hình ảnh trạng thái của nút Save
                var newCol = new DataGridViewImageColumn();
                newCol.HeaderText = "S";
                newCol.Name = "colSaveStatus";
                newCol.Image = Resources.agt_action_success;
                newCol.Visible = true;
                newCol.Width = 40;
                grdAll.Columns.Add(newCol);
                grdAll.Columns["colSaveStatus"].DisplayIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Xử lý tạo grid  btnTestType_Click
        private void btnTestType_Click(object sender, EventArgs e)
        {
            try
            {
                grdAll.DataSource = null;
                //Xóa sạch dữ liệu
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnTestType.Tag;
                //Tạo lưới để hiển thị dữ liệu

                //Cột ID (TestType_ID)
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(TTestTypeList.Columns.TestTypeId, "ID",
                                                                     TTestTypeList.Columns.TestTypeId, null, true));

                //Cột TestType_Name 
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(TTestTypeList.Columns.TestTypeName,
                                                                     "Tên Loại Xét Nghiệm",
                                                                     TTestTypeList.Columns.TestTypeName));

                //Cột Note
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(TTestTypeList.Columns.Note, "Ghi Chú",
                                                                     TTestTypeList.Columns.Note));

                //Cột IntOrder
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(TTestTypeList.Columns.IntOrder, "Số TT",
                                                                     TTestTypeList.Columns.IntOrder));

                // Cột PrintDetail
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(TTestTypeList.Columns.PrintDetail, "In Chi Tiết",
                                                                     TTestTypeList.Columns.PrintDetail));

                // Cột Abbreviation
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(TTestTypeList.Columns.Abbreviation,
                                                                     "Tên viết tắt XN",
                                                                     TTestTypeList.Columns.Abbreviation));

                grdAll.DataSource = TestTypeListBusiness.GetAllTestTypeList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // xử lý  tạo grid Rs232
        private void btnRS232_Click(object sender, EventArgs e)
        {
            try
            {
                grdAll.DataSource = null;
                //Xóa sạch dữ liệu
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnRS232.Tag;
                //Tạo lưới để hiển thị dữ liệu

                //Cột ID (Id)
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.Id, "ID",
                                                                     LRS232.Columns.Id, null, false));
                //Cột Name 
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.Name,
                                                                     "Tên cổng",
                                                                     LRS232.Columns.Name));
                //Cột BaudRate
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.BaudRate, "BaudRate",
                                                                     LRS232.Columns.BaudRate));
                //Cột DataBits
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.DataBits, "DataBits",
                                                                     LRS232.Columns.DataBits));
                // Cột StopBits
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.StopBits, "StopBits",
                                                                     LRS232.Columns.StopBits));
                // Cột Parity
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.Parity, "Parity",
                                                                     LRS232.Columns.Parity));

                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.Dtr, "Dtr",
                                                                     LRS232.Columns.Dtr));

                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.Rts, "Rts",
                                                                     LRS232.Columns.Rts));

                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LRS232.Columns.Description, "Description",
                                                                     LRS232.Columns.Description));


                grdAll.DataSource = MainBusiness.GetAllRs232Port();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // xử lý  tạo grid TcpIp
        private void btnTCPIP_Click(object sender, EventArgs e)
        {
            try
            {
                grdAll.DataSource = null;
                //Xóa sạch dữ liệu
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnTCPIP.Tag;
                //Tạo lưới để hiển thị dữ liệu

                //Cột ID (Id)
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LTcpip.Columns.Id, "ID",
                                                                     LTcpip.Columns.Id, null, false));
                //Cột IPAddress 
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LTcpip.Columns.IPAddress,
                                                                     "Địa chỉ IP",
                                                                     LTcpip.Columns.IPAddress));
                // cột Mô tả
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LTcpip.Columns.Description,
                                                                     "Mô tả",
                                                                     LTcpip.Columns.Description));
                grdAll.DataSource = HaBusiness.GetAllTCPIP();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Xử lý tạo grid Manufacturer
        private void btnManufacturer_Click(object sender, EventArgs e)
        {
            try
            {
                grdAll.DataSource = null;
                //xóa dữ liệu trên grid
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnManufacturer.Tag;

                //Tạo lưới để hiển thị dữ liệu

                //Cột ID (ID)
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LManufacture.Columns.Id, "ID",
                                                                     LManufacture.Columns.Id, null, true));

                //Cột sName 
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LManufacture.Columns.SName, "Tên Hãng sản xuất",
                                                                     LManufacture.Columns.SName));

                //Cột SDes
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LManufacture.Columns.SDesc, "Mô tả",
                                                                     LManufacture.Columns.SDesc));

                grdAll.DataSource = ManufactureBusiness.GetAllManufacture();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        //Lưu thông tin vào DB
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdAll.Tag == btnEquip.Tag)
            {
                UpdateDevice();
            }
            else if (grdAll.Tag == btnTestType.Tag)
            {
                UpdateTestType();
            }
            else if (grdAll.Tag == btnManufacturer.Tag)
            {
                UpdateManuafacture();
            }

            else if (grdAll.Tag == btnTCPIP.Tag)
            {
                UpdateTcpIp();
            }
            else if (grdAll.Tag == btnRS232.Tag)
            {
                UpdateRs232();
            }
            else if (grdAll.Tag == btnCodeToCtrlEquip.Tag)
            {
                UpdateDataControl();
            }
        }

        #endregion

        #region Warning box

        // set text cho warningbox
        private void SetTextForWarning(string text)
        {
            warningBox1.Text = text;
            warmingboxTimer.Enabled = true;
        }

        //set timer tự động reset text
        private void warmingboxTimer_Tick(object sender, EventArgs e)
        {
            warmingboxTimer.Enabled = false;
            warningBox1.Text = string.Empty;
        }

        #endregion

        #region Set-ColumnHeaderText-ReadOnly-Visibility

        private void SetAttributesGridDataControl()
        {
            grdAll.Columns[DDataControl.Columns.DeviceId].HeaderText = "Mã thiết bị";
            grdAll.Columns[DDataControl.Columns.DataName].HeaderText = "Tên xét nghiệm";
            grdAll.Columns[DDataControl.Columns.AliasName].HeaderText = "Tên viết tắt";
            grdAll.Columns[DDataControl.Columns.MeasureUnit].HeaderText = "Đơn vị đo";
            grdAll.Columns[DDataControl.Columns.NormalLevel].HeaderText = "Mức BT Nam";
            grdAll.Columns[DDataControl.Columns.NormalLevelW].HeaderText = "Mức BT Nữ";
            grdAll.Columns[DDataControl.Columns.DataPoint].HeaderText = "Độ chính xác";
            grdAll.Columns[DDataControl.Columns.DataSequence].HeaderText = "Thứ tự in";
            grdAll.Columns[DDataControl.Columns.DataControlId].HeaderText = "Mã điều khiển";
            grdAll.Columns[DDataControl.Columns.Description].HeaderText = "Mô tả";

            grdAll.Columns[DDataControl.Columns.DeviceId].ReadOnly = true;
            grdAll.Columns[DDataControl.Columns.DataTypeId].ReadOnly = true;
            grdAll.Columns[DDataControl.Columns.DataControlId].ReadOnly = true;
            grdAll.Columns[DDataControl.Columns.DataType].Visible = false;
            grdAll.Columns[DDataControl.Columns.DataTypeId].Visible = false;
        }

        private void SetAttributesGridDevice()
        {
            grdAll.Columns[DDeviceList.Columns.DeviceId].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.TestTypeId].ReadOnly = true;
            grdAll.Columns[TTestTypeList.Columns.TestTypeName].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.ManufactureId].ReadOnly = true;
            grdAll.Columns["Manufacture_Name"].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.DeviceConnectorType].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.ConnectorID].ReadOnly = true;
            //grdAll.Columns[DDeviceList.Columns.Valid].ReadOnly = true;
            //grdAll.Columns[DDeviceList.Columns.Protocol].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.DeviceLibrary].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.DeviceNameSpace].ReadOnly = true;
            grdAll.Columns[DDeviceList.Columns.DeviceClass].ReadOnly = true;
        }

        #endregion

        //lấy các tag in các buttonitem
        private string GetSelectedTag()
        {
            string result = "";

            try
            {
                ButtonItem selectedItems = (from control in navigationPane1.Items.OfType<ButtonItem>()
                                            select control).First();
                result = selectedItems.Tag.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private void grdDatacontrol_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Delete) return;
                {
                    DeleteDevice();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}