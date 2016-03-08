﻿using AngleSharp;
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

        async private Task<IEnumerable<string>> GetLinksByClass(string address, string aClass)
        {
            var document = await BrowsingContext.New(_config).OpenAsync(address);
            var selector = "a." + aClass;
            var links = document.QuerySelectorAll(selector);
            var filteredLinks = links.Select(m => m.GetAttribute("href")).Where(m => m.Contains("/drug/"));
            return filteredLinks;
        }

        async private Task ParseCategory(string categoryLink)
        {
            var links = await GetLinksByClass(categoryLink, "entry__link");

            foreach (var link in links)
            {
                Console.WriteLine("  " + link);
                //await ParseCategory(_baseAddress + link);
            }
        }

        async public Task Parse()
        {
            var links = await GetLinksByClass(_baseAddress + "/drug/", "catalog__item");

            foreach (var link in links)
            {
                Console.WriteLine(link);
                await ParseCategory(_baseAddress + link);
            }
        }
    }
}