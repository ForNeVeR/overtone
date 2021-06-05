Shape File Format
=================

Author would like to thank authors of the following tools (in no particular order) for providing the information about the shape file format (which is the same in The Tone Rebellion and an older game from The Logic Factory, Ascendancy):

- [Ascendancy Converter v1.0][ascendancy-converter] by [Михаил Бесчетнов aka Terminus][extractor.ru]
- [Tone Extractor Tools][fadoli.tone-rebellion-extractor] by Franck M. ([@Fadoli][fadoli])
- [ascendancy][daumiller.ascendancy] utilities by Darcy ([@daumiller][daumiller])

The Tone Rebellion stores its image data in the so called shape files (`*.shp`). Shape file defines a collection of sprites.

Every `*.shp` file starts from the identifying ASCII string, `1.10` (first 4 bytes).

Next 4 bytes store the sprite count.

Then, for each sprite, a small header is written. Sprite header is just two offsets: 4-byte offset to a sprite data, and 4-byte offset to a sprite palette, both are zero-based offsets to the same file. Offset to a sprite palette is optional, and may be zero (in such case, no custom palette is defined for a sprite).

**TODO:** It seems like the embedded palette is unused in the Tone Rebellion. Check this.

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

**TODO:** Describe the pixel data storage.

**TODO:** Describe how the palette gets chosen for a particular sprite.

[ascendancy-converter]: https://www.extractor.ru/files/051b8c7c6155fef1460fab189f7edb68/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[extractor.ru]: http://www.extractor.ru/
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
