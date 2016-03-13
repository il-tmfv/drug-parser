using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser
{
    public class DbManager
    {
        private ISession _session;
        
        public DbManager()
        {

        }

        public ISession Open()
        {
            _session = CreateSessionFactory().OpenSession();
            return _session;
        }

        public void SaveAndUpdate(object obj)
        {
            _session.SaveOrUpdate(obj);
        }

        private ISessionFactory CreateSessionFactory()
        {
            ISessionFactory sessionFactory = null;
            try
            {
                sessionFactory = Fluently.Configure().
                    Database(SQLiteConfiguration.Standard.UsingFile("drugs.db")).
                    Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>()).
                    ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)).
                    BuildSessionFactory();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return sessionFactory;
        }
    }
}
