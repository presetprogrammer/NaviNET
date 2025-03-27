open System.IO
open System

let args = fsi.CommandLineArgs
let mutable file = ""
let mutable offset = 0
let mutable fileIndex = false
let mutable offsetIndex = false
for arg in Environment.GetCommandLineArgs() do
    if arg = "-f" then
        fileIndex <- true
    elif arg = "-o" then
        offsetIndex <- true
    elif fileIndex then
        file <- arg
        fileIndex <- false
    elif offsetIndex then
        offset <- int arg
        offsetIndex <- false
let immutable args: bool = Environment.GetCommandLineArgs().Length = 0
#if args
    
#endif

type filemanager() =
    member x.fopen(filename: string) =
        File.Open(filename, FileMode.Open)
        
    member x.FClose(file: FileStream) =
        file.Close()

type cursor() = 
    member x.LineSelect(file: FileStream, offset: int) =
        file.Seek(int64 offset, SeekOrigin.Begin) |> ignore

[<EntryPoint>]
let main(args: string array) = 
    let mutable loop = true
    let filemanager = filemanager()
    let cursor = cursor()
    let stream = filemanager.fopen "test.txt"
    while loop do
        cursor.LineSelect(stream, offset)
        loop <- false
    filemanager.FClose stream

    0 //return 0