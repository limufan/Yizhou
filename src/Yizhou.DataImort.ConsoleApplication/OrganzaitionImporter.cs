using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Api.Organization;

namespace Yizhou.DataImort.ConsoleApplication
{
    public class OrganzaitionImporter
    {
        IUserService _userService;
        public OrganzaitionImporter()
        {
            this._userService = (IUserService)Activator.GetObject(typeof(IUserService),
                this.GenerateUrl("127.0.0.1:6301", "UserService"));
        }

        protected string GenerateUrl(string server, string serviceUri)
        {
            return string.Format("tcp://{0}/{1}", server, serviceUri);
        }

        public void Import()
        {
            UserInfo userInfo = this._userService.GetUserByAccount("yujiacheng");
            this._userService.Create("system", new UserCreateInfo { Account = "yaohuiqing", Name = "姚慧清", MainPositionId = userInfo.MainPositionId, Password = "123456", Role = UserRole.User, Status = UserStatus.Normal });
        }
    }
}
