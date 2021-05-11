COB File Format
===============

Author would like to thank authors of the following tools (in no particular order) for providing the information about the COB file format (which is the same in The Tone Rebellion and an older game from The Logic Factory, Ascendancy):

- [Ascendancy-tools][rogerbraun.ascendancy-tools] by Roger Braun ([@rogerbraun][rogerbraun])
- [Tone Extractor Tools][fadoli.tone-rebellion-extractor] by Franck M. ([@Fadoli][fadoli])
- [ascendancy][daumiller.ascendancy] utilities by Darcy ([@daumiller][daumiller])

Main game data is stored in COB files (i.e. `TONE00.COB` and `TONE01.COB`).

COB is a simple, tar-like file format which is described below.

All integers in a COB file are stored in little endian. The file looks as:

- 4-byte entry count
- for each entry: 50-byte entry name (padded with zeroes if necessary)
- for each entry: 4-byte offset in file
- for each entry: entry contents

Size of entry contents should be calculated based on the next entry offset. The
last entry ends where the file ends (because you cannot otherwise estimate its
size).

Entry paths use a backslash path separator (`\`).

[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
[rogerbraun.ascendancy-tools]: https://github.com/rogerbraun/Ascendancy-tools
[rogerbraun]: https://github.com/rogerbraun
