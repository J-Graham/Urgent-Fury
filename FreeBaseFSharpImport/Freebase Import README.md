## Freebase Import README
  * Note: You will need an API Key to get this to work, and in fact to even get the Freebase Type Provider to display without errors.
    * I would have used mine, but it's tied to my google account so I removed it. (although I'm sure I'll forget and push it at somepoint)
    * insert the key here: 
    ```fsharp
    type fbprov = FreebaseDataProvider<("API KEY HERE")>
    let fb = fbprov.GetDataContext()  
    ```
    * You can get one through the Google Developers Console
      * https://console.developers.google.com
      * search APIs for Freebase
      * Enable
      * Go to credentials on the left
      * Create new key
      * Server key
      * I left the IP blank for now, seemed easier, esp. since this won't be a public part of the app
    
    
  * Still a lot of work do be done, but a good start.
  
## TODO:
  1. Batching
  1. All data -  having an issue with Genre so far
  1. Proper relational data
  1. Resilient error handling
