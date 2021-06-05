Shape File Format
=================

Author would like to thank authors of the following tools (in no particular order) for providing the information about the shape file format (which is the same in The Tone Rebellion and an older game from The Logic Factory, Ascendancy):

- [Ascendancy Converter v1.0][ascendancy-converter] by [Михаил Бесчетнов aka Terminus][extractor.ru]
- [Tone Extractor Tools][fadoli.tone-rebellion-extractor] by Franck M. ([@Fadoli][fadoli])
- [ascendancy][daumiller.ascendancy] utilities by Darcy ([@daumiller][daumiller])

The Tone Rebellion stores its image data in the so called shape files (`*.shp`).

Every `*.shp` file starts from the identifying ASCII string, `1.10` (first 4 bytes).

Next 4 bytes store the sprite count.

Then, for each sprite, a small header is written. Sprite header is just two offsets: 4-byte offset to a sprite data, and 4-byte offset to a sprite palette, both are zero-based offsets to the same file. Offset to a sprite palette is optional, and may be zero (in such case, no custom palette is defined for a sprite).

**TODO:** It seems like the embedded palette is unused in the Tone Rebellion. Check this.

Sprite data (found by the offset from the file header) contains the following fields:

- sprite height minus one in pixels, 4-byte integer (so, you have to actually add 1 to get the real height)
- sprite width minus one in pixels, 4-byte integer
- Y coordinate of the sprite origin in pixels (2-byte unsigned integer)
- X coordinate of the sprite origin in pixels (2-byte unsigned integer)
- start X coordinate of the sprite in pixels (2-byte unsigned integer)
- start Y coordinate of the sprite in pixels (2-byte unsigned integer)
- end X coordinate of the sprite in pixels (2-byte unsigned integer)
- end Y coordinate of the sprite in pixels (2-byte unsigned integer)

Please note that X and Y coordinates are written in the opposite order (first Y, then X) in case of width, height and sprite origin, but in a normal order in case of start and end.

Start and end coordinates are relative to the origin. Essentially, this allows to select a rectangle on a (potentially bigger) sprite canvas. Sprite data only gets drawn into that rectangle; other parts of the sprite are meant to be transparent.

**TODO:** Describe actual pixel data storage.

**TODO:** Describe how the palette gets chosen for a particular sprite.

[ascendancy-converter]: https://www.extractor.ru/files/051b8c7c6155fef1460fab189f7edb68/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[extractor.ru]: http://www.extractor.ru/
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
