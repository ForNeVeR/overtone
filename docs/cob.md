COB File Format
===============

Author would like to thank GitHub users Darcy ([@daumiller][daumiller]) and
Roger Braun ([@rogerbraun][rogerbraun]) for publishing the scripts to handle
Ascendancy data archives:

- [daumiller/ascendancy][daumiller.ascendancy]
- [rogerbraun/Ascendancy-tool][rogerbraun.ascendancy-tools]

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

[daumiller]: https://github.com/daumiller
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[rogerbraun]: https://github.com/rogerbraun
[rogerbraun.ascendancy-tools]: https://github.com/rogerbraun/Ascendancy-tools
