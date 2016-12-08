using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component.Buttlerfly.Logger
{
    public class MInteractionParam
    {
        public MInteractionParam()
        {
        }

        public Dictionary<string, object> DicContext { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Message { get; set; }
        public string Module { get; set; }
        public string ReceiveContent { get; set; }
        public string SendAddress { get; set; }
        public string SendContent { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
