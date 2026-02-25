using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace selenium
{
    internal class Program
    {
        static IWebDriver driver = new ChromeDriver();
        static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //плашка с установкой
        static void ClosePop()
        {
            try
            {
                var closeButton = driver.FindElement(By.CssSelector("span[aria-label='Закрыть']"));
                if (closeButton != null && closeButton.Displayed)
                {
                    closeButton.Click();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex) { }

        }

        static bool IsServiceLink(string text, string href)
        {
            // служебные ссылки
            string[] serviceWords = {
                "войти", "вход", "регистрация", "скачать", "установите",
                "браузер", "алиса", "картинки", "видео", "карты", "товары",
                "финансы", "квартиры", "переводчик", "ещё", "все сервисы",
                "почта", "маркет", "войти", "загрузки", "приложение"
            };

            string lowerText = text.ToLower();
            string lowerHref = href.ToLower();

            foreach (string word in serviceWords)
            {
                if (lowerText.Contains(word) || lowerHref.Contains(word))
                    return true;
            }

            if (lowerHref.Contains("passport.yandex") ||
                lowerHref.Contains("yabs.yandex") ||
                lowerHref.Contains("yandex.ru/count") ||
                lowerHref.Contains("yandex.ru/search?text=") && lowerText.Length < 10 ||
                lowerHref.Contains("/campaign/") ||
                lowerHref.Contains("from=tabbar") ||
                lowerHref.Contains("source=tabbar"))
            {
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            try
            {
                driver.Navigate().GoToUrl("https://yandex.ru");

                ClosePop();

                wait.Until(d => d.FindElement(By.CssSelector("iframe.dzen-search-arrow-common__frame")));
                ClosePop();

                var searchFrame = driver.FindElement(By.CssSelector("iframe.dzen-search-arrow-common__frame"));
                driver.SwitchTo().Frame(searchFrame);

                var searchInput = wait.Until(d =>
                    d.FindElement(By.CssSelector("input.arrow__input.mini-suggest__input[name='text']"))
                );

                searchInput.Clear();
                searchInput.SendKeys("музыка");

                int originalTabsCount = driver.WindowHandles.Count;

                try
                {
                    var searchButton = driver.FindElement(By.CssSelector("button.arrow__button[type='submit']"));
                    searchButton.Click();
                }
                catch
                {
                    searchInput.SendKeys(Keys.Enter);
                }

                driver.SwitchTo().DefaultContent();
                ClosePop();

                wait.Until(d => d.WindowHandles.Count > originalTabsCount);

                string newTab = driver.WindowHandles.Last();
                driver.SwitchTo().Window(newTab);

                wait.Until(d => d.Url.Contains("yandex.ru/search") || d.Url.Contains("search"));

                Thread.Sleep(3000);

                var allLinks = driver.FindElements(By.TagName("a"));

                bool foundYandexMusic = false;
                int resultCount = 0;


                foreach (var link in allLinks)
                {
                    try
                    {
                        string text = link.Text;
                        string href = link.GetAttribute("href") ?? "";

                        if (string.IsNullOrEmpty(text) || text.Length < 3)
                            continue;

                        resultCount++;

                        if (text.Contains("Яндекс Музыка") || text.Contains("Яндекс.Музыка") ||
                            (text.Contains("Музыка") && href.Contains("music.yandex")) ||
                            href.Contains("music.yandex.ru"))
                        {
                            Console.WriteLine($"Яндекс Музыка - {resultCount} ссылка");
                            foundYandexMusic = true;
                            break;
                        }
                    }
                    catch { }
                }

                if (!foundYandexMusic) Console.WriteLine("Не найдено");
            }
            finally
            {  driver.Quit(); }
        }
    }
}
