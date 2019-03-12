using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.MQHelper
{
    public class QueueServerInfo
    {
        //消息服务器IP
        public string ServerIP { get; set; }
        //服务器端口号
        public int ServerPort { get; set; }
        //用户名
        public string UserName { get; set; }
        //密码
        public string Password { get; set; }
    }
}
