open canopy
open runner
open System
open SplashScreen
open Constants

Console.WriteLine("Launching e2e Tests")

start phantomJS
pin Left

Console.WriteLine("Launched PhantomJs")

Console.WriteLine("Launching Fake API Server")


SplashScreen.All baseUrl

run()

System.Environment.ExitCode <- runner.failedCount
Console.WriteLine(String.Format("{0} passed, {1} failed", runner.passedCount, runner.failedCount))

let a = Console.ReadKey()
quit()
