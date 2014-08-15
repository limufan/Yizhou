using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;
using Yizhou.Data.DataProviders;

namespace Yizhou.Data
{
    public class YizhouDataManager
    {
        public YizhouDataManager(YizhouCoreManager coreManager)
        {
            this.PositionDataProvider = new PositionDataProvider(coreManager.OrgManager);
            this.UserDataProvider = new UserDataProvider(coreManager.OrgManager);
            this.DepartmentDataProvider = new DepartmentDataProvider(coreManager.OrgManager);
            this.FunctionDataProvider = new FunctionDataProvider(coreManager.OrgManager);
            this.GroupDataProvider = new GroupDataProvider(coreManager.OrgManager);
            this.KehuDataProvider = new KehuDataProvider(coreManager);
            this.Load();
        }
        public UserDataProvider UserDataProvider { set; get; }

        public DepartmentDataProvider DepartmentDataProvider { set; get; }

        public FunctionDataProvider FunctionDataProvider { set; get; }

        public PositionDataProvider PositionDataProvider { set; get; }

        public GroupDataProvider GroupDataProvider { set; get; }

        public KehuDataProvider KehuDataProvider { set; get; }

        public void Load()
        {
            this.PositionDataProvider.Load();
            this.UserDataProvider.Load();
            this.PositionDataProvider.LoadUsers();
            this.DepartmentDataProvider.Load();
            this.FunctionDataProvider.Load();
            this.GroupDataProvider.Load();
            this.KehuDataProvider.Load();
        }
    }
}
