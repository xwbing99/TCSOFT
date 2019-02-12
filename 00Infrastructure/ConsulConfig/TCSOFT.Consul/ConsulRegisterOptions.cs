﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.Consul
{
    public class ApiHealthChecker
    {
        public string Protocol { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string Uri { get; set; }
    }

    public class ConsulRegisterOptions : IOptions<ConsulRegisterOptions>
    {
        public ConsulRegisterOptions Value => this;
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public ApiHealthChecker Checker { get; set; }
        public string Tag { get; set; }
        public string ServerUrl { get; set; }
        public string DataCenter { get; set; }
        public bool ReRegister { get; set; }
    }
}
