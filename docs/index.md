# GraphLite.NET

GraphLite is a lightweight GraphAPI client for .NET (.NET standard package) for the user management and reporting needs of an Azure Active Directory B2C tenant.

## Getting Started
After creating the AAD B2C tenant on Azure Portal, in order to use the GraphAPI to manage users and access reports an application has to be registered on the Azure Active Directory of the created tenant (NOT on the B2C menu item).

### Authentication
After creating the application and generating the secret key for it, and assigning the necessary rights, we can start using GraphAPI to manage the B2C users. 

```csharp
var client = new GraphApiClient(
    "<ApplicationID>", 
    "<ApplicationSecret>", 
    "<tenantname>.onmicrosoft.com");

var users = await client.UserGetAllAsync();
```

The acquisition of the access token is taken care by the GraphApiClient, and you can keep a reference of it for the lifetime of your application in order to minimize the overhead of acquiring a new access token for each new instance created.

### [User Operations >>](users)
