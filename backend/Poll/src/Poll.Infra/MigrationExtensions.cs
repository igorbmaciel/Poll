using Microsoft.EntityFrameworkCore;
using Poll.Infra.Context;
using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            Task.Factory.StartNew(() =>
            {
                var context = provider.GetRequiredService<PollContext>();
                context.Database.Migrate();
            });
        }
    }
}
