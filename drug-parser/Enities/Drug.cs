using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser.Enities
{
    public class Drug
    {
        public virtual int Id { get; protected set; }
        public virtual string Title { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<ArticleText> ArticleTexts { get; set; }

        public virtual void AddArticleText(ArticleText articleText)
        {
            this.ArticleTexts.Add(articleText);
            articleText.Drug = this;
        }

        public Drug()
        {
            ArticleTexts = new List<ArticleText>();
        }
    }
}
