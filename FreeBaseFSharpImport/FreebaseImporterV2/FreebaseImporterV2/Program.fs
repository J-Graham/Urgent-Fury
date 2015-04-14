open FSharp.Data
open System.Net.Http
open FSharp.Data.FreebaseOperators
open Microsoft.FSharp.Data.TypeProviders
open System.Linq
open System.Data.Linq

// Freebase Provider -> this way allows you to use an api key, which we'll probably need for running a lot of queries
type fbprov = FreebaseDataProvider<("API KEY HERE")>
let fb = fbprov.GetDataContext()

// our Book db -> change the connection string accordingly
type dbSchema = SqlDataConnection<"Data Source=ALEX-1525\SQLEXPRESS;Initial Catalog=Books;User ID= sa_Books;password=booky88;Integrated Security=SSPI;">
let bookDB = dbSchema.GetDataContext()
        
// take from Freebase
let bookList = 
    fb.``Arts and Entertainment``.Books.Books.Take(5)

// just a quick test
//let printBookInfo = 
//    for book in bookList do
//    printfn "%s - %s" book.Name (book.Author.FirstOrDefault().Name)
//    book |> ignore

// this function will take a single book and add to our db -> changes will not be submitted until called in the console app
// probably going to need to use a stored proc and merge though for the relational tables...
let addToDB (book : fbprov.ServiceTypes.Book.Book.BookData) = 
    new dbSchema.ServiceTypes.Books1(Title = book.Name)

let addBooks =
   bookList |> Seq.map addToDB

// tell db to add on submit
bookDB.Books1.InsertAllOnSubmit(addBooks)


[<EntryPoint>]
let main argv = 
    //printfn "%A" argv
    try
        bookDB.DataContext.SubmitChanges()
    with
        | exn -> System.Console.WriteLine(exn.ToString)
    
    System.Console.WriteLine("Success!")
    System.Console.ReadKey() |> ignore
    0 // return an integer exit code

