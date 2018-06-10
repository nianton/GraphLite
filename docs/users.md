# Users

## Basic Operations on Users

The GraphLite client allows the all the following operations on the Users to facilitate management.

### Get User
Retrieves a single user by its object identifier.
```csharp
var userObjectId = "<user-id>";
var user = await client.UserGetAsync(userObjectId);
```

### Get Users
Retrieves a list of Users based on a given OData query. 
```csharp
// Sample User query
var query = new ODataQuery<User>()                
    .Where(u => u.GivenName, "Nikos", ODataOperator.Equals) // Filter by GivenName
    .Top(10) // Fetch up to 10 users
    .OrderBy(u => u.DisplayName); // Orders the resultset by DisplayName

var users = await client.UserGetListAsync(query);

var hasMoreResults = !string.IsNullOrEmpty(users.SkipToken);
if (hasMoreResults) 
{
    query = query.SkipToken(users.SkipToken);
    var nextUsers = await client.UserGetListAsync(query);
}
```
When the returned resultset does not contain all the matched entities, the SkipToken property of the returned GraphList<User> has a value which can be used to get the next page of results, and can be used as in the example above.

### Get All Users
Retrieves all users in a single operation for a given query. Use with caution when large result sets are expected.

```csharp
var totalCount = 0;

// Optional progress reporting
var progress = new Progress<IList<User>>(pagedUsers => 
{ 
    // Report progress here
    totalCount += pagedUsers.Count; 
});

var query = new ODataQuery<User>();
var itemsPerPage = 10;
var allUsers = await client.GetUserAllAsync(query, itemsPerPage, progress);
```

### Create a User
Create a user on the B2C tenant. 
```csharp
var user = new User
{
    CreationType = "LocalAccount",
    AccountEnabled = true,
    GivenName = $"John",
    Surname = $"Smith",
    DisplayName = $"Megatron",
    SignInNames = new List<SignInName>
    {
        // Uniqueness will be enforced on SignInName values
        new SignInName()
        {
            Type = "emailAddress",
            Value = "john@smith.com" 
        }
    },
    PasswordProfile = new PasswordProfile
    {
        EnforceChangePasswordPolicy = false,
        ForceChangePasswordNextLogin = false,
        Password = "123abC!!"
    }
};

// Supports extended B2C tenant properties e.g. for a property 
// 'TaxRegistrationNumber' registered on the tenant:
user.SetExtendedProperty("TaxRegistrationNumber", "120498219");

// Returns the created user with the generated object identifier.
var createdUser = await client.UserCreateAsync(user);
```

### Delete a User
Delete a user by its object identifier -it remains as a deleted object in the B2C tenant for a period of 30 days. 
```csharp
var userObjectId = "<user-id>";
var user = await client.UserDeleteAsync(userObjectId);
```
***NOTE**: Special rights have to assigned to the authenticated principal of the client to allow User deletions.*

### Update a User
Updates a user by its object identifier and the properties affected. 
```csharp
var userObjectId = "<user-id>";
var userChanges = new { displayName = "Megatron" };
var user = await client.UserUpdateAsync(userObjectId, userChanges);
```
***NOTE**:  This call will be revisited to allow for a more strongly typed approach.*

### Get a User's thumbnail

Retrieves a User's thumbnail image data (usually JPEG format).

```csharp
var userObjectId = "<user-id>";
byte[] imageData =  await client.UserGetThumbnailAsync(userObjectId);
```

### Update a User's thumbnail

Retrieves a User's thumbnail image data (usually JPEG format).

```csharp
var userObjectId = "<user-id>";
var imageData = new byte[0]; // Thumbnail's byte array
var contentType = "image/jpeg";

await client.UserUpdateThumbnailAsync(userObjectId, imageData, contentType);
```
***NOTE**: Max allowed size for an image (as of now) is **100 kB**.*

## User Querying

[More documentation to be added soon...]

[<< Go back](./)