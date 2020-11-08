using System.Collections.Generic;
using System.Reflection;

namespace Tnf.Dapper
{
    public class DapperOptions
    {
        public List<Assembly> MapperAssemblies { get; set; }
        public DapperDbType DbType { get; set; }
        public string DefaultNameOrConnectionString { get; set; }

        public DapperOptions()
        {
            DbType = DapperDbType.SqlServer;
            MapperAssemblies = new List<Assembly>();
        }
    }
}