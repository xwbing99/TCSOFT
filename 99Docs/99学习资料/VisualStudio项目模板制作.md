### 简单模板制作步骤

#### 1. 模板项目下新建文件夹.template.config

#### 2. 该文件夹下创建template.json文件

内容：

``` javascript
{
  //必须 
  "author": "Kyle Wong",
  //必须，这个对应模板的Tags 
  "classifications": [ "Web" ],
  //必须，这个对应模板的Templates 
  "name": "WMSApiTemplate",
  //可选，模板的唯一名称
  "identity": "WMSApiTemplate",
  //必须，这个对应模板的Short Name 
  "shortName": "wmsapi",
  "tags": {
    "language": "C#",
    "type": "project"
  },
  // 可选，要替换的名字
  "sourceName": "WMSApi",
  // 可选，添加目录 
  "preferNameDirectory": true
}
```



#### 3. VS中选中项目，菜单->项目->导出模板

#### 4. 复制生成的*.zip文件到Documents/Visual Studio 201x/Templates/目录下即可

