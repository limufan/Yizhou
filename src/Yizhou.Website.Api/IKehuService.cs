using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Website.Api
{
    public interface IKehuService
    {
        KehuDetailsModel Create(KehuDetailsModel createModel);

        void Change(KehuDetailsModel changeModel);

        void Delete(string kehuId);

        KehuDetailsModel GetKehu(string kehuId);

        List<KehuGridModel> GetKehu(KehuFilterModel model, out int totalCount);
    }
}
