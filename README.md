# VRI-ComDatabase-Automation

## Project Description ##

The VRI-ComDatabase-Automation is a automation project with SELENIUM Webdriver using NUnit framework, NUnit framework is an open–source unit testing framework in C#, for this project I created the automation for Add a new computer feature from this app [ComputerDatabaseApp](http://computer-database.gatling.io/computers), this web application allows users to manage and keep track of computer data.

*This is a Selenium project developed in C# using Visual Studio and NUnit for testing automation of the computer database website.*



## Prerequisites ##

Before running this project, make sure to have the following installed:

   - Visual Studio
   - Create Selenium C# automation framework .NET Core
   - Google Chrome or Firefox
   - ChromeDriver or GeckoDriver

 You can follow the instructions here [Selenium Installing Guide](https://www.browserstack.com/guide/selenium-with-c-sharp-for-automated-test)

 
 
## Installing ##

 Clone or download the project.
 Open the project using Visual Studio.
    
 To open the project file in Visual Studio, you can follow these steps:

   1. Install Visual Studio on your computer if you don't have it already installed.
   2. Install Git on your computer if you don't have it already installed.
   3. Open Visual Studio and click on "Clone or checkout code" under the "Start" tab or go to "File" -> "Open" -> "Project/Solution"
   4. In the "Clone Repository" section, paste the link to the GitHub repository [GitHub](https://github.com/vriandy07/SELENIUM-VRI-ComDatabase-Automation.git) and  click "Clone".
   5. Visual Studio will now clone the repository and you should see the project files in the Solution Explorer.
   6. To open the solution, you can double-click on the .sln file in the Solution Explorer.
   7. Set the ChromeDriver or GeckoDriver path in Setup() method in **AddComputer.cs** class as mentioned below:
```
string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
driver = new ChromeDriver(path + @"\drivers\");
```
    
 You should now be able to build and run the project, or you can check this link [OpenProject](https://learn.microsoft.com/en-us/visualstudio/get-started/tutorial-open-project-from-repo?view=vs-2022)
 
 If there are any missing packages or dependencies, you can install them by right-clicking on the solution in the Solution Explorer and choosing "Manage NuGet Packages".

Note: Make sure to have the required software and libraries installed on your computer before opening the project.


## Tests ##

This is the set up before the test
```
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

```
Explanation:

In Selenium C#, the [OneTimeSetUp] attribute is used to define a setup method that will run only once before all the tests in a test fixture are run. This is useful for performing actions that need to be done once before the tests start, such as initializing test data, starting a browser, or logging in to an application.

The [OneTimeSetUp] method is part of the NUnit testing framework and is used to set up a test fixture, which is a collection of tests that are intended to be run together. The [OneTimeSetUp] method will run before any of the tests in the test fixture and is only run once, even if multiple tests are run within the same fixture.


Set the ChromeDriver or GeckoDriver path in Setup() method
```
string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(path + @"\drivers\");
```
 


This is to maximize the windows browser
```
driver.Manage().Window.Maximize();
```





The following tests have been automated:

   - TC01_VerifyNavigateToAddComputerPage
      > In the first test case **TC01_VerifyNavigateToAddComputerPage** I want to verify when click on 'Add a new computer' button it will success to navigate to the 'Add computer' page with verify the text in the page, the text that I verify is "Add a computer".
       ```
       [Test, Order(1) ]
        public void TC01_VerifyNavigateToAddComputerPage()
        {
            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers");

            By add = By.XPath("//*[@id=\"add\"]");
            driver.FindElement(add).Click();
            Assert.That(driver.PageSource.Contains("Add a computer"), Is.True);

        }
       ```
       
   - TC02_VerifyComputerNameFieldMandatory
      > In the second test case **TC02_VerifyComputerNameFieldMandatory** I want to verify the 'computer name field' is mandatory by directly click on 'create' button in the add computer page while it still empty, I verified the warning error message that should displayed by assert.
       ```
       [Test, Order(2)]

        public void TC02_VerifyComputerNameFieldMandatory()

        {
            driver.Navigate().GoToUrl("http://computer-database.gatling.io/computers/new");

            By name = By.Id("name");
            driver.FindElement(name).Clear();
            driver.FindElement(By.XPath("//*[@id=\"main\"]/form/div/input")).Click();
            Assert.That(driver.PageSource.Contains("Failed to refine type : Predicate isEmpty() did not fail."), Is.True);

        }
       ```

   - TC03_VerifyComputerNameAbleToFilled
      > In the third test case **TC03_VerifyComputerNameAbleToFilled** I want to verify the 'computer name field' is able to filled by validate the actual value in the computer name field with the expected name.
       ```
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
       ```
   - TC04_VerifyValidIntroducedDateFormat
      > In the fourth test case **TC04_VerifyValidIntroducedDateFormat** I want to verify the 'introduced date' format, the valid format should be ('yyyy-MM-dd'), then in this test case I enter "2023-01-20" as the introduced date with the valid date format then I compare with my expected date with the correct format.
       ```
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
       ```

   - TC05_VerifyInvalidIntroducedDateFormat
      > In the fifth test case **TC05_VerifyInvalidIntroducedDateFormat** I want to verify the invalid 'introduced date' format, the valid format should be ('yyyy-MM-dd'), then in this test case I enter "20-01-2023" as the introduced date with the invalid date format then I compare with my expected date with the correct format, the warning message should appear.
       ```
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
       ```

   - TC06_VerifySuccessAddComputer
      > In the sixth test case **TC06_VerifySuccessAddComputer** I highlighted this part, I create 'random number' for the computer name, so it will be not the same whenever it run.
      ```
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
            
      ```
      Sample result

      ![image](https://user-images.githubusercontent.com/39895518/218294371-e1383872-44ec-4134-9e35-58cf76721910.png)
      ![image](https://user-images.githubusercontent.com/39895518/218294419-3063cd9f-e355-4df0-9e9a-8f75f2a306a3.png)









## Built With ##
   - Visual Studio - Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft.
   - NUnit - The unit testing framework used.
   - OpenQA.Selenium - The browser automation tool used.
   - Google ChromeDriver - The web driver used for Google Chrome browser.
   - Firefox GeckoDriver - The web driver used for Firefox browser.
