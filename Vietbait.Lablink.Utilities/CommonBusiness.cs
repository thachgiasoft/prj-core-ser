using System;
using SubSonic;

namespace Vietbait.Lablink.Utilities
{
    public abstract class CommonBusiness
    {
        static CommonBusiness()
        {
            try
            {
                var strConnection = Common.GetDefaultConnectionString();
                DataService.Providers = new DataProviderCollection();
                var myProvider = new CustomSqlProvider(strConnection);
                if (DataService.Providers[myProvider.Name] == null)
                {
                    DataService.Providers.Add(myProvider);
                    DataService.Provider = myProvider;
                }
                else
                {
                    DataService.Provider.DefaultConnectionString = strConnection;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}