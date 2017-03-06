﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using RmiterCoreUwp;
using RmiterCoreUwp.LibraryInfo;
using RmiterCoreUwp.MyRmit;
using RmiterCoreUwp.Errors;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RmiterUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // RmiterCoreUwp CookieContainer declaration
        private CookieContainer RmitCookieContainer { get; set; }

        // MainPage constructor
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            AnnouncementTab.IsEnabled = false;
            TimetableTab.IsEnabled = false;
            
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Declare CasLogin class
            var casLogin = new CasLogin();

            if(UsernameTextbox.Text != "Username" && PasswordTextbox.Password != "Password")
            {
                // Let the progress ring start to rotate
                LoginProgressRing.IsActive = true;

                // Run the login and grab the cookies
                var casLoginResult = await casLogin.RunCasLogin(UsernameTextbox.Text, PasswordTextbox.Password);

                // If CookieContainer is still null, then the login procedure must be somehow failed.
                if (casLoginResult.CasError != CasLoginError.NoError)
                {
                    var errorDialog = new MessageDialog(
                        string.Format("Login failed! \r\nError code: {0}, Reason: {1} \r\nHTTP Response code: {2}",
                        Enum.GetName(typeof(CasLoginError), casLoginResult.CasError),
                        ((int)casLoginResult.CasError).ToString(),
                        ((int)casLoginResult.HttpResponseStatusCode).ToString()
                        ), "Error!");
                    await errorDialog.ShowAsync();
                }
                else
                {
                    var successfulDialog = new MessageDialog("Login successful!", "Done");
                    await successfulDialog.ShowAsync();
                    RmitCookieContainer = casLoginResult.CasCookieContainer;
                    TimetableTab.IsEnabled = true;
                    AnnouncementTab.IsEnabled = true;
                }

                LoginProgressRing.IsActive = false;
            }
            else
            {
                var errorDialog = new MessageDialog("Please specify a valid RMIT University ID!", "Done");
                await errorDialog.ShowAsync();
            }
        }

        private async Task<List<AnnouncementUIContent>> GetAnnouncementList()
        {
            var getMyPortal = new MyRmitPortal(RmitCookieContainer);
            var homeObject = await getMyPortal.GetHomeMessages();
            var announcementUIContent = new List<AnnouncementUIContent>();

            foreach(var announcement in homeObject.Announcements)
            {
                var uiContent = new AnnouncementUIContent()
                {
                    Title = announcement.Title,
                    FullContent = announcement.Content
                };

                uiContent.BriefContent = GetBriefAnnouncement(announcement.Content);

                announcementUIContent.Add(uiContent);

            }

            return announcementUIContent;
        }

        // Since it needs regular expressions (that I'm not quite familiar with)
        //    so I used the code from here to remove all HTML tags that are not needed.
        // See: http://stackoverflow.com/questions/18153998/how-do-i-remove-all-html-tags-from-a-string-without-knowing-which-tags-are-in-it
        private string GetBriefAnnouncement(string rawText)
        {
            return (Regex.Replace(rawText, "<.*?>", string.Empty).Substring(0, 15) + "...");
        }
    }
}
