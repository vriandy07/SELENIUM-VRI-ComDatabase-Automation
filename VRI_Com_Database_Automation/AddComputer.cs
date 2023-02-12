using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;

using System;

using System.Collections.ObjectModel;

using System.IO;



using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using NUnit.Framework.Internal;
using System.Security.Cryptography;

namespace VRI_Com_Database_Automation
{
    public class AddComputer
    {
        IWebDriver driver;
        [OneTimeSetUp]
        public void Setup()
        {
            

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            

            driver = new ChromeDriver(path + @"\drivers\");
            
            driver.Manage().Window.Maximize();

            
        }

        [Test, Order(1) ]
        public void TC01_VerifyNavigateToAddComputerPage()
        {
            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers");

            By add = By.XPath("//*[@id=\"add\"]");
            driver.FindElement(add).Click();
            Assert.That(driver.PageSource.Contains("Add a computer"), Is.True);


        }


        [Test, Order(2)]

        public void TC02_VerifyComputerNameFieldMandatory()

        {
            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers/new");

            By name = By.Id("name");
            driver.FindElement(name).Clear();
            driver.FindElement(By.XPath("//*[@id=\"main\"]/form/div/input")).Click();

            Assert.That(driver.PageSource.Contains("Failed to refine type : Predicate isEmpty() did not fail."), Is.True);

        }


        [Test, Order(3) ]

        public void TC03_VerifyComputerNameAbleToFilled()

        {
            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers/new");

            By name = By.Id("name");
            driver.FindElement(name).SendKeys("LAPTOP-DA1234B5");

            String expectedname = "LAPTOP-DA1234B5";
            String actualname = driver.FindElement(By.Id("name")).GetAttribute("value");
            Assert.That(actualname, Is.EqualTo(expectedname));

        }


        [Test, Order(4) ]

        public void TC04_VerifyValidIntroducedDateFormat()

        {

            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers/new");

            By introduced = By.Id("introduced");
            driver.FindElement(introduced).Clear();
            driver.FindElement(introduced).SendKeys("2023-01-20");


            String expectedintroduced = "2023-01-20";
            String actualintroduced = driver.FindElement(By.Id("introduced")).GetAttribute("value");
           
            Assert.That(actualintroduced, Is.EqualTo(expectedintroduced));

            

        }



        [Test, Order(5) ]

        public void TC05_VerifyInvalidIntroducedDateFormat()

        {

            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers/new");

            By introduced = By.Id("introduced");
            driver.FindElement(introduced).Clear();
            driver.FindElement(introduced).SendKeys("20-01-2023");

            By create = By.XPath("//*[@id=\"main\"]/form/div/input");
            driver.FindElement(create).Click();

            Assert.That(driver.PageSource.Contains("Failed to decode date : java.time.format.DateTimeParseException: Text '20-01-2023' could not be parsed at index 0"), Is.True);
        }



        [Test, Order(6)]

        public void TC06_VerifySuccessAddComputer()

        {

            Random ran = new Random();
            int a = ran.Next();


            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers/new");

            By name = By.Id("name");
            driver.FindElement(name).Clear();
            driver.FindElement(name).SendKeys("LAPTOP" + a);

            By introduced = By.Id("introduced");
            driver.FindElement(introduced).Clear();
            driver.FindElement(introduced).SendKeys("2023-01-20");

            By discontinued = By.Id("discontinued");
            driver.FindElement(discontinued).Clear();
            driver.FindElement(discontinued).SendKeys("2025-01-20");




            // select the drop down list
            var company = driver.FindElement(By.Id("company"));

            //create select element object 
            var selectElement = new SelectElement(company);

            
            selectElement.SelectByText("Netronics");
            String expectedvalue = "4";
            String actualvalue = driver.FindElement(By.Id("company")).GetAttribute("value");
            

            By create = By.XPath("//*[@id=\"main\"]/form/div/input");
            driver.FindElement(create).Click();


            var e = new WebDriverWait(driver, new TimeSpan(0, 0, 60)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("add")));
            
            Assert.That(driver.PageSource.Contains("LAPTOP" + a), Is.True);




        }




        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

    }
}