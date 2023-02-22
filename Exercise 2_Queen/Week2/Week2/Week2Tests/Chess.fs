module Week2Tests

open Week2.Chess
open NUnit.Framework
open Swensen.Unquote

[<SetUp>]
let Setup () =
    ()

//[<Test>]
//let CreatePiece () =
//    Assert.That(create (2,2), Is.True)
//    Assert.That(create (7,7), Is.True)
//    Assert.That(create (-2, -2), Is.False)
//    Assert.That(create (8, 2), Is.False)
//    Assert.That(create (3, 8), Is.False)


[<Test>]
let CreatePiece () =
    test <@ create (2,2) = true @> // With unquote
    test <@ create (7,7) = true @>
    test <@ create (-2,-2) = false @>
    test <@ create (8,2) = false @>
    test <@ create (3,8) = false @>
    
[<Test>]
let CanAttack () =
    test <@ canAttack (2,4) (6,6) = false  @>
    test <@ canAttack (2,4) (2,6) = true @>
    test <@ canAttack (4,4) (2,4) = true @>
    test <@ canAttack (2,2) (0,4) = true @> // sw
    test <@ canAttack (2,2) (3,1) = true @> // ne
    test <@ canAttack (2,2) (1,1) = true @> // nw
    test <@ canAttack (0,6) (1,7) = true @> // se
    