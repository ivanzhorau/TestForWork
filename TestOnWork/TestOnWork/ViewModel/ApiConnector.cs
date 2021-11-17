using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using TestOnWork.Model;

namespace TestOnWork.ViewModel
{
    static class ApiConnector
    {
        public static string toJSON(Lot lot)
        {
            return new JavaScriptSerializer().Serialize(lot);
        }

        public static async void doRequest(Lot lot, string method)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/lot");
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Method = method;
                string json = toJSON(lot);
                request.ContentType = "application/json; charset = utf-8";
                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] bytes = encoding.GetBytes(json);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                WebResponse response = await request.GetResponseAsync();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка. Возможно одно из ключевых полей пустое");
            }

        }
        public static Lot[] getList()
        {
            string json = "";
            WebRequest request = WebRequest.Create("https://localhost:5001/api/lot");
            WebResponse response =  request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();

                }
            }
            response.Close();
            Lot[] lotArr = new JavaScriptSerializer().Deserialize<Lot[]>(json);
            return lotArr;
            
        }
    }
}
