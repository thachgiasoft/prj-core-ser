using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vietbait.Lablink.Interface
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                grdProperties.SelectedObject = InterfaceHelper.MyProperties;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi trong quá trình nạp form {0}", ex.ToString());
            }
        }

        private void FrmConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                InterfaceHelper.SaveInterfaceConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi trong quá lưu cấu hình {0}", ex.ToString());
            }
        }
    }
}
