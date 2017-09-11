using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite.Internal;
using Newtonsoft.Json;

namespace CoTech.Bi.Util
{
    public class MailsHelpers
    {
        public static bool MailPassword(string user, string password)
        {
            List<string> list = new List<string>();
            list.Add(user);
            var request = new RequestMail(list.ToArray(),"Asignación de contraseña",password);
            string passwordUrl = string.Format("http://resapi.cotecnologias.com/api/MailBiPass");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Content-Type","application/json");
            string postBody = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = client.PostAsync(passwordUrl, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public class ResponseMail
    {
        public string message;
        public int line;
        public int code;

        public ResponseMail(string message,int line,int code)
        {
            this.message = message;
            this.line = line;
            this.code = code;
        }
    }

    public class RequestMail
    {
        public string[] to;
        public string subject;
        public string password;

        public RequestMail(string[] to,string subject,string password)
        {
            this.to = to;
            this.subject = subject;
            this.password = password;
        }
    }
}