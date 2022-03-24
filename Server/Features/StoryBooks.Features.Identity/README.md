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
### 1. Add this values into the secrets.json:
```JSON
{
	"Authentication:Secret": "{Common secret key}",
	"ConnectionStrings:Identity": "{SQL server connection string}",
}
```

### 2. Add this section into appsettings.json:
```JSON
{
	"IdentitySettings": {
		"MinPasswordLength": {int},
		"RequireDigit": {bool},
		"RequireLowercase": {bool},
		"RequireNonAlphanumeric": {bool},
		"RequireUppercase": {bool}
	}
}
```

### 3. Add reference to the StoryBooks.Features.Identity to the selected Web Service


### 4. Add this line in application builder services:
```C#
services.AddIdentityFeature(configuration);
```

### 5. Make sure to include Email service library in your web configurations. This Feature depends on it.

## API

### Account
Path prefix:  /api/v1
|Functionality			|Path											|Return Codes	 |
|-----------------------|-----------------------------------------------|----------------|
|User details			|GET /account									|200 OK			 |
|						|												|404 Not Found	 |
|Register				|POST /account									|200 OK			 |
|						|												|400 Bad Request |
|Update user details	|PUT /account									|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |
|Login user				|POST /account/login							|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |

### Account manage
Path prefix:  /api/v1
|Functionality			|Path											|Return Codes	 |
|-----------------------|-----------------------------------------------|----------------|
|Change password		|POST /account/manage/password					|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |
|Confirm email			|GET /account/manage/email{userId}/{token}		|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |

## Development In Progress
* Enable two factor authentication
