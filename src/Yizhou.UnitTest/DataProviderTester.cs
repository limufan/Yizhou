using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Yizhou.Core;
using Yizhou.Data;

namespace Yizhou.UnitTest
{
    public class DataProviderTester
    {
        YizhouCoreManager _coreManager;
        YizhouDataManager _dataManager;
        public DataProviderTester()
        {
            this._coreManager = new YizhouCoreManager();
            this._dataManager = new YizhouDataManager(this._coreManager);
        }

        [Test]
        public void KehuDataProviderTest()
        {
            KehuDataProvider dataProvider = this._dataManager.KehuDataProvider;
            Kehu kehu = new Kehu();
            TestHelper.FillTestData(kehu);
            kehu.Yewuyuan = this._coreManager.OrgManager.System;
            kehu.CreateTime = DateTime.Now;
            dataProvider.Insert(kehu);
            dataProvider.Load();
            Kehu reloadKehu = this._coreManager.KehuManager.GetKehuById(kehu.Id);
            TestHelper.AreEqual(kehu, reloadKehu);

            kehu.Beizhu = Guid.NewGuid().ToString();
            kehu.Changed();
            dataProvider.Update(kehu);
            this._coreManager.KehuManager.Remove(reloadKehu);
            dataProvider.Load();
            reloadKehu = this._coreManager.KehuManager.GetKehuById(kehu.Id);
            TestHelper.AreEqual(kehu, reloadKehu);

            dataProvider.Delete(reloadKehu);
            this._coreManager.KehuManager.Remove(reloadKehu);
            dataProvider.Load();
            reloadKehu = this._coreManager.KehuManager.GetKehuById(kehu.Id);
            Assert.IsNull(reloadKehu);
        }

        [Test]
        public void ChanpinDataProviderTest()
        {
            ChanpinDataProvider dataProvider = this._dataManager.ChanpinDataProvider;
            Chanpin chanpin = new Chanpin();
            TestHelper.FillTestData(chanpin);
            dataProvider.Insert(chanpin);
            dataProvider.Load();
            Chanpin reloadChanpin = this._coreManager.ChanpinManager.GetChanpinById(chanpin.Id);
            TestHelper.AreEqual(chanpin, reloadChanpin);

            chanpin.Beizhu = Guid.NewGuid().ToString();
            chanpin.Changed();
            dataProvider.Update(chanpin);
            this._coreManager.ChanpinManager.Remove(reloadChanpin);
            dataProvider.Load();
            reloadChanpin = this._coreManager.ChanpinManager.GetChanpinById(chanpin.Id);
            TestHelper.AreEqual(chanpin, reloadChanpin);

            dataProvider.Delete(reloadChanpin);
            this._coreManager.ChanpinManager.Remove(reloadChanpin);
            dataProvider.Load();
            reloadChanpin = this._coreManager.ChanpinManager.GetChanpinById(chanpin.Id);
            Assert.IsNull(reloadChanpin);
        }

        [Test]
        public void DingdanDataProviderTest()
        {
            KehuDataProvider kehuDataProvider = this._dataManager.KehuDataProvider;
            Kehu kehu = new Kehu();
            TestHelper.FillTestData(kehu);
            kehu.Yewuyuan = this._coreManager.OrgManager.System;
            kehuDataProvider.Insert(kehu);
            this._coreManager.KehuManager.Add(kehu);

            DingdanDataProvider dataProvider = this._dataManager.DingdanDataProvider;
            DingdanCreateInfo createInfo = new DingdanCreateInfo();
            createInfo.Yewuyuan = this._coreManager.OrgManager.System;
            createInfo.Kehu = kehu;
            TestHelper.FillTestData(createInfo);
            Dingdan dingdan = new Dingdan(createInfo);
            dataProvider.Insert(dingdan);
            dataProvider.Load();
            Dingdan reloadDingdan = this._coreManager.DingdanManager.GetDingdanById(dingdan.Id);
            TestHelper.AreEqual(dingdan, reloadDingdan);

            DingdanChangeInfo changeInfo = new DingdanChangeInfo(dingdan);
            changeInfo.Beizhu = Guid.NewGuid().ToString();
            dingdan.Change(changeInfo);
            dataProvider.Update(dingdan);
            this._coreManager.DingdanManager.Remove(reloadDingdan);
            dataProvider.Load();
            reloadDingdan = this._coreManager.DingdanManager.GetDingdanById(dingdan.Id);
            TestHelper.AreEqual(dingdan, reloadDingdan);

            dataProvider.Delete(reloadDingdan);
            this._coreManager.DingdanManager.Remove(reloadDingdan);
            dataProvider.Load();
            reloadDingdan = this._coreManager.DingdanManager.GetDingdanById(dingdan.Id);
            Assert.IsNull(reloadDingdan);
        }
    }
}
