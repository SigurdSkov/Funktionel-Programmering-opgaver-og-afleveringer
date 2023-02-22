module Week2.Chess

let create (y,x) =
    let size = 8
    let checkCoordinate = function
        | x when x >= 0 && x < size -> true
        | _ -> false
    checkCoordinate x && checkCoordinate y


let canAttack (y1,x1) (y2,x2) =
    let canAttackRow = y1 = y2
    let canAttackColumn = x1 = x2
    let southWest (y,x) = (y-1, x+1)
    let northEast (y,x) = (y+1, x-1)
    let northWest (y,x) = (y-1, x-1)
    let southEast (y,x) = (y+1, x+1)
    let rec canAttackDiagonal direction = function
        | (y,x) when not (create (y,x)) -> false
        | (y,x) when y2=y && x2=x -> true
        | (y,x) -> canAttackDiagonal direction (direction (y,x))

    canAttackRow || canAttackColumn
    || canAttackDiagonal southWest (y1, x1)
    || canAttackDiagonal northEast (y1, x1)
    || canAttackDiagonal northWest (y1, x1)
    || canAttackDiagonal southEast (y1, x1)