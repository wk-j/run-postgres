open System
open StartProcess
open System.IO

type Settings = {
    Password: string
    User: string
    Port: string
}

let createDir() =
    let dir = ".working/postgres"
    if Directory.Exists dir |> not then
        Directory.CreateDirectory |> ignore
    DirectoryInfo(dir).FullName

let rec parseSettings argv (settings: Settings) =
    match argv with
    | "--user" :: xs ->
        match xs with
        | user :: xss ->
            parseSettings xss { settings with User = user }
        | _ -> settings
    | "--password" :: xs ->
        match xs with
        | password :: xss ->
            parseSettings xss { settings with Password = password }
        | _ -> settings
    | "--port" :: xs ->
        match xs with
        | port :: xss ->
            parseSettings xss { settings with Port = port }
        | _ -> settings
    | [] -> settings


[<EntryPoint>]
let main argv =
    let dir = createDir()

    let settings = parseSettings (List.ofArray argv) { User = "postgres"; Password = "1234"; Port = "5432" }

    "docker stop postgres"
    |> Processor.StartProcess

    "docker rm postgres"
    |> Processor.StartProcess

    [ "docker run"
      "-d"
      "--name postgres"
      "-v {db}:/var/lib/postgresql/data"
        .Replace("{db}", dir)
      "-e POSTGRES_PASSWORD={password}"
        .Replace("{password}", settings.Password)
      "-e POSTGRES_USER={user}"
        .Replace("{user}", settings.User)
      "-p {port}:5432"
        .Replace("{port}", settings.Port)
      "postgres" ]
    |> String.concat " "
    |> Processor.StartProcess

    printfn "> Host=localhost;User=%s;Password=%s;Port=%s;Database=?"
        settings.User
        settings.Password
        settings.Port

    0