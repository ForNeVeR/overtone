module Overtone.Utils.Constants

module Scenes =
    let MainMenu: int = 0
    let NewGame: int = 1
    let IslandsView: int = 2
    let GameView: int = 3
    // Loading and saving menus are just guesses, there is no way to look at it and know which is which, as long as we refer to those we should be fine
    let LoadingMenu: int = 4
    let SavingMenu: int = 5
    let IntoMovie: int = 6
    let OutroMovie: int = 7

// TODO, fullfill types as needed
module WindowTypes =
    let Button: int = 1
    let TemporaryElements: int = 2 // used in load/save and islands rendering ?
    let Background: int = 3
    let OverlayOverBackground: int = 4 // only used in scene 1, seems to be identical to "background"
    let Unknown: int = 5 // used in scene 3, shape TEMPHOLE, CLIPBDX
    let IslandView: int = 10 // actual in game, for scene 3
    let WorldView: int = 11 // actual worlds map, for scene 2
    let Video: int = 12
    let DropDownMenu: int = 13

module Shapes =
    module TitleScreen =
        let Id: string = "TITSCRN"
        let TitleFrame: int = 1
