# Applications

## Basic operations on Applications

The GraphLite client allows the following operations on the Applications to facilitate management.

### Get special B2C extensions application
Retrieves the default reserved application responsible for managing the B2C tenant.

```csharp
// Get the reserved B2C application
var app = await client.GetB2cExtensionsApplicationAsync();
```

### Get application's extension properties
Retrieves custom user (extension) properties defined for an application in the B2C tenant.

```csharp
// Get the reserved B2C application
var app = await client.GetB2cExtensionsApplicationAsync();

// Get the custom properties defined
var appId = app.ObjectId;
var extensionProperties = await client.GetApplicationExtensionsAsync(appId);
```

[<< Go back](./)