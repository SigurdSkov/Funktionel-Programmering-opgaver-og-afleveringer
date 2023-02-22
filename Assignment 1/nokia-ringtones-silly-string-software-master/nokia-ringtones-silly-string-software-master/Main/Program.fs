
open System
open WavePacker

// TODO 8 take input from file/cmd and save it to a file
[<EntryPoint>]
let main argv =
    //"1d2 4d2 4e2 2#f2 1g2 4a2 4a1 4d2 4#c2 8#f2 8#f2 8e2 4#f2 4e2 4#f2 4e2 8e2 4b2 4a2 8#f2 8#f2 8e2 4#f2 4e2 4d2 4d2 8e2 2e2 8#f2 8#f2 8e2 4#f2 4e2 4#f2 4e2 8e2 4b2 4a2 8#f2 8#f2 8e2 4#f2 4e2 4d2 4d2 8e2 2e2"

    match Assembler.assembleToPackedStream "1d2 4d2 4e2 2#f2 1g2 4a2 4a1 4d2 4#c2 8#f2 8#f2 8e2 4#f2 4e2 4#f2 4e2 8e2 4b2 4a2 8#f2 8#f2 8e2 4#f2 4e2 4d2 4d2 8e2 2e2 8#f2 8#f2 8e2 4#f2 4e2 4#f2 4e2 8e2 4b2 4a2 8#f2 8#f2 8e2 4#f2 4e2 4d2 4d2 8e2 2e2" with
            | Choice2Of2 ms ->
                WavePacker.write "music.wav" ms
            | Choice1Of2 err -> failwith err
    0