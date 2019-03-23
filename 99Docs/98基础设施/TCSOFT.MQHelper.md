## 配置文件说明

``` json
{
  "RabbitMQ": { //消息服务器信息
    "IP": "192.168.0.9",
    "Port": "5672",
    "UserName": "testuser",
    "Password": "123456"
  },
  "MQInfo": { //队列定义信息
    "testsimplequeue": { //队列ID
      "queueName": "testqueue", //队列名
      "durable": true, //是否持久化
      "exclusive": false, //是否排他
      "autoDelete": false, //是否自动删除
      "exchangeName": "", //交换机名
      "routingKey": "testqueue", //路由键
      "typeName": "direct", //类型
      "randomQueue": false //是否生成自动队列名后缀
    }
  }
}
```

## 使用示例

### 发送消息

``` C#
 //By File
MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("testsimplequeue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));

//By Consul
MessageSenderFactory.Instance("http://192.168.0.24:8500", "MQInfo/MQInfo").SendMessageByQueueId("testsimplequeue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
```



### 消费消息

##### 消费处理机

消费对象需实现IMessageConsumer接口中的ConsumeMessage方法

``` C#
using System;

namespace MQConsumerDemo
{
    public class TestMQConsumer : TCSOFT.MQHelper.Consumer.IMessageConsumer
    {
        public bool ConsumeMessage(string messageContent)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + messageContent);
            return true;
        }
    }
}
```

##### 消费者

``` C#
//启动消费者
MessageConsumerFactory.Instance("mqconfig.json", new TestMQConsumer()).StartConsumeByQueueId(queueId);
```

