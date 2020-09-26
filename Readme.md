Overtone [![Status Zero][status-zero]][andivionian-status-classifier]
========
Overtone is a remake of [The Tone Rebellion][wikipedia.the-tone-rebellion], a
strategy game developed by The Logic Factory and released in 1997.

Developer Documentation
-----------------------
- [Running the Original Game][docs.running-original]
- [Game Resources][docs.resources]
- [COB File Format][docs.cob]

Getting the Game Resources
--------------------------
Overtone will reuse the original resources as much as possible. To run the game,
the user will have to provide the resources (it is unlikely we'll ever be able
to provide these resources ourselves).

The game is considered [abandonware][wikipedia.abandonware], so feel free to
download the game from [My Abandonware][myabandonware.the-tone-rebellion].

Prerequisites
-------------
To develop the project, you'll need [.NET SDK][dotnet-sdk] 3.1.402 installed.

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

```console
$ dotnet run --project Overtone.Cob -- ls <path to a COB archive>
$ dotnet run --project Overtone.Cob -- x <path to a COB archive> <path to the output dir>
```

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-zero-
[docs.cob]: docs/cob.md
[docs.resources]: docs/resources.md
[docs.running-original]: docs/running-original.md
[dotnet-sdk]: https://dot.net/
[myabandonware.the-tone-rebellion]: https://www.myabandonware.com/game/the-tone-rebellion-cjc
[status-zero]: https://img.shields.io/badge/status-zero-lightgrey.svg
[wikipedia.abandonware]: https://en.wikipedia.org/wiki/Abandonware
[wikipedia.the-tone-rebellion]: https://en.wikipedia.org/wiki/The_Tone_Rebellion
