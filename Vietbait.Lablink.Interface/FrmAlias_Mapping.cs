using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Interface
{
    public partial class FrmAlias_Mapping : Office2007Form
    {
        #region Attributes

        private string MsgDelete = "Bạn đã xóa thành công";
        private string MsgEror = "Có lỗi xảy ra khi thêm dữ liệu";
        private string MsgExits = "Đã tồn tại";
        private string MsgNew = "Bạn đã thêm mới thành công";
        private string MsgUpdate = "Bạn đã cập nhật thành công";
        private DataTable dtAlias;
        private DataTable dt_TestTypeList = new DataTable();

        #endregion

        #region Contructor

        public FrmAlias_Mapping()
        {
            InitializeComponent();
            GrdAliasPara.AutoGenerateColumns = false;
        }

        #endregion

        #region Form Event

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateAliasMaping();
        }

        private void FrmAlias_Mapping_Load_1(object sender, EventArgs e)
        {
            //load pospaint cho gridview
            GrdAliasPara.RowPostPaint += UI.GridviewRowPostPaint;
            //Load các thông số
            LoadAlias();
            dt_TestTypeList = new Select().From(TTestTypeList.Schema).ExecuteDataSet().Tables[0];
            cboTestType_Name.DataSource = dt_TestTypeList;
            cboTestType_Name.DisplayMember = TTestTypeList.Columns.TestTypeName;
            cboTestType_Name.ValueMember = TTestTypeList.Columns.TestTypeId;
        }

        private void FrmAlias_Mapping_KeyDown(object sender, KeyEventArgs e)
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

        private void GrdAliasPara_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteAliasMapping();
            }
        }

        #endregion

        #region Private Method

        public void UpdateAliasMaping()
        {
            TblAliasMapping pitems;

            string localali = "", TestId = "";
            int id_xn = 0;
            var dataTable = GrdAliasPara.DataSource as DataTable;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                object c = dr[TblAliasMapping.Columns.Id];
                //Kiểm tra trạng thái của dòng:
                if ((dr.RowState == DataRowState.Modified) || (dr.RowState == DataRowState.Added))
                {
                    localali = dr[TblAliasMapping.Columns.LocalAlias].ToString();
                    TestId = dr[TblAliasMapping.Columns.TestTypeId].ToString();
                    id_xn = Convert.ToInt32(dr[TblAliasMapping.Columns.IdHisXn].ToString());

                    if ((localali == ""))
                    {
                        SetTextForWarning(MsgEror);
                        continue;
                    }
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    pitems = new TblAliasMapping(dr[TblAliasMapping.Columns.Id]);
                    pitems.LocalAlias = localali;
                    pitems.TestTypeId = Convert.ToInt32(TestId);
                    pitems.IdHisXn = Convert.ToInt32(id_xn);
                    AliasMappingBusiness.UpdateAliasMapping(pitems);
                    SetTextForWarning(MsgUpdate);
                }
                else if (dr.RowState == DataRowState.Added)
                {
                    pitems = new TblAliasMapping();
                    pitems.LocalAlias = localali;
                    pitems.TestTypeId = Convert.ToInt32(TestId);
                    pitems.IdHisXn = Convert.ToInt32(id_xn);
                    string id = AliasMappingBusiness.InsertAliasMapping(pitems);
                    if (id == "-1")
                    {
                        //MessageBox.Show("Tên thông số  " + pitems.LocalAlias + "  đã tồn tại", "Thông báo:",
                        //                MessageBoxButtons.OK,
                        //                MessageBoxIcon.Error);
                        SetTextForWarning(MsgExits);
                        dr.Delete();
                    }
                    else
                    {
                        dr[TblAliasMapping.Columns.Id] = AliasMappingBusiness.InsertAliasMapping(pitems);
                        SetTextForWarning(MsgNew);
                    }
                }
            }
            dataTable.AcceptChanges();
        }

        private void SetTextForWarning(string text)
        {
            warningBox1.Text = text;
            warmingboxTimer.Enabled = true;
        }

        private void DeleteAliasMapping()
        {
            try
            {
                DataGridViewRow currentRow = GrdAliasPara.CurrentRow;

                if (currentRow != null)
                {
                    string pid = UI.GetCellValue(currentRow.Cells[TblAliasMapping.Columns.Id]);
                    if (pid != "")
                    {
                        // int pid = Convert.ToInt32(.Value);
                        if (
                            MessageBox.Show(@"Bạn có muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            AliasMappingBusiness.DeleteAliasMapping(Convert.ToInt32(pid));
                            GrdAliasPara.Rows.Remove(currentRow);
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
                (GrdAliasPara.DataSource as DataTable).AcceptChanges();
            }
        }

        public void LoadAlias()
        {
            dtAlias = AliasMappingBusiness.GetAllAlias();
            GrdAliasPara.DataSource = dtAlias;
        }

        #endregion

        private void warmingboxTimer_Tick(object sender, EventArgs e)
        {
            warmingboxTimer.Enabled = false;
            warningBox1.Text = string.Empty;
        }
    }
}