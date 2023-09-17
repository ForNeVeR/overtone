Text Configuration Files
------------------------
Most of the game configuration data is stored in the text files inside [the COB archives][docs.cob]. See the full file list in [the Game Resources document][docs.resources].

The text files use ASCII encoding, with lines separated by CR + LF. As documented in `TONE00.COB\names.txt`, these files allow comments prefixed by either `;` or `//` characters. Comments aren't always full-line, and may be placed after useful data in the same line, too. Comments always span till the end of the current line.

Every non-empty and non-comment file line will be named here a _statement_.

Some configuration files are divided into explicit sections (say, `BLDNAME` statement opens a section in `TONE00.COB\bldname.txt`, and `END` statement ends it). In certain files, only the ending statement is required (e.g. `TONE00.COB\spells.txt`), and some files have the free structure (say, `TONE00.COB\effects.txt` where there are no explicit section starting and ending commands).

Some files (say, `TONE00.COB\floater.txt`) have a table-like structure. Indentation between columns in these tables is not strict, and may include any number of space or tab characters (as seen in `TONE00.COB\fltinf.txt`). Redundant spaces after the end of the line are ignored.

See more in the corresponding documentation files:
- [Events][docs.events]
- [Windows][docs.windows]

[docs.cob]: ./cob.md
[docs.events]: game/events.md
[docs.resources]: ./resources.md
[docs.windows]: game/windows.md
