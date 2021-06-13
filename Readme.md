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
- [Text Configuration Files][docs.txt]

Getting the Game Resources
--------------------------
Overtone will reuse the original resources as much as possible. To run the game,
the user will have to provide the resources (it is unlikely we'll ever be able
to provide these resources ourselves).

The game is considered [abandonware][wikipedia.abandonware], so feel free to
download the game from [My Abandonware][my-abandonware.the-tone-rebellion].

Prerequisites
-------------
To develop the project, you'll need [.NET SDK][dotnet-sdk] 5.0 or later installed.

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

- Darcy ([@daumiller][daumiller]), the author of the [ascendancy][daumiller.ascendancy] utilities
- Franck M. ([@Fadoli][fadoli]), the author of the [Tone Extractor Tools][fadoli.tone-rebellion-extractor]
- Roger Braun ([@rogerbraun][rogerbraun]), the author of the [Ascendancy-tools][rogerbraun.ascendancy-tools]
- [The Logic Factory][wikipedia.the-logic-factory], the authors of The Tone Rebellion
- [Михаил Бесчетнов aka Terminus][extractor.ru], the author of the [Ascendancy Converter v1.0][ascendancy-converter]

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-umbra-
[ascendancy-converter]: https://www.extractor.ru/files/051b8c7c6155fef1460fab189f7edb68/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[docs.cob]: docs/cob.md
[docs.pal]: ./docs/pal.md
[docs.resources]: docs/resources.md
[docs.running-original]: docs/running-original.md
[docs.shp]: ./docs/shp.md
[docs.txt]: ./docs/txt.md
[dotnet-sdk]: https://dot.net/
[extractor.ru]: http://www.extractor.ru/
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
[my-abandonware.the-tone-rebellion]: https://www.myabandonware.com/game/the-tone-rebellion-cjc
[rogerbraun.ascendancy-tools]: https://github.com/rogerbraun/Ascendancy-tools
[rogerbraun]: https://github.com/rogerbraun
[wikipedia.abandonware]: https://en.wikipedia.org/wiki/Abandonware
[wikipedia.the-logic-factory]: https://en.wikipedia.org/wiki/The_Logic_Factory
[wikipedia.the-tone-rebellion]: https://en.wikipedia.org/wiki/The_Tone_Rebellion

[status-umbra]: https://img.shields.io/badge/status-umbra-red.svg
