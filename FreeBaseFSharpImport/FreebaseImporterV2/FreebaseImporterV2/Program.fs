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
type bookTable = sqlprog.dbo.``User-Defined Table Types``.tt_BookAuthorImport
type bulkInsert = sqlprog.dbo.Books_BulkInsert

let bulker = new bulkInsert("Data Source=SAGER\SQLEXPRESS;Initial Catalog=Books;User ID= sa_Books;password=booky88;Integrated Security=SSPI;")
// note: would love to remove the redundant connection string, but when I tried to set a value it threw an error saying that 
// the connection string had to be static. Needs work.


// Get list of books from Freebase
let bookList = 
    fb.``Arts and Entertainment``.Books.Books



let addBookFields (book : fbprov.ServiceTypes.Book.Book.BookData) = 
    let genres =  
        book.Genre |> Seq.map(fun g -> if g <> null then g.Name + "|" else "") |> System.String.Concat
    let author =
        if book.Author.FirstOrDefault() <> null then book.Author.FirstOrDefault().Name else ""
    // -- Comment out here when not debugging
    let line = sprintf "%s %s %s" book.Name author genres
    System.Console.WriteLine line
    // --
    bookTable(Title = book.Name, Author = author, Genres = genres)
    

// batch insert
let rec batchGet n offset ender = 

    let insertList = 
        bookList
        |> Seq.skip(offset)
        |> Seq.truncate(n)
        //|> Seq.map(fun book -> bookTable(title = book.Name))
        |> Seq.map addBookFields
    bulker.Execute(insertList) |> ignore
    match offset with
    | i when i + n < ender -> batchGet n (offset + n) ender
    | _ -> ignore



// run the job!
[<EntryPoint>]
let main argv = 
    try
        let runBatches = batchGet 10 0 100
        runBatches |> ignore
        System.Console.WriteLine("Success!")
    with
        | exn -> System.Console.WriteLine(exn.InnerException.ToString)
    
    
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
//    // bookDB.DataContext.SubmitChanges()
//
//
//    let genres =  
//        match book.Genre with
//        | null -> ""
//        | _ -> Seq.map(fun (g : fbprov.ServiceTypes.Media_common.Media_common.Literary_genreData) -> g.Name + "|") |> System.String.Concat
//    let author =
//        book.Author.FirstOrDefault()
//    let authorName =
//        match author with
//        | null -> ""
//        | _ -> author.Name