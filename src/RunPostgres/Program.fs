open System
open StartProcess
open System.IO

let createDir() =
    let dir = ".working/db"
    if Directory.Exists dir |> not then
        Directory.CreateDirectory |> ignore
    DirectoryInfo(dir).FullName

[<EntryPoint>]
let main argv =
    let dir = createDir()

    "docker stop postgres"
    |> Processor.StartProcess

    "docker rm postgres"
    |> Processor.StartProcess

    [ "docker run"
      "-d"
      "--name postgres"
      "-v {db}:/var/lib/postgresql/data".Replace("{db}", dir)
      "-e POSTGRES_PASSWORD=1234"
      "-e POSTGRES_USER=postgres"
      "-p 5432:5432"
      "postgres" ]
    |> String.concat " "
    |> Processor.StartProcess

    0