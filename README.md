# GraphLite

[![NuGet](https://img.shields.io/nuget/dt/GraphLite.svg)](https://www.nuget.org/packages/GraphLite/)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat)](http://makeapullrequest.com)

This is a lightweight Graph API client for .NET (Framework 4.5 and Standard 2.0) for the user management and reporting needs of an Active Directory B2C Client tenant. 

More detailed documentation on: **https://nianton.github.io/GraphLite**

Updates in version **1.2.5**
> * Added 2 new properties to User (UserState and UserStateChangedOn) -new in AAD B2C

Updates in version **1.2.2**
> * Added support for .NET 4.6.1 (less dependencies)

Updates in version **1.2.1**
> * Added support for .NET 4.5
> * Targets .NET 4.5 and NetStandard 2.0

### Installation
You can install the package [from NuGet](http://nuget.org/packages/GraphLite/) using the Visual Studio Package Manager or NuGet UI:

```
PM> Install-Package GraphLite
```

or the `dotnet` command line:

```
dotnet add package GraphLite
```

## User (and related entities) Management
* Users
  * Create User (supports extended B2C properties)
  * Update User (supports extended B2C properties)
  * Reset User Password
  * Delete User ([Enable User Deletion for an Application](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-graph-dotnet#configure-delete-permissions-for-your-application))
  * Get User by ObjectId
  * Get multiple Users by their ObjectIds
  * Get all Users
  * Find Users based on an OData query (supports querying with extended B2C properties)
* Groups (and user membership)
  * Create Group
  * Update Group
  * Delete Group
  * Add Member to Group
  * Remove Member from Group
  * Get Group by ObjectId
  * Get multiple Groups by their ObjectIds
  * Get all Groups
* Extension properties <br/>the custom properties defined via the Azure Portal AAD B2C administration
  * Support for extension properties on User
* Applications

## Usage Reporting API client
Usage reporting client for B2C included in version 1.2 (soon to be published) which will allow the following:
* Get User Counts (local accounts, external identity providers etc)
* Get daily authentication counts for a period
* Get authentication counts summary for the last 30 days
* Get daily MFA requests count for a period
