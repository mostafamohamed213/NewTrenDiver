using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    public class DbColumn
    {
        private string _typeName;
        private bool _isNullable;

        public string Name { get; set; }
        public string DbTypeName
        {
            set
            {
                _typeName = value;
            }
        }
        public bool IsNullable
        {
            set
            {
                _isNullable = value;
            }
        }
        public Type ColumnType
        {
            get
            {
                switch (_typeName)
                {
                    case "int":
                        return _isNullable ? typeof(int?) : typeof(int);
                    case "bigint":
                        return _isNullable ? typeof(long?) : typeof(long);
                    case "bit":
                        return _isNullable ? typeof(bool?) : typeof(bool);
                    case "nvarchar":
                    case "varchar":
                    case "nchar":
                    case "char":
                    case "text":
                        return typeof(string);
                    case "uniqueidentifier":
                        return _isNullable ? typeof(Guid?) : typeof(Guid);
                    case "date":
                    case "datetime":
                    case "datetime2":
                        return _isNullable ? typeof(DateTime?) : typeof(DateTime);
                    case "money":
                    case "decimal":
                    case "numeric":
                        return _isNullable ? typeof(decimal?) : typeof(decimal);
                    case "float":
                        return _isNullable ? typeof(float?) : typeof(float);
                    case "timestamp":
                        return _isNullable ? typeof(TimeSpan?) : typeof(TimeSpan);
                    case "binary":
                    case "varbinary":
                    case "image":
                        return typeof(byte[]);
                    case "smallint":
                        return _isNullable ? typeof(short?) : typeof(short);
                    case "tinyint":
                        return _isNullable ? typeof(byte?) : typeof(byte);
                }

                throw new ArgumentException("Unsupported Database Column Type!");
            }
        }

    }
}
