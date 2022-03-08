# Identity Feature

## Description
This feature contains all user account actions:
* Register
* Login
* Change passwords
* Reset password
* Update user details
* Confirm user email
* Enable two factor authentication

## Setup
**1. Add this values into the secrets.json:**
```C#
{
	...
	"Authentication:Secret": "{Secret key}",
	"ConnectionStrings:DefaultConnection": "{SQL server connection string}",
	...
}
```

**2. Add this section into appsettings.json:**
```JSON
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
```C#
services.AddIdentityFeature(configuration);
```

**5. Make sure to include Email service library in your web configurations. This Feature depends on it.**

## Development In Progress
* Fluid email templates
* Confirm user email
* Reset password
* Enable two factor authentication
