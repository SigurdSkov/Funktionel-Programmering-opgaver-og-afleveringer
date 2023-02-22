namespace exercise_2_Queen_can_attack

module Queen =
    //Location
    let LegalX x =
        if (x > 0 || x < 7) then true
        else false

    let LegalY y =
        if (y > 0 || y < 7) then true
        else false

    //Movement
    let MoveXLinear oldX newX =
        if (oldX = newX && LegalX newX) then true
        else false

    let MoveYLinear oldY newY = 
        if (oldY = newY && LegalY newY) then true
        else false

    let rec MoveDiagonally oldCoordinates newCoordinates =
        //Are old coordinates the same as new coordinates? If yes return true, if not:
        if oldCoordinates = newCoordinates then true else
        //First off, move one down. Is this a legal position?
        //What I did instead: check which direction it is trying to move. Move one up/down and one right/left.
        //This should result in a diagonal move, until it either hits the desired space, or it parallel on one axis, but not the other
        if (fst oldCoordinates > fst newCoordinates && snd oldCoordinates > snd newCoordinates) then MoveDiagonally ((fst oldCoordinates)-1, (snd oldCoordinates)-1) newCoordinates
        else if (fst oldCoordinates < fst newCoordinates && snd oldCoordinates < snd newCoordinates) then MoveDiagonally ((fst oldCoordinates)+1, (snd oldCoordinates)+1) newCoordinates
        else if (fst oldCoordinates > fst newCoordinates && snd oldCoordinates < snd newCoordinates) then MoveDiagonally ((fst oldCoordinates)-1, (snd oldCoordinates)+1) newCoordinates
        else if (fst oldCoordinates < fst newCoordinates && snd oldCoordinates > snd newCoordinates) then MoveDiagonally ((fst oldCoordinates)+1, (snd oldCoordinates)-1) newCoordinates
        else false


    let LegalMove oldCoordinates newCoordinates =
        if (MoveXLinear (fst oldCoordinates) (fst newCoordinates)) = true then true
        else if (MoveYLinear (snd oldCoordinates) (snd newCoordinates)) = true then true
        else if (MoveDiagonally oldCoordinates newCoordinates) = true then true
        else false

    //Placement legality
    let Place x y =
        if (LegalX x || LegalY y) then true
        else false

    //Movement legality
    //let Move oldX oldY newX newY = 
    let Move oldCoordinates newCoordinates =
        if (LegalX (fst oldCoordinates) && LegalX (fst newCoordinates) && LegalY (snd oldCoordinates) && LegalY (snd newCoordinates)) then
            if (LegalMove oldCoordinates newCoordinates) then true
            else false
        else false
