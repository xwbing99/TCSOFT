## 使用方式

### 获取Consul配置

``` C#
IConfiguration configuration = TCSOFT.ConfigManager.ConsulConfigurationExtensions.AddConsul(new ConfigurationBuilder(), "http://192.168.0.24:8500", "MQInfo/MQInfo").Build();
```

### 获取Json文件配置

``` C#
IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), "mqconfig.json").Build();
```

