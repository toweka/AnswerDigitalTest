using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AnswerDigitalTest
{
    class Program
    {
        IWebDriver driver = new ChromeDriver();
        string CorrectUsername = "tomsmith";
        string CorrectPassword = "SuperSecretPassword!";
        string IncorrectUsername = "jduis";
        string IncorrectPassword = "njodsviov";

        static void Main(string[] args)
        {

        }

        [SetUp]
        public void Initialize()
        {
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/");
        }

        [Test]
        public void TestCase1Scenario1()
        {
            driver.FindElement(By.LinkText("Form Authentication")).Click();

            driver.FindElement(By.Name("username")).SendKeys(CorrectUsername);

            driver.FindElement(By.Name("password")).SendKeys(IncorrectPassword);

            driver.FindElement(By.ClassName("fa-sign-in")).Click();

            IWebElement FlashMessage = driver.FindElement(By.Id("flash-messages"));

            Assert.IsNotNull(FlashMessage);
        }

        [Test]
        public void TestCase1Scenario2()
        {
            IWebElement form = driver.FindElement(By.LinkText("Form Authentication"));
            form.Click();

            IWebElement username = driver.FindElement(By.Name("username"));
            username.SendKeys(IncorrectUsername);

            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys(CorrectPassword);

            IWebElement login = driver.FindElement(By.ClassName("fa-sign-in"));
            login.Click();

            var FlashMessage = driver.FindElement(By.Id("flash-messages"));

            Assert.IsNotNull(FlashMessage);
        }

        [Test]
        public void TestCase1Scenario3()
        {
            IWebElement form = driver.FindElement(By.LinkText("Form Authentication"));
            form.Click();

            IWebElement username = driver.FindElement(By.Name("username"));
            username.SendKeys(CorrectUsername);

            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys(CorrectPassword);

            IWebElement login = driver.FindElement(By.ClassName("fa-sign-in"));
            login.Click();

            IWebElement logout = driver.FindElement(By.ClassName("icon-signout"));
            logout.Click();

            var FlashMessage = driver.FindElement(By.Id("flash-messages"));

            Assert.IsNotNull(FlashMessage);
        }

        [Test]
        public void TestCase2()
        {
            IWebElement form = driver.FindElement(By.LinkText("Infinite Scroll"));
            form.Click();
            ScrollDown();
            ScrollDown();
            ScrollUp();

            var Title = driver.FindElement(By.XPath("/ html / body / div[2] / div / div / h3")).Displayed;

            Assert.IsTrue(Title);
        }

        public void ScrollDown()
        {
            IJavaScriptExecutor scriptExecutor = driver as IJavaScriptExecutor;
            System.Threading.Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("window.scrollBy(0,1000);");
        }

        public void ScrollUp()
        {
            IJavaScriptExecutor scriptExecutor = driver as IJavaScriptExecutor;
            System.Threading.Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("window.scroll(900,0);");
        }

        [Test]
        public void TestCase3()
        {
            IWebElement form = driver.FindElement(By.LinkText("Key Presses"));
            form.Click();

            for (int i = 0; i < 4; i = i + 1)
            {
                string test = i.ToString();
                var KeyTest = driver.FindElement(By.Id("target"));
                KeyTest.SendKeys(test);
                var enteredTextMessage = driver.FindElement(By.Id("result")).Text;

                Assert.AreEqual("You entered: " + test, enteredTextMessage);
                KeyTest.Clear();     
            }
        }

    }
}
