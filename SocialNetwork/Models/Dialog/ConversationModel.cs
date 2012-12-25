using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace SocialNetwork.Models
{
    [Serializable]
    public class ConversationModel
    {
        public int CurrentUserID { get; set; }
        public int InterlocutorID { get; set; }
        public string InterlocutorUsername { get; set; }
        
        public List<DialogModel> Dialogs { get; set; }
        
        public MessageModel NewMessage { get; set; }
        
        public List<InterlocutorModel> Interlocutors { get; set; }

        public ConversationModel()
        {
            InterlocutorUsername = "";
            NewMessage = new MessageModel();
        }
    }
}