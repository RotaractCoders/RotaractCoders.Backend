using AngleSharp.Parser.Html;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://www.omirbrasil.org.br/");
                driver.ExecuteScript("TrocaInclude('Sistema_OmirBrasil');");

                var paginaDistrito = new HtmlParser().Parse(driver.PageSource);

                var listaDistritosHtml = paginaDistrito.QuerySelectorAll("#accordion tr")
                    .Where(x => x.OuterHtml.Contains("javascript:AbreFichaDistrito"))
                    .ToList();

                var distritos = new List<Distrito>();

                listaDistritosHtml.ForEach(distritoHtml =>
                {
                    var distrito = new Distrito();

                    var numero = distritoHtml.QuerySelectorAll("strong").FirstOrDefault(x => x.InnerHtml.Contains("D."));
                    var site = distritoHtml.QuerySelectorAll("strong").FirstOrDefault(x => x.InnerHtml.Contains("www"));
                    var email = distritoHtml.QuerySelectorAll("strong").FirstOrDefault(x => x.InnerHtml.Contains("@"));

                    if (numero != null)
                    {
                        distrito.Numero = numero.TextContent.Trim().Replace("D.", string.Empty);
                    }

                    if (site != null)
                    {
                        distrito.Site = site.TextContent.Trim().ToLower();
                    }

                    if (email != null)
                    {
                        distrito.Emails = email.TextContent.Trim().ToLower().Split('/').Select(x => x.Trim()).ToList();
                    }

                    distritos.Add(distrito);
                });

                driver.Close();
                driver.Dispose();
            }
        }
    }
}
