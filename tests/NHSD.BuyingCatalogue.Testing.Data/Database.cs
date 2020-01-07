using System.IO;
using System.Threading.Tasks;

namespace NHSD.BuyingCatalogue.Testing.Data
{
    public static class Database
    {
        private static readonly string s_Create = File.ReadAllText(@"SqlResources\Create.sql");
        private static readonly string s_User = File.ReadAllText(@"SqlResources\User.sql");
        private static readonly string s_ReferenceData = File.ReadAllText(@"SqlResources\ReferenceData.sql");
        private static readonly string s_Clear = File.ReadAllText(@"SqlResources\Clear.sql");

        public static async Task CreateAsync()
        {
            await ConnectionAwaiter.AwaitConnectionAsync(ConnectionStrings.Master, 30).ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.Master, $"CREATE DATABASE [{DataConstants.DatabaseName}]").ConfigureAwait(false);
            await BuildDatabaseAsync().ConfigureAwait(false);
        }

        public static async Task AwaitDatabaseAsync()
        {
            await ConnectionAwaiter.AwaitConnectionAsync(ConnectionStrings.Master).ConfigureAwait(false);
        }

        private static async Task BuildDatabaseAsync()
        {
            await ConnectionAwaiter.AwaitConnectionAsync(ConnectionStrings.GPitFuturesSetup, 30).ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, s_User).ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, s_Create).ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, s_ReferenceData).ConfigureAwait(false);
        }

        public static async Task ClearAsync()
        {
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, s_Clear).ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, "ALTER SERVER ROLE sysadmin ADD MEMBER [NHSD];").ConfigureAwait(false);
        }

        public static async Task DropServerRoleAsync()
        {
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, "ALTER SERVER ROLE sysadmin DROP MEMBER [NHSD];").ConfigureAwait(false);
        }

        public static async Task DropAsync()
        {
            await DropUserAsync().ConfigureAwait(false);
            await DropDatabaseAsync().ConfigureAwait(false);
        }

        private static async Task DropUserAsync()
        {
            await SqlRunner.ExecuteAsync(ConnectionStrings.GPitFuturesSetup, "DROP USER NHSD").ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.Master, "DROP LOGIN NHSD").ConfigureAwait(false);
        }

        private static async Task DropDatabaseAsync()
        {
            await SqlRunner.ExecuteAsync(ConnectionStrings.Master, $"ALTER DATABASE [{DataConstants.DatabaseName}]  SET SINGLE_USER WITH ROLLBACK IMMEDIATE").ConfigureAwait(false);
            await SqlRunner.ExecuteAsync(ConnectionStrings.Master, $"DROP DATABASE [{DataConstants.DatabaseName}]").ConfigureAwait(false);
        }
    }
}
