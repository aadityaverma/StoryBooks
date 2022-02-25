## Email helper library

### Description
This library contains common Email sender class that is used accross the whole application
for sending emails. Internaly it uses SendGrid. 

### Configuration
**Add this variables into the secrets.json:**
```
{
	...
	"EmailSettings:SendGridKey": "{key}",
	...
}
```

**Add this section to appsettings.json**
```
{
	...
	EmailSettings: {
		SenderName: "{name}",
		SenderAddress: "{email}"
	},
	...
}
```

**Add this line in application builder services:**
```
services.AddEmail(configuration);
```