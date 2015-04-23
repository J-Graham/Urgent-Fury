## Freebase Import README

  * Imports book information from Freebase. Job now functional, though requires error handling. I've processed about 1000 records at once, but was hit with a WebExcepion towards the end of the job. Still need to investigate. Could improve the null hanlding. Also would love to try to do some asychronous processing, as the biggest hit time-wise is waiting for the API.
  
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
    
    
  
## TODO:
  - [ ] Resilient error handling!!
  - [ ] Maybe explore some async options
