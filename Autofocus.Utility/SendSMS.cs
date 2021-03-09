using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Autofocus.Utility
{
   public  class SendSMS
    {
        public bool smsSent(string mobileNo, string message)
        {
            #region "sms"
            try
            {

                string Password = "Bingo@5151";
              //  string Msg = "OTP :" + no + ".  Please Use this OTP.This is usable once and expire in 10 minutes"; ;
                string userdetails = "ezacus";
                string OPTINS = "MSGSAY";
                string MobileNumber = mobileNo;
                // string type = "3";
                string template_id = "123";
                string strUrl = "https://bulksms.co/sendmessage.php?&user=" + userdetails + "&password=" + Password + "&mobile=" + MobileNumber + "&message=" + message  + "&sender=" + OPTINS + "&type=" + 3 + "&template_id=" + template_id;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                System.Net.WebRequest request = System.Net.WebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                s.Close();
                readStream.Close();
                response.Close();
                return true;
            }

            catch
            {
                return false;
            }
            #endregion
             
        }
    }
}
