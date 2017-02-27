# RMITer
 
An opensource, pure & native .NET/C# library and client for RMIT University online systems.

## Disclaimer

I made this project as I hope I can write a similar [MonashTimetable](http://joshparnham.com/projects/monash-timetable/) app for RMIT University.

- This project and its source code are not affiliated with RMIT University. Meanwhile, it also contains some API reverse engineerings which I've inquired and I have been granted permissions orally from IT Service staff in RMIT (those who work in Level 3, Building 80), though they still hold the API copyrights and management rights.

- Technically speaking, this project can only be used as accessing your own personal data, it cannot access others, if you don't know his/her accounts. **So it is definitely NOT a illegal hacking tool, at least I am NOT hoping to design a hacking tool and get into trouble.**

- So far, this project is licenced under [CC-BY-NC-SA 3.0 (Australian version) Licence](https://creativecommons.org/licenses/by-nc-sa/3.0/au). **Any other personnel use or modify it for commercial purpose is not allowed, unless futher permissions are granted both by me and RMIT.**

- This project may become an extra, selective assignment for Web Programming (COSC2413) class. Some third-party code came online which are not yet written with citations will be cited as soon as possible.

## Features

### "Backend"

 - [x] CAS (Central Authentication Service) login simulation **(i.e.WE DON'T NEED web browser or webviews any more!)**
 - [x] myRMIT Portal data handling, **including timetables (Working in progress)**
 - [x] Library Timetable parsing
 - [ ] Library search 
 - [ ] **Merge classic library to Portable Class Library (need changing HTML parser)**
 - [ ] Blackboard data handling (Future planned)
 
### "Frontend"
 - [x] Commandline-based console demo  
 - [x] Windows desktop apps (WPF, ***working in progress***)
 - [ ] iOS/Android apps (Planned)


## Example (For PCL Library)

**This example illustrate the procedure of **

```csharp
string username = "s1234567";
string password = "who_the_hell_knows_your_password";

var casLogin = new RmiterCorePcl.CasLogin(); 
var cookie = await casLogin.RunCasLogin(username, password);
var myRmitPortal = new MyRmitPortal(cookie);
var homeObject = myRmitPortal.GetHomeMessages().Result;

foreach(var announcement in homeObject.Announcements)
{
      Console.WriteLine("[Title] \"{0}\"", announcement.Title);
}
```


## Details about RMIT CAS 

Here is a detailed graph for the whole login request procedure:

![cas-process](https://raw.githubusercontent.com/huming2207/Rmiter/resources/CAS%20procedure.png)
