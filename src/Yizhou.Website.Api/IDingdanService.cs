using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Website.Api
{
    public interface IDingdanService
    {
        void Create(DingdanDetailsModel createModel);

        void Change(DingdanDetailsModel changeModel);

        void Delete(string dingdanId);

        DingdanDetailsModel GetDingdan(string dingdanId);

        List<DingdanGridModel> GetDingdan(DingdanFilterModel model, out int totalCount);

        DingdanListModel GetDingdan(DingdanFilterModel model);

        DingdanMingxiListModel GetDingdanMingxi(DingdanMingxiFilterModel model);

        DingdanMingxiDetailsModel JisuanMingxi(DingdanMingxiDetailsModel model);

        double JisuanTicheng(DingdanDetailsModel dingdanModel, ShoukuanDetailsModel model);
    }
}
