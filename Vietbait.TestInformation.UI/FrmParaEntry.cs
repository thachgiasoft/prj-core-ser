using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmParaEntry : Office2007Form
    {
        #region Attributes

        public DataTable GvDtParaEntry;
        public string GvPatientId;
        public string GvTestTypeId;
        public string SelectProperties;
        public bool VbStatusResult;

        #endregion

        #region Contructor

        public FrmParaEntry()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Fills the tree node para.
        /// </summary>
        private void FillTreeNodePara()
        {
            DataTable dtDevice;
            DataTable dtTestType;
            try
            {
                //Lấy ra tên loại Test dựa vào TestTypeID đổ vào datatable
                dtTestType = DevicesListBusiness.GetTestType(GvTestTypeId);
                foreach (DataRow drTestType in dtTestType.Rows)
                {
                    var nodeTestType = new Node(); //node level 1
                    nodeTestType.Text = drTestType[TTestTypeList.Columns.TestTypeName].ToString();
                    // gán TestTypeName vào node
                    nodeTestType.Expand();

                    //Lấy những thiết bị theo drTestType[TTestTypeList.Columns.TestTypeId]
                    dtDevice =
                        DevicesListBusiness.GetDeviceNameAndIdListByTestTypeId(
                            drTestType[TTestTypeList.Columns.TestTypeId].ToString()); //danh sách tên thiết bị
                    foreach (DataRow dr in dtDevice.Rows)
                    {
                        var nodeDeviceName = new Node(); //node level 2
                        nodeDeviceName.Text = dr[DDeviceList.Columns.DeviceName].ToString();

                        nodeDeviceName.CheckBoxVisible = true;
                        nodeDeviceName.Editable = false;

                        //Lấy Data Control theo thiết bị
                        DataTable dt1 =
                            CodeToCtrlEquip.GetDataControlForDevice(dr[DDeviceList.Columns.DeviceId].ToString());
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            //Lọc dữ liệu
                            DataRow[] arrDr =
                                GvDtParaEntry.Select("Para_Name='" + dr1[DDataControl.Columns.DataName] + "'");
                            if (arrDr.GetLength(0) > 0)
                            {
                                dr1.Delete();
                            }
                        }
                        dt1.AcceptChanges();


                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var cMale = new Cell();
                            cMale.Name = "cMale";
                            var cFemale = new Cell();
                            cFemale.Name = "cFemale";
                            var cUnit = new Cell();
                            cUnit.Name = "cUnit";
                            var nodeDataControl = new Node();
                            nodeDataControl.Text = dr1[DDataControl.Columns.DataName].ToString();
                            nodeDataControl.TagString = drTestType[TTestTypeList.Columns.TestTypeId].ToString();
                            cMale.Text = dr1[DDataControl.Columns.NormalLevel].ToString();
                            cFemale.Text = dr1[DDataControl.Columns.NormalLevelW].ToString();
                            cUnit.Text = dr1[DDataControl.Columns.MeasureUnit].ToString();
                            nodeDataControl.Cells.Add(cMale);
                            nodeDataControl.Cells.Add(cFemale);
                            nodeDataControl.Cells.Add(cUnit);
                            nodeDataControl.CheckBoxVisible = true;

                            nodeDeviceName.Nodes.Add(nodeDataControl); //gắn node level 2 vào node level 
                        }
                        nodeTestType.Nodes.Add(nodeDeviceName); //gắn node level 1 vào node level 0
                    }
                    treePara.Nodes.Add(nodeTestType); //gắn node level 0 vào tree 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Autoes the check.
        /// </summary>
        private void AutoCheck()
        {
            try
            {
                if (treePara.SelectedNode.Level != 0)
                {
                    treePara.SelectedNode.Checked = !treePara.SelectedNode.Checked;


                    if (treePara.SelectedNode.HasChildNodes)
                    {
                        treePara.SelectedNode.Expand();
                        // treePara.SelectedNode.Checked =! treePara.SelectedNode.Checked;
                        Node node = treePara.SelectedNode.Nodes[0];

                        while (node != null)
                        {
                            if (treePara.SelectedNode.Checked)
                            {
                                node.Checked = true;
                            }
                            else
                            {
                                node.Checked = false;
                            }
                            // node.Expand();
                            node = node.NextNode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts the para.
        /// </summary>
        private void InsertPara()
        {
            try
            {
                //duyệt qua các node level 0
                for (int i = 0; i < treePara.Nodes.Count; i++)
                {
                    //nếu node là parent node  và có node con level 1
                    if (treePara.Nodes[i].HasChildNodes && treePara.Nodes[i].Nodes != null)
                    {
//duyệt qua các node level 1
                        for (int j = 0; j < treePara.Nodes[i].Nodes.Count; j++)
                        {
//nếu node là parent node  và có node con level 2
                            if (treePara.Nodes[i].Nodes[j].HasChildNodes && treePara.Nodes[i].Nodes[j].Nodes != null)
                            {
                                Node node = treePara.Nodes[i].Nodes[j].Nodes[0];
                                while (node != null)
                                {
                                    if (node.Cells.Count == 4 && node.Checked)
                                    {
                                        DataRow dr = GvDtParaEntry.NewRow();

                                        if (!dr.Table.Columns.Contains(TResultDetail.Columns.PatientId))
                                        {
                                            GvDtParaEntry.Columns.Add("Patient_ID");
                                        }
                                        dr[TResultDetail.Columns.ParaName] = node.Cells[0].Text;
                                        dr[TResultDetail.Columns.NormalLevel] = node.Cells["cMale"].Text;
                                        dr[TResultDetail.Columns.NormalLevelW] = node.Cells["cFemale"].Text;
                                        dr[TResultDetail.Columns.PatientId] = GvPatientId;
                                        dr[TResultDetail.Columns.TestTypeId] = node.Cells[0].TagString;
                                        //dr[TResultDetail.Columns.TestDetailId] = -1;
                                        GvDtParaEntry.Rows.Add(dr);
                                    }
                                    node = node.NextNode;
                                }
                            }
                        }
                    }
                }
                VbStatusResult = true;
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            FillTreeNodePara();
        }

        private void FrmParaEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            else if ((e.KeyCode == Keys.Enter) && (e.Modifiers == Keys.Shift))
            {
                InsertPara();
            }
            if (e.KeyCode == Keys.Enter)
            {
                AutoCheck();
            }
        }


        private void treePara_AfterCollapse(object sender, AdvTreeNodeEventArgs e)
        {
        }

        private void treePara_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            AutoCheck();
        }

        private void treePara_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            AutoCheck();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertPara();
        }

        #endregion
    }
}