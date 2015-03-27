﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System.IO;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;

namespace HtmlAgilityPack
{
    class WebPost
    {

        public static void ReListAll()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("user-data-dir=C:/Users/JLin/AppData/Local/Google/Chrome/User Data/Default");
            using (var driver = new ChromeDriver(options))
            {

                driver.Navigate().GoToUrl("http://backpack.tf/classifieds/?steamid=76561198049414145");

                bool done=false;
                while(!done)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                    
                    int number_left = driver.FindElements(By.CssSelector(".btn.btn-xs.btn-bottom.btn-default.listing-relist")).Count;

                    if (number_left == 0)
                    {
                        break;
                    }

                    var refreshbutton = driver.FindElement(By.CssSelector(".btn.btn-xs.btn-bottom.btn-default.listing-relist"));

                    refreshbutton.SendKeys(OpenQA.Selenium.Keys.Return);

                    bool done_1 = false;
                    int i = 0;
                                                           
                    while(!done_1)
                    {
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                        var submitbutton = driver.FindElement(By.Id("button_save"));
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                        bool enabled = submitbutton.Enabled;
                        if(enabled)
                        {
                            submitbutton.Click();
                        }
                        else 
                        {
                            break;
                        }
 

                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                        string url = driver.Url;
                        if (url == "http://backpack.tf/classifieds/?steamid=76561198049414145")
                        {
                            done_1 = true;
                        }

                        else if (i > 1 && !done_1)
                        {
                            Console.WriteLine("Cannot post after 3 retries");
                            Console.Read();
                        }
                        else { }
                        i++;
                        
                    }


                }
                Console.WriteLine("Completed");
                Console.Read();

             }





        }
        public static void ListItem(uint itemid,double metalprice, int keyprice, int budprice)
        {

            ChromeOptions options = new ChromeOptions();

            options.AddArguments("user-data-dir=C:/Users/JLin/AppData/Local/Google/Chrome/User Data/Default");
            
            using (var driver = new ChromeDriver(options))
            {
                
                driver.Navigate().GoToUrl("http://backpack.tf/classifieds/add/" + itemid);

                
                try
                {

                    bool done_1 = false;
                    int i = 0;
                    while (!done_1)
                    {
                        var metalfield = driver.FindElementById("metal");
                        var keyfield = driver.FindElementById("keys");
                        var budfield = driver.FindElementById("earbuds");
                        var submitbutton = driver.FindElementById("button_save");

                        metalfield.SendKeys(metalprice.ToString());

                        keyfield.SendKeys(keyprice.ToString());

                        budfield.SendKeys(budprice.ToString());

                        submitbutton.SendKeys(OpenQA.Selenium.Keys.Return);
                        
                        if(driver.Url!="http://backpack.tf/classifieds/?steamid=76561198049414145")
                        {
                            done_1 = false;
                        }
                        else
                        {
                            done_1 = true;
                        }
                        if (i > 1 && !done_1)
                        {
                            Console.WriteLine("Cannot post after 3 retries");
                            Console.Read();
                        }
                        else { }
                        i++;

                    }
                    Console.WriteLine("Successful");
                    return;
                }
                catch
                {
                    if(driver.Url == "http://backpack.tf/profiles/76561198049414145")
                    {
                        Console.WriteLine("Error: Cannot Relist Yet");
                        return;
                    }
                    else if (driver.Url == "https://steamcommunity.com/openid/login?openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.mode=checkid_setup&openid.return_to=http%3A%2F%2Fbackpack.tf%2Flogin&openid.realm=http%3A%2F%2Fbackpack.tf&openid.ns.sreg=http%3A%2F%2Fopenid.net%2Fextensions%2Fsreg%2F1.1&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select")
                    {
                        bool steamdone = false;
                        int j = 0;
                        while (!steamdone)
                        {
                            var usernamefield = driver.FindElementById("steamAccountName");
                            var passwordfield = driver.FindElementById("steamPassword");
                            var loginbutton = driver.FindElementById("login_btn_signin");

                            string username = "jon88097";
                            string password = "mA9*n3t1c";
                            usernamefield.SendKeys(username);
                            passwordfield.SendKeys(password);
                            loginbutton.Click();
                            if(driver.Url != "http://backpack.tf/classifieds/add/" + itemid)
                            {
                                steamdone = false;
                            }
                            else
                            {
                                steamdone = true;
                            }
                            if (j > 1 && !steamdone)
                            {
                                Console.WriteLine("Cannot login after 3 retries. It might be Steamguard");
                                return;
                            }
                            else { }
                            j++;
                        }
                        Console.WriteLine("Success!");

                    }
                    else
                    {
                        Console.WriteLine(driver.Url);
                        Console.WriteLine("Page URL unrecognized. Exiting method");
                        return;

                    }
                    
                try
                {
                    var metalfield = driver.FindElementById("metal");
                    var keyfield = driver.FindElementById("keys");
                    var budfield = driver.FindElementById("earbuds");
                    var submitbutton = driver.FindElementByXPath("//*[@id='button_save']");

                    metalfield.SendKeys(metalprice.ToString());
                    keyfield.SendKeys(keyprice.ToString());
                    budfield.SendKeys(budprice.ToString());

                    submitbutton.Click();
                    Console.WriteLine("Listing Successful");
                    Console.Read();
                }

                catch
                {
                    Console.WriteLine("WTF?");

                    return;

                }
                }
            }  
        }
    }
}
