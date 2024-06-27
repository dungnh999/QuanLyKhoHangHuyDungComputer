﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [Serializable]
    public partial class Entities
    {
        private Entities(DbConnection connectionString, bool contextOwnsConnection = true) : base(connectionString, contextOwnsConnection) { }

        public static Entities CreateEntities(bool contextOwnsConnection = true)
        {
            // DOC FILE CONNECT
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read);
            connect cp = (connect)bf.Deserialize(fs);

            //Decryptor noi dung
            string servername = Encryptor.Decrypt(cp.servername, "qwertyuiop", true);
            string username = Encryptor.Encrypt(cp.username, "qwertyuiop", true);
            string password = Encryptor.Encrypt(cp.password, "qwertyuiop", true);
            string database = Encryptor.Encrypt(cp.database, "qwertyuiop", true);


            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            SqlConnectionStringBuilder sqlConnectBuilder = new SqlConnectionStringBuilder();

            sqlConnectBuilder.DataSource = servername;
            sqlConnectBuilder.InitialCatalog = database;
            sqlConnectBuilder.UserID = username;
            sqlConnectBuilder.Password = password;

            string sqlConnectString = sqlConnectBuilder.ToString();
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlConnectString;

            entityBuilder.Metadata = @"res://*/khohang.csdl://*/khohang.ssdl|res://*/khohang.msl";
            EntityConnection connection = new EntityConnection(entityBuilder.ConnectionString);

            fs.Close();
            return new Entities(connection);
        }
    }
}
