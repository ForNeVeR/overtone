Game Resources
==============
Overtone is utilizing the resources from the original CD. We consider the
English ISO of the game from [My Abandonware][myabandonware.the-tone-rebellion]
(the "Alternative ISO Version") to serve as the reference, though other versions
of the game are known to exist (e.g. check [these][fadoli.title-screen] French
title screen buttons). Ideally, they shouldn't differ in resource contents too
much from the English one.

Files on the CD
---------------
- `AUTORUN.*`: the main CD launcher/installer, we have no interest of fully
  replicating it
- `DISK1.ID`: a file with contents "one fish two fish", possibly used for disk
  identification
- `DSETUP*.DLL`: DirectX installation files?
- `TONEICON.ICO`: a crude game icon file
- `DIRECTX/*`: DirectX installation files, of no interest
- `SETUP/*`: setup support files, of no interest
- `THING1` directory (files from this directory get copied onto the local disk
  by the game installer)
    - `00.SAV`–`07.SAV`: seven files, all of 1 byte size, all containing a space
      character
    - `COB.CFG`: a list of all COB archives inside of a game; there're two of
      them here
    - `FLOAT.CFG`: a configuration file of single-player game; contains no
       useful settings for the remake
    - `FLOAT.EXE`: main game executable file
    - `FLOAT.OUT`: initially a 1-byte file with a single space character, this
      file will be overwritten with, presumably, some game logs on every run
    - `FLOAT.PRF`: purpose is currently unknown
    - `MULTI.CFG`: a configuration file for multiplayer game mode
    - `README.TXT`: a game manual
    - `RESUME.GAM`: initially a 1-byte file with a single space inside, will be
      overwritten by the current game state on exit (used as a temporary save
      file)
    - `TONE00.COB`–`TONE01.COB`: the game data archives in the
      [COB format][docs.cob], see the description below
- `THING2` directory (files from this directory are always read from the CD)
    - `ACT1.WAV`–`ACT4.WAV`: main in-game music, presumably remastered versions
      of these're available in the "Bonus Content" archive from [My
      Abandonware][myabandonware.the-tone-rebellion] (TODO: yet to determine)
    - `AMBIENT1.WAV`–`AMBIENT4.WAV`: other game music files, presumably
      remastered versiobns of these're available in the "Bonus Content" archive
      from [My Abandonware][myabandonware.the-tone-rebellion] (TODO: yet to
      determine)
    - `INTRO.AVI`: the introduction video; it wasn't shown in-game, but there's
      a **Start** menu item to show it in the standard video player.

      ```console
      $ MediaInfo.exe .\INTRO.AVI
      General
      Complete name                            : .\INTRO.AVI
      Format                                   : AVI
      Format/Info                              : Audio Video Interleave
      File size                                : 85.6 MiB
      Duration                                 : 3 min 33 s
      Overall bit rate                         : 3 362 kb/s

      Video
      ID                                       : 0
      Format                                   : Cinepak
      Codec ID                                 : cvid
      Duration                                 : 3 min 33 s
      Bit rate                                 : 2 973 kb/s
      Width                                    : 640 pixels
      Height                                   : 480 pixels
      Display aspect ratio                     : 4:3
      Frame rate                               : 15.000 FPS
      Bits/(Pixel*Frame)                       : 0.645
      Stream size                              : 75.7 MiB (88%)

      Audio
      ID                                       : 1
      Format                                   : PCM
      Format settings                          : Unsigned
      Codec ID                                 : 1
      Duration                                 : 3 min 33 s
      Bit rate mode                            : Constant
      Bit rate                                 : 352.8 kb/s
      Channel(s)                               : 2 channels
      Sampling rate                            : 22.05 kHz
      Bit depth                                : 8 bits
      Stream size                              : 8.98 MiB (10%)
      Alignment                                : Aligned on interleaves
      Interleave, duration                     : 67  ms (1.00 video frame)
      ```
    - `START.WAV`: the main menu music file
    - other `*.WAV` files: various music files, unsorted yet (TODO: determine
      the purpose of each one)
        - `LEVIATAN.WAV`
        - `LIFEGIVE.WAV`
        - `LOSEND.WAV`
        - `MYSTICS.WAV`
        - `PROTECT.WAV`
        - `SEEKERS.WAV`
        - `START.WAV`
        - `WINNER.WAV`
    - `VIELOGO.TGA`: a Virgin Interactive Entertainment logo in a, presumably,
      TGA format.

      Interestingly, it wasn't easy to render this image; it may be corrupted or
      just using older TGA format. Though it was rendered perfectly by
      [nomacs][] 3.16.1709.

      Search across the game binaries for this file's name didn't yield any
      results, so maybe this file is unused. Also, the game doesn't access this
      file in runtime (verified with the Process Monitor).

Start Menu Items
----------------
These're the Windows' **Start** menu items created by the game installer (for
completeness of this overview).

Game installs the following links to the Windows' **Start** menu:
- `1. View History.lnk`:
  `C:\WINDOWS\system32\RunDll32.exe amovie.ocx,RunDll D:\thing2\intro.avi`
- `2. Readme.lnk`: `C:\WINDOWS\system32\NOTEPAD.EXE C:\PROGRA~1\Tone\README.TXT`
- `3. The Tone Rebellion.lnk`: `"C:\Program Files\Tone\FLOAT.EXE"  float.cfg`
- `4. MultiPlayer Rebellion.lnk`: `"C:\Program Files\Tone\FLOAT.EXE"  multi.cfg`

COB Archive Contents
--------------------
TODO: Enumerate the archives

[docs.cob]: ./cob.md
[fadoli.title-screen]: https://github.com/Fadoli/ToneRebellion_Raw/tree/932909a9561b9f3666fb7b25ac011016a010fe6f/extracted_shp/TITSCRN
[myabandonware.the-tone-rebellion]: https://www.myabandonware.com/game/the-tone-rebellion-cjc
[nomacs]: https://nomacs.org/
