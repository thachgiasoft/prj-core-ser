using System;
using System.Collections.Generic;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class H800 : Rs232Devcie
    {
        #region "Tách số trong chuỗi"

        /// <summary>
        /// hàm tách số trong chuỗi
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>số cuối cùng trong chuỗi. Nếu không có thì trả về chuỗi rỗng</returns>
        private string getFloatInString(string input)
        {
            string strTemp = string.Empty;
            input.Trim();
            int offset = -1;
            int length = 0;
            for (int i = 0; i < input.Length; i++)
            {
//nếu char thứ i là number hoặc có dấu . thì gán offset và đếm
                if (char.IsNumber(input[i]) || input[i].ToString() == ".")
                {
                    if (offset == -1)
                    {
                        offset = i;
                    }
                    length = i;
                }
                //nếu không là số thì dừng đếm và cắt chuỗi
                if ((offset != -1 && !char.IsNumber(input[i]) && input[i].ToString() != ".") ||
                    (i == input.Length - 1) && char.IsNumber(input[i]))
                {
                    strTemp = input.Substring(offset, length - offset + 1);
                    offset = -1;
                    length = 0;
                }
            }
            //nếu có dấu . thì cắt bỏ
            if (strTemp.Length > 0 && strTemp.EndsWith("."))
            {
                strTemp.Remove(strTemp.LastIndexOf('.'));
            }
            return strTemp;
        }

        #endregion

        #region "Cắt chuỗi chuyển thành mảng"

        private string[] substringToArray(string input)
        {
            input.Trim(); //cắt khoảng trắng của chuỗi nhập vào
            var lCount = new List<string>(); //khởi tạo 1 thử thể kiểu List<string>
            int pOffset = -1; //Biến này đánh dấu điểm bắt đầu
            int pLength = 0; //Độ dài của chuỗi con
            string strTemp = string.Empty; //Biến lưu chuỗi con đã được cắt

            //chạy vòng lặp
            for (int i = 0; i < input.Length; i++)
            {
                //nếu không phải khoảng trắng hoặc dấu * thì gán vào pOffset
                // và đếm
                if (input[i] != ' ' && input[i] != '*')
                {
                    if (pOffset == -1) //nếu pOffset chưa được gán
                    {
                        pOffset = i;
                    }
                    pLength = i;
                }

                //nếu điểm bắt đầu được đánh dấu và kí tự thứ i là khoảng trắng hoặc nằm ở cuối chuỗi thì cắt
                if ((input[i] == ' ' || i == input.Length - 1) && pOffset != -1)
                {
                    //cắt chuỗi và lưu vào biến
                    strTemp = input.Substring(pOffset, pLength - pOffset + 1);
                    lCount.Add(strTemp); //gán biến vào List 
                    pOffset = -1; //trả pOffset về giá trị ban đầu
                }
            }
            string strSub;
            int subIndex = -1;

            #region "Nếu List có 3 phần tử thì kiểm tra lại"

            //Đoạn này dùng để tách chuỗi VD: 3.4umol/L thành 2 chuỗi mới 3.4 và umol/l rồi gán vào list
            if (lCount.Count == 3)
            {
                //duyệt qua từng phần tử của List

                strSub = lCount[lCount.Count - 1]; //gán phần tử cuối vào biến
                //duyệt qua từng từng char trong phần tử đó
                for (int cnt = 0; cnt < strSub.Length; cnt++)
                {
                    //nếu char thứ cnt là số
                    if (char.IsNumber(strSub[cnt]))
                    {
                        subIndex = cnt; //gán cnt vào subIndex
                    }

                    //nếu char tiếp theo ko là số và subIndex có giá trị
                    if (subIndex != -1 && cnt > subIndex && !char.IsNumber(strSub[cnt]) && strSub[cnt].ToString() != ".")
                    {
                        lCount.RemoveAt(lCount.Count - 1); //xóa phần tử cuối đi
                        subIndex = subIndex + 1;
                        lCount.Add(strSub.Substring(0, strSub.IndexOf(strSub[subIndex])));
                        lCount.Add(strSub.Substring(subIndex));
                        break;
                    }
                }
            }

            #endregion

            var arrResult = new string[4];
            arrResult[0] = lCount[0]; //Lưu Test Name
            arrResult[1] = string.Empty; //Lưu Normal, High .....
            arrResult[2] = string.Empty; //Lưu Result
            arrResult[3] = string.Empty; // Lưu Unit
            if (lCount.Count == 2) //Nếu List chỉ có 2 phần tử
            {
                arrResult[2] = lCount[1]; //gán phần tử cuối của List vào Result
            }
            if (lCount.Count == 4)
            {
                arrResult[0] = lCount[0]; //Tên xét nghiệm
                arrResult[1] = lCount[1]; //Trị số
                arrResult[2] = lCount[2]; //Giá trị
                arrResult[3] = lCount[3]; //Đơn vị
            }
            return arrResult;
        }

        #endregion

        public override void ProcessRawData()
        {
            try
            {
                string[] strResult = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF);
                TestResult.TestDate = strResult[1].Substring(0, 10);
                TestResult.TestDate = string.Format("{0}/{1}/{2}", TestResult.TestDate.Substring(0, 2),
                                                    TestResult.TestDate.Substring(3, 2),
                                                    TestResult.TestDate.Substring(6, 4));
                if (String.IsNullOrEmpty(strResult[3].Substring(strResult[3].LastIndexOf(' ') + 1)))
                {
                    TestResult.Barcode = "0";
                }

                {
                    TestResult.Barcode = getFloatInString(strResult[3]);
                }
                AddResult(new ResultItem(substringToArray(strResult[5])[0], substringToArray(strResult[5])[2],
                                         substringToArray(strResult[5])[3]));
                AddResult(new ResultItem(substringToArray(strResult[6])[0], substringToArray(strResult[6])[2],
                                         substringToArray(strResult[6])[3]));
                AddResult(new ResultItem(substringToArray(strResult[7])[0], substringToArray(strResult[7])[2],
                                         substringToArray(strResult[7])[3]));
                AddResult(new ResultItem(substringToArray(strResult[8])[0], substringToArray(strResult[8])[2],
                                         substringToArray(strResult[8])[3]));
                AddResult(new ResultItem(substringToArray(strResult[9])[0], substringToArray(strResult[9])[2],
                                         substringToArray(strResult[9])[3]));
                AddResult(new ResultItem(substringToArray(strResult[10])[0], substringToArray(strResult[10])[2],
                                         substringToArray(strResult[10])[3]));
                AddResult(new ResultItem(substringToArray(strResult[11])[0], substringToArray(strResult[11])[2],
                                         substringToArray(strResult[11])[3]));
                AddResult(new ResultItem(substringToArray(strResult[12])[0], substringToArray(strResult[12])[2],
                                         substringToArray(strResult[12])[3]));
                AddResult(new ResultItem(substringToArray(strResult[13])[0], substringToArray(strResult[13])[2],
                                         substringToArray(strResult[13])[3]));
                AddResult(new ResultItem(substringToArray(strResult[14])[0], substringToArray(strResult[14])[2],
                                         substringToArray(strResult[14])[3]));
                ImportResults();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ClearData();
            }
        }
    }
}