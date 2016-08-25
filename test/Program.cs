using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace test
{
    class Program
    {
        private static int min = 100001;
        private static int max = 100006;
        private static int kivetel = 100001;
        static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            bool letezik = false;
            int hiba = 0;
            for (int i = min; i <= max; i++)
            {
                Console.Write("[Státusz] ");
                Console.WriteLine("Bejelentkezés {0} felhasználóba...", i);
                driver.Navigate().GoToUrl("https://clicpltest.egroup.hu/Login/LoginWithRSADemo");
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                try
                {
                    driver.FindElement(By.Id("loginId")).Clear();
                    driver.FindElement(By.Id("loginId")).SendKeys(i.ToString());
                    driver.FindElement(By.Id("login")).Click();
                    Thread.Sleep(1000);
                    Console.Write("[Státusz] ");
                    Console.WriteLine("Bejelentkezve.");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sikertelen bejelentkezés {0} felhasználóval!", i);
                    break;
                }
                try
                {
                    driver.FindElement(By.Id("submit")).Click();
                }
                catch { }
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl("https://clicpltest.egroup.hu/Domestic/New");
                Thread.Sleep(2000);
                Console.Write("[Státusz] ");
                Console.WriteLine("Új Domestic Payment");
                //kitöltés: Beneficiary data
                driver.FindElement(By.Id("Input_BnAccount_formatted")).SendKeys("47160014752463344927179927");
                driver.FindElement(By.Id("Input_BnName")).SendKeys("ikgGXKlY EUZLi");
                //kitöltés: Payment data
                driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000");
                driver.FindElement(By.Id("Input_Details")).SendKeys("automatic test");
                //authorizálás
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                try
                {
                    driver.FindElement(By.Id("actionButton_Authorize")).Click();
                    driver.FindElement(By.Id("actionButton_fakeExecute")).Click(); Thread.Sleep(1000);
                    Thread.Sleep(2000);
                    try
                    {
                        if (driver.FindElement(By.ClassName("alertMessage")).Text != "")
                        {
                            letezik = false;
                        }
                        else
                        {
                            letezik = true;
                        }
                    }
                    catch
                    {
                        letezik = true;
                    }
                    if (letezik == true && i == kivetel)
                    {
                        //tud authorizálni
                        Console.ForegroundColor = ConsoleColor.Red;
                        hiba++;
                        Console.WriteLine("Sikerült!");
                        Console.ResetColor();
                    }
                    else if (letezik == true && i != kivetel)
                    {
                        //tud authorizálni
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Sikerült!");
                        Console.ResetColor();
                    }
                    else
                    {
                        //nem tud authorizálni
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nem Sikerült!");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    Console.Write("[Státusz] ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Nem küldhető auth.-ra");
                    Console.ResetColor();
                }
            }
            driver.Close();
            if (hiba > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("[Státusz] ");
            Console.WriteLine("Teszt vége");
            Console.WriteLine("Üss le egy billentyűt a kilépéshez...");
            Console.ReadKey();
        }
    }
}
