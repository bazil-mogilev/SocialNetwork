using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Interfaces;
using SocialNetwork.Components;
using System.Configuration;

namespace SocialNetwork.Models
{
    public class BaseRepository
    {
        protected SocialNetworkDB _db = new SocialNetworkDB();
        protected IEventMessageGenerator _messageGenerator = new EventMessageGenerator();
        protected INotifyService _notifyService;
        
        public BaseRepository()
        {
            var str = ConfigurationManager.AppSettings.Get("notify_service");
            
            if (str=="email")
            {
                    _notifyService = new Email();
            }
            
            if (str=="textfile")
            {

                _notifyService = new TextFileLog(ConfigurationManager.AppSettings.Get("textfile_path"));
            }            
        }
        

        protected string GetEmailByAccountID(int id)
        {
            var result = from acc in _db.Accounts where (acc.AccountID == id) select acc;
            if (result.Count() != 0)
            {
                var dbaccount = result.FirstOrDefault();
                return dbaccount.Email;
            }
            else
            {
                return "";
            }
        }

        protected string GetFirstLastNameByAccountID(int id)
        {
            var result = from acc in _db.Accounts where (acc.AccountID == id) select acc;
            if (result.Count() != 0)
            {
                var dbaccount = result.FirstOrDefault();
                return dbaccount.FirstName + " " + dbaccount.LastName;
            }
            else
            {
                return "";
            }
        }
    }
}