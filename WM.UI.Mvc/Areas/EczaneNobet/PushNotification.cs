using System.Net;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WM.UI.Mvc.Areas.EczaneNobet
{
    public class PushNotification
    {
        public PushNotification(string Message, string Title, string CihazId)
        {
            try
            {
                var applicationID = "AAAA68QvRzk:APA91bEBFmrtD-gz7KXfoCtvoEfvhhvHVj3UPHvnGCa_WFojZKVaYqGY3Bz2esyYAobmM1ShnQj-HQncCO5z00NNYEPXgnJa3KbXIdXV3LU8xMdlILnYEpfeIpdaUCT0KRT3xFq4d5Ua";
                var senderId = "1012608747321";
                string cihazId = CihazId;

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = cihazId,
                    notification = new
                    {
                        body = Message,
                        title = Title,
                        icon = "ic_stat_ic_notification"
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}