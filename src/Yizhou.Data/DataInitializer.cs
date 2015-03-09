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
        YizhouCoreManager _yizhouManager;
        YizhouDataManager _dataManager;

        public DataInitializer(YizhouCoreManager yizhouManager, YizhouDataManager dataManager)
        {
            this._yizhouManager = yizhouManager;
            this._dataManager = dataManager;

            if (yizhouManager.OrgManager.DepartmentManager.Departments.Count == 0)
            {
                this.InitOrg(yizhouManager, dataManager);
            }

            Position wenyuanPosition = yizhouManager.OrgManager.PositionManager.GetPositionByName("文员");
            if (wenyuanPosition == null)
            {
                Position topPosition = yizhouManager.OrgManager.PositionManager.TopPosition;
                wenyuanPosition = yizhouManager.OrgManager.PositionManager.Create(yizhouManager.OrgManager.System, new PositionCreateInfo { Name = "文员", ParentId = topPosition.ID });
                dataManager.PositionDataProvider.Insert(wenyuanPosition);
                this.CreateUser("heyuchi", "何玉池", wenyuanPosition.ID);
                this.CreateUser("liangminxia", "梁敏霞", wenyuanPosition.ID);
                this.CreateUser("lishuxing", "黎淑兴", wenyuanPosition.ID);
                this.CreateUser("lirong", "李荣", wenyuanPosition.ID);
            }
        }

        private void InitOrg(YizhouCoreManager yizhouManager, YizhouDataManager dataManager)
        {

            Position topPosition = yizhouManager.OrgManager.PositionManager.Create(yizhouManager.OrgManager.System, new PositionCreateInfo { Name = "总经理" });
            dataManager.PositionDataProvider.Insert(topPosition);
            Department topDepartment = yizhouManager.OrgManager.DepartmentManager.Create(yizhouManager.OrgManager.System,
                    new DepartmentCreateInfo
                    {
                        Name = "益州",
                        ManagerPosition = topPosition
                    });
            dataManager.DepartmentDataProvider.Insert(topDepartment);
            User admin = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System, new UserCreateInfo
            {
                Name = "Administrator",
                Account = "admin",
                Password = "123456",
                Role = UserRole.Administrator,
                Status = UserStatus.Normal
            });
            dataManager.UserDataProvider.Insert(admin);

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
            dataManager.UserDataProvider.Insert(lianglin);

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

            User yaohuiqing = yizhouManager.OrgManager.UserManager.Create(yizhouManager.OrgManager.System,
                new UserCreateInfo
                {
                    Account = "yaohuiqing",
                    Name = "姚慧清",
                    MainPositionId = yewuyuanPosition.ID, 
                    Password = "123456", Role = UserRole.User, Status = UserStatus.Normal });
            dataManager.UserDataProvider.Insert(yaohuiqing);

        }

        private void CreateUser(string account, string name, string positionId)
        {
            User yaohuiqing = this._yizhouManager.OrgManager.UserManager.Create(this._yizhouManager.OrgManager.System,
                new UserCreateInfo
                {
                    Account = account,
                    Name = name,
                    MainPositionId = positionId,
                    Password = "123456",
                    Role = UserRole.User,
                    Status = UserStatus.Normal
                });
            this._dataManager.UserDataProvider.Insert(yaohuiqing);
        }
    }
}
