module Week2.Starter

// 2.10
let test(c,e) = if c then e else 0
    // 1 type of test bool*int -> int
    // 2 evaluation of test(false, fact(-1)) will evaluate fact(-1) before test is executed
    // 3 evalutaion of if false then fact -1 else 0 will not evaluate fact -1

// 2.11
let VAT n x = x * (1.0 + float n / 100.0)
let unVAT n x = x / (1.0 + float n / 100.0)
    
// 2.12
let min =
    let minRec n = function
        | f when f(n) = 0 -> n
        | f -> f(n+1)
    minRec 1


// 3.1
let orderTime (t1: int*int*string, t2) =
    let (_, _, f1) = t1
    let (_, _, f2) = t2
    if (f1 = f2)
    then
        if t1 < t2 then t1 else t2
    else
        if f1 < f2 then t1 else t2

type Time = { f: string; hour: int; min: int; }

let orderTimeR (t1: Time, t2) =
    if (t1 < t2) then t1 else t2

// 3.2
let private add a b c size = ((a+b+c) % size, (a+b+c) / size)
let private subtract a b c size = if a-b-c < 0 then (size+a-b-c, 1) else (a-b-c, 0)

let (+.) (pe1, sh1, po1) (pe2, sh2, po2) =
    let (pence, carryP) = add pe1 pe2 0 12
    let (shilling, carryS) = add sh1 sh2 carryP 20
    (pence, shilling, po1+po2+carryS )
    
let (-.) (pe1, sh1, po1) (pe2, sh2, po2) =
    let (pence, borrowP) = subtract pe1 pe2 0 12
    let (shilling, borrowS) = subtract sh1 sh2 borrowP 20
    (pence, shilling, po1 - po2 - borrowS)
    
// Record type
type GBPR = { pence: int; shilling: int; pounds: int }

let (.+.) gbp1 gbp2 =
    let (pence, carryP) = add gbp1.pence gbp2.pence 0 12
    let (shilling, carryS) = add gbp1.shilling gbp2.shilling carryP 20
    { pence = pence; shilling = shilling; pounds = gbp1.pounds + gbp2.pounds + carryS }

let (.-.) gbp1 gbp2 =
    let (pence, borrowP) = subtract gbp1.pence gbp2.pence 0 12
    let (shilling, borrowS) = subtract gbp1.shilling gbp2.shilling borrowP 20
    { pence = pence; shilling = shilling; pounds = gbp1.pounds - gbp2.pounds - borrowS }
    
// 4.8
let split xs =
    let rec splitRec = function
        | (_, [])           -> ([], [])
        | (even, (x) :: xs) ->
            let (fst, snd) = splitRec (not even, xs)
            if even then (x::fst, snd) else (fst, x::snd)
    splitRec (true, xs)

// 4.9
let rec zip = function
    | [], []       -> []
    | x::xs, y::ys -> x :: y :: zip (xs, ys)
    | _ -> failwith "Argument"

// 4.11

module WeaklyAssList =
    // 1)
    let rec count = function
        | ([], _) -> 0
        | (y :: _, x) when x < y -> 0
        | (y :: ys, x) when x = y -> 1 + count(ys, x)
        | (_ :: ys, x) -> count(ys, x)
        
    // 2)
    let rec insert = function
        | ([], x) -> [x]
        | (y :: ys, x) when y >= x -> x :: y :: ys
        | (y :: ys, x) when y < x -> y :: insert(ys, x)
    
    // 3)
    let rec intersect = function
        | ([], _)                       -> []
        | (_, [])                       -> []
        | (x :: _, y :: _) when x > y   -> []
        | (x :: xs, y :: ys) when x < y -> intersect (xs, y::ys)
        | (x :: xs, y :: ys) when x = y -> x :: intersect (xs, ys)
        
    // 4)
    let rec plus = function
        | ([], [])                       -> []
        | (xs, [])                       -> xs
        | ([], ys)                       -> ys
        | (x :: xs, y :: ys) when x <> y ->
            let next = if x < y then x else y
            let xs' = if x < y then xs else x :: xs
            let ys' = if y < x then ys else y :: ys
            next :: plus(xs', ys')
        | (x :: xs, y :: ys) when x = y  -> x :: plus (xs, y :: ys)
        
    // 5)
    let rec minus = function
        | ([], _) -> []
        | (xs, []) -> xs
        | (x :: xs, y :: ys) when x < y -> x :: minus (xs, y::ys)
        | (x :: xs, y :: ys) when x > y -> minus (x :: xs, ys)
        | (x :: xs, y :: ys) when x = y -> minus(xs, ys)

// 5.1
let filter pred xs =
    List.foldBack (fun a b -> if (pred a) then a :: b else b) xs []

// 5.2
let revrev xs =
    let revInner ys =
        List.foldBack (fun a b -> b @ [a]) ys []
    List.foldBack (fun a b -> b @ [revInner a]) xs []

