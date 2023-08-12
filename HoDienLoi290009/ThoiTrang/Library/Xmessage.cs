using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThoiTrang.Library
{
    public class Xmessage
    {
        public string TypeMsg { get; set; }

        public string Msg { get; set; }
        public Xmessage()
        {

        }

        public Xmessage(string TypeMsg, string Msg)
        {
            this.TypeMsg = TypeMsg;
            this.Msg = Msg;
        }
    }
}