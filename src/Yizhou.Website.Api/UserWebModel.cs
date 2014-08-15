using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;

namespace Yizhou.Website.Api
{
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
}
