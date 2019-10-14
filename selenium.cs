public Models.BankDownloadResult DownloadFile(string fileName)
        {
            var result = new BankDownloadResult();
            //http://code.google.com/p/selenium/wiki
            //example from https://www.seleniumhq.org/docs/03_webdriver.jsp
            //https://www.seleniumhq.org/docs/04_webdriver_advanced.jsp

            var driveroptions = new ChromeOptions();
            driveroptions.AddUserProfilePreference("download.default_directory", this.SeleniumDownloadPath);
            driveroptions.AddUserProfilePreference("disable-popup-blocking", "true");
            
            using (IWebDriver driver = new ChromeDriver(this.SeleniumDriverPath, driveroptions))
            {
                try
                {
                    driver.Navigate().GoToUrl("http://www.google.com/");

                    // Find the text input element by its name
                    IWebElement query = driver.FindElement(By.Name("q"));

                    // Enter something to search for
                    query.SendKeys("link");

                    // Now submit the form. WebDriver will find the form for us from the element
                    query.Submit();

                    // Google's search is rendered dynamically with JavaScript.
                    // Wait for the page to load, timeout after 10 seconds
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    //wait.Until(d => d.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));

                    //
                    var listElement = driver.FindElements(OpenQA.Selenium.By.XPath(".//*[@id='search']//div[@class='g']"));
                    //
                    foreach( var e in listElement)
                    {

                        var parent = e.FindElement(OpenQA.Selenium.By.XPath("./.."));   //parent element
                        //include ctrl+click:
                        //var action = new OpenQA.Selenium.Interactions.Actions(driver);
                        //action.KeyDown(Keys.Control).Build().Perform();
                        //or click:
                        e.Click();
                        
                    }
                    //


                    // Should see: "Cheese - Google Search" (for an English locale)
                    Console.WriteLine("Page title is: " + driver.Title);

                    //string output = driver.FindElement(OpenQA.Selenium.By.XPath(".//*[@id='files']/div")).Text;

                    result.IsSuccess = true;
                    result.Error = String.Empty;
                }
                catch(Exception ex)
                {
                    Helpers.Log.LogError(ex);
                    result.IsSuccess = false;
                    result.Error = ex.ToString();
                }

                try
                {
                    driver.Quit();
                }
                catch {; }
            }
                              

            return result;
        }
    }
