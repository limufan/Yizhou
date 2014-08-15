using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;

namespace Yizhou.Core
{
    public class OperationEventArgs : EventArgs
    {
        public User Operator { set; get; }
    }
}
