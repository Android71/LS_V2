using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace EF_Connect
{

    public class LS
    {
        public static string CS = CreateConnectionString();

        public static string CreateConnectionString()
        {
            string hostName;
            string dataSource;

            const string appName = "EntityFramework";
            const string providerName = "System.Data.SqlClient";
            const string metaData = @"res://*/LSModel.csdl|res://*/LSModel.ssdl|res://*/LSModel.msl";
            hostName = Dns.GetHostName();
            if (hostName != "ak")
                dataSource = Dns.GetHostName();
            else
                dataSource = @"AK\SQLEXPRESS";
            const string initialCatalog = "LightSystemV1";

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            sqlBuilder.DataSource = dataSource;
            sqlBuilder.InitialCatalog = initialCatalog;
            sqlBuilder.MultipleActiveResultSets = true;
            sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.ApplicationName = appName;

            EntityConnectionStringBuilder efBuilder = new EntityConnectionStringBuilder();
            efBuilder.Metadata = metaData;
            efBuilder.Provider = providerName;
            efBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;

            return efBuilder.ConnectionString;
        }
    }
}