using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using drug_parser.Enities;
using NHibernate;

namespace drug_parser
{
    class Parser
    {
        private string _baseAddress;
        private IConfiguration _config;
        private DbManager _db;
        private ISession _session;
        
        public Parser(DbManager db)
        {
            _db = db;
            _session = _db.Open();
            _baseAddress = "https://health.mail.ru";
            _config = Configuration.Default.WithDefaultLoader();
        }

        async private Task<IEnumerable<AngleSharp.Dom.IElement>> GetLinksByClass(string address, string aClass)
        {
            var document = await BrowsingContext.New(_config).OpenAsync(address);
            var selector = "a." + aClass;
            var links = document.QuerySelectorAll(selector).Where(m => m.GetAttribute("href").Contains("/drug/"));
            return links;
        }

        async private Task ParseDrug(string address, Drug drug)
        {
            var document = await BrowsingContext.New(_config).OpenAsync(address);
            var selector = "div.article__item_header";
            var articlesHeaders = document.QuerySelectorAll(selector);
            selector = "div.article__item_text";
            var articlesTexts = document.QuerySelectorAll(selector);

            int length = new int[] {articlesHeaders.Length, articlesTexts.Length}.Min();

            ArticleText articleText = null;

            for (int i = 0; i < length; i++)
            {
                articleText = new ArticleText();
                if (articlesHeaders[i].HasChildNodes)
                    if (articlesHeaders[i].FirstChild.HasChildNodes)
                    {
                        Console.WriteLine("    " + articlesHeaders[i].FirstChild.FirstChild.NodeValue);
                        articleText.Title = articlesHeaders[i].FirstChild.FirstChild.NodeValue;
                    }

                if (articlesTexts[i].HasChildNodes)
                {
                    Console.WriteLine("    " + articlesTexts[i].InnerHtml);
                    articleText.Text = articlesTexts[i].InnerHtml;
                }

                drug.AddArticleText(articleText);
            }
        }

        async private Task ParseCategory(string categoryLink, Category category)
        {
            var links = await GetLinksByClass(categoryLink, "entry__link");
            Drug drug = null;
            foreach (var link in links)
            {
                Console.WriteLine("  " + link.FirstChild.NodeValue);
                drug = new Drug() { Title = link.FirstChild.NodeValue };
                await ParseDrug(_baseAddress + link.GetAttribute("href"), drug);
                category.AddDrug(drug);
            }
        }

        async public Task Parse()
        {
            var links = await GetLinksByClass(_baseAddress + "/drug/", "catalog__item");
            Category category = null;
            using (var transaction = _session.BeginTransaction())
            {
                foreach (var link in links)
                {
                    Console.WriteLine(link.FirstChild.FirstChild.NodeValue);
                    category = new Category() { Title = link.FirstChild.FirstChild.NodeValue };
                    await ParseCategory(_baseAddress + link.GetAttribute("href"), category);
                    _session.SaveOrUpdate(category);
                }
                transaction.Commit();
            }
            Console.WriteLine("Всё сохранено в базе!");
        }
    }
}
