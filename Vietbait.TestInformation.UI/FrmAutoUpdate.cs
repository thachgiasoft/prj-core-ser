using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Vietbait.Lablink.Model;
using Vietbait.TestInformation.Business;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmAutoUpdate : Office2007Form
    {
        private DataTable _dtAllTodayTestResult;
        private DataTable _dtTestResult;
        private DataTable dtTestTypeList;

        public FrmAutoUpdate()
        {
            InitializeComponent();

            grdTestResult.AutoGenerateColumns = false;
            grdTestResult.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdAllTodayTestInfo.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdResultStatus.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
        }

        private void FrmAutoUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F5:
                    btnRefresh.PerformClick();
                    break;
            }
        }

        private void FrmAutoUpdate_Load(object sender, EventArgs e)
        {
            GetAllTodayTestResult();
            _dtAllTodayTestResult.DefaultView.Sort = "Test_Date DESC";
            grdAllTodayTestInfo.DataSource = _dtAllTodayTestResult;

            grdAllTodayTestInfo_SelectionChanged(null, null);
            grdTestResult.DataSource = _dtTestResult;
        }

        private void nmRefreshInterval_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(nmRefreshInverval.Value*60*1000);
            timer1.Stop();
            timer1.Start();
        }

        private void ProcessGrdResultStatus()
        {
            foreach (DataRow dr in dtTestTypeList.Rows)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = dr["Abbreviation"].ToString();
                col.Name = dr[TTestTypeList.Columns.TestTypeId].ToString();
                col.Visible = true;
                grdResultStatus.Columns.Add(col);
            }
        }

        private void GetAllTodayTestResult()
        {
            _dtAllTodayTestResult = TestInfoBusiness.GetAllTodayTestResult();
        }

        private void grdAllTodayTestInfo_SelectionChanged(object sender, EventArgs e)
        {
            if (grdAllTodayTestInfo.CurrentRow != null)
            {
                _dtTestResult = TestInfoBusiness.GetTestResultByTestId(grdAllTodayTestInfo.CurrentRow.Cells["colTestID_XN"].Value.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetAllTodayTestResult();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetAllTodayTestResult();
        }
    }
}