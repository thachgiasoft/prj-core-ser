using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;

namespace Vietbait.Lablink.List.UI

{
    public partial class FrmPublicList : Office2007Form
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

        public FrmPublicList()
        {
            InitializeComponent();
            //BtnDepartmentClick(grdAll, new EventArgs());
        }

        #region Events

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

        #endregion

        #region UpdateDataToDB

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

        #endregion

        #region DeleteDataFromDB

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

        #endregion

        #region Main Buttons Click Event

        //thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        //Lưu thông tin vào DB
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdAll.Tag == btnDoctor.Tag)
            {
                UpdateDoctor();
            }
            else if (grdAll.Tag == btnDepartment.Tag)
            {
                UpdateDepartment();
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

        //lấy các tag in các buttonitem
        //private string GetSelectedTag()
        //{
        //    string result = "";

        //    try
        //    {
        //        ButtonItem selectedItems = (from control in navigationPane1.Items.OfType<ButtonItem>()
        //                                    select control).First();
        //        result = selectedItems.Tag.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return result;
        //}

        private void FrmPublicList_Load(object sender, EventArgs e)
        {
            grdAll.RowPostPaint += Utilities.UI.GridviewRowPostPaint;
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                grdAll.DataSource = null;
                //xóa dữ liệu trên grid
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnDepartment.Tag;

                //Cột ID (ID)
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LDepartment.Columns.Id, "ID",
                                                                     LDepartment.Columns.Id, null, true));

                //Cột SName 
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LDepartment.Columns.SName, "Tên Khoa",
                                                                     LDepartment.Columns.SName));

                //Cột SDesc
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LDepartment.Columns.SDesc, "Mô tả",
                                                                     LDepartment.Columns.SDesc));

                grdAll.DataSource = DepartmentBusiness.GetAllDepartment();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            grdAll.DataSource = DepartmentBusiness.GetAllDepartment();
        }

        private void btnDoctor_Click_1(object sender, EventArgs e)
        {
            try
            {
                grdAll.DataSource = null;
                //xóa dữ liệu trên grid
                grdAll.Rows.Clear();
                grdAll.Columns.Clear();
                grdAll.Tag = btnDoctor.Tag;

                //Cột ID (UserId)
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LUser.Columns.UserId, "ID",
                                                                     LUser.Columns.UserId, null, true));

                //Cột UserName 
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LUser.Columns.UserName, "Tên bác sỹ",
                                                                     LUser.Columns.UserName));

                //Cột RoleId
                grdAll.Columns.Add(Utilities.UI.CreateGridTextColumn(LUser.Columns.RoleId, "Vai trò",
                                                                     LUser.Columns.RoleId));
                grdAll.DataSource = DoctorBusiness.GetAllDoctor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdAll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            if (grdAll.Tag == btnDoctor.Tag)
            {
                DeleteDocTor();
            }
            else if (grdAll.Tag == btnDepartment.Tag)
            {
                DeleteDepartment();
            }
        }
    }
}