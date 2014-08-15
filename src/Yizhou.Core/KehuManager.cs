using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Yizhou.Core
{
    public class KehuManager
    {
        protected ReaderWriterLock _lock;
        List<Kehu> _kehuList;

        public KehuManager()
        {
            this._lock = new ReaderWriterLock();
            this._kehuList = new List<Kehu>();
        }

        public void Add(Kehu kehu)
        {
            this._lock.AcquireWriterLock(0);
            try
            {
                this._kehuList.Add(kehu);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public void Remove(Kehu kehu)
        {
            this._lock.AcquireWriterLock(0);
            try
            {
                this._kehuList.Remove(kehu);
            }
            finally
            {
                this._lock.ReleaseWriterLock();
            }
        }

        public Kehu GetKehuById(string kehuId)
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                return this._kehuList.Find(k => k.Id == kehuId);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }

        public List<Kehu> GetKehu(KehuFilter filter)
        {
            this._lock.AcquireReaderLock(0);
            try
            {
                return filter.Filtrate(this._kehuList);
            }
            finally
            {
                this._lock.ReleaseReaderLock();
            }
        }
    }
}
