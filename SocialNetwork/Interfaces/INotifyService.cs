using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Interfaces
{
    public interface INotifyService
    {
        void SendMessage(string to, string subject, string body);
    }
}
