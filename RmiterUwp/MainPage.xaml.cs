using System;
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
using RmiterCore;
using RmiterCore.LibraryInfo;
using RmiterCore.MyRmit;
using RmiterCore.Errors;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RmiterUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // RmiterCore CookieContainer declaration
        private CookieContainer RmitCookieContainer { get; set; }

        // MainPage constructor
        public MainPage()
        {
            this.InitializeComponent();
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
                }

                LoginProgressRing.IsActive = false;
            }
            else
            {
                var errorDialog = new MessageDialog("Please specify a valid RMIT University ID!", "Done");
                await errorDialog.ShowAsync();
            }
        }
    }
}
