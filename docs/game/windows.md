Windows
===============

This is just guesses, based on what happens in the game, and the windows code : <https://github.com/Fadoli/ToneRebellion_Raw/blob/master/original_content/windows.txt>

Note : those lists are most likely not complete

## NAME

The unique? name of the element

## STATE

| IDs | Description |
|:-----:|:-------------|
| 0 | Main menu |
| 1 | Game creation menu |
| 2 | Islands view |
| 3 | Main game view (on an island) |
| 4 | loading view |
| 5 | saving view |
| 6 | Intro Movie |
| 7 | Ending view |

## WINTYPE

Defines the type of the current item, it seems like :

| IDs | Description |
|:-----:|:-------------|
| 1  | Button |
| 2  | ????? (in island view) |
| 3  | Image |
| 4  | ?? Image |
| 5  | ?? CLIPBD1 |
| 10 | IslandView |
| 11 | WorldView |
| 12 | Video |
| 13 | Upper menu (drop down ?) |
| 14 | Load save ? |
| 16 | Small map view |
| 17 | Realm view (bottom left) |
| 18 | IWConstruct : Construction menu |
| 21 | Task view |
| 22 | Detailed Task view (selected one) |
| 23 | Trade view |
| 24 | Diplomacy view |
| 26 | Location related ? Give user options for selected location |
| 27 | Display graphic info for selected location |
| 28 | Info on selected bld in IWConstruct (18) |
| 29 | Inventory view |
| 30 | Glyph view |
| 31 | ??????????? |
| 32 | task info ? |
| 33 | Unit info ? |
| 34 | Moving building ? |
| 35 | Town hall stockpile |
| 36 | End Game Result view |
| 37 | End Game Result view part |
| 38 | End Game Result text |
| 39 | Debug for special effects |
| 40 | Debug for special effects |
| 41 | Turn building ON/OFF |
| 42 | Upgrade building |
| 43 | Repair building |
| 46 | ?? BC_Obtain ?? most likely grab item |
| 35 | Town hall stockpile |
| 47 | ?? BC_UpgSpread ?? |
| 48 | ?? CLIPSEL ?? |

## SHAPEID / SHAPEFRAME / PANE / MOUSEFOCUS

Defines the texture to use (shape + frame) and the area in which it needs to be applied

the id of the shape is defined by the `shapes.txt` script that maps ID to a shapefile

MOUSEFOCUS allows to "hover" on buttons

## SENDMESSAGE / MSGDEST

see [events.md](./events.md)
