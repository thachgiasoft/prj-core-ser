using System.ComponentModel;

namespace Vietbait.Lablink.Service.Helper
{
    partial class ServicesHelper
    {
        private const string SERVICENAME = "Vietbait Lablink Service";
        //private const string SERVICENAME = "VietBa Dicom Store";

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "ServicesHelper";
        }

        #endregion
    }
}