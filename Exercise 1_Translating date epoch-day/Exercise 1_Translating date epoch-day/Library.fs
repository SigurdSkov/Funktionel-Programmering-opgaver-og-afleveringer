namespace Exercise_1_Translating_date_epoch_day

open System

module Part1 =
    let rule1OfLeap y =
        if (y%400 = 0) then true
        else false

    let rule2OfLeap y =
        if (not (y%100 = 0) && y%4 = 0) then true
        else false

    let isLeap y = 
        if rule1OfLeap y = true then true
        else if rule2OfLeap y = true then true
        else false

    let rec LeapYears y =
        if y = 1970 then 0
        else if isLeap y = true then 1 + LeapYears (y-1)
        else LeapYears (y-1)

    let rec Years y =
        if y = 1970 then 1
        else 1+Years (y-1)
        
    let daysToEndYear y =
        Years y*365 + (LeapYears y) 
        //To do this with years before, either make a set of recursive functions that handle addition, or make a check in the existing recursive function to add if below 1970

    let daysToEndMonth m y =
        if m = 1 then
            Console.WriteLine("First month from month ({0}) days from start of year to end of month: {1}", m, (367*m+5)/12)
        else if isLeap y = true then
            Console.WriteLine("Leap year - from month ({0}) days from start of leap year to end of month: {1}", m, (367*m+5)/12 - 1)
        else
            Console.WriteLine("Not leap year - From month ({0}) days from start of year to end of month: {1}", m, (367*m+5)/12 - 2)

    let intDaysToEndMonth m y =
        if m = 1 then
             (367*m+5)/12
        else if isLeap y = true then
             (367*m+5)/12 - 1
        else
             (367*m+5)/12 - 2

    let rec checkAllMonths m y =
        if m = 0 then Console.WriteLine("Finished")
        else
        Console.WriteLine("")
        daysToEndMonth m y
        checkAllMonths (m-1) y

    let rec daysSinceChrist d m y =
        if y = 0 then 0
        else if d > 0 then 1 + daysSinceChrist (d-1) m y
        else if m > 1 then match m with
            | 2 -> 28 + daysSinceChrist d (m-1) y // February
            | 4|6|9|11 -> 30 + daysSinceChrist d (m-1) y // April, June, September, November
            | _ -> 31 + daysSinceChrist d (m-1) y  // All other months
        else if isLeap y then 366 + daysSinceChrist d m (y-1)
        else
            365 + daysSinceChrist d m (y-1)

module Part2 =
    let rec Count400 x =
        if x = 0 then 0
        else if (x-1)%400 = 0 then 1+Count400(x-1)
        else Count400(x-1)

    let rec Count100 x =
        if x = 0 then 0
        else if (x-1)%100 = 0 then 1+Count100(x-1)
        else Count100(x-1)

    let rec Count4 x =
        if x = 0 then 0
        else if (x-1)%4 = 0 then 1+Count4(x-1)
        else Count4(x-1)

//    let rec Secret x =
//        if x = 0 then 0
//        else if (x)%365 = 0 then 1+Secret(x-1)
//        else Secret(x-1)

//    let rec RealYearChecker c a =
//        if c > a then RealYearChecker (c-1) a

//    let CountFullYearsFromDays x =
        //
//        let c = Secret x
//        Console.WriteLine("Naively calculated years: {0}", c)
        //let extraDaysRequired : int = x/(365*4) //For every 4 years, an additional day is required to count a year
//        let ActualYears = (float(x)/365.25)
//        Console.WriteLine("Actual years passed in int: {0}, and in float: {1}", int(ActualYears), ActualYears)
        //if int(extraDaysRequired) > r then r-int(extraDaysRequired)   //if x*365.25 (AKA a real year) is less than the counted year, minus 1... It should actually be -1 for every 365 years
//        RealYearChecker (c, int(ActualYears))

    let YearOf days =
        let rec LeapCounterAux y rem acc =
            if rem < 365 then acc
            else if not (Part1.isLeap y) then LeapCounterAux (y+1) (rem-365) (acc+1)
            else if rem < 366 then acc
            else LeapCounterAux (y+1) (rem-366) (acc+1) 
        LeapCounterAux 1970 days 0

    let LeapCounter2 days =
        let rec LeapCounterAux y rem acc =
            if rem < 365 then y
            else if not (Part1.isLeap y) then LeapCounterAux (y+1) (rem-365) (acc+1)
            else if rem < 366 then acc
            else LeapCounterAux (y+1) (rem-366) (acc+1) 
        LeapCounterAux 1970 days 0

    let LeapCounter3 days =
        let rec LeapCounterAux y rem acc =
            if rem < 365 then rem
            else if not (Part1.isLeap y) then LeapCounterAux (y+1) (rem-365) (acc+1)
            else if rem < 366 then acc
            else LeapCounterAux (y+1) (rem-366) (acc+1) 
        LeapCounterAux 1970 days 0

    let MonthOf d =
        let y = LeapCounter2 d //The year is given by
        let z = LeapCounter3 d //z is the number of days from the first of january to the month
        let c = if Part1.isLeap y then 0 else if z < 59 then 1 else 2 //What is c? Defined here!
        (12*(z+c)+373)/367
        //Console.WriteLine("Day {0} falls in month {1}", d, (12*(z+c)+373)/367)

    let rec MonthOfRec d =
        MonthOf d
        if d = 1 then 0
        else MonthOfRec(d-1)

    let DayOf d m y =
        match m with
        | 1 -> d
        | 2 -> (d-31)
        | 3 -> if not (Part1.isLeap y) then (d-59) else (d-60)
        | 4 -> if not (Part1.isLeap y) then (d-90) else (d-91)
        | 5 -> if not (Part1.isLeap y) then (d-120) else (d-121)
        | 6 -> if not (Part1.isLeap y) then (d-151) else (d-152)
        | 7 -> if not (Part1.isLeap y) then (d-181) else (d-182)
        | 8 -> if not (Part1.isLeap y) then (d-212) else (d-113)
        | 9 -> if not (Part1.isLeap y) then (d-243) else (d-244)
        | 10 -> if not (Part1.isLeap y) then (d-273) else (d-274)
        | 11 -> if not (Part1.isLeap y) then (d-304) else (d-305)
        | 12 -> if not (Part1.isLeap y) then (d-334) else (d-335)

    let DateOf d =
        Console.WriteLine("Year: {0}, Month: {1}, Days: {2}", (1970 + YearOf d), MonthOf d, DayOf (LeapCounter3 d) (MonthOf d) (1970+YearOf d))