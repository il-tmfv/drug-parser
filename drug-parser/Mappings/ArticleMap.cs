using drug_parser.Enities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser.Mappings
{
    class ArticleMap : ClassMap<Article>
    {
        public ArticleMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            HasMany(x => x.ArticleTexts).Inverse().Cascade.All();
        }
    }
}
