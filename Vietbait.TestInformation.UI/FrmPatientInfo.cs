using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Microsoft.VisualBasic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;
using Vietbait.TestInformation.Business;
using VB6 = Microsoft.VisualBasic.Strings;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmPatientInfo : Office2007Form
    {
        private readonly string sFlow = "LIST";
        private ActionResult actionResult;
        public DataRow drPatientInfo;
        private DataTable dtPatientList;
        public action em_Action = action.Insert;
        public DataGridView grdList;
        public DataTable gv_dtpatient;
        private DataTable m_dtDataList = new DataTable();
        public int v_PatientID = -1;

        public FrmPatientInfo(string FlowAction)
        {
            InitializeComponent();
            InitializeEventControl();
            KeyPreview = true;
            sFlow = FlowAction;
            dtpDate.Text = DateTime.Now.ToString();
            txtYearOfBirth.Text = DateTime.Now.Year.ToString();
            checkBox1.Checked = true;
            txtPID.Text = GetPid("");
            txtName.Focus();
        }

        private void InitializeEventControl()
        {
            txtAge.KeyUp += txtAge_KeyUp;
            txtAge.TextChanged += txtAge_TextChanged;
            txtName.TextChanged += txtName_TextChanged;
        }

        private void FrmPatientInfor_Load(object sender, EventArgs e)
        {
            setValivariable();

            LoadDataComboBox(cboDepartment, DepartmentBusiness.GetAllDepartment(), LDepartment.Columns.SName,
                             LDepartment.Columns.Id, -1);

            LoadDataComboBox(cboObject, ObjectBusiness.GetAllObject(), LObjectType.Columns.SName, LObjectType.Columns.Id,
                             0);
            cboObject.SelectedIndex = 0;
            cboSex.SelectedIndex = 0;
            switch (sFlow)
            {
                case "LIST":
                    if (em_Action == action.Update)
                    {
                        GetData();
                    }
                    break;
            }
            txtName.Focus();
            txtName.SelectAll();
            //GenerateNewSid();
        }

        private void GetData()
        {
            txtPID.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Pid]);
            txtName.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.PatientName]);
            txtRoom.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Room]);
            txtBed.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Bed]);
            txtRoom.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Room]);
            txtDiagnose.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Diagnostic]);
            txtInsurance.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.InsuranceNum]);
            txtInvoiceNumber.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.IdentifyNum]);
            txtAddress.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Address]);
            txtAge.Text = Utility.sDbnull(drPatientInfo[LPatientInfo.Columns.Age]);
            cboSex.SelectedIndex = Utility.Int32Dbnull(drPatientInfo[LPatientInfo.Columns.Sex]);
            cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment,
                                                                   (drPatientInfo[LPatientInfo.Columns.DepartmentID].
                                                                       ToString()));
            cboObject.SelectedIndex = Utility.GetSelectedIndex(cboObject,
                                                               (drPatientInfo[LPatientInfo.Columns.ObjectType].ToString()));
        }

        private void LoadDataComboBox(ComboBox cbo, DataTable dt, string colName, string colId, int allItemsID)
        {
            if (allItemsID != 0)
            {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0][colName] = "";
                dt.Rows[0][colId] = allItemsID;
            }

            cbo.DataSource = dt;
            cbo.DisplayMember = colName;
            cbo.ValueMember = colId;
            cbo.SelectedIndex = 0;
        }

        private void FrmPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) btnSave.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện xử lý keyup cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAge_KeyUp(object sender, KeyEventArgs e)
        {
            int len = txtAge.Text.Length;
            int age = 0;
            if (len > 0 && !char.IsDigit(txtAge.Text, len - 1)) txtAge.Text = txtAge.Text.Substring(0, len - 1);
            //MessageBox.Show(txtAge.Text);
            //txtYearOfBirth.Text = Convert.ToString(DateTime.Now.Year - Utility.Int32Dbnull(txtAge.Text, 0));
        }

        public bool bcheckdata()
        {
            Utility.ResetMessageError(errorProvider1);
            if (txtName.Text == "")
            {
                Utility.SetMessageError(errorProvider1, txtName, "Bạn phải nhập tên Bệnh Nhân!");
                txtName.Focus();
                return false;
            }
            if (txtAge.Text == "")
            {
                //VietBaIT.CommonLibrary.Utility.ShowMsg("");
                Utility.SetMessageError(errorProvider1, txtAge, "Bạn phải nhập tuổi!");
                txtAge.Focus();
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!bcheckdata()) return;
            switch (em_Action)
            {
                case action.Insert:
                    InsertNewPatientInfo();
                    Clearcontrol();
                    break;
                case action.Update:
                    UpdatePatient();
                    ProcessData();
                    break;
            }

            if (checkBox1.Checked)
            {
                cmdExit.PerformClick();
            }
        }

        private void setValivariable()
        {
            Utility.ResetMessageError(errorProvider1);
            if (txtName.Text == "")
            {
                Utility.SetMessageError(errorProvider1, txtName, "Bạn phải nhập tên Bệnh Nhân!");
            }
            if (txtAge.Text == "")
            {
                //VietBaIT.CommonLibrary.Utility.ShowMsg("");
                Utility.SetMessageError(errorProvider1, txtAge, "Bạn phải nhập tuổi!");
            }
        }

        private void UpdatePatient()
        {
            actionResult = PatientBusiness.UpdatePatientInfo(CreatePatientInfo());
            MessageStatus();
            if (checkBox1.Checked)
            {
                Close();
            }
        }

        private void MessageStatus()
        {
            switch (actionResult)
            {
                case ActionResult.Success:
                    ProcessData();
                    Utility.ShowMsg("Cập nhập thành công");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi");
                    break;
            }
        }

        /// <summary>
        /// hàm thực hiện xử lý thông tin 
        /// </summary>
        private void ProcessData()
        {
            v_PatientID = Utility.Int32Dbnull(drPatientInfo[LPatientInfo.Columns.PatientId], -1);
            DataRow[] arrDr = gv_dtpatient.Select("Patient_ID=" + v_PatientID);
            if (arrDr.GetLength(0) > 0)
            {
                arrDr[0][LPatientInfo.Columns.PatientName] = txtName.Text;
                arrDr[0][LPatientInfo.Columns.Age] = txtAge.Text;
                arrDr[0][LPatientInfo.Columns.ObjectType] = cboObject.SelectedValue;
                arrDr[0][LPatientInfo.Columns.Address] = txtAddress.Text;
                arrDr[0][LPatientInfo.Columns.Room] = txtRoom.Text;
                arrDr[0][LPatientInfo.Columns.IdentifyNum] = txtInvoiceNumber.Text;
                arrDr[0][LPatientInfo.Columns.InsuranceNum] = txtInsurance.Text;
                arrDr[0][LPatientInfo.Columns.Diagnostic] = txtDiagnose.Text;
                arrDr[0][LPatientInfo.Columns.YearBirth] = txtYearOfBirth.Text;
                arrDr[0][LPatientInfo.Columns.Sex] = cboSex.SelectedIndex;
                arrDr[0][LPatientInfo.Columns.DepartmentID] = Utility.Int32Dbnull(cboDepartment.SelectedValue, -1);
                arrDr[0]["Department_Name"] = Utility.sDbnull(cboDepartment.Text, "");
                arrDr[0]["SexName"] = Utility.sDbnull(cboSex.Text, "");
            }
            Utility.GotoNewRow(grdList, "colPatient_ID", v_PatientID.ToString());
            gv_dtpatient.AcceptChanges();
        }

        /// <summary>
        /// hàm thực hiện làm sách thông tin 
        /// </summary>
        private void Clearcontrol()
        {
            foreach (object control in grpInformationPK.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    var textBox = (TextBox) control;
                    textBox.Clear();
                }
                if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    var comboBox = (ComboBox) control;
                    comboBox.SelectedIndex = 0;
                }
            }
            foreach (object control in grpInformationFK.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    var textBox = (TextBox) control;
                    textBox.Clear();
                }
                if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    var comboBox = (ComboBox) control;
                    comboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// hàm thực hiện lấy barcode cho bệnh nhân
        /// </summary>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        public string GetPid(string sOrder)
        {
            return DateAndTime.Now.Year + VB6.Right("0" + DateAndTime.Now.Month, 2) +
                   VB6.Right("0" + DateAndTime.Now.Day, 2) + VB6.Right("0" + DateAndTime.Now.Hour, 2) +
                   VB6.Right("0" + DateAndTime.Now.Minute, 2) + VB6.Right("0" + DateAndTime.Now.Second, 2) + sOrder;
        }

        private void InsertNewPatientInfo()
        {
            string newPid = PatientBusiness.InsertPatient(CreatePatientInfo());
            if (sFlow == "LIST")
            {
                if (newPid != "-1")
                {
                    DataRow dr = gv_dtpatient.NewRow();
                    dr[LPatientInfo.Columns.Pid] = GetPid("");
                    dr[LPatientInfo.Columns.PatientId] = newPid;
                    dr[LPatientInfo.Columns.PatientName] = txtName.Text;
                    dr["SexName"] = cboSex.Text;
                    dr[LPatientInfo.Columns.Age] = txtAge.Text;
                    dr["Department_Name"] = cboDepartment.Text;
                    dr[LPatientInfo.Columns.Bed] = txtBed.Text;
                    dr[LPatientInfo.Columns.IdentifyNum] = txtInvoiceNumber.Text;
                    dr[LPatientInfo.Columns.Room] = txtRoom.Text;
                    dr[LPatientInfo.Columns.InsuranceNum] = txtInsurance.Text;
                    dr[LPatientInfo.Columns.Address] = txtAddress.Text;
                    dr[LPatientInfo.Columns.Diagnostic] = txtDiagnose.Text;
                    dr[LPatientInfo.Columns.Dateupdate] = dtpDate.Value;
                    dr[LPatientInfo.Columns.YearBirth] = txtYearOfBirth.Text;
                    dr[LPatientInfo.Columns.ObjectType] = cboObject.SelectedValue;
                    dr[LPatientInfo.Columns.DepartmentID] = cboDepartment.SelectedValue;
                    dr[LPatientInfo.Columns.Sex] = cboSex.SelectedIndex;
                    gv_dtpatient.Rows.Add(dr);
                    gv_dtpatient.AcceptChanges();
                    Utility.GotoNewRow(grdList, "colPatient_ID", newPid);
                    if (checkBox1.Checked)
                    {
                        Close();
                    }
                }
            }
        }

        /// <summary>
        /// hàm thực hiện khởi tạo thông tin bệnh nhân
        /// </summary>
        /// <returns></returns>
        private LPatientInfo CreatePatientInfo()
        {
            var pItem = new LPatientInfo();
            pItem.Pid = GetPid("");
            pItem.PatientName = txtName.Text;
            pItem.Sex = (byte?) Convert.ToInt16(cboSex.SelectedIndex);
            pItem.Age = Convert.ToInt16(txtAge.Text);
            pItem.IdentifyNum = txtInvoiceNumber.Text;
            pItem.YearBirth = Utility.Int32Dbnull(txtYearOfBirth.Text, 0);
            pItem.DepartmentID = Convert.ToInt16(cboDepartment.SelectedValue);
            pItem.ObjectType = Convert.ToInt16(cboObject.SelectedValue);
            pItem.Address = txtAddress.Text;
            pItem.Diagnostic = txtDiagnose.Text;
            pItem.InsuranceNum = txtInsurance.Text;
            pItem.Bed = txtBed.Text;
            pItem.Room = txtRoom.Text;
            if (em_Action == action.Update)
            {
                pItem.PatientId = Utility.Int32Dbnull(drPatientInfo[LPatientInfo.Columns.PatientId], -1);
            }
            return pItem;
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            setValivariable();
            if (!string.IsNullOrEmpty(txtAge.Text) & !Information.IsNumeric(txtAge.Text))
            {
                Interaction.MsgBox("Giá trị tuổi không hợp lệ !", MsgBoxStyle.Exclamation, "Thông báo)");
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(txtAge.Text))
                    {
                        txtYearOfBirth.Text = DateAndTime.Now.Year.ToString();
                        return;
                    }
                    if (Information.IsNumeric(txtAge.Text))
                    {
                        txtYearOfBirth.Text = (DateAndTime.Now.Year - Convert.ToInt32(txtAge.Text)).ToString();
                    }
                    else
                    {
                        txtYearOfBirth.Text = DateAndTime.Now.Year.ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// hàm thực hiện thay đổi năm sinh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtYearOfBirth_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtYearOfBirth.Text) & !Information.IsNumeric(txtYearOfBirth.Text))
            {
                Utility.ShowMsg("Giá trị năm sinh không hợp lệ !");
                txtYearOfBirth.Focus();
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(txtYearOfBirth.Text))
                    {
                        txtAge.Text = "";
                        return;
                    }
                    if (Information.IsNumeric(txtYearOfBirth.Text))
                    {
                        txtAge.Text = (DateAndTime.Now.Year - Convert.ToInt32(txtYearOfBirth.Text)).ToString();
                    }
                    else
                    {
                        //txtAge.Text = Now.Year.ToString
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện thay đổi thông tin của validate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            setValivariable();
        }

        /// <summary>
        /// hàm thực hiện thông tin không cho nhập ksy tự
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        /// <summary>
        /// hàm thực hiện không cho nhập ký tự cho năm sinh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtYearOfBirth_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
    }
}