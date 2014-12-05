using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Report.UI.Reports;
using Vietbait.Lablink.Utilities;
using Vietbait.TestInformation.Business;

namespace Vietbait.TestInformation.UI
{
    public partial class frm_ColorConfig : Form
    {
        private DataTable _dtPrintColor = new DataTable();
        private DataTable _dtPrintColorDetail = new DataTable();
        private ContextMenu _ctx = new ContextMenu();
        private MenuItem _mnuAdd = new MenuItem("Thêm nhóm");
        private MenuItem _mnuSeperate = new MenuItem("-");
        private MenuItem _mnuDel = new MenuItem("Xóa nhóm đang chọn");
        private MenuItem _mnuChooseColor = new MenuItem("Chọn màu sắc");
        private action act = action.Update;

        public frm_ColorConfig()
        {
            InitializeComponent();
            InitializeEvents();
            InitData();
        }

        private void InitializeEvents()
        {
            tvwColorConfig.MouseDown += new MouseEventHandler(tvwColorConfig_MouseDown);
            tvwColorConfig.Click += new EventHandler(tvwColorConfig_Click);
            tvwColorConfig.AfterSelect += new TreeViewEventHandler(tvwColorConfig_AfterSelect);
            _ctx.MenuItems.AddRange(new MenuItem[] {_mnuAdd, _mnuSeperate, _mnuDel, _mnuChooseColor});
            _mnuAdd.Click += new EventHandler(_mnuAdd_Click);
            _mnuDel.Click += new EventHandler(_mnuDel_Click);
            _mnuChooseColor.Click += new EventHandler(_mnuChooseColor_Click);
            tvwColorConfig.ContextMenu = _ctx;
        }

        private void tvwColorConfig_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvwColorConfig_Click(tvwColorConfig, new EventArgs());
        }

        private void _mnuChooseColor_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog _CDialog = new ColorDialog();
                if (_CDialog.ShowDialog() == DialogResult.OK)
                {
                    tvwColorConfig.SelectedNode.ForeColor = _CDialog.Color;
                    new Update(SysPrintColor.Schema).Set(SysPrintColor.ArgbColumn.ColumnName)
                                                    .EqualTo(_CDialog.Color.ToArgb())
                                                    .Where(SysPrintColor.IdColumn)
                                                    .IsEqualTo(tvwColorConfig.SelectedNode.Tag.ToString())
                                                    .Execute();
                }
            }
            catch
            {
            }
        }

        private void _mnuDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có muốn xóa nhóm " + txtName.Text.Trim() + " không?",
                                           "Hỏi trước khi xóa", true))
                {
                    DeleteGroup(Utility.Int32Dbnull(txtID.Text, -1));
                    tvwColorConfig.SelectedNode.Remove();
                }
            }
            catch
            {
            }
        }

        private void _mnuAdd_Click(object sender, EventArgs e)
        {
            try
            {
                act = action.Insert;
                cmdCancel.Visible = true;
                tvwColorConfig.Enabled = false;
                txtName.Clear();
                lblColor.BackColor = Color.White;
                ClearSelected();
                cmdSave.Enabled = true;
                txtName.Focus();
            }
            catch
            {
            }
        }

        private void tvwColorConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvwColorConfig.SelectedNode == null) return;
                if (tvwColorConfig.SelectedNode.Tag.ToString() == "-1")
                {
                    ClearSelected();
                    txtName.Text = tvwColorConfig.SelectedNode.Text;
                    txtID.Text = tvwColorConfig.SelectedNode.Tag.ToString();

                    lblColor.BackColor = Color.White;

                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdSave.Enabled = true;
                    txtName.Text = tvwColorConfig.SelectedNode.Text;
                    txtID.Text = tvwColorConfig.SelectedNode.Tag.ToString();
                    lblColor.BackColor = tvwColorConfig.SelectedNode.ForeColor;
                    ModifyColorList();
                    txtName.Focus();
                }
            }
            catch
            {
            }
        }

        private void ClearSelected()
        {
            for (int i = 0; i <= cboTestTypeList.Items.Count - 1; i++)
            {
                cboTestTypeList.SetItemChecked(i, false);
            }
        }

        private void ModifyColorList()
        {
            try
            {
                ClearSelected();
                DataRow[] arrDr = _dtPrintColorDetail.Select("ID=" + txtID.Text);
                if (arrDr.Length > 0)
                {
                    for (int i = 0; i <= cboTestTypeList.Items.Count - 1; i++)
                    {
                        cboTestTypeList.SetSelected(i, true);
                        if (
                            _dtPrintColorDetail.Select("ID=" + txtID.Text + " and testType_id=" +
                                                       cboTestTypeList.SelectedValue.ToString()).Length > 0)
                            cboTestTypeList.SetItemChecked(i, true);
                    }
                }
            }
            catch
            {
            }
        }

        private void tvwColorConfig_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (!tvwColorConfig.SelectedNode.Equals(tvwColorConfig.GetNodeAt(e.X, e.Y)))
                    tvwColorConfig.SelectedNode = tvwColorConfig.GetNodeAt(e.X, e.Y);
            }
            catch
            {
            }
        }

        private void InitData()
        {
            try
            {
                _dtPrintColor = new Select().From(SysPrintColor.Schema).ExecuteDataSet().Tables[0];
                _dtPrintColorDetail = new Select().From(SysPrintColorDetail.Schema).ExecuteDataSet().Tables[0];
                DataTable dtTestTypeList = TestTypeListBusiness.GetAllTestTypeList();
                DataBinding.BindData(cboTestTypeList, dtTestTypeList, TTestTypeList.Columns.TestTypeId,
                                     TTestTypeList.Columns.TestTypeName);
                CreateTreeView();
                tvwColorConfig.Nodes[0].ExpandAll();
            }
            catch
            {
            }
        }

        private void CreateTreeView()
        {
            try
            {
                tvwColorConfig.Nodes.Clear();
                tvwColorConfig.ImageList = imageList1;
                TreeNode rNode = new TreeNode("Nhóm màu sắc");
                rNode.Tag = "-1";
                rNode.NodeFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
                tvwColorConfig.Nodes.Add(rNode);
                foreach (DataRow dr in _dtPrintColor.Rows)
                {
                    TreeNode _newNode = new TreeNode(Utility.sDbnull(dr["GroupName"], "NoName"));
                    _newNode.NodeFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
                    _newNode.SelectedImageIndex = 0;
                    _newNode.ForeColor = Color.FromArgb(Utility.Int32Dbnull(dr["Argb"], Color.White.ToArgb()));
                    _newNode.Tag = Utility.sDbnull(dr["ID"], "-1");
                    rNode.Nodes.Add(_newNode);
                }
            }
            catch
            {
            }
        }

        private void frm_ColorConfig_Load(object sender, EventArgs e)
        {
        }

        private void cmdcolor_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog _CDialog = new ColorDialog();
                if (_CDialog.ShowDialog() == DialogResult.OK)
                {
                    lblColor.BackColor = _CDialog.Color;
                }
            }
            catch
            {
            }
        }

        private bool hasNoneGroup()
        {
            foreach (DataRow dr in _dtPrintColor.Rows)
            {
                if (_dtPrintColorDetail.Select("ID=" + dr["ID"].ToString()).Length <= 0) return true;
            }
            return false;
        }

        private bool IsValidData()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (txtName.Text.Trim() == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin tên nhóm", true);
                    txtName.Focus();
                    return false;
                }
                if (act == action.Insert && hasNoneGroup())
                {
                    bool bHasItemChecked = false;
                    for (int i = 0; i <= cboTestTypeList.Items.Count - 1; i++)
                    {
                        if (cboTestTypeList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            bHasItemChecked = true;
                            break;
                        }
                    }
                    if (!bHasItemChecked)
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải chọn các Test ứng với nhóm", true);
                        cboTestTypeList.Focus();
                        return false;
                    }
                }
                if (act == action.Insert)
                {
                    if (_dtPrintColor.Select("argb=" + lblColor.BackColor.ToArgb()).Length > 0)
                    {
                        if (
                            !Utility.AcceptQuestion(
                                "Màu sắc này đã được dùng cho nhóm " +
                                _dtPrintColor.Select("argb=" + lblColor.BackColor.ToArgb())[0]["groupName"].ToString() +
                                "\n Bạn có muốn tiếp tục sử dụng màu sắc này hay không?", "Xác nhận trùng màu", true))
                        {
                            cmdcolor.PerformClick();
                            return false;
                        }
                    }
                }
                else
                {
                    if (
                        _dtPrintColor.Select("argb=" + lblColor.BackColor.ToArgb() + " AND ID<>" + txtID.Text.Trim())
                                     .Length > 0)
                    {
                        if (
                            !Utility.AcceptQuestion(
                                "Màu sắc này đã được dùng cho nhóm " +
                                _dtPrintColor.Select("argb=" + lblColor.BackColor.ToArgb() + " AND ID<>" +
                                                     txtID.Text.Trim())[0]["groupName"].ToString() +
                                "\n Bạn có muốn tiếp tục sử dụng màu sắc này hay không?", "Xác nhận trùng màu", true))
                        {
                            cmdcolor.PerformClick();
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DeleteGroup(int ID)
        {
            try
            {
                new Delete().From(SysPrintColor.Schema).Where(SysPrintColor.IdColumn).IsEqualTo(ID).Execute();
                new Delete().From(SysPrintColorDetail.Schema)
                            .Where(SysPrintColorDetail.IdColumn)
                            .IsEqualTo(ID)
                            .Execute();
                if (_dtPrintColor.Select("ID=" + txtID.Text).Length > 0)
                    _dtPrintColor.Rows.Remove(_dtPrintColor.Select("ID=" + txtID.Text)[0]);
                DataRow[] arrDr = _dtPrintColorDetail.Select("ID=" + ID.ToString());
                int _count = arrDr.GetLength(0);
                int _idx = 0;
                while (_idx < _count)
                {
                    _dtPrintColorDetail.Rows.Remove(arrDr[_idx]);
                    _idx++;
                }
                _dtPrintColor.AcceptChanges();
                _dtPrintColorDetail.AcceptChanges();
            }
            catch
            {
            }
        }

        private void UpdateGroup(int ID)
        {
            try
            {
                new Delete().From(SysPrintColorDetail.Schema)
                            .Where(SysPrintColorDetail.IdColumn)
                            .IsEqualTo(ID)
                            .Execute();
                new Update(SysPrintColor.Schema).Set(SysPrintColor.ArgbColumn.ColumnName)
                                                .EqualTo(lblColor.BackColor.ToArgb())
                                                .Set(SysPrintColor.GroupNameColumn.ColumnName)
                                                .EqualTo(txtName.Text.Trim())
                                                .Where(SysPrintColor.IdColumn).IsEqualTo(ID).Execute();
                DataRow[] arrDr = _dtPrintColor.Select("ID=" + ID.ToString());
                if (arrDr.Length > 0)
                {
                    arrDr[0]["GroupName"] = txtName.Text.Trim();
                    arrDr[0]["Argb"] = lblColor.BackColor.ToArgb();
                }
                DataRow[] arrDrDetail = _dtPrintColorDetail.Select("ID=" + ID.ToString());
                int _count = arrDrDetail.GetLength(0);
                int _idx = 0;
                while (_idx < _count)
                {
                    _dtPrintColorDetail.Rows.Remove(arrDrDetail[_idx]);
                    _idx++;
                }
                _dtPrintColorDetail.AcceptChanges();
                _dtPrintColorDetail.AcceptChanges();
            }
            catch
            {
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            TreeNode _newNode = null;
            try
            {
                if (!IsValidData()) return;

                Int32 maxID = 0;

                if (act == action.Insert)
                {
                    //Save2DB
                    SysPrintColor _new = new SysPrintColor();
                    _new.GroupName = txtName.Text.Trim();

                    _new.Argb = lblColor.BackColor.ToArgb();
                    _new.IsNew = true;
                    _new.Save();
                    maxID =
                        Convert.ToInt32(
                            new Select().From(SysPrintColor.Schema)
                                        .OrderDesc(SysPrintColor.IdColumn.ColumnName)
                                        .ExecuteDataSet()
                                        .Tables[0].Rows[0]["ID"]);

                    _newNode = new TreeNode(txtName.Text.Trim());
                    _newNode.ForeColor = lblColor.BackColor;
                    _newNode.NodeFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
                    _newNode.Tag = maxID.ToString();
                    _newNode.SelectedImageIndex = 0;
                    tvwColorConfig.Nodes[0].Nodes.Add(_newNode);


                    DataRow _newDr = _dtPrintColor.NewRow();
                    _newDr["ID"] = maxID;
                    _newDr["GroupName"] = maxID;
                    _newDr["Argb"] = lblColor.BackColor.ToArgb();
                    _dtPrintColor.Rows.Add(_newDr);
                }
                else
                {
                    maxID = Convert.ToInt32(txtID.Text);
                    UpdateGroup(maxID);

                    tvwColorConfig.SelectedNode.ForeColor = lblColor.BackColor;
                    tvwColorConfig.SelectedNode.Text = txtName.Text.Trim();
                }
                for (int i = 0; i <= cboTestTypeList.Items.Count - 1; i++)
                {
                    if (cboTestTypeList.GetItemCheckState(i) == CheckState.Checked)
                    {
                        cboTestTypeList.SetSelected(i, true);
                        SysPrintColorDetail _newDetail = new SysPrintColorDetail();
                        _newDetail.Id = maxID;
                        _newDetail.TestTypeId = Convert.ToInt32(cboTestTypeList.SelectedValue);

                        _newDetail.IsNew = true;
                        _newDetail.Save();
                        DataRow _newDrDetail = _dtPrintColorDetail.NewRow();
                        _newDrDetail["ID"] = maxID;
                        _newDrDetail["TestType_ID"] = _newDetail.TestTypeId;

                        _dtPrintColorDetail.Rows.Add(_newDrDetail);
                    }
                }
                if (act == action.Insert) tvwColorConfig.SelectedNode = _newNode;
                act = action.Update;
                tvwColorConfig.Enabled = true;
            }
            catch
            {
            }
            finally
            {
                tvwColorConfig_Click(tvwColorConfig, e);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                act = action.Update;
                cmdCancel.Visible = false;
                cmdSave.Enabled = true;
                tvwColorConfig.Enabled = true;
                tvwColorConfig_Click(tvwColorConfig, e);
            }
            catch
            {
            }
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            cmdcolor.PerformClick();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}