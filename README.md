# GraphLite

This is a lightweight Graph API client for the user management and reporting needs of an Active Directory B2C Client tenant. More detailed documentation will be soon available on https://nianton.github.io/GraphLite.

The entities that can be managed via this client are:

## User Management
* Users
  * Create User
  * Update User
  * Reset User Password
  * Delete User ([Enable User Deletion for an Application](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-graph-dotnet#configure-delete-permissions-for-your-application))
  * Get User by ObjectId
  * Get multiple Users by their ObjectIds
  * Get all Users
  * Find Users based on an OData query
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
