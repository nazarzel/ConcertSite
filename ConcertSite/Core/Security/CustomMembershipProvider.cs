using System;
using System.Collections.Generic;
using System.Web.Security;
using ConcertSite.Models;
using System.Linq;


namespace ConcertSite.Core.Security
{
    //public class User
    //{
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //}
    public class CustomMembershipProvider : MembershipProvider
    {
        private DataBaseContext Db;
        #region Private Fields
        //private static readonly List<User> Users = new List<User> {
        //    new User {UserName="Yves", Password="123456"},
        //    new User {UserName="Jeam", Password="123456"},
        //    new User {UserName="Georges", Password="123456"}
        //};
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override bool ValidateUser(string username, string password)
        {
            //return Users.Exists(m => m.UserName == username && m.Password == password);
            Db = new DataBaseContext();
            User user = Db.Users.SingleOrDefault(u => u.login == username && u.pass == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}