﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;

namespace Yizhou.Core
{
    public delegate void TEventHandler<Sender, Args>(Sender sender, Args args);

    public delegate void TEventHandler<Args>(Args args);
}
