using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser
{
    class Parser
    {
        private string _baseAddress;
        private IConfiguration _config;

        public Parser()
        {
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

        async private Task ParseDrug(string address)
        {
            var document = await BrowsingContext.New(_config).OpenAsync(address);
            var selector = "div.article__item_header";
            var articlesHeaders = document.QuerySelectorAll(selector);
            selector = "div.article__item_text";
            var articlesTexts = document.QuerySelectorAll(selector);

            int length = new int[] {articlesHeaders.Length, articlesTexts.Length}.Min();

            for (int i = 0; i < length; i++)
            {
                if (articlesHeaders[i].HasChildNodes)
                    if (articlesHeaders[i].FirstChild.HasChildNodes)
                        Console.WriteLine("    " + articlesHeaders[i].FirstChild.FirstChild.NodeValue);
                if (articlesTexts[i].HasChildNodes)
                    Console.WriteLine("    " + articlesTexts[i].InnerHtml);
            }
        }

        async private Task ParseCategory(string categoryLink)
        {
            var links = await GetLinksByClass(categoryLink, "entry__link");

            foreach (var link in links)
            {
                Console.WriteLine("  " + link.FirstChild.NodeValue);
                await ParseDrug(_baseAddress + link.GetAttribute("href"));
            }
        }

        async public Task Parse()
        {
            var links = await GetLinksByClass(_baseAddress + "/drug/", "catalog__item");

            foreach (var link in links)
            {
                Console.WriteLine(link.FirstChild.FirstChild.NodeValue);
                await ParseCategory(_baseAddress + link.GetAttribute("href"));
            }
        }
    }
}
