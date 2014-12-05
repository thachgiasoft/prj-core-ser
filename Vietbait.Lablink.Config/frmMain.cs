using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Vietbait.Lablink.Config
{
    public partial class FrmMain : Form
    {
        #region Attributies

        private const string StrNotEmpty = "Not empty";
        private const string IsNumber = "Phải là số";
        private const string Port = "port";
        private const string Timerinterval = "timerinterval";
        private const string Delaytime = "delaytime";
        private const string StrLabLinkService = "LABLinkService";
        private const string StrLablinkBusiness = "LABLinkBusiness";
        private const string FileName = "App.config";

        private readonly Vietbait.Config.Config _serviceConfig;
        private readonly Vietbait.Config.Config _businessConfig;

        #endregion

        #region Contructor

        public FrmMain()
        {
            InitializeComponent();
            lblMessage.Text = "";
            _serviceConfig = new Vietbait.Config.Config(FileName, StrLabLinkService);
            _businessConfig = new Vietbait.Config.Config(FileName,StrLablinkBusiness);
            _businessConfig.Set("Demo", "Demovalue");
            _businessConfig.SaveConfig();
            ReadFileConfig();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Hàm dùng để đọc file theo tham số truyền vào
        /// </summary>
        private void ReadFileConfig()
        {
           txtPort.Text = _serviceConfig.Get(Port).ToString();
          txtInter.Text = _serviceConfig.Get(Timerinterval).ToString();
            txtDelay.Text = _serviceConfig.Get(Delaytime).ToString();
        }

        /// <summary>
        /// Hàm kiểm tra dữ liệu nhập vào 
        /// </summary>
        /// <returns>True: Thành công, False: Không thành công</returns>
        private bool Validation()
        {
            lblMessage.ForeColor = Color.Red;

            //Kiểm tra trường Delay Time rỗng hay không
            if (txtDelay.Text.Trim().Equals(""))
            {
                lblMessage.Text = StrNotEmpty;
                return false;
            }

            ////Kiểm tra trường Delay Time có là số không
            if (!IsItNumber(txtDelay.Text.Trim()))
            {
                
                lblMessage.Text = IsNumber;
                return false;
            }

            //Kiểm tra trường Timer Interval rỗng hay không
            if (txtInter.Text.Trim().Equals(""))
            {
                lblMessage.Text = StrNotEmpty;
                return false;
            }

            //Kiểm tra trường Timer Interval có là số không
            if (!IsItNumber(txtInter.Text.Trim()))
            {
                lblMessage.Text = IsNumber;
                return false;
            }

            //Kiểm tra trường Port rỗng hay không
            if (txtPort.Text.Trim().Equals(""))
            {
                lblMessage.Text = StrNotEmpty;
                return false;
            }

            //Kiểm tra trường Port có là số không
            if (!IsItNumber(txtPort.Text.Trim()))
            {
                lblMessage.Text = IsNumber;
                return false;
            }
            lblMessage.Text = "";
            return true;
        }

        /// <summary>
        /// Kiểm tra đầu vào là số hay không
        /// </summary>
        /// <param name="inputvalue">Tham số truyền vào</param>
        /// <returns>True: là số, False: không là số </returns>
        private static bool IsItNumber(string inputvalue)
        {
            var isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(inputvalue);
        }

        #endregion

        #region Form Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                _serviceConfig.Set(Port, txtPort.Text.Trim());
                _serviceConfig.Set(Timerinterval, txtInter.Text.Trim());
                _serviceConfig.Set(Delaytime, txtDelay.Text.Trim());
                _serviceConfig.SaveConfig();
                lblMessage.Text = "Saved";
                lblMessage.ForeColor = Color.Green;
            }
        }

        #endregion
    }
}