//Mangler:
//Hjælpefunktioner
//let toSave s ? parser_type a .>> noget1 behold det på ? siden og giv til ???
//>>.
//.>>
//spaces
//restOfLineAfter
//anyCharsTill
//lookAhead

module Parser

open FParsec

type Token = { duration: uint8; note: float; octave: uint8 }

//http://www.quanttec.com/fparsec/reference/charparsers.html#members.pfloat
//http://www.quanttec.com/fparsec/tutorial.html#parsing-a-single-float
let DurationParser: Parser<uint8, unit> =
    pint32 >>= fun duration -> match duration with
        |1 |2 |4 |8 |16 |32 -> preturn ((byte)duration)
        | _ -> failFatally "Invalid duration - RTTL protocol" //delete Fatally if it is desired to run the rest of the parsing
//unit er den interne state
//>>= kør venstreside, anvend resultat, byg & kør ny PARSER

let NoteParser: Parser<float, unit> = //Noder ændres per halve med #, duration ændrer sig ikke
//http://www.quanttec.com/fparsec/reference/parser-overview.html 6.1.6
    choice [
        pchar   'a'     >>% 1.0
        pstring "#a"    >>% 1.5
        pchar   'b'     >>% 2.0
        pchar   'c'     >>% 3.0
        pstring "#c"    >>% 3.5
        pchar   'd'     >>% 4.0
        pstring "#d"    >>% 4.5
        pchar   'e'     >>% 5.0
        pchar   'f'     >>% 6.0
        pstring "#f"    >>% 6.5
        pchar   'g'     >>% 7.0
        pstring "#g"    >>% 7.5
    ]
//    opt pchar //#
//    pchar >>= fun note => match note with
//    |'a' -> preturn 1 |'b' -> preturn 3
    //If score contains string

let OctaveParser: Parser<uint8, unit> =
    pint32 >>= fun octave -> match octave with
        |1 |2 |3 -> preturn ((byte)octave)
        | _ -> failFatally "Invalid octave - RTTL protocol"

let TokenParser =
    DurationParser //opt can be used if it might be there. If it isn't... It still has to be there... What do? Idunno, ask Henrik
    >>= fun duration -> 
    NoteParser >>= fun note ->
    OctaveParser >>= fun octave ->
    preturn  {duration = duration; note = note; octave = octave}

let pScore: Parser<Token list, unit> = (sepBy TokenParser (pchar ' ')) // TODO 2 builder parser

let parse (input: string): Choice<string, Token list> =
    match run pScore input with
    | Failure(errorMsg,_,_)-> Choice1Of2(errorMsg)
    | Success(result,_,_) -> Choice2Of2(result)

// Helper function to test parsers
let test (p: Parser<'a, unit>) (str: string): unit =
    match run p str with
    | Success(result, _, _) ->  printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Oops Failure: %s" errorMsg


// TODO 3 calculate duration from token.
// bpm = 120 (bpm = beats per minute)
// 'Duration in seconds' * 1000 * 'seconds per beat' (if extended *1.5)
// Whole note: 4 seconds
// Half note: 2 seconds
// Quarter note: 1 second
// Eight note: 1/2 second
// Sixteenth note 1/4 second
// thirty-second note: 1/8 second
let durationFromToken (token: Token): float = 
    let durationVal = float token.duration
    (durationVal ** -1.0) * 4.0 * 1000.0 //Jeg har ændret (* 1.5). Jeg kan simpelthen ikke se hvorfor den er der, og hvad den gør godt for. Det er ikke sådan musik virker

//    match token with
//    | { note = noteVal } when noteVal = 1.5 || noteVal = 3.5 || noteVal = 4.5 || noteVal = 6.5 || noteVal = 7.5
//                -> (durationVal ** -1.0) * 4.0 * 1000.0// * 1.5
//                //durationVal * 2.0 * 1000.0 * 1.5
//    | { note = noteVal } when noteVal = 1.0 || noteVal = 2.0 || noteVal = 3.0 || noteVal = 4.0 || noteVal = 5.0 || noteVal = 6.0 || noteVal = 7.0
//                -> (durationVal ** -1.0) * 4.0 * 1000.0
//                //durationVal * 2.0 * 1000.0
//    | _ -> failwith "Incalculable note value - Something went wrong in durationFromToken - Durationparser likely couldn't find a number, or noteparser couldn't find a #" //For some reason "failFatally does not work this time. I want it to

// TODO 4 calculate overall index of octave
// note index + (#octave-1) * 12
let overallIndex (note, octave) = 
    (note-1.0) + float (octave - 1uy) * 12.0

// TODO 5 calculate semitones between to notes*octave
// [A; A#; B; C; C#; D; D#; E; F; F#; G; G#]
// overallIndex upper - overallIndex lower
let semitonesBetween lower upper = 
    overallIndex upper - overallIndex lower

// TODO 6
// For a tone frequency formula can be found here: http://www.phy.mtu.edu/~suits/NoteFreqCalcs.html
// 220 * 2^(1/12) ^ semitonesBetween (A1, Token.pitch) 
let frequency (token: Token): float =
    220.0 * (2.0 ** (1.0/12.0)) ** float (semitonesBetween (1.0, 1uy) (token.note, token.octave))
//Jeg tror det er ved frequency og de dertilhørende udregninger at kæden hopper af.