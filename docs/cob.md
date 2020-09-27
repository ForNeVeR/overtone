COB File Format
===============
Main game data is stored in COB files (i.e. `TONE00.COB` and `TONE01.COB`).

COB is a simple, tar-like file format which is described below.

All integers in a COB file are stored in little endian. The file looks as:

- 4-byte entry count
- for each entry: 50-byte entry name (padded with zeroes if necessary)
- for each entry: 4-byte offset in file
- for each entry: entry contents

Size of entry contents should be calculated based on the next entry offset. The
lest entry ends where the file ends (because you cannot otherwise estimate its
size).

Entry paths use a backslash path separator (`\`).
