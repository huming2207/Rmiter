using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Security;
using RmiterCore;
using RmiterCore.LibraryInfo;
using RmiterCore.MyRmit;

namespace RmiterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var casLogin = new CasLogin();

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

            // Test login 
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

            // Test on access myRMIT portal
            stopWatch.Start();
            var portal = new MyRmitPortal(cookie);
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

            // Test on RmiterCore.LibraryInfo.InfoParser
            stopWatch.Start();
            var libraryInfoParser = new InfoParser();
            var libraryInfoResult = libraryInfoParser.GetOpeningHours().Result;
            stopWatch.Stop();
            string libInfoTime = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();


            Console.WriteLine("[Info] Today's Carlton library opening hour is: {0}", libraryInfoResult.CarltonLibrary);

            Console.WriteLine("\n\n[Performance] Performance details are shown below: ");
            Console.WriteLine("[Performance] CAS Login, {0} ms", casLoginTime);
            Console.WriteLine("[Performance] myRMIT Portal, {0} ms", myRmitTime);
            Console.WriteLine("[Performance] Library opening hours, {0} ms", libInfoTime);
            Console.WriteLine("\n\n[Info] Demo finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
