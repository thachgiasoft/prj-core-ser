using System.Data;
using System.Globalization;
using System.Text;
using System;

namespace Vietbait.Lablink.TestResult
{
    public class Result
    {
        private ResultItemCollection _iTems ;
        public string TestSequence { get; set; }
        public string TestDate { get; set; }
        public string Barcode { get; set; }
        public decimal DeviceId { get; set; }
        public decimal PatientId { get; set; }

        public Result() : this(-1)
        {
        }

        public Result(decimal deviceId)
        {
            _iTems = new ResultItemCollection();
            DeviceId = deviceId;
        }

        public ResultItemCollection Items
        {
            get { return _iTems ?? (_iTems = new ResultItemCollection()); }
            set { _iTems = value; }
        }

        public int Add(ResultItem resultItem)
        {
            return _iTems.Add(resultItem);
        }

        public void RemoveAt(int index)
        {
            _iTems.RemoveAt(index);
        }

        public void Clear()
        {
            _iTems = new ResultItemCollection();
            Barcode = "";
            TestDate = "";
            TestSequence = "";
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("DeviceId:{0}", DeviceId));
            sb.Append('\n');
            sb.Append(string.Format("TestDate:{0}", TestDate));
            sb.Append('\n');
            sb.Append(string.Format("Barcode:{0}", Barcode));
            sb.Append('\n');//TestSequence
            sb.Append(string.Format("TestSequence:{0}", TestSequence));
            sb.Append('\n');
            sb.Append(string.Format("Patient ID:{0}", PatientId));
            sb.Append('\n');
            try
            {
                var tempDt = new DataTable("Result");
                // Khai báo các cột
                var arrColName = new[] { "TestName", "TestValue", "MeasureUnit", "DataSequence", "NormalLevel", "NormalLevelW", "TestTypeID", "TestDataId" };
                var maxLength = new int[arrColName.Length];
                // Thêm các cột vào Data Table
                for (var i = 0; i < arrColName.Length; i++)
                {
                    var colName = arrColName[i];
                    maxLength[i] = colName.Length;
                    if (!tempDt.Columns.Contains(colName))
                        tempDt.Columns.Add(colName);
                }

                foreach (ResultItem resultItem in _iTems)
                {
                    var dr = tempDt.NewRow();
                    dr[arrColName[0]] = resultItem.TestName;
                    if (resultItem.TestName.Length > maxLength[0]) maxLength[0] = resultItem.TestName.Length;
                    dr[arrColName[1]] = resultItem.TestValue;
                    if (resultItem.TestValue.Length > maxLength[1]) maxLength[1] = resultItem.TestValue.Length;
                    dr[arrColName[2]] = resultItem.MeasureUnit;
                    if (resultItem.MeasureUnit.Length > maxLength[2]) maxLength[2] = resultItem.MeasureUnit.Length;
                    dr[arrColName[3]] = resultItem.DataSequence;
                    if (resultItem.DataSequence.ToString(CultureInfo.InvariantCulture).Length > maxLength[3])
                        maxLength[3] = resultItem.DataSequence.ToString(CultureInfo.InvariantCulture).Length;
                    dr[arrColName[4]] = resultItem.NormalLevel;
                    if (resultItem.NormalLevel.Length > maxLength[4]) maxLength[4] = resultItem.NormalLevel.Length;
                    dr[arrColName[5]] = resultItem.NormalLevelW;
                    if (resultItem.NormalLevelW.Length > maxLength[5]) maxLength[5] = resultItem.NormalLevelW.Length;
                    dr[arrColName[6]] = resultItem.TestTypeId;
                    if (resultItem.TestTypeId.ToString(CultureInfo.InvariantCulture).Length > maxLength[6])
                        maxLength[6] = resultItem.TestTypeId.ToString(CultureInfo.InvariantCulture).Length;
                    dr[arrColName[7]] = resultItem.TestDataId;
                    if (resultItem.TestDataId.ToString(CultureInfo.InvariantCulture).Length > maxLength[7])
                        maxLength[7] = resultItem.TestDataId.ToString(CultureInfo.InvariantCulture).Length;
                    tempDt.Rows.Add(dr);
                }

                for (var i = 0; i < tempDt.Columns.Count; i++)
                    sb.Append(tempDt.Columns[i].ColumnName.PadRight(maxLength[i] + 2));
                sb.Append("\n");

                foreach (DataRow dr in tempDt.Rows)
                {
                    for (var i = 0; i < tempDt.Columns.Count; i++)
                    {
                        var s = dr[i].ToString();
                        sb.Append(!string.IsNullOrEmpty(s)
                                      ? s.PadRight(maxLength[i] + 2)
                                      : new string(' ', maxLength[i] + 2));
                    }
                    if(dr!= tempDt.Rows[tempDt.Rows.Count-1]) sb.Append("\n");
                }
            }
            catch (System.Exception ex)
            {
                return "ERROR STRING";
            }
            return sb.ToString();
        }

        public string[] ToStringArray()
        {
            return ToString().Split('\n');
        }
    }
}