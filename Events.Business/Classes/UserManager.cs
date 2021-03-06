﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Events.Business;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.Business.Classes
{
    public class UserManager : BaseManager
    {
        IUserDataProvider userDataProvider;

        SimpleCrypto.PBKDF2 crypto = new SimpleCrypto.PBKDF2();

        protected override string Name { get; set; }

        public UserManager(IUserDataProvider userDataProvider, ICacheManager cacheManager)
            : base(cacheManager)
        {
            Name = "Users";
            this.userDataProvider = userDataProvider;
        }

        public IList<User> GetAllUsers()
        {
            return FromCache<IList<User>>("AllUsersList",
                    () =>
                    {
                        return userDataProvider.GetAllUsers();
                    });
        }

        public User GetById(string id)
        {
            return CacheManager.FromCache<User>("id:" + id,
                 () =>
                 {
                     return userDataProvider.GetById(id);
                 });
        }

        public User GetByEmail(string mail)
        {
            return CacheManager.FromCache<User>("email:" + mail,
                 () =>
                 {
                     return userDataProvider.GetByMail(mail);
                 });
        }

        private void CreateUser(User user)
        {
            userDataProvider.CreateUser(user);
            ClearCache();
        }

        public void UpdateUser(User user)
        {
            userDataProvider.UpdateUser(user);
            ClearCache();
        }

        public void ChangePassword(User user, string password)
        {
            var encrPass = crypto.Compute(password);
            user.Password = encrPass;
            user.PasswordSalt = crypto.Salt;
            UpdateUser(user);
        }

        public void RegisterUser(User user)
        {
            var encrPass = crypto.Compute(user.Password);

            user.Password = encrPass;
            user.PasswordSalt = crypto.Salt;
            user.IsActive = false;

            CreateUser(user);
        }

        public void GenerateNewPassword(User user)
        {
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);

            var encrPass = crypto.Compute(newPassword);

            user.Password = encrPass;
            user.PasswordSalt = crypto.Salt;

            UpdateUser(user);

            SendEmailWithNewPassword(user, newPassword);
        }

        private void SendEmailWithNewPassword(User user, string newPassword)
        {
            string body = user.Name + ", ваш пароль: \n" + newPassword;
            string subject = "Новый пароль";

            EmailSender(user.Email, body, subject);
        }

        public bool ValidatePassword(string email, string password)
        {
            User user = GetByEmail(email);
            if (user != null)
            {
                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                {
                    return true;
                }
            }
            return false;
        }

        public void EmailSender(string userEmail, string body, string subject)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("team2project222@gmail.com");
            msg.To.Add(userEmail);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            msg.AlternateViews.Add(alternate);

            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();

            try
            {
                client.Send(msg);
            }
            catch
            {
            }
        }
    }
}
