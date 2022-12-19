Game Resources
==============
Overtone is utilizing the resources from the original CD. We consider the
English ISO of the game from [My Abandonware][my-abandonware.the-tone-rebellion]
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
    - `COB.CFG`: a list of all COB archives inside a game; there are two of
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
      of these are available in the "Bonus Content" archive from [My
      Abandonware][my-abandonware.the-tone-rebellion] (TODO: yet to determine)
    - `AMBIENT1.WAV`–`AMBIENT4.WAV`: other game music files, presumably
      remastered versions of these are available in the "Bonus Content" archive
      from [My Abandonware][my-abandonware.the-tone-rebellion] (TODO: yet to
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
These are the Windows' **Start** menu items created by the game installer (for
completeness of this overview).

Game installs the following links to the Windows' **Start** menu:
- `1. View History.lnk`:
  `C:\WINDOWS\system32\RunDll32.exe amovie.ocx,RunDll D:\thing2\intro.avi`
- `2. Readme.lnk`: `C:\WINDOWS\system32\NOTEPAD.EXE C:\PROGRA~1\Tone\README.TXT`
- `3. The Tone Rebellion.lnk`: `"C:\Program Files\Tone\FLOAT.EXE"  float.cfg`
- `4. MultiPlayer Rebellion.lnk`: `"C:\Program Files\Tone\FLOAT.EXE"  multi.cfg`

COB Archive Contents
--------------------
The archives contain files in the following formats:

- `*.fnt`: [font files][docs.fnt],
- `*.gif`: a standard GIF file,
- `*.haz`: documented as "shading files" in the `README.md` file of the [ascendancy][daumiller.ascendancy] utilities repository; exact format is currently unknown,
- `*.hzt`: binary files of unknown format,
- `*.pal`: [palette files][docs.pal] corresponding to the shapes,
- `*.shp`: [shape files][docs.shp],
- `*.txt`: [text configuration files][docs.txt],
- `*.wav`: sound files of a well-known [Waveform Audio File Format][wav].

### `TONE00.COB`
- `bldinf.txt`: "Building Animation Segments" description
- `bldname.txt`: building names
- `bldtxt.txt`: building and effect (like `FEAT00`?) descriptions
- `credits.txt`: game credits
- `effects.txt`: various effect definitions
- `endgame.txt`: the text presented when winning the game?
- `floater.txt`: unit statistics
- `fltinf.txt`, `flting.txt`: "Floater shape info file", two copies of the same file
- `gamey.txt`: some labels for the UI
- `names.txt`: names of various entities (tribes, islands, Lefiathan beasts)
- `newgame.txt`: texts shown when starting a new game
- `newtext.txt`: some additional UI labels and texts
- `pad.txt`: "the generic pad info": the building characteristics
- `plotobj.txt`: looks like descriptions of plot items
- `shapeinf.txt`: "Shape info file": the information about animation frames for various units
- `shapes.txt`: annotated list of certain shape files
- `sound.txt`: barely annotated list of certain sound files, including the files that are stored outside of COB archives (in the `THING2` directory on the CD)
- `spells.txt`: spell descriptions and certain characteristics
- `tonehelp.txt`: help texts for UI
- `windows.txt`: UI screen definitions with various UI element positions

### `TONE01.COB`
- various `data\*.shp` files enumerated in `TONE00.COB\shapes.txt`: shape data
- the following `data\*.shp` files, not enumerated in `TONE00.COB\shapes.txt` _(TODO: determine their purpose)_:
  - `data\clip1.shp`
  - `data\clip2.shp`
  - `data\clip3.shp`
  - `data\editor00.shp`
  - `data\editor01.shp`
  - `data\endtem1.shp`
  - `data\endtem2.shp`
  - `data\endtem3.shp`
  - `data\endtemp.shp`
  - `data\glinv0.shp`
  - `data\glinv1.shp`
  - `data\glinv2.shp`
  - `data\glinv3.shp`
  - `data\halllife.shp`
  - `data\hallmyst.shp`
  - `data\hallprot.shp`
  - `data\hallseek.shp`
  - `data\hallwand.shp`
  - `data\i00-feat.shp`
  - `data\i01-feat.shp`
  - `data\i02-feat.shp`
  - `data\i03-feat.shp`
  - `data\i04-feat.shp`
  - `data\i05-feat.shp`
  - `data\i06-feat.shp`
  - `data\i07-feat.shp`
  - `data\i08-feat.shp`
  - `data\i09-feat.shp`
  - `data\i10-feat.shp`
  - `data\i11-feat.shp`
  - `data\i12-feat.shp`
  - `data\i13-feat.shp`
  - `data\i13-trig.shp`
  - `data\i14-feat.shp`
  - `data\inci1.shp`
  - `data\inci2.shp`
  - `data\island00.shp`
  - `data\island01.shp`
  - `data\island02.shp`
  - `data\island03.shp`
  - `data\island04.shp`
  - `data\island05.shp`
  - `data\island06.shp`
  - `data\island07.shp`
  - `data\island08.shp`
  - `data\island09.shp`
  - `data\island10.shp`
  - `data\island11.shp`
  - `data\island12.shp`
  - `data\island13.shp`
  - `data\island14.shp`
  - `data\islandxx.shp`
  - `data\isletmp1.shp`
  - `data\isletmp2.shp`
  - `data\isletmp3.shp`
  - `data\l-bldton.shp`
  - `data\l-cryton.shp`
  - `data\l-enrich.shp`
  - `data\l-extend.shp`
  - `data\l-magton.shp`
  - `data\loadsav1.shp`
  - `data\loadsav2.shp`
  - `data\loadsav3.shp`
  - `data\loadsave.shp`
  - `data\m-spec2.shp`
  - `data\mainbut1.shp`
  - `data\mainbut2.shp`
  - `data\mainbut3.shp`
  - `data\ngbutton.shp`
  - `data\p-medic.shp`
  - `data\p-pctrn.shp`
  - `data\p-prtrn.shp`
  - `data\p-ronin.shp`
  - `data\p-speltw.shp`
  - `data\p-splton.shp`
  - `data\p-srtrn.shp`
  - `data\p-tcrys.shp`
  - `data\pspl13.shp`
  - `data\pt2b.shp`
  - `data\s3c.shp`
  - `data\scarrow1.shp`
  - `data\scarrow2.shp`
  - `data\scarrow3.shp`
  - `data\slidwin1.shp`
  - `data\slidwin2.shp`
  - `data\slidwin3.shp`
  - `data\smilse.shp`
  - `data\spiral.shp`
  - `data\spiral2.shp`
  - `data\spl2b.shp`
  - `data\spl8e.shp`
  - `data\st1b.shp`
  - `data\st1c.shp`
  - `data\st2e.shp`
  - `data\temphol1.shp`
  - `data\temphol2.shp`
  - `data\temphol3.shp`
  - `data\thall.shp`
  - `data\thall1.shp`
  - `data\thall2.shp`
  - `data\thall3.shp`
  - `data\v-at2fx.shp`
  - `data\v-at5fx.shp`
  - `data\vblegs.shp`
  - `data\vlegs.shp`
  - `data\vmbods.shp`
  - `data\vmsrlegs.shp`
- various `data\*.wav` files enumerated in `TONE00.COB\sound.txt`
- the following `data\*.wav` files, not enumerated in `TONE00.COB\sounds.txt` _(TODO: determine their purpose)_:
  - `feature.wav`
  - `format.wav`
  - `fxformat.wav`
- `data\bgoutfnt.fnt`, `data\bigfont.fnt`, `data\smfont.fnt`: obviously, font-related files _(TODO: determine when to use every one)_
- `data\logo.pal`: a palette file not accompanied by a shape file
- `data\newgame.hzt`: a binary file of unknown format
- `data\*.pal`: the palette files for the corresponding shapes (see [the palettes documentation][docs.pal] for the full association list)
- `data\imremap.haz`: a binary file, purpose unknown
- `data\island00.hzt`–`data\island14.hzt`: binary files obviously related to the corresponding `.shp`/`.pal` files, purpose currently unknown
- `data\island00.txt`–`data\island14.txt`: object coordinates in the islands
- `data\island04.tx2`, `data\island07.tx2`–`data\island10.tx2`: old copies of the same coordinate data?
- `data\logo.gif`: The Logic Factory logo
- `data\worldpos.txt`: definitions of the islands shown on the main map screen

[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[docs.cob]: ./cob.md
[docs.fnt]: fnt.md
[docs.pal]: ./pal.md
[docs.shp]: ./shp.md
[docs.txt]: ./txt.md
[fadoli.title-screen]: https://github.com/Fadoli/ToneRebellion_Raw/tree/932909a9561b9f3666fb7b25ac011016a010fe6f/extracted_shp/TITSCRN
[my-abandonware.the-tone-rebellion]: https://www.myabandonware.com/game/the-tone-rebellion-cjc
[nomacs]: https://nomacs.org/
[wav]: https://en.wikipedia.org/wiki/WAV
