using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser.Enities
{
    public class ArticleText
    {
        public virtual int Id { get; protected set; }
        public virtual string Text { get; set; }
        public virtual string Title { get; set; }
        public virtual Drug Drug { get; set; }
    }
}
