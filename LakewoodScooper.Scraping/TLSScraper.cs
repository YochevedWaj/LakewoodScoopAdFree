using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LakewoodScooper.Scraping
{
    public class TLSScraper
    {
        public static List<TLSPost> Scrape()
        {
            var html = GetTLSHtml();
            return ParseTLSHtml(html);
        }

        private static List<TLSPost> ParseTLSHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".post");
            var items = new List<TLSPost>();
            foreach (var div in resultDivs)
            {
                var post = new TLSPost();
                var titleSpan = div.QuerySelector("h2");
                if (titleSpan != null)
                {
                    post.Title = titleSpan.TextContent;
                }

                var imageTag = div.QuerySelector(".aligncenter.size-large");
                if (imageTag != null)
                {
                    post.ImageUrl = imageTag.Attributes["src"].Value;
                }

                var linkTag = div.QuerySelector("h2 > a");
                if (linkTag != null)
                {
                    post.Link = linkTag.Attributes["href"].Value;
                }

                var textSpan = div.QuerySelector("p");
                if (textSpan != null)
                {
                    post.Text = textSpan.TextContent;
                }

                var commentsCountSpan = div.QuerySelector(".backtotop > a");
                if (commentsCountSpan != null)
                {
                    post.CommentsCount = commentsCountSpan.TextContent;
                }

                items.Add(post);
            }

            return items;
        }


        private static string GetTLSHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
            using var client = new HttpClient(handler);
            var url = $"https://www.thelakewoodscoop.com";
            var html = client.GetStringAsync(url).Result;
            return html;
        }
    }
}
