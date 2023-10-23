using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Net;
using System.Text;

namespace NunitTestProject
{
    public class Tests
    {
        public static IWebDriver Driver = new ChromeDriver();
        public static string Cpf = string.Empty;
        List<string> listaCPFs = new List<string>();

        [SetUp]
        public void Setup()
        {
            Driver.Manage().Window.Maximize();
            Driver.Url = "https://vestibular.grupounibra.com/TWluaGEgcGlwYQ==/login";
        }

        [Test]
        public void TestGenerateCpfList()
        {
            for (int i = 0; i < 500; i++)
            {
                listaCPFs.Add(SendPostRequest());
            }

            string SendPostRequest()
            {
                CookieContainer cookieContainer = GetCookies();

                string cpfForm = "acao=gerar_cpf&pontuacao=N&cpf_estado=";

                byte[] cpfBytes = Encoding.ASCII.GetBytes(cpfForm);

                try
                {
                    HttpWebRequest requestWeb = (HttpWebRequest)HttpWebRequest.Create("https://www.4devs.com.br/ferramentas_online.php ");
                    requestWeb.Method = "POST";
                    requestWeb.ProtocolVersion = new Version(1, 1);
                    requestWeb.Host = "www.4devs.com.br";
                    requestWeb.KeepAlive = true;
                    requestWeb.ContentLength = 38;
                    requestWeb.Headers.Add("sec-ch-ua", "Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"Opera GX\";v=\"102");
                    requestWeb.Accept = "*/*";
                    requestWeb.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    requestWeb.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    requestWeb.Headers.Add("sec-ch-ua-mobile", "?0");
                    requestWeb.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36 OPR/102.0.0.0");
                    requestWeb.Headers.Add("sec-ch-ua-platform", "Windows");
                    requestWeb.Headers.Add("Origin", "https://www.4devs.com.br");
                    requestWeb.Headers.Add("Sec-Fetch-Site", "same-origin");
                    requestWeb.Headers.Add("Sec-Fetch-Mode", "cors");
                    requestWeb.Headers.Add("Sec-Fetch-Dest", "empty");
                    requestWeb.Referer = "https://www.4devs.com.br/gerador_de_cpf";
                    requestWeb.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                    requestWeb.Headers.Add(HttpRequestHeader.AcceptLanguage, "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                    requestWeb.AutomaticDecompression = DecompressionMethods.GZip;

                    requestWeb.CookieContainer = cookieContainer;

                    using (var stream = requestWeb.GetRequestStream())
                    {
                        stream.Write(cpfBytes, 0, cpfBytes.Length);
                    }

                    WebResponse response = requestWeb.GetResponse();

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("HTTPREQUESTERROR");
                }

                CookieContainer GetCookies()
                {
                    var cookiesFind = Driver.Manage().Cookies.AllCookies;
                    List<OpenQA.Selenium.Cookie> cookies = new List<OpenQA.Selenium.Cookie>();
                    foreach (OpenQA.Selenium.Cookie cookie in cookiesFind)
                    {
                        cookies.Add(cookie);
                    }

                    List<System.Net.Cookie> newCookies = new List<System.Net.Cookie>(cookiesFind.Count);

                    for (int count = 0; count < cookies.Count; count++)
                    {
                        System.Net.Cookie cookie = new System.Net.Cookie();
                        newCookies.Add(cookie);
                        newCookies[count].Name = cookies[count].Name;
                        newCookies[count].Value = cookies[count].Value;
                        newCookies[count].Path = cookies[count].Path;
                        newCookies[count].Domain = cookies[count].Domain;
                    }

                    CookieContainer cookieContainer = new CookieContainer();
                    foreach (var cookie in newCookies)
                    {
                        cookieContainer.Add(cookie);
                    }
                    return cookieContainer;
                }
            }

            Assert.Pass();
        }
    }
}