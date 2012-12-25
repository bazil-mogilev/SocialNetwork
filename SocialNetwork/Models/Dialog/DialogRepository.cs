using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SocialNetwork.Models
{
    public class DialogRepository: BaseRepository 
    {
        public void SendMessage(DialogModel newMessage)
        {
            Message msg = new Message();
            msg.SenderAccountID = newMessage.SenderID;
            msg.RecepientAccountID = newMessage.RecepientID;
            msg.Datetime = DateTime.Now;
            msg.Subject = newMessage.Subject;
            msg.MessageText = newMessage.MessageText;

            _db.Messages.AddObject(msg);
            _db.SaveChanges();
        }

        public List<DialogModel> GetMessagesForDialog(int firstUserID, int secondUserID)
        {

            //SELECT     dbo.Message.SenderAccountID AS SenderID, Sender.Username AS SenderUsername, dbo.Message.RecepientAccountID AS RecepientID, 
            //                Recepient.Username AS ReceptionUsername, dbo.Message.Datetime, dbo.Message.Subject, dbo.Message.MessageText
            //FROM         dbo.Message INNER JOIN
            //          dbo.Account AS Sender ON dbo.Message.SenderAccountID = Sender.AccountID INNER JOIN
            //          dbo.Account AS Recepient ON dbo.Message.RecepientAccountID = Recepient.AccountID

             var messagesOutput = (from msg in _db.Messages
                                
                                        join sender in _db.Accounts
                                        on msg.SenderAccountID equals sender.AccountID

                                        join recepient in _db.Accounts
                                        on msg.RecepientAccountID equals recepient.AccountID

                                    where (msg.SenderAccountID == firstUserID && msg.RecepientAccountID == secondUserID)
                                   
                                    select new DialogModel()
                                        {
                                            SenderID = msg.SenderAccountID,
                                            SenderUsername = sender.Username,
                                            RecepientID = msg.RecepientAccountID,
                                            RecepientUsername = recepient.Username,
                                            DateTime = msg.Datetime,
                                            Subject = msg.Subject,
                                            MessageText = msg.MessageText,
                                            IsMyMessage = true}
                                        );

             var messagesInput = (from msg in _db.Messages

                                   join sender in _db.Accounts
                                   on msg.SenderAccountID equals sender.AccountID

                                   join recepient in _db.Accounts
                                   on msg.RecepientAccountID equals recepient.AccountID

                                   where (msg.SenderAccountID == secondUserID && msg.RecepientAccountID == firstUserID)

                                   select new DialogModel()
                                   {
                                       SenderID = msg.SenderAccountID,
                                       SenderUsername = sender.Username,
                                       RecepientID = msg.RecepientAccountID,
                                       RecepientUsername = recepient.Username,
                                       DateTime = msg.Datetime,
                                       Subject = msg.Subject,
                                       MessageText = msg.MessageText,
                                       IsMyMessage = false}
                                 );

            List<DialogModel> allMessage = messagesInput.Union(messagesOutput).ToList();

            allMessage.Sort();

            return allMessage;
        }

        public List<InterlocutorModel> GetInterlocutors(int id)
        {
            var rightFriends = from fr in _db.Friends
                               join acc in _db.Accounts
                               on fr.SecondAccountID equals acc.AccountID
                               where ((fr.FirstAccountID == id) && (fr.IsFriend == true))
                               select new { acc.AccountID, acc.FirstName, acc.LastName };

            var leftFriends = from fr in _db.Friends
                              join acc in _db.Accounts
                              on fr.FirstAccountID equals acc.AccountID
                              where ((fr.SecondAccountID == id) && (fr.IsFriend == true))
                              select new { acc.AccountID, acc.FirstName, acc.LastName};

            var allFriends = rightFriends.Union(leftFriends);

            List<InterlocutorModel> result = new List<InterlocutorModel>();
            foreach (var fr in allFriends)
            {
                result.Add(new InterlocutorModel
                {
                    accountID = fr.AccountID,
                    Name = String.Format("{0} {1}", fr.FirstName, fr.LastName)
                });
            }
            return result;
        }

        public string GetInterlocutorNameByID(int id)
        {
            var account = _db.Accounts.FirstOrDefault(x => x.AccountID ==id);
            
            return (account!=null) ? account.Username : "";
        }


        public List<DialogModel> GetAllMessages()
        {
            List<DialogModel> allMessages = (from msg in _db.Messages

                                  join sender in _db.Accounts
                                  on msg.SenderAccountID equals sender.AccountID

                                  join recepient in _db.Accounts
                                  on msg.RecepientAccountID equals recepient.AccountID

                                  select new DialogModel()
                                  {
                                      MessageID = msg.MessageID,
                                      SenderID = msg.SenderAccountID,
                                      SenderUsername = sender.Username,
                                      RecepientID = msg.RecepientAccountID,
                                      RecepientUsername = recepient.Username,
                                      DateTime = msg.Datetime,
                                      Subject = msg.Subject,
                                      MessageText = msg.MessageText,
                                      IsMyMessage = true
                                  }
                                  ).ToList();

            return allMessages;
        }

        public void RemoveMessageByID(int id)
        {
            Message msg = _db.Messages.SingleOrDefault(x => x.MessageID == id);

            if (msg != null)
            {
                _db.Messages.DeleteObject(msg);
                _db.SaveChanges();
            }
        }

        public DialogModel GetMessageByID(int id)
        {
            return GetAllMessages().FirstOrDefault(x => x.MessageID == id);
        }
    }
}