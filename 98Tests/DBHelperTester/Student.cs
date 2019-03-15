using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace DBHelperTester
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("student")]
    public partial class Student
    {
        public Student()
        {


        }

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

    }
}
