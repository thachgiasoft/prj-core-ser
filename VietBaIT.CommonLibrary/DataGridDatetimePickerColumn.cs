using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace VietBaIT.CommonLibrary
{
    public class CalendarColumn : DataGridViewColumn
    {

        public CalendarColumn()
            : base(new CalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {

                // Ensure that the cell used for the template is a CalendarCell.
                if ((value != null) && !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;

            }
        }

    }

    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
        {
            // Use the short date format.
            this.Style.Format = "dd/MM/yyyy";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {

            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            object OldValue = this.Value;
            CalendarEditingControl ctl = (CalendarEditingControl)DataGridView.EditingControl;

            ctl.Value = (DateTime)OldValue;

        }

        public override Type EditType
        {
            // Return the type of the editing contol that CalendarCell uses.
            get { return typeof(CalendarEditingControl); }
        }

        public override Type ValueType
        {
            // Return the type of the value that CalendarCell contains.
            get { return typeof(DateTime); }
        }

        public override object DefaultNewRowValue
        {
            // Use the current date and time as the default value.
            get { return DateTime.Now; }
        }

    }

    public class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {

        private DataGridView dataGridViewControl;
        private bool valueIsChanged = false;
        private int rowIndexNum;

        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "dd/MM/yyyy";
            this.ShowUpDown = true;
        }

        public object EditingControlFormattedValue
        {

            get { return this.Value.ToShortDateString(); }

            set
            {
                if (value is string)
                {
                    this.Value = DateTime.Parse((string)value);
                }
            }
        }


        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {

            return this.Value.ToString();

        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {

            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;

        }

        public int EditingControlRowIndex
        {

            get { return rowIndexNum; }
            set { rowIndexNum = value; }
        }


        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {

            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:

                    return true;

                default:
                    return !dataGridViewWantsInputKey;
            }

        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {

            // No preparation needs to be done.

        }

        public bool RepositionEditingControlOnValueChange
        {

            get { return false; }
        }


        public DataGridView EditingControlDataGridView
        {

            get { return dataGridViewControl; }
            set { dataGridViewControl = value; }
        }


        public bool EditingControlValueChanged
        {

            get { return valueIsChanged; }
            set { valueIsChanged = value; }
        }


        public Cursor EditingControlCursor
        {

            get { return base.Cursor; }
        }
        Cursor IDataGridViewEditingControl.EditingPanelCursor
        {
            get { return EditingControlCursor; }
        }


        protected override void OnValueChanged(EventArgs eventargs)
        {

            // Notify the DataGridView that the contents of the cell have changed.
            valueIsChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);

        }
    }
}
