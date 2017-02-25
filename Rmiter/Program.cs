using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security;
using RmiterCore;
using RmiterCore.MyRmit;

namespace RmiterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CasLogin casLogin = new CasLogin();

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

            CookieContainer cookie = casLogin.RunCasLogin(username, password).Result;

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
            MyRmitPortal portal = new MyRmitPortal(cookie);
            var homeObject = portal.GetHomeMessages().Result;

            foreach(var announcement in homeObject.Announcements)
            {
                Console.WriteLine("[Title] \"{0}\"", announcement.Title);
            }

            Console.WriteLine("\n\n[Info] Demo finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
