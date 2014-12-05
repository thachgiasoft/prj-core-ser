using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using VietBaIT.CommonLibrary;
using Microsoft.VisualBasic;

namespace VietBaIT.CommonLibrary
{
    public partial class frmPrintPreview : Form
    {
        
    CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
	private bool MustCreate;
	public DataSet dsXML = new DataSet();
	public string RType;
	public bool OE;
	public bool bSetMargin = false;
	public int TopMargin;
	public int LeftMargin;
	public int BottomMargin;
	public int RightMargin;
	public DataRow ORow;
	public DataRow ERow;
    public int vPageCopy = 1;
       
	public CrystalDecisions.Shared.PageMargins margins;
	public bool bCanhLe = false;
	public string mv_sNgayQT = string.Empty;
	//bien nay dung de hien thi thong tin trinh ky
	//XuanDT them vao
	cls_SignInfor mv_oNguoiKy;
	CrystalDecisions.CrystalReports.Engine.TextObject mv_oRptText;
	CrystalDecisions.CrystalReports.Engine.FieldObject mv_oRptFieldObj;
	CrystalDecisions.CrystalReports.Engine.ReportDocument mv_oRptDoc;
	CrystalDecisions.Windows.Forms.CrystalReportViewer mv_oViewDoc;
    
	CrystalDecisions.Shared.ParameterFields mv_oRptPara;
	bool mv_bNgayQToan = false;
	bool mv_bSetContent = true;
	bool mv_bAdded = false;
        public frmPrintPreview(string FormTitle, CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc, bool pv_bSetContent,bool pv_bDisplayPrintButton)
            : base()
        {
            
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            //Add any initialization after the InitializeComponent() call
            this.Text = FormTitle;
            this.RptDoc = RptDoc;
            
            //
            this.crptViewer.ReportSource = this.RptDoc;
            cmdTrinhKy.Visible = pv_bSetContent;
            mv_bSetContent = pv_bSetContent;
            this.crptViewer.ShowRefreshButton = false;
            this.crptViewer.ShowPrintButton = pv_bDisplayPrintButton;
            InitializeEvents();
        }
        public frmPrintPreview()
        {
           
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            //Add any initialization after the InitializeComponent() call
            cmdTrinhKy.Visible = false;
            this.crptViewer.ShowRefreshButton = true;
            InitializeEvents();
        }
        private void InitializeEvents()
        {
            cmdTrinhKy.Click+=new EventHandler(cmdTrinhKy_Click);
            cmdExcel.Click+=new EventHandler(cmdExcel_Click);
            this.Load+=new EventHandler(frmPrintPreview_Load);
            this.KeyDown+=new KeyEventHandler(frmPrintPreview_KeyDown);

        }
	private void frmPrintPreview_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape) this.Close(); 
		if (e.KeyCode == Keys.P || (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)) {
			crptViewer.PrintReport();
			return;
		}
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S) {
			cmdExcel.PerformClick();
			return;
		}
	}

	private void frmPrintPreview_Load(object sender, System.EventArgs e)
	{
		try {
		//SetLanguage(globalVariables.DisplayLanguage, this, "GOLFMAN", globalVariables.SqlConn);
			addTrinhKy_OnFormLoad();
		}
		catch (Exception ex) {
			//SetLanguage(globalVariables.DisplayLanguage, this, "GOLFMAN", globalVariables.SqlConn);
		}

	}
	public void Hide_Control()
	{
		cmdTrinhKy.Visible = false;
	}
	private void cmdTrinhKy_Click(object sender, System.EventArgs e)
	{
		//XuanDT Them ham nay vao
		addTrinhKy_OnButtonClick();
	}

	public void addTrinhKy_OnFormLoad()
	{
		//Ham nay XuanDT them vao
		try {
			//doan gan cac bien: doan nay co the phai thay doi ten bien cho phu hop
			mv_oRptDoc = RptDoc;
			mv_oViewDoc = this.crptViewer;
			//ket thuc doan gan bien
			mv_oRptFieldObj = mv_oRptDoc.ReportDefinition.ReportObjects["Field150181"] as FieldObject;
			mv_oNguoiKy = new cls_SignInfor(mv_oRptDoc.ToString(), "");
			//chkPrint_CheckedChanged(chkPrint, New System.EventArgs)
			if (mv_oNguoiKy._TonTai) {
				mv_oNguoiKy.setValueToRPT(ref mv_oRptFieldObj);
				if (mv_bSetContent) {
					mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + mv_oNguoiKy.mv_NOI_DUNG.Replace("&NHANVIEN", globalVariables.gv_sStaffName).Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
				}
				else {
					mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = "";
				}
				mv_oViewDoc.ReportSource = RptDoc;
			}
			else {
				mv_oNguoiKy = new cls_SignInfor(mv_oRptFieldObj, "", RptDoc.ToString(), mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text);
			}
		}
		catch (Exception ex) {
			mv_oRptText = null;
			//an nut tuy chon di
			this.cmdTrinhKy.Visible = false;
		}
	}
	private void ms_SetPositionForTextObj(string pv_sName)
	{
		try {
			CrystalDecisions.CrystalReports.Engine.TextObject sv_oRptText = default(CrystalDecisions.CrystalReports.Engine.TextObject);
			sv_oRptText = mv_oRptDoc.ReportDefinition.ReportObjects[pv_sName] as TextObject;
			sv_oRptText.Top = mv_oRptFieldObj.Top + mv_oRptFieldObj.Height + 5;
		}
		catch (Exception ex) {

		}
	}
	public void addTrinhKy_OnButtonClick()
	{
		if (mv_oRptFieldObj == null) return;
 
		try {
			//Hien form de thay doi tuy chon ky
			frm_SignInfor sv_fTuyChonKy = new frm_SignInfor();
			sv_fTuyChonKy.txtBaoCao.Text = this.mv_oNguoiKy.mv_TEN_BIEUBC;
			//sv_fTuyChonKy.txtCoChu.Text = Me.mv_oNguoiKy.mv_CO_CHU.ToString()

			//sv_fTuyChonKy.txtKieuFont.Text = Me.mv_oNguoiKy.mv_KIEU_CHU
			//#$X$# cua XuanDT, khong duoc doi
			sv_fTuyChonKy.txtNoiDungKy.Text = this.mv_oNguoiKy.mv_NOI_DUNG.Replace("#$X$#", Constants.vbCrLf);
			//sv_fTuyChonKy.txtTenFont.Text = Me.mv_oNguoiKy.mv_FONT_CHU
			sv_fTuyChonKy.mv_sFontName = this.mv_oNguoiKy.mv_FONT_CHU;
			sv_fTuyChonKy.mv_sFontSize = this.mv_oNguoiKy.mv_CO_CHU.ToString();
			sv_fTuyChonKy.mv_sFontStyle = this.mv_oNguoiKy.mv_KIEU_CHU;
			sv_fTuyChonKy.ShowDialog();
			if (sv_fTuyChonKy.mv_bChapNhan) {
				this.mv_oNguoiKy.mv_TEN_BIEUBC = sv_fTuyChonKy.txtBaoCao.Text.Trim();
				this.mv_oNguoiKy.mv_CO_CHU =Convert.ToInt32( sv_fTuyChonKy.cboFontSize.SelectedItem.ToString());
				//Me.mv_oNguoiKy.mv_CHIEU_DAI = Val(sv_fTuyChonKy.txtDai.Text.Trim)
				this.mv_oNguoiKy.mv_KIEU_CHU = sv_fTuyChonKy.cboFontStyle.SelectedItem.ToString();
				if (!mv_bAdded) {
					this.mv_oNguoiKy.mv_NOI_DUNG = sv_fTuyChonKy.txtNoiDungKy.Text.Replace(Constants.vbCrLf, "#$X$#") + " ";
				}
				else {
					this.mv_oNguoiKy.mv_NOI_DUNG = sv_fTuyChonKy.txtNoiDungKy.Text.Replace(Constants.vbCrLf, "#$X$#").Substring(0, sv_fTuyChonKy.txtNoiDungKy.Text.Length - 1);
				}
				//Me.mv_oNguoiKy.mv_CHIEU_RONG = Val(sv_fTuyChonKy.txtRong.Text.Trim)
				this.mv_oNguoiKy.mv_FONT_CHU = sv_fTuyChonKy.cboFontName.SelectedItem.ToString();
				//Me.mv_oNguoiKy.mv_TOADO_NGANG = Val(sv_fTuyChonKy.txtViTriX.Text.Trim)
				//Me.mv_oNguoiKy.mv_TOADO_DOC = Val(sv_fTuyChonKy.txtViTriY.Text.Trim)
				if (sv_fTuyChonKy.chkGhiLai.Checked == true) {
					this.mv_oNguoiKy.updateRPTtoDB();
				}
				mv_oNguoiKy.setValueToRPT(ref mv_oRptFieldObj);
				SetParamAgain(mv_oViewDoc.ParameterFieldInfo);
				if (mv_bSetContent) {
					mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + mv_oNguoiKy.mv_NOI_DUNG.Replace("&NHANVIEN", globalVariables.gv_sStaffName).Replace( "#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
				}
				else {
					mv_oRptDoc.DataDefinition.FormulaFields["Formula_1"].Text = "";
				}
				//mv_oViewDoc.ReportSource = Nothing
				mv_oViewDoc.ReportSource = RptDoc;
			}
		}
		catch (Exception ex) {
			
		}
	}
	private void SetParamAgain(CrystalDecisions.Shared.ParameterFields p)
	{
		try {
			CrystalDecisions.Shared.ParameterFields p0 = new CrystalDecisions.Shared.ParameterFields();
			for (int i = 0; i <= p.Count - 1; i++) {
				if (p[i].ParameterFieldName.ToUpper() == "TXTNGUOIKY1") {
					RptDoc.SetParameterValue(p[i].ParameterFieldName, mv_oNguoiKy.mv_NOI_DUNG.Replace("&NHANVIEN", globalVariables.gv_sStaffName));
				}
				else if (p[i].ParameterFieldName.ToUpper() == "TXTNGAYQT") {
				}
				else {
					RptDoc.SetParameterValue(p[i].ParameterFieldName, ((CrystalDecisions.Shared.ParameterDiscreteValue)p[i].CurrentValues[0]).Value);
				}
			}
		}
		catch (Exception ex) {
		}
	}
	

	private void crptViewer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
	{
	}

	private void cmdExcel_Click(object sender, System.EventArgs e)
	{
		string sFileName = "";
		try {
			SaveFileDialog SaveFileDlg = new SaveFileDialog();
			SaveFileDlg.Title = "VietBaJC-->Save to Excel file";
			SaveFileDlg.Filter = "Excel files|*.XLS";
			if (SaveFileDlg.ShowDialog() == DialogResult.OK) {
				sFileName = SaveFileDlg.FileName;
				if (sFileName.Contains(".XLS")) {
				}
				else {
					sFileName += ".XLS";
				}
				this.Text = "Đang lưu dữ liệu ra file: " + sFileName;
				RptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, sFileName);
				this.Text = "In dữ liệu";
				if (Utility.AcceptQuestion("Đã xuất dữ liệu thành công ra file Excel ở đường dẫn: " + sFileName + Constants.vbCrLf + "Bạn có muốn mở file Excel ra xem không?","Thông báo",true)) {
					System.Diagnostics.Process.Start(sFileName);
				}
			}
		}
		catch (Exception ex) {

		}
	}

   
    }
}
