﻿using System;
using System.Collections.Generic;
using System.Web;

namespace Yizhou.Website.Models
{
    public enum ControllerResult
    {
        Succeed,
        Error
    }

    public class ControllerResultModel
    {
        public ControllerResult result;

        public string message;

        public object data;
    }
}