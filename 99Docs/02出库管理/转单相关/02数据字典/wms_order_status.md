-  配送点叫货状态表，储存下单相关信息

| 字段          | 类型          | 空   | 默认 | 注释                    |
| :------------ | :------------ | :--- | ---- | ----------------------- |
| siteid        | varchar(8)    | 否   |      | 配送点ID                |
| ifdeli        | tinyint(1)    | 否   |      | 1:参与配货 0:不参与配货 |
| askbillno     | varchar(50)   |      |      | 叫货单ID                |
| asktime       | datetime      |      |      | 叫货提交时间            |
| asksum        | decimal(12,4) |      |      | 叫货额                  |
| ordertime     | datetime      |      |      | 电商订单最早提交时间    |
| coldchaintime | datetime      |      |      | 冷链最早提交时间        |

- 备注：无

