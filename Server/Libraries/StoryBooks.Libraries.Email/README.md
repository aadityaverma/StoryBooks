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
	"EmailSettings:ViewsLocation": "{path}",  // Example: '/Resources/EmailTemplates'
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

### Razor email templates
If you want to use Razor templates for email body you must provide relative path to their location
in the sercrets or appsettings json. In order templates to work straightforward you need to keep a
naming conventions. Each template should be in its own separate folder in this format:
{TemplateName}
	{TemplateName}Email.cshtml
	{TemplateName}EmailModel.cs