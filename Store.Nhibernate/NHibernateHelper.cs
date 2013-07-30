using System.Net.Mime;
using System.Reflection;
using System.Web;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Store.Domain.Model;

namespace Store.Nhibernate
{
    public static class NHibernateHelper
    {
        private static readonly object lockObject = new object();
        private static Configuration configuration;
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Возвращает открытую сессию из текущего контекста
        /// </summary>
        /// <returns>Сессия NHibernate</returns>
        public static ISession GetSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
                CurrentSessionContext.Bind(SessionFactory.OpenSession());

            return SessionFactory.GetCurrentSession();
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (lockObject)
                    {
                        if (sessionFactory == null)
                            sessionFactory = Configuration.BuildSessionFactory();
                    }
                }

                return sessionFactory;
            }
        }

        private static Configuration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    lock (lockObject)
                    {
                        if (configuration == null)
                            configuration = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("Store")))
                            .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Entity>(t => t.IsSubclassOf(typeof(Entity)) && !t.IsAbstract).Override<Account>(map => map.HasMany(x => x.Purchases).Cascade.All())))
                            .ExposeConfiguration(cfg => cfg.SetProperty(Environment.CurrentSessionContextClass, HttpContext.Current == null ? "NHibernate.Context.CallSessionContext" : "NHibernate.Context.WebSessionContext"))
                            .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(false, true, false))
                            .BuildConfiguration();
                    }
                }
                return configuration;
            }
        }
    }
}