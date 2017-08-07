using System;
using RmiterCore;
using RmiterCore.Errors;
using Xunit;

namespace RmiterTest
{
    public class LoginTest
    {
        [Fact]
        public async void Login()
        {
            var casLogin = new CasLogin();
            var casResult = await casLogin.RunCasLogin(TestConfig.UserName, TestConfig.Password);
            Assert.NotNull(casResult);
            Assert.True(casResult.CasError.Equals(CasLoginError.NoError) 
                        || casResult.CasError.Equals(CasLoginError.UserPasswordAboutToExpire));

            if (casResult.CasError.Equals(CasLoginError.UserPasswordAboutToExpire))
            {
                Console.WriteLine("[WARNING] Please change your password as it is going to expired.");
            }
        }
    }
}
