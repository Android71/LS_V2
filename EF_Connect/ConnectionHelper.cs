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
            dataSource = Dns.GetHostName();
            if (hostName == "ak")
                dataSource = @"AK\SQLEXPRESS";
            if (hostName == "LSWB")
                //dataSource = @"LSWB\SQLEXPRESS";
                dataSource = @"(localdb)\MSSQLLocalDB";
            const string initialCatalog = "LightSystemV1";

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            sqlBuilder.DataSource = dataSource;
            //sqlBuilder.InitialCatalog = initialCatalog;
            //sqlBuilder.AttachDBFilename = @"D:\Repos\LS_V2\EFData\RestoreData\LightSystemV1.mdf";
            sqlBuilder.AttachDBFilename = @"D:\Repos\LS_V2\LS_Designer_WPF\SQLDataBase\LightSystemV1.mdf";
            sqlBuilder.InitialCatalog = initialCatalog;  // без этого оператора имя базы - имя файла
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