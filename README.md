# OWIN Mixed Authentication

OWIN middleware implementation mixing Windows and Forms Authentication.

![mixed-auth](https://cloud.githubusercontent.com/assets/4712046/4690732/0bbe62f8-56f8-11e4-8757-2d10cdeca17e.png)

# Running the samples

Before running the samples, make sure to unlock `windowsAuthentication` section:

### IIS
1. Open IIS Manager, select the server node, then Feature Delegation.
2. Set `Authentication - Windows` to `Read/Write`

 ![unlock-section](https://cloud.githubusercontent.com/assets/4712046/4689687/d28f8df8-56c6-11e4-9b88-8f5cb769ae93.png)

### IIS Express
1. Open **applicationhost.config** located at *$:\Users\{username}\Documents\IISExpress\config*
2. Search for `windowsAuthentication` section and update `overrideModeDefault` value to `Allow`.

  ```XML
   <section name="windowsAuthentication" overrideModeDefault="Allow" />
  ```

# Usage

1. Add reference to `MohammadYounes.Owin.Security.MixedAuth.dll`

2. Register `MixedAuth` in **Global.asax**
  ```C#
  //add using statement
  using MohammadYounes.Owin.Security.MixedAuth;

  public class MyWebApplication : HttpApplication
  {
     //ctor
     public MyWebApplication()
     {
       //register MixedAuth
       this.RegisterMixedAuth();
     }
     .
     .
     .
  }
```
3. Use `MixedAuth` in **Startup.Auth.cs**
  ```C#
  //Enable Mixed Authentication
  //As we are using LogonUserIdentity, its required to run in PipelineStage.PostAuthenticate
  //Register this after any middleware that uses stage marker PipelineStage.Authenticate

  app.UseMixedAuth(cookieOptions);
  ```
  **Important!** MixedAuth is required to run in `PipelineStage.PostAuthenticate`, make sure the use statement is after any other middleware that uses `PipelineStage.Authenticate`. See [OWIN Middleware in the IIS integrated pipeline](http://www.asp.net/aspnet/overview/owin-and-katana/owin-middleware-in-the-iis-integrated-pipeline).

4. Enable Windows authentication in **Web.config**

  ```XML
  <!-- Enable Mixed Auth -->
  <location path="MixedAuth">
    <system.webServer>
      <security>
        <authentication>
          <windowsAuthentication enabled="true" />
        </authentication>
      </security>
    </system.webServer>
  </location>
  ```
  **Important!** Enabling windows authentication for a sub path requires `windowsAuthentication` section to be unlocked at a parent level.

------
##### Please [share any issues](https://github.com/MohammadYounes/OWIN-MixedAuth/issues?state=open) you may have.
