<!--
SPDX-FileCopyrightText: 2022-2025 Friedrich von Never <friedrich@fornever.me>

SPDX-License-Identifier: MIT
-->

Font File Format
----------------

Author would like to thank authors of the following tools (in no particular order) for providing the information about the shape file format (which is the same in The Tone Rebellion and an older game from The Logic Factory, Ascendancy):

- [Ascendancy Font Editor v1.1+][ascendancy-font-editor] by Roman Tkachuk (2:4600/68.2@fidonet),
- [ascendancy][daumiller.ascendancy] utilities by Darcy ([@daumiller][daumiller]).

The Tone Rebellion fonts are known to have the header of the following structure:
- version, 4 bytes: `"1.\0\0"`,
- `character-count`, 4-byte unsigned integer (it's unknown whether it's actually signed or unsigned, because we have font specimen consisting of more than 226 characters; for consistency, let's consider it unsigned, even if all the other tools read it as signed),
- `rows-per-character`, 4-byte unsigned integer,
- `transparent-color`, 4-byte unsigned integer (considering the color indices are 1-byte long, this is a bit redundant),
- `offsets`, array of unsigned 4-byte integers of length `character-count`, where every item is an offset in bytes for the corresponding character data to be placed,
- character data array (until the end of the file).

Every known font file has exactly 226 characters, which seems to correspond to characters of the code page 437 (starting from code 0).

Every character corresponds to the following structure:

- `columns`, 4-byte unsigned integer,
- byte array of `rows-per-character`×`columns`, storing the color data.

The color data is meant to be an array of indices corresponding to some palette, except for one color, marked by color `transparent-color`, which is transparent (despite what the palette says).

Certain characters may have zero columns defined; this means those characters are absent from the font.

Read more about palettes in [the corresponding format description][docs.pal].

## Oddities

Character with index 225, which normally looks like `ß` in the code page 437, for some reason looks more like `¡` in the fonts (in `smfont.fnt`, it is also the same as `i`, but from the `bigfont.fnt`, we can conclude it looks exactly like an inverted exclamation mark).

[ascendancy-font-editor]: https://www.extractor.ru/files/cbd334b175b9b8721a653077e37cbabd/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[docs.pal]: pal.md
