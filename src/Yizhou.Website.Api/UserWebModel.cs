using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class UserInputModel
    {
        public UserInputModel()
        {

        }

        public UserInputModel(User user)
        {
            this.name = user.Name;
            this.account = user.Account;
        }

        public string name;

        public string account;
    }

    [Serializable]
    public class UserComplexSelectSourceModel
    {
        public string name;

        public string account;
    }

    public class UserDetailsModel
    {
        public UserDetailsModel()
        { 
        }

        public UserDetailsModel(User user)
        {
            this.id = user.ID;
            this.account = user.Account;
            this.name = user.Name;
            this.email = user.Email;
        }

        public string id;
        public string account;
        public string name;
        public string email;
    }
}
