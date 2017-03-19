# RMITer
 
An opensource, pure & native .NET/C# library and client for RMIT University online systems.

[**For Chinese README, here's the link!**](https://github.com/huming2207/Rmiter/blob/master/README.cn.md)

[**中文README见此**](https://github.com/huming2207/Rmiter/blob/master/README.cn.md)

(PS: Please note that the Chinese edition is not fully consistent with this English one. 请注意，中英文版README并不一定完全对应)

## About this project

### UPDATE - Mar 10, 2017

**This project is temporarily discontinued. After discussing to my teachers, it seems that it is unlikely to approve by the Uni because it can be redistributed with illegal backdoors (e.g. illegally caching/redirecting user data to hackers' server). So unless they approve my work, I won't restart this project.**

I made this project as I hope I can write a similar [MonashTimetable](http://joshparnham.com/projects/monash-timetable/) app for RMIT University. I've made a python one before but I've got stuck on CAS login simulation. As a result, I gave up that project. But later I tried again in C# with HTML Agility Pack and Jumony HTML parser, and it works as I expected.

Here are some details about this project.

- This project and its source code are not affiliated with RMIT University. Meanwhile, it also contains some API reverse engineerings which I've inquired and I have been granted permissions orally from IT Service staff in RMIT (those who work in Level 3, Building 80), though they still hold the API copyrights and management rights.

- Technically speaking, this project can only be used as accessing users' own personal data based on users' requirements. It cannot access others, if you don't know his/her accounts. **So it is definitely NOT a illegal hacking tool, at least I am NOT hoping to design a hacking tool and get into trouble. I just want to make an alternative for those web-based systems to enhance user experience, while it also save bandwidth for our school. It doesn't need to load CSS/Javascripts and pictures/icons. So, theoretically it may have improvements in some restricted networks such as mainland China).**

- So far, this project is licenced under [CC-BY-NC-SA 3.0 (Australian version) Licence](https://creativecommons.org/licenses/by-nc-sa/3.0/au). **Any other personnel use or modify it for commercial purpose is not allowed, unless futher permissions are granted both by me and RMIT.**

- This project may become an extra, selective assignment for Web Programming (COSC2413) course. Some third-party code came online which are not yet written with citations will be cited as soon as possible.

## Features

### "Backend"

 - [x] CAS (Central Authentication Service) login simulation **(i.e.WE DON'T NEED web browser or webviews any more!)**
 - [x] myRMIT Portal data handling, **including timetables (Working in progress)**
 - [x] Library Timetable parsing
 - [ ] **"Portablize" class library (merging, changing HTML parser etc)**
 - [ ] Library search 
 - [ ] Blackboard data handling (Future planned)
 
### "Frontend"
 - [x] Commandline-based console demo  
 - [x] Windows desktop apps (WPF, ***working in progress***)
 - [ ] iOS/Android apps (Planned)


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
