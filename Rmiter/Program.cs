using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Security;

namespace RmiterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var casLoginPcl = new RmiterCorePcl.CasLogin();
            var casLogin = new RmiterCore.CasLogin();

            Console.WriteLine("[Demo #1] Get myRMIT announcement messages' title");

            // Get username
            Console.Write("[Login] Student ID: ");
            string username = Console.ReadLine();

            // Get password with "*" cover.
            // Using SecureString to ensure the password is safe in the memory
            Console.Write("[Login] Password:   ");
            string password = "";
            while(true)
            {
                var keyValue = Console.ReadKey(true);
                if(keyValue.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if(keyValue.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Substring(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password += keyValue.KeyChar;
                    Console.Write("*");
                }
            }


            Console.WriteLine("\n[Info] Please wait...");
            Console.WriteLine("[Info] Preparing stopwatch...");
            var stopWatch = new Stopwatch();

            // Test login with PCL library
            stopWatch.Start();
            var cookiePcl = casLoginPcl.RunCasLogin(username, password).Result;
            stopWatch.Stop();
            string casLoginPclTime = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();

            // Test login with classic library
            stopWatch.Start();
            var cookie = casLogin.RunCasLogin(username, password).Result;
            stopWatch.Stop();
            string casLoginTime = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();

            // Clear up the password variable to ensure it's not being (easily) captured later
            password = "";

            if (cookie != null)
            {
                Console.WriteLine("[Info] Login successful!");
            }
            else
            {
                Console.WriteLine("[Error] Login failed!");
                Console.Read();
                Environment.Exit(1);
            }

            Console.WriteLine("[Info] Grabbing announcement messages' title...\n\n");

            // Test on accesing myRMIT portal via portable library
            stopWatch.Start();
            var portalPcl = new RmiterCorePcl.MyRmit.MyRmitPortal(cookiePcl);
            var homeObjectPcl = portalPcl.GetHomeMessages().Result;
            stopWatch.Stop();
            string myRmitTimePcl = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();

            // Test on access myRMIT portal via classic library
            stopWatch.Start();
            var portal = new RmiterCore.MyRmit.MyRmitPortal(cookie);
            var homeObject = portal.GetHomeMessages().Result;
            stopWatch.Stop();
            string myRmitTime = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();

            foreach (var announcement in homeObject.Announcements)
            {
                Console.WriteLine("[Title] \"{0}\"", announcement.Title);
            }

            Console.WriteLine("\n[Info] Demo #1 finished, continue on the next one...");
            Console.WriteLine("[Demo #2] Get Carlton library's opening hour, DOES NOT need to login this time.");

            // Test on RmiterCorePcl.LibraryInfo.InfoParser
            stopWatch.Start();
            var libraryInfoParserPcl = new RmiterCorePcl.LibraryInfo.InfoParser();
            var libraryInfoResultPcl = libraryInfoParserPcl.GetOpeningHours().Result;
            stopWatch.Stop();
            string libInfoPclTime = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();

            // Test on RmiterCorePcl.LibraryInfo.InfoParser
            stopWatch.Start();
            var libraryInfoParser = new RmiterCore.LibraryInfo.InfoParser();
            var libraryInfoResult = libraryInfoParserPcl.GetOpeningHours().Result;
            stopWatch.Stop();
            string libInfoTime = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();

            Console.WriteLine("[Info] Today's Carlton library opening hour is (from PCL): {0}", libraryInfoResultPcl.CarltonLibrary);

            Console.WriteLine("\n\n[Performance] Performance details are shown below: ");
            Console.WriteLine("[Performance] CAS Login, Classic (via HAP Engine): {0} ms; PCL (via Jumony Engine): {1} ms", casLoginTime, casLoginPclTime);
            Console.WriteLine("[Performance] myRMIT Portal, Classic (JSON.NET): {0} ms; PCL (JSON.NET PCL): {1} ms", myRmitTime, myRmitTimePcl);
            Console.WriteLine("[Performance] Library opening hours, Classic (via HAP Engine): {0} ms; PCL (via Jumony Engine): {1} ms", libInfoTime, libInfoPclTime);
            Console.WriteLine("\n\n[Info] Demo finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
