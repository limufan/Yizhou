using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Api.Organization;
using Yizhou.Core;
using Yizhou.Core.Organization;

namespace Yizhou.Data
{
    public class DataInitializer
    {
        public DataInitializer(YizhouCoreManager yizhouManager, YizhouDataManager dataManager)
        {
            Position topPosition = yizhouManager.OrgManager.PositionManager.Create(yizhouManager.OrgManager.System, new PositionCreateInfo { Name = "益州" });
            dataManager.PositionDataProvider.Insert(topPosition);
            Department topDepartment = yizhouManager.OrgManager.DepartmentManager.Create(yizhouManager.OrgManager.System,
                    new DepartmentCreateInfo
                    {
                        Name = "总经理",
                        ManagerPosition = topPosition
                    });
            dataManager.DepartmentDataProvider.Insert(topDepartment);
            yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "Administrator",
                Account = "admin",
                Password = "123456",
                Role = UserRole.Administrator,
                Status = UserStatus.Normal
            });

            Position yewuyuanPosition = yizhouManager.OrgManager.PositionManager.Create(yizhouManager.OrgManager.System, new PositionCreateInfo { Name = "业务员", ParentId = topPosition.ID });
            dataManager.PositionDataProvider.Insert(yewuyuanPosition);

            User mengdong = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "刘燕",
                Account = "liuyan",
                Password = "123456",
                Status = UserStatus.Normal,
                MainPositionId = yewuyuanPosition.ID
            });
            dataManager.UserDataProvider.Insert(mengdong);

            User luohuaili = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "李世全",
                Account = "lishiquan",
                Password = "123456",
                Status = UserStatus.Normal,
                MainPositionId = yewuyuanPosition.ID
            });
            dataManager.UserDataProvider.Insert(luohuaili);

            User lianglin = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "余佳承",
                Account = "yujiacheng",
                Password = "123456",
                Status = UserStatus.Normal,
                MainPositionId = yewuyuanPosition.ID
            });

            User yangke = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "杨科",
                Account = "yangke",
                Password = "123456",
                Status = UserStatus.Normal,
                MainPositionId = yewuyuanPosition.ID
            });
            dataManager.UserDataProvider.Insert(yangke);

            User qudenggui = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "瞿灯桂",
                Account = "qudenggui",
                Password = "123456",
                Status = UserStatus.Normal,
                MainPositionId = yewuyuanPosition.ID
            });
            dataManager.UserDataProvider.Insert(qudenggui);
        }
    }
}
