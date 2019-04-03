using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTransEffect1
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("teacher")]
    public partial class Teacher
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
        public int? Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>          
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>          
        [SugarColumn(ColumnName = "memo1", IsNullable = true)]
        public string Memo1 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>          
        [SugarColumn(ColumnName = "memo2", IsNullable = true)]
        public string Memo2 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>          
        [SugarColumn(ColumnName = "memo3", IsNullable = true)]
        public string Memo3 { get; set; }
    }
}
