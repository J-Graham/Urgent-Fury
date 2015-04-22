open FSharp.Data
open System.Net.Http
open FSharp.Data.FreebaseOperators
open Microsoft.FSharp.Data.TypeProviders
open System.Linq
open System.Data.Linq

//--------------------------------------------------------------------------------------
// NOTE: commented out code at the end was for the old method using SqlDataConnection TypeProvider. 
// This works, but there is no bulk insert and can't use user-defined table type in stored procedure


// Freebase Provider -> this way allows you to use an api key, which we'll probably need for running a lot of queries
type fbprov = FreebaseDataProvider<("API KEY HERE")>
let fb = fbprov.GetDataContext()


// SQL Programmaility Provider
type sqlprog = SqlProgrammabilityProvider<"Data Source=SAGER\SQLEXPRESS;Initial Catalog=Books;User ID= sa_Books;password=booky88;Integrated Security=SSPI;">
type bookTable = sqlprog.dbo.``User-Defined Table Types``.tt_BookNames
type bulkInsert = sqlprog.dbo.Books_BulkInsert

let bulker = new bulkInsert("Data Source=SAGER\SQLEXPRESS;Initial Catalog=Books;User ID= sa_Books;password=booky88;Integrated Security=SSPI;")
// note: would love to remove the redundant connection string, but when I tried to set a value it threw an error saying that 
// the connection string had to be static. Needs work.


// Get list of books from Freebase
let bookList = 
    fb.``Arts and Entertainment``.Books.Books

// batch insert
let rec batchGet n offset ender = 
    let insertList = 
        bookList
        |> Seq.skip(offset)
        |> Seq.truncate(n)
        |> Seq.map(fun book -> bookTable(title = book.Name))
    bulker.Execute(insertList) |> ignore
    match offset with
    | i when i + n < ender -> batchGet n (offset + n) ender
    | _ -> ignore



// run the job!
[<EntryPoint>]
let main argv = 
    try
        let runBatches = batchGet 10 0 20
        runBatches |> ignore
    with
        | exn -> System.Console.WriteLine(exn.ToString)
    
    System.Console.WriteLine("Success!")
    System.Console.ReadKey() |> ignore
    0


    //---------------------------------------------------------------------
    // type dbSchema = SqlDataConnection<"Data Source=SAGER\SQLEXPRESS;Initial Catalog=Books;User ID= sa_Books;password=booky88;Integrated Security=SSPI;", StoredProcedures = true>
    // let bookDB = dbSchema.GetDataContext()

    //let addToDB (book : fbprov.ServiceTypes.Book.Book.BookData) =
    //    System.Console.WriteLine book.Name //for debugging
    //    new dbSchema.ServiceTypes.Books1(Title = book.Name)
    //
    //    let rec batchGet n offset ender = 
    //      let insertList = 
    //          bookList
    //            |> Seq.skip(offset)
    //            |> Seq.truncate(n)
    //            |> Seq.map addToDB
    //            |> bookDB.Books1.InsertAllOnSubmit
    //          match offset with
    //          | i when i + n < ender -> batchGet n (offset + n) ender
    //          | _ -> ignore

    // let x = batchGet 10 0 20
    // bookDB.DataContext.SubmitChanges()