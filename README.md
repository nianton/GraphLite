# GraphLite

This is a lightweight Graph API client for the user management needs of an Active Directory B2C Client tenant. The entities that can be managed via this client are:

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