-  配送点转单状态表，储存转单相关信息

| 字段          | 类型        | 空   | 默认 | 注释                   |
| :------------ | :---------- | :--- | ---- | ---------------------- |
| siteid        | varchar(8)  | 否   |      | 配送点ID               |
| warehouseid   | varchar(12) | 否   |      | 仓库ID                 |
| transed       | tinyint(1)  |      |      | 1:今日已转  0:今日未转 |
| transtime     | datetime    |      |      | 转单时间               |
| cltime        | datetime    |      |      | 拆零完成时间           |
| cztime        | datetime    |      |      | 拆零整件完成时间       |
| zjtime        | datetime    |      |      | 整件完成时间           |
| ectime        | datetime    |      |      | 电商完成时间           |
| coldchaintime | datetime    |      |      | 冷链完成时间           |

- 备注：无

