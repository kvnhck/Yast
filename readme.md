Yast
====

A .NET Library for the [Yast TimeTracker API](http://www.yast.com/timetracker/)

**To run the tests:**

- you need a Yast account
- and create a file called PrivateApp.config

```
<?xml version="1.0" encoding="utf-8" ?>
<appSettings>
  <add key="Yast.User" value="yast_username_here" />
  <add key="Yast.Password" value="yast_password_here" />
</appSettings>
```