using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Components
{
    public class TextFileLog: INotifyService
    {
        const string DEFAULT_PATH = "C:\\TEMP\\snlog.txt";
        private string _path;

        public TextFileLog(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                _path = path;
            }
            else
            {
                _path = DEFAULT_PATH;
            }

        }

        public void SendMessage(string to, string subject, string body)
        {
            string tempFile = _path;
            string tempLogMessage = string.Format("to: {0}\r\nsubject: {1}\r\nmessage: {2}\r\n\r\n", to, subject, body);
            System.IO.File.AppendAllText(tempFile, tempLogMessage);
        }
    }
}