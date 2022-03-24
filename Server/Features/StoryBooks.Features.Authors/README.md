# Authors Feature

## Setup
### 1. Add this values into the secrets.json:
```JSON
{
	"Authentication:Secret": "{Common secret key}",
	"ConnectionStrings:Authors": "{SQL server connection string}"
}
```

### 2. Add this section into appsettings.json:
```JSON
{
	"AuthorsSettings": {
		
	}
}
```

### 3. Add reference to the StoryBooks.Features.Authors to the selected Web Service


### 4. Add this line in application builder services:
```C#
services.AddAuthorsFeature(configuration);
```

## API

### Authors
Path prefix:  /api/v1
|Functionality			|Path											|Return Codes	 |
|-----------------------|-----------------------------------------------|----------------|
|Author details			|GET /author{id}								|200 OK			 |
|						|												|404 Not Found	 |
|Become an Author		|POST /author									|200 OK			 |
|						|												|400 Bad Request |
|Update Author details	|PUT /author/{id}								|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |

### Books
Path prefix:  /api/v1/author/{id}
|Functionality			|Path											|Return Codes	 |
|-----------------------|-----------------------------------------------|----------------|
|Get book full details	|GET /book/{bookId}								|200 OK			 |
|						|												|404 Not Found	 |
|List books by author	|GET /books?name=&genre…						|200 OK			 |
|						|												|400 Bad Request |
|Add new book			|POST /book										|201 Created	 |
|						|												|400 Bad Request |
|Update book details	|PUT /book/{bookId}								|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |
|Remove book			|DELETE /book/{bookId}							|200 OK			 |
|						|												|404 Not Found	 |
|Add chapter			|POST /book/{bookId}/chapter					|201 Created	 |
|						|												|400 Bad Request |
|Update chapter details	|PUT /book/{bookId}/chapter/{chapterId}			|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |
|Remove chapter			|DELETE /book/{bookId}/chapter/{chapterId}		|200 OK			 |
|						|												|404 Not Found	 |
|Add user stats			|POST /book/{bookId}/user-stats/				|201 Created	 |
|						|												|400 Bad Request |
|Update user stats		|PUT /book/{bookId}/user-stats/{chapterId}		|200 OK			 |
|						|												|400 Bad Request |
|						|												|404 Not Found	 |
|Remove user stats		|DELETE /book/{bookId}/user-stats/{chapterId}	|200 OK			 |
|						|												|404 Not Found	 |