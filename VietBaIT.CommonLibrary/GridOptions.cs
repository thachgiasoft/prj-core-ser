using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace VietBaIT.CommonLibrary
{
    public partial class GridOptions : BaseForm
    {
        //Danh sách các cột đang bị cấu hình ẩn của DataGrid
        public ArrayList InvisibleCols = new ArrayList();
        //Danh sách tất cả các cột của DataGrid
        public ArrayList AllCols = new ArrayList();
        //Biến xác định xem có chấp nhận cấu hình hay hủy bỏ quay về cấu hình cũ
        public bool _Cancel = true;
        public Int16 Codan = 0;
        public int Alignment = 0;
        private DataTable DT = new DataTable();
        //KHởi tạo Form
        public ArrayList ArrAlign = new ArrayList() { "Mặc định", "Trái", "Giữa", "Phải" };
        public GridOptions(ArrayList AllCols, ArrayList InvisibleCols)
        {
            InitializeComponent();
             this.AllCols = AllCols;
            this.InvisibleCols = InvisibleCols;
            this.KeyDown += new KeyEventHandler(GridOptions_KeyDown);
        }

        void GridOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        //Khởi tạo danh sách các cột đưa lên Grid
        private void LoadColumns()
        {
            //Khởi tạo bảng dữ liệu
            DT.Columns.Add(new DataColumn("CHON"));
            DT.Columns.Add(new DataColumn("Header"));
            DT.Columns.Add(new DataColumn("ColName"));
            DT.Columns.Add(new DataColumn("Align"));
            DT.AcceptChanges();
            if (AllCols == null) return;
            for (int i = 0; i <= AllCols.Count - 1; i++)
            {
                string[] ArrItem=AllCols[i].ToString().Split(':');
                DataRow dr = DT.NewRow();
                dr["Header"] = ArrItem[0];
                dr["ColName"] = ArrItem[1];
                dr["Align"] = TranslateAlign(Convert.ToInt16( ArrItem[3]));
                if (InvisibleCols.Contains(AllCols[i]))
                    dr["CHON"] = 0;
                else
                    dr["CHON"] = 1;
                DT.Rows.Add(dr);
                DT.AcceptChanges();
                DT.DefaultView.AllowNew = false;
                //DT.DefaultView.AllowEdit = false;
                //DT.DefaultView.AllowDelete = false;
                grdList.AutoGenerateColumns = false;
                grdList.DataSource = DT.DefaultView;
            }
        }
        private string TranslateAlign(Int16 Align)
        {
            return ArrAlign[Align].ToString();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

       

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in DT.Rows)
            {
                dr["CHON"] = chkAll.Checked ? 1 : 0;
            }
            DT.AcceptChanges();
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in DT.Rows)
            {
                dr["CHON"] =Convert.ToInt32(dr["CHON"]) == 1 ? 0 : 1;
            }
            DT.AcceptChanges();
        }

        private void optNo_CheckedChanged(object sender, EventArgs e)
        {
            Codan = 0;
        }

        private void optEqual_CheckedChanged(object sender, EventArgs e)
        {
            Codan = 1;
        }

        private void optLast_CheckedChanged(object sender, EventArgs e)
        {
            Codan = 2;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Alignment = cboAlign.SelectedIndex;
            InvisibleCols.Clear();
            foreach (DataRow dr in DT.Rows)
            {
                if (Convert.ToInt32(dr["CHON"]) ==0)
                    InvisibleCols.Add(dr["Header"].ToString() + ":" + dr["ColName"].ToString()) ;
            }
            _Cancel = false;
            this.Close();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            Alignment = cboAlign.SelectedIndex;
            foreach (DataRow dr in DT.Rows)
            {
                dr["Align"] = ArrAlign[Alignment].ToString(); 
            }
            DT.AcceptChanges();
            //Tùy biến cột Align trên Grid
            grdList.BeginEdit(true);
            
            grdList.RowsDefaultCellStyle.Alignment = TranslateFromIntToAlignment(cboAlign.SelectedIndex);
             grdList.EndEdit();
        }
        private DataGridViewContentAlignment TranslateFromIntToAlignment(int Alignment)
        {
            switch (Alignment)
            {
                case 0:
                    return DataGridViewContentAlignment.NotSet;
                case 1:
                    return DataGridViewContentAlignment.MiddleLeft;
                case 2:
                    return DataGridViewContentAlignment.MiddleCenter;
                case 3:
                    return DataGridViewContentAlignment.MiddleRight;
                default:
                    return DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void GridOptions_Load(object sender, EventArgs e)
        {
            cboAlign.SelectedIndex = Alignment;
            if (Codan == 0) optNo.Checked = true;
            if (Codan == 1) optEqual.Checked = true;
            if (Codan == 2) optLast.Checked = true;
            LoadColumns();
            cmdApply.PerformClick();
        }

      

       

       


    }
}
