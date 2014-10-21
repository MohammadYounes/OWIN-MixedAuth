# OWIN Mixed Authentication

OWIN middleware implementation mixing Windows and Forms Authentication. 

![mixed-auth](https://cloud.githubusercontent.com/assets/4712046/4690732/0bbe62f8-56f8-11e4-8757-2d10cdeca17e.png)

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
  ##### IIS
  1. Open IIS Manager, select the server node, then Feature Delegation.
  2. Set `Authentication - Windows` to `Read/Write`
![unlock-section](https://cloud.githubusercontent.com/assets/4712046/4689687/d28f8df8-56c6-11e4-9b88-8f5cb769ae93.png)

  ##### IIS Express
  1. Open **applicationhost.config** located at *$:\Users\{username}\Documents\IISExpress\config*
  2. Search for `windowsAuthentication` section and update `overrideModeDefault` value to `Allow`.

     ```XML
       <section name="windowsAuthentication" overrideModeDefault="Allow" />
     ```


# Pitfalls

   * **Internet Explorer XHR ignores Bearer Authorization header after authenticating a Windows Account?**
   
     When IE authenticates a Windows Account against a given domain, it gets added to the browser credentials cache, and will be used with all sub-sequent requests to the same domain, ignoring any explicitly set Authorization header.
     
     To work around this, send a custom `X-Authorization` header holding the bearer token and use a custom `OAuthBearerAuthenticationProvider` to read the token from the custom header:
     
     ```C#
     public class ApplicationOAuthBearerProvider: OAuthBearerAuthenticationProvider
      {
          public override Task RequestToken(OAuthRequestTokenContext context)
          {
              string authorization = context.Request.Headers.Get("X-Authorization");
              if (!string.IsNullOrEmpty(authorization))
              {
                  if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                  {
                      context.Token = authorization.Substring("Bearer ".Length).Trim();
                  }
              }
              return null;
          }
      }
     ```
     
     Or replace the the `Authorization` header before `OAuthBearerAuthenticationHandler` starts processing the request by including the following middleware prior to using `OAuthBearerAuthentication` :
     
     ``` C#
     //Authorization Header Override.
     app.Use((context, next) =>
      {
          if (context.Request.Headers.ContainsKey("X-Authorization"))
          {
              var bearer = context.Request.Headers["X-Authorization"];
              //remove negotiate header
              context.Request.Headers.Remove("Authorization");
              //replace it with bearer header
              context.Request.Headers.Add("Authorization", new string[] { bearer });
          }
          return next.Invoke();
      });
    ```
     
     
------

##### Please [share any issues](https://github.com/MohammadYounes/OWIN-MixedAuth/issues?state=open) you may have.
