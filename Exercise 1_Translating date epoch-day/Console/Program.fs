// Learn more about F# at http://fsharp.org

open System
open Exercise_1_Translating_date_epoch_day

[<EntryPoint>]
let main argv =
    printfn "Exercise 1 : Part 1 : 2: \n"
    Console.WriteLine("The result of the 1992 parameter is: {0}", Part1.isLeap 1992)
    Console.WriteLine("The result of the 2000 parameter is: {0}", Part1.isLeap 2000)
    Console.WriteLine("The result of the 1990 parameter is: {0}", Part1.isLeap 1990)

    printfn "Exercise 1 : part 1 : 3: \n"
    let year1 = 1971
    let year2 = 1979
    Console.WriteLine("How many days are from the year 1970 to the year {0}? \n Answer: {1}", year1, Part1.daysToEndYear year1)
    Console.WriteLine("How many days are from the year 1970 to the year {0}? \n Answer: {1}", year2, Part1.daysToEndYear year2)

    printf "Exercise 1 : part 1 : 4: \n"
//    Console.WriteLine("How many days are there from the start of the year {0} to the end of the month {1} \n Answer: {2}", 2000, 12, Part1.daysToEndMonth 12 year1)
    Part1.checkAllMonths 12 year1
    Part1.checkAllMonths 12 2000
//    Console.WriteLine("How many days are from the year 1970 to the year {0}? \n Answer: {1}", year1, Part1.checkAllMonths 12 year1)


    printf "Exercise 1 : part 1 : 5: \n"
    Console.WriteLine("Days since year 0: {0}", Part1.daysSinceChrist 31 01 2023)

    printf "Exercise 1 : part 2 : 6: \n"
    printf "Exercise 1 : part 2 : 6 : a\n"
    let year3 = 2000
    Console.WriteLine("The amount of 400's in year {0} is {1}", year3, Part2.Count400 year3)
    printf "Exercise 1 : part 2 : 6 : b\n"
    Console.WriteLine("The amount of 100's in year {0} is {1}", year3, Part2.Count100 year3)
    printf "Exercise 1 : part 2 : 6 : c\n"
    Console.WriteLine("The amount of 4's in year {0} is {1}", year3, Part2.Count4 year3)
    printf "Exercise 1 : part 2 : 6 : d\n"
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 1460, 1460)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 1461, 1461)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 1459, 1469)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 730, 730)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 731, 731)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 729, 729)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 2920, 2920)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 2921, 2921)
    Console.WriteLine("The amount of years in {1} days are: {0}", Part2.YearOf 2922, 2922)
    Console.WriteLine("The amount of years in {1} days are: {0}\n", Part2.YearOf 2919, 2919)

    Console.WriteLine("Day {0} falls in the year {1}", 1, Part2.LeapCounter2 1)
    Console.WriteLine("Day {0} falls in the year {1}", 1000, Part2.LeapCounter2 1000)
    Console.WriteLine("Day {0} falls in the year {1}", 15000, Part2.LeapCounter2 15000)
    Console.WriteLine("Day {0} falls in the year {1}", 55134, Part2.LeapCounter2 55134)

    printf "Exercise 1 : part 2 : 7: \n"

    Console.WriteLine("Day {0} falls in month {1}", 55134, Part2.MonthOf 55134)
    Console.WriteLine("Day {0} falls in month {1}", 5134, Part2.MonthOf 5134)
    Console.WriteLine("Day {0} falls in month {1}", 31, Part2.MonthOf 31)
    Console.WriteLine("Day {0} falls in month {1}", 32, Part2.MonthOf 32)

    Part2.MonthOfRec 365

    printf "Exercise 1 : part 2 : 8: \n"

    Part2.DateOf 5134
    Part2.DateOf 50
    Part2.DateOf 1
    Part2.DateOf 5234

    0 // return an integer exit code
