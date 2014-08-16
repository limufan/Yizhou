using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Website.Api
{
    public interface IChanpinService
    {
        void Create(ChanpinDetailsModel createModel);

        void Change(ChanpinDetailsModel changeModel);

        void Delete(string kehuId);

        ChanpinDetailsModel GetChanpin(string kehuId);

        List<ChanpinGridModel> GetChanpin(ChanpinFilterModel model, out int totalCount);
    }
}
