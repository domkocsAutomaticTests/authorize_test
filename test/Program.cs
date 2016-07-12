using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            bool letezik = false;
            int hiba = 0;
            for (int i = 100001; i <= 100006; i++)
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
                    Console.WriteLine("Sikertelen bejelentkezés!");
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
                
                    Thread.Sleep(2000);
                    if (driver.FindElement(By.ClassName("form-title")).Text == "CONFIRM: AUTHORIZE")
                    {
                        letezik = true;
                    }
                    else
                    {
                        letezik = false;
                    }
                    if (letezik == true)
                    {
                        //tud authorizálni
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sikerült!");
                        hiba++;
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
            Console.ReadKey();
        }
    }
}
