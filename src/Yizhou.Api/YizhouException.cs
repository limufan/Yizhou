using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Yizhou.Api
{
    [Serializable]
    public class YizhouException : ApplicationException
    {
        public YizhouException()
        {
            this.ExceptionMessage = "操作异常!";
        }

        public YizhouException(string message)
        {
            this.ExceptionMessage = message;
        }

        public YizhouException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
            this.ExceptionMessage = info.GetString("ExceptionMessage");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ExceptionMessage", this.ExceptionMessage);
        }

        protected string ExceptionMessage { set; get; }

        public override string Message
        {
            get
            {
                return this.ExceptionMessage;
            }
        }
    }
}
