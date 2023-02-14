using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TatarTelegram.Data
{
    public static class DataAccess
    {
        public delegate void RefreshListDelegate();
        public static event RefreshListDelegate RefreshListEvent;

        public static List<Chatter> GetChatters() => TatarTelegaEntities.GetContext().Chatter.ToList();
        public static List<Company> GetCompanys() => TatarTelegaEntities.GetContext().Company.ToList();

        public static List<Chat> GetChats() => TatarTelegaEntities.GetContext().Chat.ToList();

        public static Chatter GetChatter(string username, string password)
        {
            var encryptedPassword = ComputeStringToSha256Hash(password);
            return TatarTelegaEntities.GetContext().Chatter.FirstOrDefault(x => x.Name == username && x.Password == encryptedPassword);
        }

        internal static void SaveChat(Chat chat)
        {
            if (chat.Id == 0)
                TatarTelegaEntities.GetContext().Chat.Add(chat);

            TatarTelegaEntities.GetContext().SaveChanges();
            RefreshListEvent?.Invoke();
        }

        public static List<Message> GetChatMessages() => TatarTelegaEntities.GetContext().Message.ToList();

        public static void LeaveChat(Chatter Chatter, Chat chat)
        {
            var tempChatter = TatarTelegaEntities.GetContext().Chat_Chatter
                .FirstOrDefault(x => x.Chatter == Chatter.Id && x.Chat == chat.Id);
            if (tempChatter != null)
                TatarTelegaEntities.GetContext().Chat_Chatter.Remove(tempChatter);

            TatarTelegaEntities.GetContext().SaveChanges();
            RefreshListEvent?.Invoke();
        }

        public static List<Message> GetChatMessages(Chat chat)
        {
            return GetChatMessages().FindAll(x => x.Chat == chat.Id);
        }

        public static void SaveChatMessage(Message message)
        {
            if (message.Id == 0)
                TatarTelegaEntities.GetContext().Message.Add(message);

            TatarTelegaEntities.GetContext().SaveChanges();
            RefreshListEvent?.Invoke();
        }


        static string ComputeStringToSha256Hash(string plainText)
        { 
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
    }

    public partial class Company
    {
        public bool IsChecked { get; set; }
    }

    public partial class Chat
    {
        public DateTime LastMessageDate => (DateTime)Message?.Max(x => x?.Date);
    }
}
