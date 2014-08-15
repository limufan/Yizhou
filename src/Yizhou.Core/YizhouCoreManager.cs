using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Yizhou.Core.Organization;

namespace Yizhou.Core
{
    public class YizhouCoreManager
    {
        public YizhouCoreManager()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.Logger = log4net.LogManager.GetLogger("logger");
            this.OrgManager = new OrganizationManagement();
            this.KehuManager = new KehuManager();
        }

        ILog _logger;
        public ILog Logger
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _logger = value;
            }
            get
            {
                return _logger;
            }
        }

        public OrganizationManagement OrgManager { set; get; }

        public KehuManager KehuManager { set; get; }
    }
}
