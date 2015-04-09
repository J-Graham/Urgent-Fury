# Freebase F# Import

##Overall Process
1. Create a SQL Database
1. Create tables with schema (See Below)
1. Create F# App
  1. See if there is a way to batch the downloads or record somehow. Goal is to not have to redownload if any part fails.
  1. Import Authors, Genres, and finally books also creating the book/genre relation
1. Once done, we will try it at work with 1m records.

## Schema

### Books

| Column   | DataType     |
|----------|--------------|
| BookID   | int          |
| Title    | varchar(500) |
| AuthorID | int          |

### Authors
| Column   | DataType     |
|----------|--------------|
| AuthorID | int          |
| AuthorName   | varchar(500) |

### Genres
| Column   | DataType     |
|----------|--------------|
| GenreID | int          |
| GenreName   | varchar(500) |

### BookGenres
| Column   | DataType     |
|----------|--------------|
| BookID | int          |
| GenreID  | int |
