# GraphLite.NET

GraphLite is a lightweight GraphAPI client for .NET (.NET standard package) for the user management and reporting needs of an Azure Active Directory B2C tenant.

## Getting Started
After creating the AAD B2C tenant on Azure Portal, in order to use the GraphAPI to manage users and access reports an application has to be registered on the Azure Active Directory of the created tenant (NOT on the B2C menu item).

### Authentication

In order for the client to be able to make calls on the Graph API, will have to acquire an access token on the Azure Active Directory. 
The acquisition of the access token is taken care by the GraphApiClient, and you can keep a reference of the GraphApiClient the lifetime of your 
application in order to minimize the overhead of acquiring a new access token for each new instance created.

#### Using application credentials
After the creation of an application on Azure Active Directory and generating the secret key for it, and assigning the necessary rights, we can start using GraphAPI to manage the B2C users. 
NOTE: For more information about how to register an application on your tenant's AAD follow [the official AAD B2C documentation](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-graph-dotnet#register-your-application-in-your-tenant).

```csharp
// Create a client instance
var client = new GraphApiClient(
    "<ApplicationID>", 
    "<ApplicationSecret>", 
    "<tenantname>.onmicrosoft.com");

// Ensuring client's initialization validates two things
// * authorization credentials provided
// * ensures B2C extension application
// NOTE: this is not necessary though, will be called internally when needed.
await client.EnsureInitAsync();
```

#### Using an external delegate for authentication
After the creation of an application on Azure Active Directory and generating the secret key for it, and assigning the necessary rights, we can start using GraphAPI to manage the B2C users. 

```csharp
// Setup the authorization on your actual implementation -the following is just a pseudocode sample.
var externalAuthenticator = ...[your external authentication library, eg: ADAL];
var authorizationCallback = new Func<string, Task<TokenWrapper>>(
    async resource => await externalAuthenticator.AcquireTokenAsync(parameters));

// Create a client instance which relies on the external autheticator to provide the access token.
var client = new GraphApiClient(
    "<tenantname>.onmicrosoft.com", 
    authorizationCallback);
```

## [User Operations >>](users)

## [Application Operations >>](applications)

## [Usage Reporting >>](reporting)
