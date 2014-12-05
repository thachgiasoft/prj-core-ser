using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using VB6 = Microsoft.VisualBasic.Strings;
using System.Data.SqlClient;
using System.Data;
namespace VietBaIT.CommonLibrary
{

    public class MultiLanguage
    {
        private static DataGridView CurrDtGridView;
        private static StatusStrip CurrStatusBar;
        private static ToolStrip CurrToolBar;
        private static string gv_sAnnouce = "Thông báo";
        /// <summary>
        /// Lấy về chuỗi giá trị dựa trên giá trị ngôn ngữ truyền vào
        /// </summary>
        /// <param name="Language">Mã ngôn ngữ truyền vào thường là biến gv_sLanguageDisplay</param>
        /// <param name="VnText">Giá trị tiếng Việt(Language=VN)</param>
        /// <param name="EnText">Giá trị tiếng Anh(Language=EN)</param>
        /// <returns>Nếu gv_sLanguageDisplay=EN thì trả về giá trị VnText. Ngược lại trả về giá trị EnText</returns>
        /// <remarks></remarks>
        public static string GetText(string Language, string VnText, string EnText)
        {
            if (Language.ToUpper() == "VN")
            {
                return VnText;
            }
            else
            {
                return EnText;
            }
        }
        /// <summary>
        /// Thiết lập ngôn ngữ hiển thị cho một Form chức năng
        /// </summary>
        /// <param name="Language">Mã ngôn ngữ truyền vào thường là biến gv_sLanguageDisplay</param>
        /// <param name="ctrMain">Tên Control(Form) sẽ bị tác động. Giá trị thường là 'me' trong VB.NET hoặc 'this' trong C#</param>
        /// <param name="sDLLName">Tên DLL chứa Form chức năng(Ví dụ: Invoice.Dll)</param>
        /// <param name="gv_oSqlCnn">Biến kết nối tới CSDL. Thường sử dụng luôn biến gv_oSqlCnn</param>
        /// <remarks></remarks>
        public static void SetLanguage(string Language, Control ctrMain, string sDLLName, System.Data.SqlClient.SqlConnection gv_oSqlCnn)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Sys_MULTILANGUAGE WHERE sFormName=N'" + ctrMain.Name + "' AND sDLLName=N'" + sDLLName + "'", globalVariables.SqlConn);
            DataTable DT1 = new DataTable();
            string sVnText = null;
            string sEnText = null;
            sVnText = "";
            sEnText = "";
            try
            {
                da1.Fill(DT1);
                if (DT1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in DT1.Rows)
                    {
                        if (!Information.IsDBNull(dr1["sVnText"]) & string.IsNullOrEmpty(sVnText))
                        {
                            sVnText = Utility.sDbnull(dr1["sVnText"]);
                        }
                        if (!Information.IsDBNull(dr1["sEnText"]))
                        {
                            sEnText = Utility.sDbnull(dr1["sEnText"]);
                        }
                        if (!string.IsNullOrEmpty(sVnText) & !string.IsNullOrEmpty(sEnText))
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (Language.ToUpper() == "VN")
                    {
                        ctrMain.Text = sVnText;
                    }
                    else
                    {
                        ctrMain.Text = sEnText;
                    }
                    UserControl UC = new UserControl();
                    foreach (Control ctr in ctrMain.Controls)
                    {
                        if ((ctr) is TextBox || (ctr) is UserControl || (ctr) is Label | (ctr) is Button | (ctr) is GroupBox | (ctr) is CheckBox | (ctr) is RadioButton | (ctr) is Panel | (ctr) is ComboBox)
                        {
                            string CtrlName = null;
                            if ((ctr.Parent != null) && ctr.Parent.GetType().BaseType.FullName == UC.GetType().FullName)
                            {
                                CtrlName = ctr.Parent.Name + "." + ctr.Name;
                            }
                            else
                            {
                                CtrlName = ctr.Name;
                            }
                            DataRow[] dr = null;
                            dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CtrlName + "'");
                            if (dr.GetLength(0) > 0)
                            {
                                if (Language.ToUpper() == "VN")
                                {
                                    if ((ctr) is ComboBox)
                                    {
                                        ComboBox cbo = null;
                                        string[] splt = Utility.sDbnull(dr[0]["sVn"]).Split( ',');
                                        cbo = (ComboBox)ctr;
                                        cbo.Items.Clear();
                                        for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                        {
                                            cbo.Items.Add(splt[i]);
                                        }
                                    }
                                    else
                                    {
                                        ctr.Text = Utility.sDbnull(dr[0]["sVn"]);
                                    }
                                }
                                else
                                {
                                    if ((ctr) is ComboBox)
                                    {
                                        ComboBox cbo = null;
                                        string[] splt = Utility.sDbnull(dr[0]["sEn"]).Split( ',');
                                        cbo = (ComboBox)ctr;
                                        cbo.Items.Clear();
                                        for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                        {
                                            cbo.Items.Add(splt[i]);
                                        }
                                    }
                                    else
                                    {
                                        ctr.Text = Utility.sDbnull(dr[0]["sEn"]);
                                    }
                                }
                            }
                            else
                            {
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                        else if ((ctr) is StatusStrip && ctr.GetType().FullName != "System.Windows.Forms.ToolStrip")
                        {
                            //StatusStrip or ToolStrip
                            objGetCurrentStatusBar((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            for (int i = 0; i <= CurrStatusBar.Items.Count - 1; i++)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CurrStatusBar.Items[i].Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        CurrStatusBar.Items[i].Text = Utility.sDbnull(dr[0]["sVn"]);
                                    }
                                    else
                                    {
                                        CurrStatusBar.Items[i].Text = Utility.sDbnull(dr[0]["sEn"]);
                                    }
                                }
                                else
                                {
                                }
                            }
                        }
                        else if ((ctr) is ToolStrip && ctr.GetType().FullName == "System.Windows.Forms.ToolStrip")
                        {
                            //StatusStrip or ToolStrip
                            objGetCurrentToolbar((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            for (int i = 0; i <= CurrToolBar.Items.Count - 1; i++)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + CurrToolBar.Items[i].Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        CurrToolBar.Items[i].ToolTipText = Utility.sDbnull(dr[0]["sVn"]);
                                    }
                                    else
                                    {

                                        CurrToolBar.Items[i].ToolTipText = Utility.sDbnull(dr[0]["sEn"]);
                                    }
                                }
                                else
                                {
                                }
                            }
                        }
                        else if ((ctr) is DataGridView)
                        {
                            objGetCurrentDataGridView((Form)ctrMain, ctr.Name);
                            // CType(ctr, DataGridView)
                            foreach (DataGridViewColumn col in CurrDtGridView.Columns)
                            {
                                DataRow[] dr = null;
                                dr = DT1.Select("sFormName='" + ctrMain.Name + "' AND sControlName='" + col.Name + "'");
                                if (dr.GetLength(0) > 0)
                                {
                                    if (Language.ToUpper() == "VN")
                                    {
                                        col.HeaderText = Utility.sDbnull(dr[0]["sVn"]);
                                    }
                                    else
                                    {
                                        col.HeaderText = Utility.sDbnull(dr[0]["sEn"]);
                                    }
                                }
                                else
                                {
                                }
                            }
                            LoopControl(ctrMain, DT1, ctr, Language);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private static void LoopControl(Control mainForm, DataTable dt1, Control objCtr, string pv_sLang)
        {
            UserControl UC = new UserControl();
            foreach (Control ctr in objCtr.Controls)
            {
                if ((ctr) is TextBox || (ctr) is UserControl || (ctr) is Label | (ctr) is Button | (ctr) is GroupBox | (ctr) is CheckBox | (ctr) is RadioButton | (ctr) is Panel | (ctr) is ComboBox)
                {

                    DataRow[] dr = null;
                    string CtrlName = null;
                    if ((ctr.Parent != null) && ctr.Parent.GetType().BaseType.FullName == UC.GetType().FullName)
                    {
                        CtrlName = ctr.Parent.Name + "." + ctr.Name;
                    }
                    else
                    {
                        CtrlName = ctr.Name;
                    }

                    dr = dt1.Select("sFormName='" + mainForm.Name + "' AND sControlName='" + CtrlName + "'");
                    if (dr.GetLength(0) > 0)
                    {
                        if (pv_sLang.ToUpper() == "VN")
                        {
                            if ((ctr) is ComboBox)
                            {
                                ComboBox cbo = null;
                                string[] splt = Utility.sDbnull(dr[0]["sVn"]).Split( ',');
                                cbo = (ComboBox)ctr;
                                cbo.Items.Clear();
                                for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                {
                                    cbo.Items.Add(splt[i]);
                                }
                            }
                            else
                            {
                                ctr.Text = Utility.sDbnull(dr[0]["sVn"]);
                            }
                        }
                        else
                        {
                            if ((ctr) is ComboBox)
                            {
                                ComboBox cbo = null;
                                string[] splt = Utility.sDbnull(dr[0]["sEn"]).Split( ',');
                                cbo = (ComboBox)ctr;
                                cbo.Items.Clear();
                                for (int i = 0; i <= splt.GetLength(0) - 1; i++)
                                {
                                    cbo.Items.Add(splt[i]);
                                }
                            }
                            else
                            {
                                ctr.Text = Utility.sDbnull(dr[0]["sEn"]);
                            }
                        }
                    }
                    else
                    {
                    }
                    LoopControl(mainForm, dt1, ctr, pv_sLang);
                }
                else if ((ctr) is StatusStrip)
                {
                }
                else if ((ctr) is ToolStrip)
                {
                }
                else if ((ctr) is DataGridView)
                {
                    objGetCurrentDataGridView((Form)mainForm, ctr.Name);
                    // CType(ctr, DataGridView)
                    foreach (DataGridViewColumn col in CurrDtGridView.Columns)
                    {
                        DataRow[] dr = null;
                        dr = dt1.Select("sFormName='" + mainForm.Name + "' AND sControlName='" + col.Name + "'");
                        if (dr.GetLength(0) > 0)
                        {
                            if (pv_sLang.ToUpper() == "VN")
                            {
                                col.HeaderText = Utility.sDbnull(dr[0]["sVn"]);
                            }
                            else
                            {
                                col.HeaderText = Utility.sDbnull(dr[0]["sEn"]);
                            }
                        }
                        else
                        {
                        }
                    }
                    LoopControl(mainForm, dt1, ctr, pv_sLang);
                }
            }
        }
        private static void objGetCurrentDataGridView(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrDtGridView = (DataGridView)ctr;
                }
                else
                {
                    objGetCurrentDataGridView(ctr, pv_sName);
                }
            }
        }
        private static void objGetCurrentStatusBar(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrStatusBar = (StatusStrip)ctr;
                }
                else
                {
                    objGetCurrentStatusBar(ctr, pv_sName);
                }
            }
        }
        private static void objGetCurrentToolbar(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    CurrToolBar = (ToolStrip)ctr;
                }
                else
                {
                    objGetCurrentToolbar(ctr, pv_sName);
                }
            }
        }
    }
}
