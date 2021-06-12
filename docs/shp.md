Shape File Format
=================

Author would like to thank authors of the following tools (in no particular order) for providing the information about the shape file format (which is the same in The Tone Rebellion and an older game from The Logic Factory, Ascendancy):

- [Ascendancy Converter v1.0][ascendancy-converter] by [Михаил Бесчетнов aka Terminus][extractor.ru]
- [Tone Extractor Tools][fadoli.tone-rebellion-extractor] by Franck M. ([@Fadoli][fadoli])
- [ascendancy][daumiller.ascendancy] utilities by Darcy ([@daumiller][daumiller])

The Tone Rebellion stores its image data in the so called shape files (`*.shp`). Shape file defines a collection of sprites.

Every `*.shp` file starts from the identifying ASCII string, `1.10` (first 4 bytes).

Next 4 bytes store the sprite count.

Then, for each sprite, a small header is written. Sprite header is 8 bytes: 4-byte offset to a sprite data (zero-based offset into the same file), and 4 bytes of unknown data. Unknown data is documented to be a palette offset in the [ascendancy][daumiller.ascendancy] utilities source code, though it is only present in two files of The Tone Rebellion (`TONE01\l-magton.shp` and `TONE01\p-splton.shp`), both of which are 256 bytes only, and look corrupted.

Sprite data (found by the offset from the file header) defines two distinct areas: a canvas area, and a sprite area and position on said canvas. Sprite data contains the following fields:

- canvas height minus one in pixels, 2-byte unsigned integer (so, you have to add 1 to get the real height)
- canvas width minus one in pixels, 2-byte unsigned integer
- absolute Y coordinate of the sprite origin in pixels, 2-byte unsigned integer
- absolute X coordinate of the sprite origin in pixels, 2-byte unsigned integer
- relative start X coordinate of the sprite in pixels, 4-byte integer
- relative start Y coordinate of the sprite in pixels, 4-byte integer
- relative end X coordinate of the sprite in pixels, 4-byte integer
- relative end Y coordinate of the sprite in pixels, 4-byte integer

Please note that X and Y coordinates are written in the opposite order (first Y, then X) in case of canvas dimensions and sprite origin, but in a normal order in case of sprite starting and ending corners.

Start and end coordinates are relative to the origin. Essentially, this allows to select a rectangle on a (potentially bigger) sprite canvas. Sprite data only gets drawn into that rectangle; other parts of the canvas are meant to be transparent.

Sprite pixel data immediately follows this structure (so, it starts at offset of 24 bytes from the initial sprite offset).

The sprite pixel data is a slightly packed representation of palette indices stream. It contains a sequence of commands, each starting with an indicator byte.

- indicator byte = 0: end of current row (remaining part of the pixel row contains only transparent pixels)
- indicator byte = 1: next byte contains a number of pixels to skip (fill them with the transparent color)
- indicator byte > 1:
    - calculate _count_ by right shifting it by 1 bit
    - if lowest bit is 0: next byte contains an index of color from the palette. Add a pixel of the same color _count_ times
    - if lowest bit is 1: next _count_ bytes contain color indices from the palette. Add _count_ pixels of these colors to the image

## Oddities

Certain sprites or sprite files look corrupted and/or couldn't be decoded following this guide.

- `TONE01\l-magton.shp` and `TONE01\p-splton.shp` are looking like they have embedded palette, though both the sprite and the palette seem to have ridiculous offset (4481064 bytes in a file of 256 bytes), so they're completely broken.
- Certain sprites seems to have ridiculous start/end coordinates (greater than 2000000000). At the same time, they seem to have zero bytes of data (either first byte of the sprite data is supposed to be just after the end of the file, or the next sprite starts immediately after the current one, without any gap in-between). As of now, I recommend to detect those by the first two bytes of all four of their start and end X and Y coordinates being `0x7fff`.

[ascendancy-converter]: https://www.extractor.ru/files/051b8c7c6155fef1460fab189f7edb68/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[extractor.ru]: http://www.extractor.ru/
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
