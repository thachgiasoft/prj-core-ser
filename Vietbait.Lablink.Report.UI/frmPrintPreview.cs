using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;
using Microsoft.VisualBasic;
using VietBaIT.CommonLibrary;


namespace Vietbait.Lablink.Report.UI
{
    public partial class frmPrintPreview : Office2007Form
    {
        private readonly ReportDocument RptDoc;
        private readonly bool mv_bSetContent = true;
        public int BottomMargin;
        public DataRow ERow;
        public int LeftMargin;
        private bool MustCreate;
        public bool OE;
        public DataRow ORow;
        public string RType;
        public int RightMargin;
        public int TopMargin;
        public bool bCanhLe;
        public bool bSetMargin;
        public DataSet dsXML = new DataSet();
        public PageMargins margins;
        private bool mv_bAdded;
        private bool mv_bNgayQToan;
        //bien nay dung de hien thi thong tin trinh ky
        //XuanDT them vao
        private cls_SignInfor mv_oNguoiKy;
        private ReportDocument mv_oRptDoc;
        private FieldObject mv_oRptFieldObj;
        private ParameterFields mv_oRptPara;
        private TextObject mv_oRptText;
        private CrystalReportViewer mv_oViewDoc;
        public string mv_sNgayQT = string.Empty;

        public frmPrintPreview(string FormTitle, ReportDocument RptDoc, bool pv_bSetContent, bool pv_bDisplayPrintButton)
        {
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            //Add any initialization after the InitializeComponent() call
            Text = FormTitle;
            this.RptDoc = RptDoc;
            //
            crptViewer.ReportSource = this.RptDoc;
            cmdTrinhKy.Visible = pv_bSetContent;
            mv_bSetContent = pv_bSetContent;
            crptViewer.ShowRefreshButton = false;
            crptViewer.ShowPrintButton = pv_bDisplayPrintButton;
            InitializeEvents();
        }

        public frmPrintPreview()
        {
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            //Add any initialization after the InitializeComponent() call
            cmdTrinhKy.Visible = false;
            crptViewer.ShowRefreshButton = true;

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            cmdTrinhKy.Click += cmdTrinhKy_Click;
            cmdExcel.Click += cmdExcel_Click;
            Load += frmPrintPreview_Load;
            KeyDown += frmPrintPreview_KeyDown;
        }

        private void frmPrintPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.P || (e.Modifiers == Keys.Control && e.KeyCode == Keys.P))
            {
                crptViewer.PrintReport();
                return;
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                cmdExcel.PerformClick();
                return;
            }
        }

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            try
            {
                addTrinhKy_OnFormLoad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Hide_Control()
        {
            cmdTrinhKy.Visible = false;
        }

        private void cmdTrinhKy_Click(object sender, EventArgs e)
        {
            //XuanDT Them ham nay vao
            addTrinhKy_OnButtonClick();
        }

        public void addTrinhKy_OnFormLoad()
        {
            //Ham nay XuanDT them vao
            try
            {
                //doan gan cac bien: doan nay co the phai thay doi ten bien cho phu hop
                mv_oRptDoc = RptDoc;
                mv_oViewDoc = crptViewer;
                //ket thuc doan gan bien
                mv_oRptFieldObj = mv_oRptDoc.ReportDefinition.ReportObjects["Field150181"] as FieldObject;
                mv_oNguoiKy = new cls_SignInfor(mv_oRptDoc.ToString(), "");
                //chkPrint_CheckedChanged(chkPrint, New System.EventArgs)
                if (mv_oNguoiKy._TonTai)
                {
                    mv_oNguoiKy.setValueToRPT(ref mv_oRptFieldObj);
                    if (mv_bSetContent)
                    {
                        mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text =
                            Strings.Chr(34) +
                            mv_oNguoiKy.mv_NOI_DUNG.Replace("&NHANVIEN", globalVariables.gv_sStaffName).Replace(
                                "#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                    }
                    else
                    {
                        mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = "";
                    }
                    mv_oViewDoc.ReportSource = RptDoc;
                }
                //else
                //{
                //    //mv_oNguoiKy = new cls_SignInfor(mv_oRptFieldObj, "", RptDoc.ToString(), mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text);
                //}
            }
            catch (Exception ex)
            {
                mv_oRptText = null;
                //an nut tuy chon di
                cmdTrinhKy.Visible = false;
            }
        }

        private void ms_SetPositionForTextObj(string pv_sName)
        {
            try
            {
                TextObject sv_oRptText = default(TextObject);
                sv_oRptText = mv_oRptDoc.ReportDefinition.ReportObjects[pv_sName] as TextObject;
                sv_oRptText.Top = mv_oRptFieldObj.Top + mv_oRptFieldObj.Height + 5;
            }
            catch (Exception ex)
            {
            }
        }

        public void addTrinhKy_OnButtonClick()
        {
            if (mv_oRptFieldObj == null) return;

            try
            {
                //Hien form de thay doi tuy chon ky
                var sv_fTuyChonKy = new frm_SignInfor();
                sv_fTuyChonKy.txtBaoCao.Text = mv_oNguoiKy.mv_TEN_BIEUBC;
                //sv_fTuyChonKy.txtCoChu.Text = Me.mv_oNguoiKy.mv_CO_CHU.ToString()

                //sv_fTuyChonKy.txtKieuFont.Text = Me.mv_oNguoiKy.mv_KIEU_CHU
                //#$X$# cua XuanDT, khong duoc doi
                sv_fTuyChonKy.txtNoiDungKy.Text = mv_oNguoiKy.mv_NOI_DUNG.Replace("#$X$#", Constants.vbCrLf);
                //sv_fTuyChonKy.txtTenFont.Text = Me.mv_oNguoiKy.mv_FONT_CHU
                sv_fTuyChonKy.mv_sFontName = mv_oNguoiKy.mv_FONT_CHU;
                sv_fTuyChonKy.mv_sFontSize = mv_oNguoiKy.mv_CO_CHU.ToString();
                sv_fTuyChonKy.mv_sFontStyle = mv_oNguoiKy.mv_KIEU_CHU;
                sv_fTuyChonKy.ShowDialog();
                if (sv_fTuyChonKy.mv_bChapNhan)
                {
                    mv_oNguoiKy.mv_TEN_BIEUBC = sv_fTuyChonKy.txtBaoCao.Text.Trim();
                    mv_oNguoiKy.mv_CO_CHU = Convert.ToInt32(sv_fTuyChonKy.cboFontSize.SelectedItem.ToString());
                    //Me.mv_oNguoiKy.mv_CHIEU_DAI = Val(sv_fTuyChonKy.txtDai.Text.Trim)
                    mv_oNguoiKy.mv_KIEU_CHU = sv_fTuyChonKy.cboFontStyle.SelectedItem.ToString();
                    if (!mv_bAdded)
                    {
                        mv_oNguoiKy.mv_NOI_DUNG = sv_fTuyChonKy.txtNoiDungKy.Text.Replace(Constants.vbCrLf, "#$X$#") +
                                                  " ";
                    }
                    else
                    {
                        mv_oNguoiKy.mv_NOI_DUNG =
                            sv_fTuyChonKy.txtNoiDungKy.Text.Replace(Constants.vbCrLf, "#$X$#").Substring(0,
                                                                                                         sv_fTuyChonKy.
                                                                                                             txtNoiDungKy
                                                                                                             .Text.
                                                                                                             Length - 1);
                    }
                    //Me.mv_oNguoiKy.mv_CHIEU_RONG = Val(sv_fTuyChonKy.txtRong.Text.Trim)
                    mv_oNguoiKy.mv_FONT_CHU = sv_fTuyChonKy.cboFontName.SelectedItem.ToString();
                    //Me.mv_oNguoiKy.mv_TOADO_NGANG = Val(sv_fTuyChonKy.txtViTriX.Text.Trim)
                    //Me.mv_oNguoiKy.mv_TOADO_DOC = Val(sv_fTuyChonKy.txtViTriY.Text.Trim)
                    if (sv_fTuyChonKy.chkGhiLai.Checked)
                    {
                        mv_oNguoiKy.updateRPTtoDB();
                    }
                    mv_oNguoiKy.setValueToRPT(ref mv_oRptFieldObj);
                    SetParamAgain(mv_oViewDoc.ParameterFieldInfo);
                    if (mv_bSetContent)
                    {
                        mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                                                                                    mv_oNguoiKy.mv_NOI_DUNG.Replace(
                                                                                        "&NHANVIEN",
                                                                                        globalVariables.gv_sStaffName).
                                                                                        Replace("#$X$#",
                                                                                                Strings.Chr(34) +
                                                                                                "&Chr(13)&" +
                                                                                                Strings.Chr(34)) +
                                                                                    Strings.Chr(34);
                    }
                    else
                    {
                        mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = "";
                    }
                    //mv_oViewDoc.ReportSource = Nothing
                    mv_oViewDoc.ReportSource = RptDoc;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetParamAgain(ParameterFields p)
        {
            try
            {
                var p0 = new ParameterFields();
                for (int i = 0; i <= p.Count - 1; i++)
                {
                    if (p[i].ParameterFieldName.ToUpper() == "TXTNGUOIKY1")
                    {
                        RptDoc.SetParameterValue(p[i].ParameterFieldName,
                                                 mv_oNguoiKy.mv_NOI_DUNG.Replace("&NHANVIEN",
                                                                                 globalVariables.gv_sStaffName));
                    }
                    else if (p[i].ParameterFieldName.ToUpper() == "TXTNGAYQT")
                    {
                    }
                    else
                    {
                        RptDoc.SetParameterValue(p[i].ParameterFieldName,
                                                 ((ParameterDiscreteValue) p[i].CurrentValues[0]).Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string sFileName = "";
            try
            {
                var SaveFileDlg = new SaveFileDialog();
                SaveFileDlg.Title = "VietBaJC-->Save to Excel file";
                SaveFileDlg.Filter = "Excel files|*.XLS";
                if (SaveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    sFileName = SaveFileDlg.FileName;
                    if (sFileName.Contains(".XLS"))
                    {
                    }
                    else
                    {
                        sFileName += ".XLS";
                    }
                    Text = "Đang lưu dữ liệu ra file: " + sFileName;
                    RptDoc.ExportToDisk(ExportFormatType.Excel, sFileName);
                    Text = "In dữ liệu";
                    if (
                        Utility.AcceptQuestion(
                            "Đã xuất dữ liệu thành công ra file Excel ở đường dẫn: " + sFileName + Constants.vbCrLf +
                            "Bạn có muốn mở file Excel ra xem không?", "Thông báo", true))
                    {
                        Process.Start(sFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void crptViewer_Load(object sender, EventArgs e)
        {
        }
    }
}