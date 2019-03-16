-  配送点叫货状态表，储存下单相关信息

| 字段                | 类型         | 空   | 默认 | 注释                                           |
| :------------------ | :----------- | :--- | ---- | ---------------------------------------------- |
| siteid              | varchar(8)   | 否   |      | 配送点ID                                       |
| ifdeli              | tinyint(1)   | 否   |      | 是否配货                                       |
| groupid             | tinyint(2)   | 否   | 2    | 1为管理员，2为普通用户。此字段保留方便以后扩展 |
| password            | varchar(50)  | 否   |      | 密码                                           |
| cookie_token        | varchar(50)  | 否   |      |                                                |
| cookie_token_expire | int(11)      | 否   |      | 过期时间                                       |
| avatar              | varchar(200) | 是   |      | 头像                                           |
| avatar_small        | varchar(200) | 是   |      | 小头像                                         |
| email               | varchar(50)  | 否   |      | 邮箱                                           |
| name                | varchar(15)  | 是   |      | 昵称                                           |
| reg_time            | int(11)      | 否   | 0    | 注册时间                                       |
| last_login_time     | int(11)      | 否   | 0    | 最后一次登录时间                               |

- 备注：无