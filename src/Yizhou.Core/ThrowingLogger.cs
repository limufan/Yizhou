using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Spring.Aop;

namespace Yizhou.Core
{
    public class ThrowingLogger : IThrowsAdvice
    {
        YizhouCoreManager _yizhouManager;

        public ThrowingLogger(YizhouCoreManager yizhouManager)
        {
            _yizhouManager = yizhouManager;
        }

        public void AfterThrowing(Exception ex)
        {
            this._yizhouManager.Logger.Error(ex.Message, ex);
        }
    }
}
