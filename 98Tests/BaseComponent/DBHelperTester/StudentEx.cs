using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace DBHelperTester
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("studentex")]
    public partial class StudentEx :Student
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>          
        [SugarColumn(ColumnName = "memo4", IsNullable = true)]
        public string Memo4 { get; set; }
    }
}
