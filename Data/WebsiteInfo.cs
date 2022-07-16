using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AngleSharp;
using AngleSharp.Dom;

namespace Hranilka.Data
{
    public class WebsiteInfo
    {
        public WebsiteInfo(string url)
        {
            this.Url = url;
            GetWebsiteInfo();
        }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        private IDocument Document { get; set; }

        private async void GetHtmlSourse()
        {
            try
            {
                string sourse;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.UserAgent = "Hranilka";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default, true, 8192))
                {
                    sourse = reader.ReadToEnd();
                }

                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                this.Document = await context.OpenAsync(req => req.Content(sourse));
            }
            catch
            {
                MessageBox.Show("Некорректная ссылка");
            }
        }

        private void GetWebsiteInfo()
        {
            GetHtmlSourse();
            try
            {

                this.Title = Document.QuerySelector("title").TextContent;

                bool isUrlYoutube = this.Url.Contains("youtube");

                if (isUrlYoutube)
                {

                    // Тег link с именем автора канала  расположен 23-м по счету на HTML страницы youtube
                    try
                    {
                        this.Author = Document.GetElementsByTagName("link")[23].OuterHtml.ToString();
                        Author = Author.Split(new char[] { '"' })[3];

                        this.Description = Document.GetElementsByTagName("meta")[4].GetAttribute("Content");
                    }
                    catch
                    {

                        throw;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Некорректная ссылка");

            }
        }
    }
}
