# Client application

## Technology
We are using Ionic with react to build native experience for every platform.

## Hosting
This is PWA enabled application that is hosted in Firebase.
To deploy new version use this commands:

`ionic build --prod`
`firebase hosting:channel:deploy stage`

To deploy to production use:
`firebase hosting:clone story-books-d0c11:stage story-books-d0c11:live`