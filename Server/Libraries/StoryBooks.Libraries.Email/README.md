## Email helper library

### Description
This library contains common Email sender class that is used accross the whole application
for sending emails. Internaly it uses SendGrid.

### Configuration
**Add this variables into the secrets.json:**
```
{
	...
	"EmailSettings:SenderApiKey": "{key}",
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

**Registering email service in your startup configuration:**
You have two options depending on wich template engine you 
deside to user. Currently supported engines are Razor and Fluid.
```
services.AddEmailWithRazor(configuration);  
services.AddEmailWithFluid(configuration);
```

**Additional configurations**
You can provide your own file provider that will be used from
the template renderers. For example:
```
...
services.Configure<EmailSettings>(c =>
{
    c.Dev.SetFileProvider(new CustomFileProvider());
});
...
```

### Templates naming conventions and locations
By default email templates shoud be placed in:
/Resources/EmailTemplates

Each email template has its own folder with template
file and template model files. Structure:

{TemplateName}
	{TemplateName}Email.html  => For Fluid templates
	{TemplateName}Email.cshtml => For Razor templates
	{TemplateName}EmailModel.cs


### Email service with Razor templates
Razor templates are using RazorLight project: https://github.com/toddams/RazorLight
This is because of the sealing of IFileProvider setting since .net 3.
It is available with RazorRuntimeCompilation package but it adds a lot of weight to the project.
Downside of this approach for templetization of emails is that Razor library takes a lot of 
resources. In the latest .net 6 version runtime compilation addsadditional 20mb to the 
application and 100mb usage of application memory just to compile small cshtml file. 

### Email service with Fluid templates
I think that this is the better approach to email tempating. Fluid is wrapper library
over Liquid template engine for better work with .net core. It is very light and fas 
and easy to learn. Here is reference to the two projets:
https://github.com/sebastienros/fluid#using-fluid-in-your-project
https://shopify.github.io/liquid/