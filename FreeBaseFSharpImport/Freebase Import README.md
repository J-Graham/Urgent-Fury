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
    
    
  * Updated with batching and bulk insert.
  
## TODO:
  - [x] ~~Batching~~
  - [x] ~~Bulk Insert POC~~
  - [ ] All data -  having an issue with Genre so far
  - [ ] Proper relational data
  - [ ] Resilient error handling
