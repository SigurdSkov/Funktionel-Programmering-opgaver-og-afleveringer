module ParserTests

open NUnit.Framework
open Parser

[<SetUp>]
let Setup () =
    ()

[<Test>]
let durationTest () =
    //input
    let score = "1d2 2d2 4d2 8d2 16d2 32d2"
    //act
    let result = Parser.parse score

    let result = match result with
        | Choice2Of2 ms -> ms
        | Choice1Of2 err -> failwith err

    //assert
    Assert.AreEqual(6, result.Length)

    Assert.AreEqual(1, (result.Item 0).duration)
    Assert.AreEqual(2, (result.Item 1).duration)
    Assert.AreEqual(4, (result.Item 2).duration)
    Assert.AreEqual(8, (result.Item 3).duration)
    Assert.AreEqual(16, (result.Item 4).duration)
    Assert.AreEqual(32, (result.Item 5).duration)

    let dur0 = Parser.durationFromToken (result.Item 0)
    let dur1 = Parser.durationFromToken (result.Item 1)
    let dur2 = Parser.durationFromToken (result.Item 2)
    let dur3 = Parser.durationFromToken (result.Item 3)
    let dur4 = Parser.durationFromToken (result.Item 4)
    let dur5 = Parser.durationFromToken (result.Item 5)

    Assert.AreEqual(4000, dur0)
    Assert.AreEqual(2000, dur1)
    Assert.AreEqual(1000, dur2)
    Assert.AreEqual(500, dur3)
    Assert.AreEqual(250, dur4)
    Assert.AreEqual(125, dur5)
