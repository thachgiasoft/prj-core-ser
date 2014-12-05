using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace VietBaIT.CommonLibrary
{
    public partial class Processing : Form
    {
        private delegate void StartupCallBack();
        CommonLibrary.ProcessStatus CurrentStatus;
        private IAsyncResult _ar;
        public Processing()
        {
            InitializeComponent();
            this.Load += new EventHandler(Processing_Load);
        }

        public string Status
        {
            set
            {
                Label1.Text = value;
                Label1.Refresh();
            }
            get { return Label1.Text; }
        }
        void Processing_Load(object sender, EventArgs e)
        {
          Application.DoEvents();
           _ar = BeginInvoke(new StartupCallBack(new ThreadStart(ProcessStatus)));
        }
        private void ProcessStatus()
        {
            EndInvoke(_ar);
            Application.DoEvents();
            try
            {
                while (!CommonLibrary.globalVariables.FinishProcess)
                {
                    Application.DoEvents();
                    switch (CurrentStatus)
                    {
                        case CommonLibrary.ProcessStatus.PreparingData:
                            Status = "Đang thực hiện tạo điều kiện tìm kiếm dữ liệu...";
                            break;
                        case CommonLibrary.ProcessStatus.RequestingData:
                            Status = "Đang thực hiện lấy dữ liệu từ Server...";
                            break;
                        case CommonLibrary.ProcessStatus.ProcessDataBeforeUsing:
                            Status = "Đang thực hiện xử lý dữ liệu...";
                            break;
                        case CommonLibrary.ProcessStatus.PushDataIntoDataGridview:
                            Status = "Đang thực hiện đẩy dữ liệu vào lưới...";
                            break;
                        case CommonLibrary.ProcessStatus.PushDataIntoReport:
                            Status = "Đang thực hiện đẩy dữ liệu vào báo cáo...";
                            break;
                        case CommonLibrary.ProcessStatus.WritetoFile:
                            Status = "Đang thực hiện đẩy dữ liệu vào file...";
                            break;
                        default:
                            Status = "Xin vui lòng chờ đợi trong giây lát...";
                            break;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                this.Close();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
