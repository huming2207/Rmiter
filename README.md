# RMITer
 
An open source, pure & native .NET/C# library and client for RMIT University online systems.

## About this project

### EULA for developers 

- This project is based on CC-BY-NC-SA 3.0 Australian licence, plus:
	- 1. Developer who use the code contains in this project **should NOT** make any illegal hacking purpose changes, for example, hacking other students or staffs' account;
	- 2. Developer who use the code contains in this project **should NOT** abuse any school server's API, e.g. performs more than 1 query per second (which is way faster than normal human being).
	- 3. Developer who use the code contains in this project **should NOT** violate any other Australian and/or Mainland Chinese laws or regulations, e.g. use proxy when you've got firewalled in mainland China. 😂
	- 4. **Developer should never, ever, use my code to make cheating programs on some online services, e.g. writing an unfair app that can help certain users choose a course automatically.**

- If bugs from APIs are found, **please report to the Uni ASAP in order to protect our own cyber security.** If something goes wrong with my code, please feel free to post an issue in this project page.



## Features

### "Backend"

 - [x] CAS (Central Authentication Service) login simulation **(i.e.WE DON'T NEED web browser or webviews any more!)**
 - [x] myRMIT Portal data handling, **including timetables (Working in progress)**
 - [x] Library Timetable parsing
 - [x] **"Portablize" class library (.NET Standard 1.4)**
 - [x] myTimetable (Allocate+, initial support) 
 - [ ] Blackboard data handling (Future planned)
 
### "Frontend"
 - [x] Commandline-based console demo  
 - [x] Windows apps (UWP for Windows 10, ***working in progress***)
 - [ ] iOS/Android apps (Planned)

### What it can do right now

I've done a demo app for Windows 10 Universal platform. It can load announcement messages from myRMIT portal. Here are some screenshots:

![uwp-1](https://raw.githubusercontent.com/huming2207/Rmiter/resources/Win10-1.png)

![uwp-2](https://raw.githubusercontent.com/huming2207/Rmiter/resources/Win10-2.png)

![uwp-3](https://raw.githubusercontent.com/huming2207/Rmiter/resources/Win10-3.png)


## Example

**This example illustrate the procedure of reading your myRMIT announcements' title.**

```csharp
string username = "s1234567";
string password = "who_the_hell_knows_your_password";

var casLogin = new RmiterCore.CasLogin(); 
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
