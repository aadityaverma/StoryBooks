## Email library

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