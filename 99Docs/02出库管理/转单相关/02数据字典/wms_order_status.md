-  配送点叫货状态表，储存下单相关信息

| 字段          | 类型          | 空   | 默认 | 注释                            |
| :------------ | :------------ | :--- | ---- | ------------------------------- |
| siteid        | varchar(8)    | 否   |      | 配送点ID                        |
| normaltrans   | tinyint(1)    | 否   |      | 1:参与常温转单 0:不参与常温转单 |
| ectrans       | tinyint(1)    | 否   |      | 1:参与电商转单 0:不参与电商转单 |
| askbillno     | varchar(50)   |      |      | 叫货单ID                        |
| asktime       | datetime      |      |      | 叫货提交时间                    |
| asksum        | decimal(12,4) |      |      | 叫货额                          |
| ordertime     | datetime      |      |      | 电商订单提交时间                |
| coldchaintime | datetime      |      |      | 冷链提交时间                    |

- 备注：无

