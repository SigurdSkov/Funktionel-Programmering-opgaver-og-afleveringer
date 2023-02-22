module Clock.Lecture2

open System

let multiply a b = a * b

let multiplyBy42 = multiply 42


let add1 a = 2 + a
let apply fn a = fn a

let result = apply (fun a -> a+2
                   ) 2

let a = [1;2;3;5;6]
        |> List.filter (fun a -> a < 4)
        |> List.map (fun a -> string a)
        |> List.map (fun a -> "-" + a)
        
let temp1 = List.filter (fun a -> a < 4) [1;2;3;5;6]
let temp2 = List.map (fun a -> string a) temp1
let temp3 = List.map (fun a -> "-" + a) temp2


let addOne a = a+1
let convertToFloat a = float a

let result1 a = convertToFloat (addOne a)

type Course = { name: string; semester: string;
                students: int list; teacher: int}
//    member = Default = ..


type CourseM = { name: string; mutable semester: string;
                students: int list; teacher: int}

let mutable abc = 3
abc <- 4


let getCourseName (course: Course) = course.name 

let swafp = {name = "SWAF"; semester = "F21"; 
             students = []; teacher = 33 }

let newResult a = match a with
                  | { students = students; } when List.length students > 3 ->  students
                  | _ -> []
                  
                  
let multiplyFloats (a:float) (b:float) : float = a * b

let min' f a =
    f a

let min f =
    min' f 1
    
let fn a = match a with                        
            | [] -> "empty"
            | x::y::xs -> "asdf"
            | x::xs -> "first element " + (string x);;
// val it : string = "first element a"

let round (celcius: float) = Math.Round(celcius)
let convert fahrenheit = ((float fahrenheit) - 32.0) * (5.0/9.0)
let tempSF = [59;61;62;64;63;67;66;67;70;70;64;64;58;64]
             |> List.filter (fun temp -> temp > 60)
             |> List.map convert 
             |> List.map round













        
        
        
        
        
        
        
        
        
        
         