using drug_parser.Enities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser.Mappings
{
    class ArticleTextMap : ClassMap<ArticleText>
    {
        public ArticleTextMap()
        {
            Id(x => x.Id);
            Map(x => x.Text);
            References(x => x.Article);
            References(x => x.Drug);
        }
    }
}
