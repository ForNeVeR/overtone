module Overtone.Utils.Geometry

open Microsoft.Xna.Framework

let rectFromCorners(struct(x0: int, y0: int), struct(x1: int, y1: int)): Rectangle =
    Rectangle(Point(x0, y0), Point(x1 - x0 + 1, y1 - y0 + 1))
