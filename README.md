# OWIN Mixed Authentication

OWIN middleware implementation mixing Windows and Forms Authentication.

![mixed-auth](https://cloud.githubusercontent.com/assets/4712046/4690732/0bbe62f8-56f8-11e4-8757-2d10cdeca17e.png)

## Install with [NuGet](https://www.nuget.org/packages/OWIN-MixedAuth/)
```
PM> Install-Package OWIN-MixedAuth
```

# Running the samples

Before running the samples, make sure to unlock `windowsAuthentication` section:

### IIS
1. Open IIS Manager, select the server node, then Feature Delegation.
2. Set `Authentication - Windows` to `Read/Write`

 ![unlock-section](https://cloud.githubusercontent.com/assets/4712046/4689687/d28f8df8-56c6-11e4-9b88-8f5cb769ae93.png)

### IIS Express
1. Open **applicationhost.config** located at:
  * **Pre VS2015**: *$:\Users\\{username}\Documents\IISExpress\config* 
  * **VS2015**: *$(SolutionDir)\\.vs\config*
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

#### Importing Custom Claims

Adding custom claims in OWIN-MixedAuth is pretty straightforward, simply use `MixedAuthProvider` and place your own logic for fetching those custom claims. 

The following example shows how to import user Email, Surname and GiveName from Active Directory:
```C#
// Enable mixed auth
 app.UseMixedAuth(new MixedAuthOptions()
 {
   Provider = new MixedAuthProvider()
   {
     OnImportClaims = identity =>
     {
       List<Claim> claims = new List<Claim>();
       using (var principalContext = new PrincipalContext(ContextType.Domain)) //or ContextType.Machine
       {
         using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, identity.Name))
         {
           if (userPrincipal != null)
           {
             claims.Add(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress ?? string.Empty));
             claims.Add(new Claim(ClaimTypes.Surname, userPrincipal.Surname ?? string.Empty));
             claims.Add(new Claim(ClaimTypes.GivenName, userPrincipal.GivenName ?? string.Empty));
           }
         }
       }
       return claims;
     }
   }
 }, cookieOptions);
```
------
##### Please [share any issues](https://github.com/MohammadYounes/OWIN-MixedAuth/issues?state=open) you may have.
