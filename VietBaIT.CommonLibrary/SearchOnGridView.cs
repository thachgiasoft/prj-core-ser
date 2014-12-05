using System;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace VietBaIT.CommonLibrary
{
    public partial class SearchOnGridView : Form
    {
        public DataGridView mv_grd;
        public ContainerControl mv_FormofGrid;
        DataTable DT = new DataTable();
        int intStart = 0;
        int _Row;
        int _Col;
        string SubSystem = "";
        int CurrentCol = 0;
        string OldValue = "";
        public SearchOnGridView(string SubSystem)
        {
            this.KeyPress += new KeyPressEventHandler(SearchOnGridView_KeyPress);
            this.KeyDown+=new KeyEventHandler(SearchOnGridView_KeyDown);
            this.SubSystem = SubSystem;
            InitializeComponent();
        }

        void SearchOnGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    cmdSearch.PerformClick();
                    break;
                case Keys.F2:
                    chkLike.Checked = !chkLike.Checked;
                    break;
                case Keys.F3:
                    cmdSearch.PerformClick();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        void SearchOnGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Strings.Asc(e.KeyChar) == Keys.Enter) e.Handled = true;
            //}
            //catch (Exception ex)
            //{

            //}
        }
        public ContainerControl _Parent
        {
            get { return mv_FormofGrid; }
            set { mv_FormofGrid = value; }
        }
        public DataGridView GridView
        {
            get { return mv_grd; }
            set { mv_grd = value; }
        }
       
        private void LoadKeySearch()
        {
            try
            {
                cboValue.Items.Clear();
                for (int i = globalVariables.gv_arrKeySearch.Count - 1; i >= 0; i += -1)
                {
                    cboValue.Items.Add(globalVariables.gv_arrKeySearch[i]);
                }
                if (cboValue.Items.Count > 0) cboValue.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        private void CreateTable()
        {
            try
            {
                DT.Columns.Add(new DataColumn("ColName"));
                DT.Columns.Add(new DataColumn("sText"));
            }
            catch (Exception ex)
            {

            }
        }
        private void SearchNow()
        {
            try
            {
                string sValue = cboValue.Text;
                bool bFound = false;
                //----------------------------------------------------
                if (chkFirstRowFirstCol.Checked)
                {
                    intStart = 0;
                    if (mv_grd.RowCount > 0)
                    {
                        CurrentCol = 0;
                        mv_grd.CurrentCell = mv_grd[0, 0];
                    }
                }
                if (chkCol.Checked)
                {
                    if (!chkLike.Checked)
                    {
                        if (mv_grd.CurrentRow.Index == mv_grd.RowCount - 1 | intStart > mv_grd.RowCount - 1)
                        {
                            intStart = 0;
                            mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, 0];
                        }
                        for (int i = intStart; i <= mv_grd.RowCount - 1; i++)
                        {
                            if (chkCaption.Checked)
                            {
                                if (mv_grd[(string)cboColName.SelectedValue, i].Visible & Strings.InStr(Utility.sDbnull(mv_grd[(string)cboColName.SelectedValue, i].Value.ToString().ToUpper(), ""), sValue,CompareMethod.Text) > 0)
                                {
                                    mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, i];
                                    _Row = i;
                                    intStart += 1;
                                    //OldValue = mv_grd.Item(CStr(cboColName.SelectedValue), i).Value
                                    bFound = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            else
                            {
                                if (mv_grd[(string)cboColName.SelectedValue, i].Visible & Strings.InStr(Utility.sDbnull(mv_grd[(string)cboColName.SelectedValue, i].Value.ToString().ToUpper(), ""), sValue.ToUpper(), CompareMethod.Text) > 0)
                                {
                                    mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, i];
                                    
                                    _Row = i;
                                    intStart += 1;
                                    //OldValue = mv_grd.Item(CStr(cboColName.SelectedValue), i).Value
                                    bFound = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }

                            intStart += 1;
                        }
                        if (bFound)
                        {
                            StatusStrip1.Items[0].Text = "Có giá trị ở: Hàng " + (_Row + 1).ToString() + " Cột " + cboColName.Text;
                            return;
                        }
                        else
                        {
                            StatusStrip1.Items[0].Text = "Không tìm thấy giá trị bạn nhập từ vị trí sau ô hiện thời";
                            return;
                        }
                    }
                    else
                    {
                        if (mv_grd.CurrentRow.Index == mv_grd.RowCount - 1 | intStart > mv_grd.RowCount - 1)
                        {
                            intStart = 0;
                            mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, 0];
                        }
                        for (int i = intStart; i <= mv_grd.RowCount - 1; i++)
                        {
                            if (chkCaption.Checked)
                            {
                                if (mv_grd[(string)cboColName.SelectedValue, i].Visible & Utility.sDbnull(mv_grd[(string)cboColName.SelectedValue, i].Value, "") == sValue)
                                {
                                    mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, i];
                                    OldValue = mv_grd[(string)cboColName.SelectedValue, i].Value.ToString();
                                    _Row = i;
                                    intStart += 1;
                                    bFound = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            else
                            {
                                if (mv_grd[(string)cboColName.SelectedValue, i].Visible & Utility.sDbnull(mv_grd[(string)cboColName.SelectedValue, i].Value, "").ToString().ToUpper() == sValue.ToUpper())
                                {
                                    mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, i];
                                    OldValue = mv_grd[(string)cboColName.SelectedValue, i].Value.ToString();
                                    _Row = i;
                                    intStart += 1;
                                    bFound = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }

                            intStart += 1;
                        }
                        if (bFound)
                        {
                            mv_grd.Refresh();
                            StatusStrip1.Items[0].Text = "Có giá trị ở: Hàng " + (_Row + 1).ToString() + " Cột " + cboColName.Text;
                            return;
                        }
                        else
                        {
                            StatusStrip1.Items[0].Text = "Không tìm thấy giá trị bạn nhập từ vị trí sau ô hiện thời";
                            return;
                        }
                    }
                }
                else
                {
                    //Tìm tất cả các cột
                    if (!chkLike.Checked)
                    {
                        if (mv_grd.CurrentRow.Index == mv_grd.RowCount - 1 | intStart > mv_grd.RowCount - 1)
                        {
                            intStart = 0;
                            if (mv_grd.RowCount > 1)
                            {
                                CurrentCol = 0;
                            }
                            mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, 0];
                        }
                        else
                        {
                            intStart = mv_grd.CurrentRow.Index;
                        }
                        for (int i = intStart; i <= mv_grd.RowCount - 1; i++)
                        {
                            for (int j = CurrentCol; j <= mv_grd.Columns.Count - 1; j++)
                            {
                                CurrentCol += 1;
                                if (chkCaption.Checked)
                                {
                                    if (mv_grd[j, i].Visible & Strings.InStr(Utility.sDbnull(mv_grd[j, i].Value, "").ToString(), sValue, CompareMethod.Text) > 0)
                                    {
                                        mv_grd.CurrentCell = mv_grd[j, i];
                                        bFound = true;
                                        _Col = j;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }
                                else
                                {
                                    if (mv_grd[j, i].Visible & Strings.InStr(Utility.sDbnull(mv_grd[j, i].Value, "").ToString().ToUpper(), sValue.ToUpper(), CompareMethod.Text) > 0)
                                    {
                                        mv_grd.CurrentCell = mv_grd[j, i];
                                        bFound = true;
                                        _Col = j;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }

                            }
                            _Row = i;
                            if (bFound)
                            {
                                CurrentCol = mv_grd.CurrentCell.ColumnIndex + 1;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            else
                            {
                                CurrentCol = 0;
                                intStart += 1;
                            }
                        }
                        if (bFound)
                        {
                            mv_grd.Refresh();
                            StatusStrip1.Items[0].Text = "Có giá trị ở: Hàng " + (_Row + 1).ToString() + " Cột " + (_Col + 1).ToString();
                            return;
                        }
                        else
                        {
                            StatusStrip1.Items[0].Text = "Không tìm thấy giá trị bạn nhập từ vị trí sau ô hiện thời";
                            return;
                        }
                    }
                    else
                    {
                        if (mv_grd.CurrentRow.Index == mv_grd.RowCount - 1 | intStart > mv_grd.RowCount - 1)
                        {
                            intStart = 0;
                            if (mv_grd.RowCount > 1)
                            {
                                CurrentCol = 0;
                            }
                            mv_grd.CurrentCell = mv_grd[(string)cboColName.SelectedValue, 0];
                        }
                        else
                        {
                            intStart = mv_grd.CurrentRow.Index;
                        }
                        for (int i = intStart; i <= mv_grd.RowCount - 1; i++)
                        {
                            for (int j = CurrentCol; j <= mv_grd.Columns.Count - 1; j++)
                            {
                                if (chkCaption.Checked)
                                {
                                    if (mv_grd[j, i].Visible & Utility.sDbnull(mv_grd[j, i].Value, "").ToString() == sValue)
                                    {
                                        mv_grd.CurrentCell = mv_grd[j, i];
                                        bFound = true;
                                        _Col = j;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }
                                else
                                {
                                    if (mv_grd[j, i].Visible & Utility.sDbnull(mv_grd[j, i].Value, "").ToString().ToUpper() == sValue.ToUpper())
                                    {
                                        mv_grd.CurrentCell = mv_grd[j, i];
                                        bFound = true;
                                        _Col = j;
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }

                            }
                            _Row = i;
                            if (bFound)
                            {
                                CurrentCol = mv_grd.CurrentCell.ColumnIndex + 1;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            else
                            {
                                CurrentCol = 0;
                                intStart += 1;
                            }
                        }
                        if (bFound)
                        {
                            mv_grd.Refresh();
                            StatusStrip1.Items[0].Text = "Có giá trị ở: Hàng " + (_Row + 1).ToString() + " Cột " + (_Col + 1).ToString();
                            return;
                        }
                        else
                        {
                            StatusStrip1.Items[0].Text = "Không tìm thấy giá trị bạn nhập từ vị trí sau ô hiện thời";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Size = new Size(0, 0);
            }
            if (e.KeyCode == Keys.F3 | e.KeyCode == Keys.Enter)
            {
                cmdSearch.PerformClick();
            }
            if (e.KeyCode == Keys.F2) chkLike.Checked = !chkLike.Checked;
        }
        private void SearchOnGridView_Load(object sender, EventArgs e)
        {
            try
            {
                MultiLanguage.SetLanguage(globalVariables.DisplayLanguage, this, SubSystem, globalVariables.SqlConn);
                CreateTable();
                LoadColumnsOfDataGridView(globalVariables.DisplayLanguage,mv_FormofGrid, SubSystem);
                LoadKeySearch();
                cboValue.Focus();
            }

            catch (Exception ex)
            {
            }
        }
        public void LoadColumnsOfDataGridView(string pv_sLang, Control ctrMain, string sDLLName)
        {
            cboColName.DataSource = null;
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Sys_MULTILANGUAGE WHERE sFormName=N'" + ctrMain.Name + "' AND (upper(sDLLName)=N'" + sDLLName.ToUpper() + "' OR upper(sDLLName)=N'" + (sDLLName+".DLL").ToUpper()+"')", globalVariables.SqlConn);
            DataTable DT1 = new DataTable();
            string sVnText = null;
            string sEnText = null;
            sVnText = "";
            sEnText = "";
            try
            {
                if (mv_grd.ColumnCount > 0)
                {
                    da1.Fill(DT1);
                    DT.Rows.Clear();
                    if (DT1.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn col in mv_grd.Columns)
                        {
                            if (col.Visible)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sControlName='" + col.Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    DataRow DR1 = DT.NewRow();
                                    if (pv_sLang.ToUpper() == "VN")
                                    {
                                        {
                                            DR1["ColName"] = col.Name;
                                            DR1["sText"] = Utility.sDbnull(dr[0]["sVn"]);
                                        }
                                    }
                                    else
                                    {
                                        {
                                            DR1["ColName"] = col.Name;
                                            DR1["sText"] = Utility.sDbnull(dr[0]["sEn"]);
                                        }
                                    }
                                    DT.Rows.Add(DR1);
                                }
                                else
                                {
                                    DataRow DR1 = DT.NewRow();
                                    {
                                        DR1["ColName"] = col.Name;
                                        DR1["sText"] = col.HeaderText;
                                    }
                                    DT.Rows.Add(DR1);
                                }
                            }
                        }
                        DT.AcceptChanges();
                        {
                            cboColName.DataSource = DT;
                            cboColName.DisplayMember = "sText";
                            cboColName.ValueMember = "ColName";
                        }
                    }
                    else
                    {
                        foreach (DataGridViewColumn col in mv_grd.Columns)
                        {
                            if (col.Visible)
                            {
                                DataRow DR1 = DT.NewRow();
                                {
                                    DR1["ColName"] = col.Name;
                                    DR1["sText"] = col.HeaderText;
                                }
                                DT.Rows.Add(DR1);
                            }
                        }
                        DT.AcceptChanges();
                        {
                            cboColName.DataSource = DT;
                            cboColName.DisplayMember = "sText";
                            cboColName.ValueMember = "ColName";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (cboValue.Text.Trim() == string.Empty)
            {
                StatusStrip1.Items[0].Text = "Bạn hãy nhập giá trị tìm kiếm";
                cboValue.Focus();
            }
            else
            {
                if (!globalVariables.gv_arrKeySearch.Contains(cboValue.Text.Trim()))
                {
                    globalVariables.gv_arrKeySearch.Add(cboValue.Text.Trim());
                }
                SearchNow();
            }
        }
    }
}
