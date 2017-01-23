module FakeApi
open System.Threading
open Suave
open Suave.Operators
open Suave.Successful
open Suave.Filters
open System.IO
open System

let json fileName =
    let content = File.ReadAllText fileName
    content.Replace("\r", "").Replace("\n","")
    |> OK >=> Writers.setMimeType "application/json"

let Create url ([<ParamArray>] args: WebPart<HttpContext> list) = 
    let cts = new CancellationTokenSource()
    let conf = { defaultConfig with cancellationToken = cts.Token }
    let listening, server = startWebServerAsync conf (choose args)
    Async.Start(server, cts.Token)
    printfn "Make requests now"
    cts

let WithTeams =
    let teams = pathScan "/teams" (fun _ -> "Teams.json" |> json) 
    Create "" [teams]