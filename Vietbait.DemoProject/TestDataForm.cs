using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ScintillaNET;
using Vietbait.Lablink.Utilities;

namespace Vietbait.DemoProject
{
    public partial class TestDataForm : Form
    {
        #region Fields

        private TcpipServerImpl _demoServer;
        private TcpClient _tcpClient;

        class SettextParametter
        {
            public Control TextBox;
            public string TextData;
            public SettextParametter(Control textbox,string textData)
            {
                TextBox = textbox;
                TextData = textData;
            }
        }

        private bool _tcpEnterd = false;

        #endregion

        #region Constant

        public const char NULL = (char)0;
        public const char STX = (char)2;
        public const char ETX = (char)3;
        public const char EOT = (char)4;
        public const char ENQ = (char)5;
        public const char ACK = (char)6;
        public const char CR = (char)13;
        public const char LF = (char)10;
        public const char VT = (char)11;
        public const char NAK = (char)21;
        public const char ETB = (char)23;
        public const char FS = (char)28;
        public const char GS = (char)29;
        public const char SOH = (char)1;
        public const char SYN = (char)22;
        public const char DC1 = (char)17;
        public const char DC2 = (char)18;
        public const char DC3 = (char)19;
        public const char DC4 = (char)20;
        public static readonly string CRLF = String.Format("{0}{1}", CR, LF);

        #endregion

        #region Constructor

        public TestDataForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Method

        #region Scroll Bar Documents

        /// <summary>
        /// The line number of the longest line.
        /// </summary>
        private static int _longestLineIndex;

        /// <summary>
        /// The length of the longest line.
        /// </summary>
        private static int _longestLineLength;

        /// <summary>
        /// The modification type during DocumentChange event.
        /// </summary>
        private static uint _modificationType;

        /// <summary>
        /// The start line number for text insert.
        /// </summary>
        private static int _textInsertStartLine;

        /// <summary>
        /// The end line number for text insert.
        /// </summary>
        private static int _textInsertEndLine;

        public static void ToggleScrollBarsVisible(Scintilla scintilla)
        {
            // performance is acceptable up to 100000 lines
            scintilla.DocumentChange += ToggleScrollBarsScintillaDocumentChange;
            scintilla.TextChanged += ToggleScrollBarsScintillaTextChanged;
            //scintilla.TextChanged += new System.EventHandler(this.txtDlGui_TextChanged);
        }

        private static void ToggleScrollBarsScintillaTextChanged(object sender, EventArgs e)
        {
            var scintilla = (Scintilla)sender;

            var lineWidth = (int)GetLineSize(scintilla, _longestLineIndex).Width;

            if (GetTextAreaSize(scintilla).Width < lineWidth)
            {
                if ((scintilla.Scrolling.ScrollBars & ScrollBars.Horizontal) == 0)
                    scintilla.Scrolling.ScrollBars = ScrollBars.Both;
                scintilla.Scrolling.HorizontalWidth = lineWidth;
            }
            else
            {
                if ((scintilla.Scrolling.ScrollBars & ScrollBars.Horizontal) != 0 &&
                    scintilla.Scrolling.XOffset > 0)
                {
                    // do noting
                }
                else
                    scintilla.Scrolling.ScrollBars = ScrollBars.Vertical;
                ;
            }
        }

        /// <summary>
        /// DocumentChange event handler for ToggleScrollBarsVisible method.
        /// </summary>
        private static void ToggleScrollBarsScintillaDocumentChange(object sender, NativeScintillaEventArgs e)
        {
            var scintilla = (Scintilla)sender;

            // before insert: we need to capture the lines added, since we do not get them later
            if ((e.SCNotification.modificationType & Constants.SC_MOD_INSERTTEXT) != 0)
            {
                _modificationType = Constants.SC_MOD_INSERTTEXT;
                _textInsertStartLine = scintilla.Caret.LineNumber;
                _textInsertEndLine = _textInsertStartLine + e.SCNotification.linesAdded;
            }

                // after insert: only scan new lines
            else if (e.SCNotification.modificationType == 0x14 &&
                     _modificationType == Constants.SC_MOD_INSERTTEXT)
            {
                _modificationType = 0;
                _longestLineIndex = GetMaxLineIndex(scintilla, _longestLineIndex, _textInsertStartLine,
                                                    _textInsertEndLine);
                _longestLineLength = scintilla.Lines[_longestLineIndex].Length;
            }

                // after delete: do selective scan for longest line
            else if ((e.SCNotification.modificationType & Constants.SC_MOD_DELETETEXT) != 0)
            {
                _modificationType = 0;

                if (_longestLineIndex == scintilla.Caret.LineNumber)
                {
                    // we need a full scan, if parts of the current line were deleted
                    if (_longestLineLength > scintilla.Lines[scintilla.Caret.LineNumber].Length)
                    {
                        _longestLineIndex = GetMaxLineIndex(scintilla, 0, 0, scintilla.Lines.Count);
                        _longestLineLength = scintilla.Lines[_longestLineIndex].Length;
                    }
                }
                // we need a full scan, if one of the deleted lines contained the longest line
                else if (_longestLineIndex >= scintilla.Caret.LineNumber &&
                         _longestLineIndex <= scintilla.Caret.LineNumber - e.SCNotification.linesAdded)
                {
                    _longestLineIndex = GetMaxLineIndex(scintilla, 0, 0, scintilla.Lines.Count);
                    _longestLineLength = scintilla.Lines[_longestLineIndex].Length;
                }
                // scan beyond current line, if longest line was below deleted lines 
                else if (_longestLineIndex > scintilla.Caret.LineNumber - e.SCNotification.linesAdded)
                {
                    _longestLineIndex = GetMaxLineIndex(scintilla, _longestLineIndex, scintilla.Caret.LineNumber,
                                                        scintilla.Lines.Count);
                    _longestLineLength = scintilla.Lines[_longestLineIndex].Length;
                }
            }
        }

        /// <summary>
        /// Gets the size of a line specified by it's line number.
        /// </summary>
        public static SizeF GetLineSize(Scintilla scintilla, int lineIndex)
        {
            //
            // TO DO:   read the font settings from DEFAULT, DOCUMENT_DEFAULT, etc and make a sensible choice
            //          for now it is Courier New,10,bold
            float zoom = (scintilla.Zoom <= -10F ? 0.1F : 1 + (float)scintilla.Zoom / 10);

            string indent = "";
            for (int i = 0; i < scintilla.Indentation.IndentWidth; i++)
                indent += " ";

            string strToMeasure = scintilla.Lines[lineIndex].Text;
            strToMeasure = strToMeasure.Replace("\t", indent);

            SizeF sizeF;
            using (Graphics g = scintilla.CreateGraphics())
            {
                var font = new Font("Courier New", zoom * 10, FontStyle.Bold);
                sizeF = g.MeasureString(strToMeasure, font);
            }
            return sizeF;
        }

        /// <summary>
        /// Gets the line number of the longest (un-wrapped) line 
        /// between specified start and end line with a specified start max line index.
        /// </summary>
        public static int GetMaxLineIndex(Scintilla scintilla, int maxLineIndex, int startLineNo, int endLineNo)
        {
            int index = maxLineIndex;
            for (int i = startLineNo; i <= endLineNo; i++)
            {
                int lineLength = scintilla.Lines[i].Length;
                if (i == scintilla.Lines.Count - 1)
                    lineLength += 2;

                if (lineLength >= scintilla.Lines[index].Length)
                    index = i;
            }
            return index;
        }

        /// <summary>
        /// Gets the text area size of the Scintilla control.
        /// </summary>
        public static Size GetTextAreaSize(Scintilla scintilla)
        {
            Size size = scintilla.ClientSize;
            for (int i = 0; i < scintilla.Margins.Count; i++)
                size.Width -= scintilla.Margins[i].Width;
            return size;
        }

        #endregion

        #region Set DLNhan Text

        private void SetText(object textbox,string text)
        {
            var myTextBox = ((Control)textbox);
            if (myTextBox.InvokeRequired)
            {
                var d = new SetTextCallback(SetText);
                Invoke(d, new[] {textbox, text });
            }
            else
            {
                myTextBox.Text = myTextBox.Text + text;
            }
        }

        private void ThreadSetText(object parametter)
        {
            try
            {
                var p = (SettextParametter) parametter;
                SetText(p.TextBox, p.TextData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private delegate void SetTextCallback(object textbox,string text);

        #endregion

        private void LoadcboParity()
        {
            try
            {
                cboParity.Items.Clear();
                foreach (object obj in Enum.GetValues(typeof(Parity)))
                {
                    cboParity.Items.Add(obj);
                }
                cboParity.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Loadcbostopbits()
        {
            try
            {
                cboStopBit.Items.Clear();
                foreach (object obj in Enum.GetValues(typeof(StopBits)))
                    cboStopBit.Items.Add(obj);
                cboStopBit.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadcboHandshaking()
        {
            try
            {
                cboHandShacking.Items.Clear();
                foreach (object obj in Enum.GetValues(typeof(Handshake)))
                {
                    cboHandShacking.Items.Add(obj);
                }
                cboHandShacking.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Loadcbocom()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lbsCom.Items.Clear();
                int[] freePort = GetFreePort();

                for (int i = 0; i <= freePort.Length - 1; i++)
                    lbsCom.Items.Add(string.Format("COM{0}", freePort[i]));
                lbsCom.SelectedIndex = lbsCom.Items.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public int[] GetFreePort()
        {
            var result = new int[] { };
            int count = 0;
            for (int i = 1; i <= 50; i++)
            {
                if (!CheckPort(string.Format("COM{0}", i))) continue;
                Array.Resize(ref result, count + 1);
                result[count] = i;
                count = count + 1;
            }
            return result;
        }

        public bool CheckPort(string portname)
        {
            try
            {
                com.PortName = portname;
                com.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                com.Close();
            }
        }

        /// <summary>
        /// Thiết lập cổng COM để gửi dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool SetComPara()
        {
            try
            {
                com.PortName = lbsCom.SelectedItem.ToString();
                com.BaudRate = Convert.ToInt32(cboBaudRate.SelectedItem.ToString());
                com.DataBits = Convert.ToInt32(cboDataBits.SelectedItem.ToString());
                com.Parity = (Parity)cboParity.SelectedItem;
                com.StopBits = (StopBits)cboStopBit.SelectedItem;
                com.Handshake = (Handshake)cboHandShacking.SelectedItem;
                com.DtrEnable = true;
                com.RtsEnable = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong thủ tục thiết lập các tham số cho cổng COM");
                return false;
            }
        }

        /// <summary>
        /// Hàm trả về chuỗi Checksum của một Frame.
        /// Check khi có một trong hai giá trị ETX, ETB
        /// </summary>
        /// <param name="frame">Chuỗi cần kiểm tra</param>
        /// <returns>Chuỗi trả về là 2 ký tự dùng để checksum</returns>
        public static string GetCheckSumValue(string frame)
        {
            string checksum = "00";

            int sumOfChars = 0;
            bool complete = false;

            //take each byte in the string and add the values
            for (int idx = 0; idx < frame.Length; idx++)
            {
                int byteVal = Convert.ToInt32(frame[idx]);

                switch (byteVal)
                {
                    case STX:
                        sumOfChars = 0;
                        break;
                    case ETX:
                    case ETB:
                        sumOfChars += byteVal;
                        complete = true;
                        break;
                    default:
                        sumOfChars += byteVal;
                        break;
                }

                if (complete)
                    break;
            }

            if (sumOfChars > 0)
            {
                //hex value mod 256 is checksum, return as hex value in upper case
                checksum = Convert.ToString(sumOfChars % 256, 16).ToUpper();
            }

            //if checksum is only 1 char then prepend a 0
            return (checksum.Length == 1 ? "0" + checksum : checksum);
        }

        /// <summary>
        /// Hàm điều khiển sự kiện nhận dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string raw = "";
                while (com.BytesToRead>0)
                {
                    raw += com.ReadExisting();
                }
                SettextParametter p = new SettextParametter(txtDlNhan,raw);
                Thread t = new Thread(new ParameterizedThreadStart(ThreadSetText));
                t.Start(p);
                //var t = new Thread(ts);
                //t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error While Received Data: {0}", ex.ToString());
            }
        }

        private int GetLineCursorPosition(Scintilla editor, bool @fixed)
        {
            int lineCursorPosition = editor.Selection.Start - editor.Lines.Current.StartPosition;
            if (lineCursorPosition < 0) return 0;
            return @fixed
                       ? lineCursorPosition + 1
                       : lineCursorPosition;
        }

        private void EditorSelectionChanged(object sender, EventArgs e)
        {
            var editor = (Scintilla)sender;
            lblCurrentPosition.Text = string.Format("Current   Col:{0}   Line:{1}   Selected:{2}",
                                                    GetLineCursorPosition(editor, true),
                                                    editor.Lines.Current.Number + 1, editor.Selection.Length);

            lblLengthAndLines.Text = string.Format("Total:   Lines:{0} Length{1}", editor.Lines.Count, editor.TextLength);
        }

        private void EditorEnter(object sender, EventArgs e)
        {
            var editor = (Scintilla)sender;
            lblCurrentPosition.Text = string.Format("Current   Col:{0}   Line:{1}   Selected:{2}",
                                                    GetLineCursorPosition(editor, true),
                                                    editor.Lines.Current.Number + 1, editor.Selection.Length);

            lblLengthAndLines.Text = string.Format("Total:   Lines:{0} Length{1}", editor.Lines.Count, editor.TextLength);
        }

        private void AppendTcpLog(string text)
        {
            string log = string.Format("{0}: {1}", DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss.ffff"), text);
            var p = new SettextParametter(txtTcpLog, string.Format("{0}{1}", log,CRLF));
            Thread t = new Thread(ThreadSetText);
            t.Start(p);
        }

        #endregion

        #region Form Events

        private void TestDataFormLoad(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadcboHandshaking();
                LoadcboParity();
                Loadcbostopbits();
                Loadcbocom();
                cboDataBits.SelectedIndex = 3;
                cboBaudRate.SelectedIndex = 4;

                //Gán sự kiện xử lý các nút nhấn dữ liệu điều khiển.
                btnAck.Click += BtnGuiClick;
                btnEOT.Click += BtnGuiClick;
                btnEnq.Click += BtnGuiClick;
                btnNak.Click += BtnGuiClick;
                btnEtb.Click += BtnGuiClick;
                btnCr.Click += BtnGuiClick;
                btnLf.Click += BtnGuiClick;

                //Gán sự kiện xử lý khi nút check endofline bị thay đổi
                rbtCrlf.CheckedChanged += CbxEndOfLineCheckChanged;
                rbtCr.CheckedChanged += CbxEndOfLineCheckChanged;
                rbtLF.CheckedChanged += CbxEndOfLineCheckChanged;

                txtGui.SelectionChanged += EditorSelectionChanged;
                txtDlGui.SelectionChanged += EditorSelectionChanged;
                txtDlNhan.SelectionChanged += EditorSelectionChanged;


                ToggleScrollBarsVisible(txtDlGui);
                ToggleScrollBarsVisible(txtDlNhan);

                txtDlGui.KeyUp += TxtGuiKeyUp;
                txtDlNhan.KeyUp += TxtGuiKeyUp;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnketnoiClick(object sender, EventArgs e)
        {
            if (tabConnectionType.SelectedTab == tabTpcIpServer)
            {
                //Nếu chưa được kết nối thì khởi tạo kết nối
                if (btnketnoi.Tag.ToString() != "1")
                {
                    try
                    {
                        _demoServer = new TcpipServerImpl()
                                          {
                                              Port = Convert.ToInt32(txtPort.Text.Trim()),
                                              DelayTime = 100,
                                              TimerInterval = 100
                                          };
                        //Gán hàm xử lý sự kiện
                        _demoServer.ClientConnected += OnClientConnected;
                        _demoServer.StartServerSuccessfull += OnStartServerSuccess;
                        _demoServer.EndReciveData += OnEndReciveData;
                        _demoServer.IncommingData+=OnIncommingData;
                        _demoServer.ClientDisconnected += OnClientDisconnected;

                        _demoServer.StartServer();
                        lblStatus.Text = "Khởi động TCPIP Server thành công";
                        btnketnoi.Text = "Ngắt Kết Nối";
                        btnketnoi.Tag = 1;
                        grbDulieugui.Enabled = true;

                       

                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Khởi động TCPIP Server không thành công";
                    }
                }
                else
                {
                    _demoServer.StopServer();
                    lblStatus.Text = "Đã ngắt kết nối";
                    btnketnoi.Text = "Kết Nối";
                    btnketnoi.Tag = 0;
                    grbDulieugui.Enabled = false;
                }
            }
            else if (tabConnectionType.SelectedTab == tabCom)
            {
                //Nếu chưa được kết nối thì khởi tạo kết nối
                if (btnketnoi.Tag.ToString() != "1")
                {
                    try
                    {
                        if (SetComPara())
                        {
                            com.Open();
                            lblStatus.Text = "Khởi động cổng COM thành công";
                            btnketnoi.Text = "Ngắt Kết Nối";
                            btnketnoi.Tag = 1;
                            grbDulieugui.Enabled = true;
                        }
                    }
                    catch (Exception)
                    {
                        lblStatus.Text = "Khởi động cổng COM không thành công";
                    }
                }
                else
                {
                    com.Close();
                    lblStatus.Text = "Đã ngắt kết nối";
                    btnketnoi.Text = "Kết Nối";
                    btnketnoi.Tag = 0;
                    grbDulieugui.Enabled = false;
                }
            }

        }

        private void BtnGuiClick(object sender, EventArgs e)
        {
            if (tabConnectionType.SelectedTab == tabTpcIpServer)
            {
                if (!_demoServer.IsRunning)
                {
                    MessageBox.Show("Server is not started", "Error", MessageBoxButtons.OK);
                    return;
                }
                byte bt = Convert.ToByte(((Button) sender).Tag);
                string dataToSend;
                if (bt == 0)
                {
                    dataToSend = txtGui.Selection.Length > 0 ? txtGui.Selection.Text : txtGui.Text;
                    //Trường hợp gửi dữ liệu thông thường
                    if (chkAstm.Checked)
                    {
                        dataToSend = string.Format("{0}1{1}{2}{3}", STX, dataToSend, CR, ETX);
                        dataToSend = string.Format("{0}{1}{2}", dataToSend, GetCheckSumValue(dataToSend), CRLF);
                    }
                }
                else
                {
                    dataToSend = ((char) bt).ToString(CultureInfo.InvariantCulture);
                }

                if ((bt != 0) && ((ModifierKeys & Keys.Control) == Keys.Control))
                {
                    txtGui.InsertText(txtGui.CurrentPos, dataToSend);
                }
                else
                {
                    _tcpClient.GetStream().Write(System.Text.Encoding.ASCII.GetBytes(dataToSend), 0, dataToSend.Length);
                    txtDlGui.Text += dataToSend;
                }
            }
            else
            {
                if (!com.IsOpen)
                {
                    MessageBox.Show("Port is closed, Connect again", "Error", MessageBoxButtons.OK);
                    return;
                }
                var bt = Convert.ToByte(((Button)sender).Tag);
                string dataToSend;
                if (bt == 0)
                {
                    dataToSend = txtGui.Selection.Length > 0 ? txtGui.Selection.Text : txtGui.Text;
                    //Trường hợp gửi dữ liệu thông thường
                    if (chkAstm.Checked)
                    {
                        dataToSend = string.Format("{0}1{1}{2}{3}", STX, dataToSend, CR, ETX);
                        dataToSend = string.Format("{0}{1}{2}", dataToSend, GetCheckSumValue(dataToSend), CRLF);
                    }
                }
                else
                {
                    dataToSend = ((char)bt).ToString(CultureInfo.InvariantCulture);
                }

                if ((bt != 0) && ((ModifierKeys & Keys.Control) == Keys.Control))
                {
                    txtGui.InsertText(txtGui.CurrentPos, dataToSend);
                }
                else
                {
                    com.Write(dataToSend);
                    txtDlGui.Text += dataToSend;
                }
                
            }

            
        }

        private void ChkTopModeCheckedChanged(object sender, EventArgs e)
        {
            TopMost = chkTopMode.Checked;
        }

        private void TxtGuiKeyUp(object sender, KeyEventArgs e)
        {
            var textbox = (Scintilla)sender;
            //Xóa trắng ô dữ liệu
            if ((e.KeyCode == Keys.Delete) && (e.Modifiers == Keys.Control))
                textbox.Text = string.Empty;
            else if ((e.KeyCode == Keys.Enter) && (e.Modifiers == Keys.Control))
            {
                if (textbox.Name == txtGui.Name)
                    btnGui.PerformClick();
            }
            //Lưu file từ textbox
            else if ((e.KeyCode == Keys.S) && (e.Modifiers == Keys.Control))
            {
                var sf = new SaveFileDialog { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*" };
                if (sf.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(sf.FileName, textbox.Text);
            }
            // Mở file text có sẵn
            else if ((e.KeyCode == Keys.O) && (e.Modifiers == Keys.Control))
            {
                var sf = new OpenFileDialog { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*" };
                if (sf.ShowDialog() == DialogResult.OK)
                    textbox.Text = File.ReadAllText(sf.FileName);
            }
        }

        private void CbxEndOfLineCheckChanged(object sender, EventArgs e)
        {
            var currentChecked = (RadioButton)sender;
            switch (currentChecked.Text)
            {
                case "CRLF":
                    txtGui.EndOfLine.Mode = EndOfLineMode.Crlf;
                    break;
                case "CR":
                    txtGui.EndOfLine.Mode = EndOfLineMode.CR;
                    break;
                case "LF":
                    txtGui.EndOfLine.Mode = EndOfLineMode.LF;
                    break;
            }
        }

        private void cbxWordWrapCheckedChanged(object sender, EventArgs e)
        {
            txtGui.LineWrapping.Mode = cbxWordWrap.Checked ? LineWrappingMode.Word : LineWrappingMode.None;
            txtDlGui.LineWrapping.Mode = cbxWordWrap.Checked ? LineWrappingMode.Word : LineWrappingMode.None;
            txtDlNhan.LineWrapping.Mode = cbxWordWrap.Checked ? LineWrappingMode.Word : LineWrappingMode.None;
        }

        private void CbxShowAllCharacterCheckedChanged(object sender, EventArgs e)
        {
            txtGui.EndOfLine.IsVisible = cbxShowAllCharacter.Checked;
            txtDlGui.EndOfLine.IsVisible = cbxShowAllCharacter.Checked;
            txtDlNhan.EndOfLine.IsVisible = cbxShowAllCharacter.Checked;
        }

        private void cbxShowSpace_CheckedChanged(object sender, EventArgs e)
        {
            txtGui.Whitespace.Mode = cbxShowSpace.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
            txtDlGui.Whitespace.Mode = cbxShowSpace.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
            txtDlNhan.Whitespace.Mode = cbxShowSpace.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
        }

        private void btnRefreshPort_Click(object sender, EventArgs e)
        {
            Loadcbocom();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtGui.Text = string.Empty;
            txtDlGui.Text = string.Empty;
            txtDlNhan.Text = string.Empty;
        }

        private void tabTpcIpServer_Enter(object sender, EventArgs e)
        {
            if(_tcpEnterd) return;
            // Lọc về tất cả các IP V4
            IPAddress[] ipv4Addresses = Array.FindAll(
                Dns.GetHostEntry(string.Empty).AddressList,
                a => a.AddressFamily == AddressFamily.InterNetwork);

            cboIpAddress.Items.Clear();
            cboIpAddress.Items.Add("ALL Address");
            foreach (IPAddress ipv4Address in ipv4Addresses)
            {
                cboIpAddress.Items.Add(ipv4Address);
            }
            cboIpAddress.SelectedIndex = 0;
            _tcpEnterd = true;
        }

        #endregion

        #region TCP/IP Event

        public void OnClientConnected(TcpClient client)
        {
            _tcpClient = client;
            string log = client.Client.LocalEndPoint.ToString();
            AppendTcpLog(string.Format("Client connected: {0}",log));
           
        }
        public void OnStartServerFalse(int port, string exceptionString)
        {
            AppendTcpLog(string.Format("Server Start False on port: {0}", port));
        }
        public void OnStartServerSuccess(int port, string exceptionString)
        {
            AppendTcpLog(string.Format("Server Start Success on port: {0}",port));
        }
        public  void OnBeginReciveData(TcpClient client)
        {
            
            
        }

        public  void OnClientDisconnected(TcpClient client)
        {
            AppendTcpLog(string.Format("Client disconnected"));
        }

        public  void OnEndReciveData(TcpClient client)
        {
            _tcpClient = client;

        }
        public void OnIncommingData(DataHeader dataHeader, byte[] data)
        {
            string log = string.Format("Receive Data From:{0}:{1} - Length:{2}", dataHeader.ClientIP,
                                       dataHeader.ClientPort, data.Length);
            AppendTcpLog(log);

            var p = new SettextParametter(txtDlNhan, System.Text.Encoding.ASCII.GetString(data));
            var t = new Thread(ThreadSetText);
            t.Start(p);

        }
        #endregion

        private void btnShowHideTcpLog_Click(object sender, EventArgs e)
        {
            txtTcpLog.Visible = !txtTcpLog.Visible;
        }

        
    }
}