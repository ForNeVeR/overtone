Overtone [![Status Umbra][status-umbra]][andivionian-status-classifier]
========
Overtone is a remake of [The Tone Rebellion][wikipedia.the-tone-rebellion], a
strategy game developed by The Logic Factory and released in 1997.

Developer Documentation
-----------------------
- [Running the Original Game][docs.running-original]
- [Game Resources][docs.resources]
- [COB File Format][docs.cob]
- [Palette File Format][docs.pal]
- [Shape File Format][docs.shp]
- [Font File Format][docs.fnt]
- [Text Configuration Files][docs.txt]
  - [Events][docs.events]
  - [Windows][docs.windows]
- [Cursors][docs.cursors]

Getting the Game Resources
--------------------------
Overtone will reuse the original resources as much as possible. To run the game,
the user will have to provide the resources (it is unlikely we'll ever be able
to provide these resources ourselves).

The game is considered [abandonware][wikipedia.abandonware], so feel free to download the game from the [Internet Archive][archive-org.the-tone-rebellion].

Prerequisites
-------------
To develop the project, you'll need [.NET SDK][dotnet.sdk] 7.0 or later installed.

Build
-----
```console
$ dotnet build
```

Test
----
```console
$ dotnet test
```

Available Utilities
-------------------

### Overtone.Cob

This is the [COB file][docs.cob] extractor.

```console
$ dotnet run --project Overtone.Cob -- ls <path to a COB archive>
$ dotnet run --project Overtone.Cob -- x <path to a COB archive> <path to the output dir>
```

### Overtone.Extractor

This is a tool to extract game resources (currently, [shape files][docs.shp] rendering).

```console
$ dotnet run --project Overtone.Extractor -- args
```

Where `args` are:

- `info path.shp`: print shape file information
- `render path.shp output/directory`: render sprites from a shape file to a collection of png files in an output directory
- `palette path.shp`: print a palette file name for a shape file

All commands support wildcards (say, `render path/*.shp /output/dir`).

Testimonials
------------

Author would like to thank the following people for their help with this project (in no particular order):

- Darcy ([@daumiller][daumiller]), the author of the [ascendancy][daumiller.ascendancy] utilities,
- Franck M. ([@Fadoli][fadoli]), the author of the [Tone Extractor Tools][fadoli.tone-rebellion-extractor],
- Roger Braun ([@rogerbraun][rogerbraun]), the author of the [Ascendancy-tools][rogerbraun.ascendancy-tools],
- [The Logic Factory][wikipedia.the-logic-factory], the authors of The Tone Rebellion,
- [Михаил Бесчетнов aka Terminus][extractor.ru], the author of the [Ascendancy Converter v1.0][ascendancy-converter],
- Roman Tkachuk (2:4600/68.2@fidonet), the author of the [Ascendancy Font Editor v1.1+][ascendancy-font-editor].

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-umbra-
[archive-org.the-tone-rebellion]: https://archive.org/details/the-tone-rebellion
[ascendancy-converter]: https://www.extractor.ru/files/051b8c7c6155fef1460fab189f7edb68/
[ascendancy-font-editor]: https://www.extractor.ru/files/cbd334b175b9b8721a653077e37cbabd/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[docs.cob]: docs/cob.md
[docs.cursors]: docs/cursor.md
[docs.events]: docs/game/events.md
[docs.fnt]: docs/fnt.md
[docs.pal]: ./docs/pal.md
[docs.resources]: docs/resources.md
[docs.running-original]: docs/running-original.md
[docs.shp]: ./docs/shp.md
[docs.txt]: ./docs/txt.md
[docs.windows]: docs/game/windows.md
[dotnet.sdk]: https://dot.net/
[extractor.ru]: http://www.extractor.ru/
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
[rogerbraun.ascendancy-tools]: https://github.com/rogerbraun/Ascendancy-tools
[rogerbraun]: https://github.com/rogerbraun
[wikipedia.abandonware]: https://en.wikipedia.org/wiki/Abandonware
[wikipedia.the-logic-factory]: https://en.wikipedia.org/wiki/The_Logic_Factory
[wikipedia.the-tone-rebellion]: https://en.wikipedia.org/wiki/The_Tone_Rebellion

[status-umbra]: https://img.shields.io/badge/status-umbra-red.svg
