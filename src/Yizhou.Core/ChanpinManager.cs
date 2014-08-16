using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Yizhou.Core
{
    public class ChanpinManager
    {
        protected ReaderWriterLock _lock;
        List<Chanpin> _chanpinList;

        public ChanpinManager()
        {
            this._lock = new ReaderWriterLock();
            this._chanpinList = new List<Chanpin>();
        }

        public void Add(Chanpin chanpin)
        {
            this._lock.AcquireWriterLock(0);
            try
            {
                this._chanpinList.Add(chanpin);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public void Remove(Chanpin chanpin)
        {
            this._lock.AcquireWriterLock(0);
            try
            {
                this._chanpinList.Remove(chanpin);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public Chanpin GetChanpinById(string chanpinId)
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                return this._chanpinList.Find(k => k.Id == chanpinId);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public List<Chanpin> GetChanpin(ChanpinFilter filter)
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                return filter.Filtrate(this._chanpinList);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }
    }
}
