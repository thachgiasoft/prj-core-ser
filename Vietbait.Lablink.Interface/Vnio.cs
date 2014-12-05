using System;
using System.Data;
using System.IO;
using RestSharp;

namespace Vietbait.Lablink.Interface
{
    public class Vnio
    {
        #region Attributes

        private readonly RestClient _client;

        #endregion

        #region Contructor

        /// <summary>
        /// Hàm khởi tạo kết nối tới Web Service của Netnam tại VNIO
        /// </summary>
        /// <param name="url">Link: http://119.17.194.249:8080 </param>
        /// <param name="username">User Name: cls </param>
        /// <param name="password">Password: cls</param>
        public Vnio(string url, string username, string password)
        {
            try
            {
                //Check URL vừa truyền vào:
                if (!url.StartsWith("http://"))
                {
                    url = string.Format("http://{0}", url);
                }

                //url = url.Substring(0, url.IndexOf("8080") + 5) + "spring_security_login";

                //Khởi tạo Rest Client
                //Gán quyền truy cập cho Client
                _client = new RestClient(url)
                              {
                                  Authenticator = new HttpBasicAuthenticator(username, password)
                              };

                //Gán quyền truy cập cho Client


                //Khởi tạo Request dùng để gửi dữ liệu
                //_postRequest = new RestRequest("service/CLS/saveKetQua/{id}",Method.POST);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Trả về tất cả các thông tin của bệnh nhân qua Data Set
        /// </summary>
        /// <param name="id">ID phiếu yêu cầu</param>
        /// <returns>Data Set chứa các thông tin bệnh nhân và thông tin đăng ký dịch vụ</returns>
        public DataSet GetPatientInfomationById(string id)
        {
            try
            {
                //Khởi tạo Request dùng để lấy dữ liệu
                var getRequest = new RestRequest("{id}", Method.GET);
                //  var getRequest = new RestRequest("07110915001", Method.GET);

                //Gán ID truyền vào
                getRequest.AddUrlSegment("id", id);

                //Lấy về nội dung XML dưới dạng String
                RestResponse response = _client.Execute(getRequest);
                string content = response.Content;

                //Sử dụng String Reader để lưu string vào dataset
                var theReader = new StringReader(content);
                var dataSet = new DataSet();
                dataSet.ReadXml(theReader);

                //Trả ra Dataset cần lấy
                return dataSet;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        public static bool SendResultToHis(string id, string body)
        {
            bool ok = false;
            //_postRequest.AddUrlSegment("id", "829");

            ////_postRequest.AddHeader("key","ACCEPT")
            //_postRequest.AddBody(body);
            //_postRequest.AddHeader("content-type", "application/xml");
            //_postRequest.RequestFormat = DataFormat.Xml;

            //RestResponse response = _client.Execute(_postRequest);
            //string content = response.Content;
            //if (content == "true") ok = true;
            return ok;
        }

        #endregion
    }
}