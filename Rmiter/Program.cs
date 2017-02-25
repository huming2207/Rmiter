using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RmiterCore;
using RmiterCore.MyRmit;


namespace RmiterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CasLogin casLogin = new CasLogin();

            Console.Write("[Login] Student ID: ");
            string username = Console.ReadLine();
            Console.Write("[Login] Password:   ");
            string password = Console.ReadLine();
            Console.WriteLine("[Info] Please wait...");

            CookieContainer cookie = casLogin.RunCasLogin(username, password).Result;

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
