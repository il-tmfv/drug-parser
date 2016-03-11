using drug_parser.Enities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser.Mappings
{
    class DrugMap : ClassMap<Drug>
    {
        public DrugMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            References(x => x.Category);
            HasMany(x => x.ArticleTexts).Inverse().Cascade.All();
        }
    }
}
