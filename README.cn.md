# RMITer
 
为墨尔本皇家理工大学（RMIT University）教务系统而生的一个开源的纯.NET/C#库及相关app。鉴于本项目未来可能会添加中文化UI，本人亦在此提供部分中文概述。

## 一些说(fei)明(hua)

之前想做个类似Monash他们的人搞的[MonashTimetable](http://joshparnham.com/projects/monash-timetable/) 课程表app，用python写过，模拟CAS登录那部分不太好搞，学校的CAS登录系统又没API，得借助Chrome浏览器获取cookies，没成功。结果半年后最近一段时间试着拿C#写了一下居然搞定了。以下是一些关于本项目的说明。

- **本项目并非由RMIT官方赞助支持，纯属个人作品。同时本项目目前亦存在一些针对未公开API的逆向工程。这些研究已获得RMIT的IT部门（IT Service）工作人员口头同意。** 当然他们依然保留对其API的管理权和版权，所以如果他们做了些较大的改动，本项目可能会失效。若情况允许，我会更新本项目的后端库并恢复其功能。

- 从技术角度上讲，本项目并不是黑客工具，只能根据用户的登录情况、根据需求，从RMIT服务器上获取用户本人的个人信息。**此外，本项目设计的初衷纯属为了自制一个网页端的替代品，以改善用户体验，同时也为学校相关服务节约流量和带宽（不用加载CSS、Javascript和图片图标等，理论上在中国大陆网络“受限”地区效果可能更显著）。**

- 目前本项目开源代码根据 [CC-BY-NC-SA 3.0 (澳大利亚版) 协议](https://creativecommons.org/licenses/by-nc-sa/3.0/au)发布，并受该协议保护。 **除非获得本人和学校方面同意，任何人不得将其用作商业用途。**

- 本项目可能会被用作Web Programming (COSC2413)课程的附加assignment。 本人会逐步添加部分第三方源码的引用来源。

## 功能

### "后端"

 - [x] CAS (Central Authentication Service) 模拟登录 **(也就是说不需要外部网页浏览器和WebView，用户可以直接在app或脚本中输入账号密码登录我校教务系统)**
 - [x] myRMIT教务系统数据获取, **包括课程表。**
 - [x] 图书馆营业时间解析
 - [x] **“PCL化”项目，可移植到多种平台**
 - [ ] 图书馆搜索（未来计划支持的功能）
 - [ ] Blackboard系统数据读取 (未来计划支持)
 
### "前端"
 - [x] 基于命令行的演示程序  
 - [x] Windows桌面版应用 (基于WPF, ***正在编写中***)
 - [ ] iOS/Android应用 (计划支持)


## 举个例子

**提交登录信息，并获取myRMIT里的公告标题：**

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


## 我校登录认证系统工作原理概述

做了个图，大概是这个意思，详细见源码：

![cas-process](https://raw.githubusercontent.com/huming2207/Rmiter/resources/CAS%20procedure.png)
