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
        public DbManager()
        {

        }

        public ISession Open()
        {
            return CreateSessionFactory().OpenSession();
        }

        private ISessionFactory CreateSessionFactory()
        {
            ISessionFactory session = null;
            try
            {
                session = Fluently.Configure().
                    Database(SQLiteConfiguration.Standard.UsingFile("drugs.db")).
                    Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>()).
                    ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)).
                    BuildSessionFactory();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return session;
        }
    }
}
