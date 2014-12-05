using System;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Globalization;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Config
{
    public partial class FrmConfig : Office2007Form
    {

        #region Attribute

        private const string StrNotEmpty = "The field not empty";
        private const string IsNumber = "The field must be numeric";
        private const string Err = "Error. Not connect to database";
        private const string StrMessage = "Connect successful";
        private bool _flag;

        #endregion

        #region Contructor

        public FrmConfig()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Hàm dùng để đọc file App.config (nếu có)
        /// </summary>
        private void ReadFileConfig()
        {
            txtPort.Text = LablinkServiceConfig.GetPort();
            txtInter.Text = LablinkServiceConfig.GetTimerInternal();
            txtDelay.Text = LablinkServiceConfig.GetDelayTime();
            txtDatabase.Text = LablinkServiceConfig.GetDatabase();
            txtServer.Text = LablinkServiceConfig.GetServer();
            txtUser.Text = LablinkServiceConfig.GetUserId();
            txtPassword.Text = LablinkServiceConfig.GetPassword();
            ipAddressInput1.Value = LablinkServiceConfig.GetIpAdress();
            txtPortNumber.Text = LablinkServiceConfig.GetPortNumber();
            txtReportType.Text = LablinkBusinessConfig.GetReportType();
            txtParentBranchName.Text = LablinkBusinessConfig.GetParentBranchName();
            txtBranchName.Text = LablinkBusinessConfig.GetBranchName();
            txtAddress.Text = LablinkBusinessConfig.GetAddress();
            txtPhone.Text = LablinkBusinessConfig.GetPhone();

            //Lấy về định dạng ngày tháng từ XML file
            if (!string.IsNullOrEmpty(LablinkServiceConfig.GetDateFormat()))
            {
                cboDateFormat.Text = LablinkServiceConfig.GetDateFormat();
            }

            //Lấy về loại định dạng xét nghiệm 
            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetTestTypeBarcode()))
            {
                switchTestTypeBarcode.Value = Boolean.Parse(LablinkServiceConfig.GetTestTypeBarcode());
            }

            //Lấy trạng thái thiết lập ghi log file
            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetLogState()))
            {
                switchLogState.Value = Boolean.Parse(LablinkServiceConfig.GetLogState());
            }

            //RS232
            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetRS232Status()))
            {
                sbtnRs232Service.Value = Boolean.Parse(LablinkServiceConfig.GetRS232Status());
            }

            //TCPIP
            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetTCPIPStatus()))
            {
                sbtnTcpIpService.Value = Boolean.Parse(LablinkServiceConfig.GetTCPIPStatus());
            }

            //AutoUpdateStatusAfterSend
            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetAutoUpdateOrderStatusAfterSend()))
            {
                AutoUpdateOrderStatusAfterSend.Checked= Boolean.Parse(LablinkServiceConfig.GetAutoUpdateOrderStatusAfterSend());
            }


            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetLogState()))
            {
                switchLogState.Value = Boolean.Parse(LablinkServiceConfig.GetLogState());
            }

            if (!String.IsNullOrEmpty(LablinkServiceConfig.GetFileStatus()))
            {
                sbtFileService.Value = Boolean.Parse(LablinkServiceConfig.GetFileStatus());
            }
        }

        private bool CheckFormatDate()
        {
            try
            {
                DateTime dt = DateTime.Now;
                //tmp = String.Format("{0:" + cboDateFormat.Text + "}", dt);
                string tmp = dt.ToString(cboDateFormat.Text);

                var dtfi = new DateTimeFormatInfo {ShortDatePattern = cboDateFormat.Text};
                try
                {
                    DateTime objDate = Convert.ToDateTime(tmp, dtfi);
                    lbDate.Text = tmp;
                    //lbDate.Text = dt.ToString();
                    warningBox1.Text = "";
                    return true;
                }
                catch (Exception)
                {

                    lbDate.Text = "";
                    warningBox1.Text = "Invalid format";
                    return false; 
                }
            }
            catch (FormatException)
            {
                warningBox1.Text = "Invalid format";
                return false;
            }
        }

        private void LoadDisplay()
        {
            string[] listStyle = Enum.GetNames(typeof (eStyle));
            cboDisplay.DataSource = listStyle;
            if (!String.IsNullOrEmpty(LablinkBusinessConfig.GetStyle()))
            {
                cboDisplay.Text = LablinkBusinessConfig.GetStyle();
            }
        }
        #endregion

        #region Events

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTabIndex = 0;
            cboDateFormat.SelectedIndex = 0;
            ReadFileConfig();
            circularProgress1.Visible = false;
            LoadDisplay();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //Kiểm tra trường Server
            if (String.IsNullOrEmpty(txtServer.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;


                txtServer.Focus();
                return;
            }
            //Kiểm tra trường Database
            if (String.IsNullOrEmpty(txtDatabase.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtDatabase.Focus();

                return;
            }

            //Kiểm tra trường UserID
            if (String.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtUser.Focus();

                return;
            }

            if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtPassword.Focus();

                return;
            }
            circularProgress1.Visible = true;
            btnCheck.Enabled = false;
            circularProgress1.IsRunning = true;
            try
            {
                if (backgroundWorker1.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
          
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string strcnn;
                strcnn = "Data Source=" + txtServer.Text.Trim() + ";Initial Catalog=" +
                         txtDatabase.Text.Trim() + ";User Id=" + txtUser.Text.Trim() +
                         ";Password=" + txtPassword.Text.Trim() + "";
            _flag = Common.CheckConnection(strcnn);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            warningBox1.Text = "";
            circularProgress1.Visible = false;
            btnCheck.Enabled = true;
            circularProgress1.IsRunning = false;
            if (_flag)
            {
                btnSaveDatabase_Click(null, null);
                warningBox1.Text = StrMessage;
               
            }
            else
            {
                warningBox1.Text = Err;
            }
        }

        private void btnSaveGeneral_Click(object sender, EventArgs e)
        {
            // Kiểm tra trường Port rỗng hay không
            if (String.IsNullOrEmpty(txtPort.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtPort.Focus();
                return;
            }

            //Kiểm tra trường Port có là số không
            if (!Common.IsItNumber(txtPort.Text.Trim()))
            {
                warningBox1.Text = IsNumber;

                txtPort.Focus();

                return;
            }

            //Kiểm tra trường Timer Interval rỗng hay không
            if (String.IsNullOrEmpty(txtInter.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtInter.Focus();

                return;
            }

            //Kiểm tra trường Timer Interval có là số không
            if (!Common.IsItNumber(txtInter.Text.Trim()))
            {
                warningBox1.Text = IsNumber;

                txtInter.Focus();
                tabControl1.SelectedTabIndex = 0;
                return;
            }

            //Kiểm tra trường Delay Time rỗng hay không
            if (String.IsNullOrEmpty(txtDelay.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtDelay.Focus();

                return;
            }

            //Kiểm tra trường Delay Time có là số không
            if (!Common.IsItNumber(txtDelay.Text.Trim()))
            {
                warningBox1.Text = IsNumber;

                txtDelay.Focus();

                return;
            }
            LablinkServiceConfig.SetPort(txtPort.Text.Trim());
            LablinkServiceConfig.SetTimerInternal(txtInter.Text.Trim());
            LablinkServiceConfig.SetDelayTime(txtDelay.Text.Trim());
            LablinkServiceConfig.SaveConfig();
            warningBox1.Text = "";

           
        }

        private void btnSaveDatabase_Click(object sender, EventArgs e)
        {
            //Kiểm tra trường Server
            if (String.IsNullOrEmpty(txtServer.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;


                txtServer.Focus();
                return;
            }
            //Kiểm tra trường Database
            if (String.IsNullOrEmpty(txtDatabase.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtDatabase.Focus();

                return;
            }

            //Kiểm tra trường UserID
            if (String.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtUser.Focus();

                return;
            }

            if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;

                txtPassword.Focus();

                return;
            }
            LablinkServiceConfig.SetDatabase(txtDatabase.Text.Trim());
            LablinkServiceConfig.SetServer(txtServer.Text.Trim());
            LablinkServiceConfig.SetUserId(txtUser.Text.Trim());
            LablinkServiceConfig.SetPassword(txtPassword.Text.Trim());
            LablinkServiceConfig.SaveConfig();
            warningBox1.Text = "";
        }

        private void btnSaveExtraInfo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtReportType.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;
                txtReportType.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtParentBranchName.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;
                txtParentBranchName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtBranchName.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;
                txtBranchName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;
                txtAddress.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                warningBox1.Text = StrNotEmpty;
                txtPhone.Focus();
                return;
            }
            LablinkBusinessConfig.SetParentBanch(txtParentBranchName.Text.Trim());
            LablinkBusinessConfig.SetBanch(txtBranchName.Text.Trim());
            LablinkBusinessConfig.SetAddress(txtAddress.Text.Trim());
            LablinkBusinessConfig.SetPhone(txtPhone.Text.Trim());
            LablinkBusinessConfig.SaveConfig();
            warningBox1.Text = "";
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            if (txtPortNumber.Text.Trim().Equals(""))
            {
                warningBox1.Text = StrNotEmpty;

                txtPortNumber.Focus();

                return;
            }

            //Kiểm tra trường Port Number có là số không
            if (!Common.IsItNumber(txtPortNumber.Text.Trim()))
            {
                warningBox1.Text = IsNumber;

                txtPortNumber.Focus();

                return;
            }
            if (ipAddressInput1.Text.Trim().Equals(""))
            {
                warningBox1.Text = StrNotEmpty;

                ipAddressInput1.Focus();

                return;
            }
            LablinkServiceConfig.SetIpAdress(ipAddressInput1.Text.Trim());
            LablinkServiceConfig.SetPortNumber(txtPortNumber.Text.Trim());
            LablinkServiceConfig.SaveConfig();
            warningBox1.Text = "";
        }

        private void btnSaveGlobal_Click(object sender, EventArgs e)
        {
            if (CheckFormatDate())
            {
                LablinkServiceConfig.SetTCPIPStatus(sbtnTcpIpService.Value.ToString());
                LablinkServiceConfig.SetFileStatus(sbtFileService.Value.ToString());
                LablinkServiceConfig.SetRS232Status(sbtnRs232Service.Value.ToString());
                LablinkServiceConfig.SetDateFormat(cboDateFormat.Text);
                LablinkServiceConfig.SetAutoUpdateOrderStatusAfterSend(AutoUpdateOrderStatusAfterSend.Checked.ToString(CultureInfo.InvariantCulture));
                LablinkServiceConfig.SaveConfig();
            }
        }

        private void cboDateFormat_SelectedValueChanged(object sender, EventArgs e)
        {
            CheckFormatDate();
        }

        private void cboDateFormat_TextUpdate(object sender, EventArgs e)
        {
            CheckFormatDate();
        }

        private void switchTestTypeBarcode_ValueChanged(object sender, EventArgs e)
        {
            LablinkServiceConfig.SetTestTypeBarcode(switchTestTypeBarcode.Value.ToString());
            LablinkServiceConfig.SaveConfig();
        }

        private void sbtnRs232Service_ValueChanged(object sender, EventArgs e)
        {
            LablinkServiceConfig.SetRS232Status(sbtnRs232Service.Value.ToString());
            LablinkServiceConfig.SaveConfig();
        }

        private void sbtnTcpIpService_ValueChanged(object sender, EventArgs e)
        {
            LablinkServiceConfig.SetTCPIPStatus(sbtnTcpIpService.Value.ToString());
            LablinkServiceConfig.SaveConfig();
        }

        private void sbtFileService_ValueChanged(object sender, EventArgs e)
        {
            LablinkServiceConfig.SetFileStatus(sbtFileService.Value.ToString());
            LablinkServiceConfig.SaveConfig();
        }

        private void switchLogState_ValueChanged(object sender, EventArgs e)
        {
            LablinkServiceConfig.SetLogState(switchLogState.Value.ToString());
            LablinkServiceConfig.SaveConfig();
        }


        #endregion

        private void btnSaveDisplay_Click(object sender, EventArgs e)
        {
            LablinkBusinessConfig.SetStyle(cboDisplay.Text);
            LablinkBusinessConfig.SaveConfig();
        }

    }
}