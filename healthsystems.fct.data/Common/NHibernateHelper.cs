using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace healthsystems.fct.data.Common
{
    public static class NHibernateHelper
    {
        private static readonly string SqliteFileLocation = string.Format("{0}/App_Data/{1}", AppDomain.CurrentDomain.BaseDirectory, "fct.db");

        public enum Dialect
        {
            Sqlite = 1,
            MySql = 2,
            MsSql2005 = 3
        }

        public static ISession CreateSessionFactory(Dialect dialect = Dialect.Sqlite, bool recreate = false)
        {
            switch (dialect)
            {
                case Dialect.MySql:
                    return MySqlSession();
                case Dialect.MsSql2005:
                    return MsSqlSession(recreate);
                default:
                    return SqliteSession(recreate);
            }

        }

        private static ISession MsSqlSession(bool recreate = false)
        {
            var connectionString = 
                @"Data Source=10.10.10.33; Initial Catalog=fct; User ID=sa; Password=143rob143;";

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005
                .ConnectionString(connectionString)
                .ShowSql())

                .Mappings(x =>
                    x.FluentMappings.AddFromAssemblyOf<UserMap>())

                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(true, recreate))

                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }

        private static ISession SqliteSession(bool recreate = false)
        {
            var fileLocation = SqliteFileLocation;

            var sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                .UsingFile(fileLocation))

                .Mappings(x =>
                    x.FluentMappings.AddFromAssemblyOf<UserMap>())

                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(true, recreate))

                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }

        private static ISession MySqlSession()
        {
            string connectionString = @"Server=localhost;Database=test;Uid=root;Pwd=;";

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                .ConnectionString(connectionString))

                .Mappings(x =>
                    x.FluentMappings.AddFromAssemblyOf<UserMap>())

                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(true, false))

                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }

    }
}
