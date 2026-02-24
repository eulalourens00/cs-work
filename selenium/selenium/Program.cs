using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace selenium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("https://yandex.ru");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                try
                {
                    var closeButton = wait.Until(d =>
                        d.FindElement(By.XPath("//button[contains(text(),'Не сейчас')] | //button[contains(text(),'Закрыть')] | //div[contains(@class,'close')]"))
                    );
                    closeButton.Click();
                    Console.WriteLine("Окно 'Сделать Яндекс основным' закрыто");

                    Thread.Sleep(1000);
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Окно 'Сделать Яндекс основным' не появилось, продолжаем тест");
                }

                try
                {
                    driver.FindElement(By.Id("confirmexample")).Click();

                    driver.SwitchTo().Alert().Dismiss();

                    bool result = bool.Parse(driver.FindElement(By.Id("confirmreturn")).Text);
                    string clickedButton = driver.FindElement(By.Id("confirmexplanation")).Text;

                    if (!result && clickedButton.Contains("You clicked Cancel"))
                    {  Console.WriteLine("Alert тест пройден успешно");}
                    else
                    {  Console.WriteLine("Alert тест не пройден"); }
                }
                catch (NoSuchElementException)
                { Console.WriteLine("Элемент confirmexample не найден, пропускаем alert тест"); }

                try
                {
                    IWebElement searchInput = wait.Until(d => d.FindElement(By.Name("text")));
                    searchInput.SendKeys("музыка");

                    IWebElement searchButton = driver.FindElement(By.CssSelector("button[type='submit']"));
                    searchButton.Click();

                    wait.Until(d => d.FindElement(By.CssSelector(".serp-item")));

                    var firstLink = driver.FindElement(By.CssSelector(".serp-item:first-child a"));
                    string linkText = firstLink.Text;

                    if (linkText.Contains("Яндекс Музыка") || linkText.Contains("Music"))
                    {
                        Console.WriteLine("Тест пройден! Яндекс Музыка первая в списке");
                    }
                    else
                    {
                        Console.WriteLine($"Тест не пройден. Первая ссылка: {linkText}");
                    }
                }
                catch (Exception ex)
                { Console.WriteLine($"Ошибка при выполнении поиска: {ex.Message}"); }
            }
            catch (Exception ex)
            {  Console.WriteLine($"Общая ошибка теста: {ex.Message}"); }
            finally
            {
                Thread.Sleep(3000);
                driver.Quit();
            }
        }
    }
}
