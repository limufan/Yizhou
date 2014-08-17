using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Yizhou.Core
{
    public class DingdanManager
    {
        protected ReaderWriterLock _lock;
        List<Dingdan> _dingdanList;

        public DingdanManager()
        {
            this._lock = new ReaderWriterLock();
            this._dingdanList = new List<Dingdan>();
        }

        public string ShengchengDingdanhao()
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                List<Dingdan> dingList = this._dingdanList.Where(d => d.XiadanRiqi.Year == DateTime.Today.Year && d.XiadanRiqi.Month == DateTime.Today.Month).ToList();
                int sn = dingList.Count + 1;
                return DateTime.Today.ToString("yyyyMM") + sn.ToString().PadLeft(3);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public void Add(Dingdan dingdan)
        {
            this._lock.AcquireWriterLock(0);
            try
            {
                this._dingdanList.Add(dingdan);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public void Remove(Dingdan dingdan)
        {
            this._lock.AcquireWriterLock(0);
            try
            {
                this._dingdanList.Remove(dingdan);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public Dingdan GetDingdanById(string dingdanId)
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                return this._dingdanList.Find(k => k.Id == dingdanId);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public List<Dingdan> GetDingdan(DingdanFilter filter)
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                return filter.Filtrate(this._dingdanList);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }
    }
}
