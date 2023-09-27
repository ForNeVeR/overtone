Events
======

This is just guesses, based on what happens in the game, and the windows code : <https://github.com/Fadoli/ToneRebellion_Raw/blob/master/original_content/windows.txt>

The goal is to properly understand and handle the action related to buttons :

```text
SENDMESSAGE    55 0 0
MSGDEST        MASTER
```

Note : this list is most likely not complete

## WINMGR

| IDs | Description |
|:-----:|:-------------|
| 0 | often used as placeholders, noop |
| 0 | LOAD/SAVE TMP (state 4-5) |
| 1-0 | change to state 0 (go back to main view from creation one) |
| 1-0 | LOAD/SAVE (state 4-5) |
| 1-4 | change to state 4 (load game view) |
| 2 | Exit the game |
| 5 | Change to state 1 (game creation view) |

## MASTER

| IDs | Description |
|:-----:|:-------------|
| 52 | Start game (game creation view) |
| 53 | Change world size (game creation view) |
| 55 | Change difficulty (game creation view) |
| 60-(-1) | Select tribe 0 (game creation view) |
| 61-(-1) | Select tribe 1 (game creation view) |
| 62-(-1) | Select tribe 2 (game creation view) |
| 63-(-1) | Select tribe 3 (game creation view) |
| 101 | Interact with down UI buttons, depends on origin (SendParam1 is setup by the island master function.) |
| 101-BC_OnOff | Turn on/off the building |
| 101-BC_Move | Move the building |
| 101-BC_Upgrade | Upgrade the building |
| 101-BC_Repair | Repair the building |
| 101-BC_Obtain | Most likely grab item |
| 101-BC_TrnStkC | StockPile training building Cristal Tone |
| 101-BC_TrnStkM | StockPile training building Magic Tone |
| 101-BC_HallStkB | StockPile training building Building Tone |
| 101-BC_HallStkC | StockPile training building Cristal Tone |
| 101-BC_HallStkM | StockPile training building Magic Tone |
| 101-BC_GenInfo | Most likely general view button |
| 101-BC_Storage | Most likely Items/Storage view button |
| 101-BC_Knowledge |Most likely Knowledge view button |
| 101-BC_Trade | Most likely Trade view button |
| 101-BC_Diplomacy | Most likely Diplomacy view button |
| 101-BC_Recall | Give a Dojo, retreat order |
| 101-BC_AttackFlt | Give a Dojo, Attack unit order |
| 101-BC_AttackLoc | Give a Dojo, Attack location order |
| 101-BC_DefendFlt | Give a Dojo, Defend unit order |
| 101-BC_DefendArea | Give a Dojo, Defend location order |
| 101-BC_UpgSpread | ??? |
| 101-BC_SpellX | Select Spell X (x = 1-8) |
| 2000 | Go to map view (in game islands selection) |
| 2001 | Resume previous game (title screen) |
| 2002 | PANIC ! Toggle : make worker fight ? |
| 2003 | Display status on unit (hp / energy / realm) |
| 2005 | Open task list |
| 3001 | CLIPBD1 |
| 3002 | CLIPBD2 |
| 3003 | CLIPBD3 |
| 4000-1 | Set priority to task to normal |
| 4000-2 | Set priority to task to high |
| 4000-3 | Set priority to task to exclusiv |
| 4003 | Kill the task |
| 4010 | Trade ZERO Button |
| 4011 | Trade DOIT Button |
| 4010 | Trade Button |
| 6000 | scrollup |
| 6001 | scrolldn |
| 6002 | escrollup |
| 6003 | escrolldn |
| 7001 | CLIPSEL1 |
| 7002 | CLIPSEL2 |
| 7003 | CLIPSEL3 |

## IWConstruct

| IDs | Description |
|:-----:|:-------------|
| 69 | cycle button for build window |

## IWInventory

| IDs | Description |
|:-----:|:-------------|
| 69 | cycle button for inventory window |
