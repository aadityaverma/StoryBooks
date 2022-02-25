## Identity Feature

**1. Add this values into the secrets.json:**
```
{
	...
	"Authentication:Secret": "{Secret key}",
	"ConnectionStrings:DefaultConnection": "{SQL server connection string}",
	...
}
```

**2. Add this section into appsettings.json:**
```
{
	...
	"IdentitySettings": {
		"MinPasswordLength": {int},
		"RequireDigit": {bool},
		"RequireLowercase": {bool},
		"RequireNonAlphanumeric": {bool},
		"RequireUppercase": {bool}
	},
	...
}
```

**3. Add reference to the StoryBooks.Features.Identity to the selected Web Service**


**4. Add this line in application builder services:**
```
services.AddIdentityFeature(configuration);
```