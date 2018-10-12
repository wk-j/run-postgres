// Learn more about F# at http://fsharp.org

open System
open System.IO
open StartProcess

let createDir() =
    let dir = ".working/mysql"
    if Directory.Exists dir |> not then
        Directory.CreateDirectory |> ignore
    DirectoryInfo(dir).FullName

[<EntryPoint>]
let main argv =
    let dir = createDir()

    "docker stop mysql"
    |> Processor.StartProcess

    "docker rm mysql"
    |> Processor.StartProcess

    [ "docker run"
      "-d"
      "--name mysql"
      "-v {db}:/var/lib/mysql".Replace("{db}", dir)
      "-e MYSQL_ROOT_PASSWORD=1234"
      "-e MYSQL_USER=mysql"
      "-e MYSQL_PASSWORD=1234"
      "-e MYSQL_DATABASE=mysql"
      "-p 3306:3306"
      "mysql:5.5" ]
    |> String.concat " "
    |> Processor.StartProcess

    0