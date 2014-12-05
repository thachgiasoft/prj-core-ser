using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vietbait.Lablink
{
    public partial class frm_SignInfor : Form
    {
        public bool mv_bChapNhan;
        public string mv_sFontName;
        public string mv_sFontSize;
        public string mv_sFontStyle;

        public frm_SignInfor()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void cmdQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            //thuc hien lay gia tri
            mv_bChapNhan = true;
            Close();
        }

        private void GetFontList()
        {
            try
            {
                foreach (FontFamily s in FontFamily.Families)
                {
                    cboFontName.Items.Add(s.Name);
                }
                for (int i = 6; i <= 72; i++)
                {
                    cboFontSize.Items.Add(i.ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SetFontInfor(string pv_sFontName, string pv_sFontStyple, string pv_sFontSize)
        {
            bool sv_bFound = false;
            try
            {
                for (int i = 0; i <= cboFontName.Items.Count - 1; i++)
                {
                    if (cboFontName.Items[i].ToString().Trim().ToUpper().Equals(pv_sFontName.Trim().ToUpper()))
                    {
                        cboFontName.SelectedIndex = i;
                        sv_bFound = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (!sv_bFound) cboFontName.Text = "Arial";
                sv_bFound = false;
                for (int i = 0; i <= cboFontStyle.Items.Count - 1; i++)
                {
                    if (cboFontStyle.Items[i].ToString().Trim().ToUpper().Equals(pv_sFontStyple.Trim().ToUpper()))
                    {
                        cboFontStyle.SelectedIndex = i;
                        sv_bFound = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (!sv_bFound) cboFontStyle.SelectedIndex = 2;
                sv_bFound = false;
                for (int i = 0; i <= cboFontSize.Items.Count - 1; i++)
                {
                    if (cboFontSize.Items[i].ToString().Trim().ToUpper().Equals(pv_sFontSize.Trim().ToUpper()))
                    {
                        cboFontSize.SelectedIndex = i;
                        sv_bFound = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (!sv_bFound) cboFontSize.Text = "8";
                sv_bFound = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void frm_SignInfor_Load(object sender, EventArgs e)
        {
            try
            {
                //SetLanguage(gv_sLanguageDisplay, this, "GOLFMAN", gv_oSqlCnn);
                GetFontList();
                SetFontInfor(mv_sFontName, mv_sFontStyle, mv_sFontSize);
                txtBaoCao.Enabled = false;
                txtBaoCao.BackColor = Color.WhiteSmoke;
            }

            catch (Exception ex)
            {
                //SetLanguage(gv_sLanguageDisplay, this, "GOLFMAN", gv_oSqlCnn);
            }
        }

        private void InitializeEvents()
        {
            Load += frm_SignInfor_Load;
            cmdOK.Click += cmdOK_Click;
            cmdQuit.Click += cmdQuit_Click;
        }
    }
}