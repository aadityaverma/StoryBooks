# Email service library

## Description
This library contains Email service that is configured for easy email messages sending.
Internaly it uses SendGrid and few template engines. Email services is made realy easy
for configuration and usege through the code. Here is example of method sending user 
registration email: 
```
public async Task SendUserRegisteredEmail(User user)
{
    var emailModel = new SendEmailModel<UserRegistrationEmailModel>(
        to: user.UserName,
        subject: Email.UserRegistrationSubject,
        model: this.mapper.Map<UserRegistrationEmailModel>(user));

    await this.emailService.SendAsync(emailModel);
}
```
Thats it! If you structure your code properly, the email service will find your template 
file, build your email message content with the give data and send it to the given use. 

## Setup
### Add this variables into the secrets.json:
```
{
	...
	"EmailSettings:SenderApiKey": "{key}",
	...
}
```

### Add this section to appsettings.json
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

### Registering email service in your startup configuration:
You have two options depending on wich template engine you 
deside to user. Currently supported engines are Razor and Fluid.
```
services.AddEmailWithRazor(configuration);  // Disabled at the moment
services.AddEmailWithFluid(configuration);
```

### Additional configurations
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

## Templates naming conventions and locations
By default email templates shoud be placed in:
/Resources/EmailTemplates

Each email template has its own folder with template
file and template model files. Structure:
|{TemplateName}
|->{TemplateName}Email.html  => For Fluid templates
|->{TemplateName}Email.cshtml => For Razor templates
|->{TemplateName}EmailModel.cs

## Email service with Razor templates
Razor templates are using RazorLight project: https://github.com/toddams/RazorLight
This is because of the sealing of IFileProvider setting since .net 3.
It is available with RazorRuntimeCompilation package but it adds a lot of weight to the project.
Downside of this approach for templetization of emails is that Razor library takes a lot of 
resources. In the latest .net 6 version runtime compilation addsadditional 20mb to the 
application and 100mb usage of application memory just to compile small cshtml file. 

## Email service with Fluid templates
I think that this is the better approach to email tempating. Fluid is wrapper library
over Liquid template engine for better work with .net core. It is very light and fas 
and easy to learn. Here is reference to the two projets:
https://github.com/sebastienros/fluid#using-fluid-in-your-project
https://shopify.github.io/liquid/
