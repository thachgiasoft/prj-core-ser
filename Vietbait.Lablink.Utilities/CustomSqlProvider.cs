using SubSonic;

namespace Vietbait.Lablink.Utilities
{
    internal class CustomSqlProvider : SqlDataProvider
    {
        public CustomSqlProvider(string connectionString)
        {
            DefaultConnectionString = connectionString;
        }

        public override string Name
        {
            get { return "ORM"; }
        }
    }
}