using System;
using System.ComponentModel;
using System.Drawing;

namespace Vietbait.Lablink.Interface
{
    [Serializable]
    public class InterfaceProperties
    {
        #region Properties

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
         Description("Kích thước của barcode"),
         DisplayName("Size")]
        public Size BarcodeSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
         Description("Kích thước font của barcode"),
         DisplayName("Font Size")]
        public int BarcodeFontSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
         Description("BarcodeSymbology"),
         DisplayName("Symbology")]
        public Mabry.Windows.Forms.Barcode.Barcode.BarcodeSymbologies BarcodeSymbology { get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
         Description("BarRatio"),
         DisplayName("BarRatio")]
        public float BarRatio { get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
        Description("BarRatio"),
        DisplayName("CheckSum Style")]
        public Mabry.Windows.Forms.Barcode.Barcode.ChecksumStyles CheckSumStyle { get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
        Description("Sử dụng Barcode 4 số"),
        DisplayName("Use Short Barcode")]
        public bool UseShortBarcode { get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
        Description("Xóa trắng khi chạy chương trình"),
        DisplayName("Barcode Data")]
        public string BarcodeData{ get; set; }

        [Browsable(true), ReadOnly(false), Category("Barcode Config"),
        Description("In barcode kiểu cũ (Lão Khoa)"),
        DisplayName("Use Old Print Method")]
        public bool UseOldPrintMethod { get; set; }

        #endregion

        #region Constructor

        public InterfaceProperties()
        {
            BarcodeSize = new Size(600, 300);
            //BarcodeFont = new Font("Arial", 50, FontStyle.Regular, GraphicsUnit.Point);
            BarcodeFontSize = 50;
            BarcodeSymbology = Mabry.Windows.Forms.Barcode.Barcode.BarcodeSymbologies.Code128;
            BarRatio = 2;
            CheckSumStyle = Mabry.Windows.Forms.Barcode.Barcode.ChecksumStyles.Standard;
            UseShortBarcode = true;
            BarcodeData = "";
            UseOldPrintMethod = false;
        }

        #endregion
    }
}