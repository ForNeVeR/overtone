Palette File Format
===================

Author would like to thank authors of the following tools (in no particular order) for providing the information about the palette file format (which is the same in The Tone Rebellion and an older game from The Logic Factory, Ascendancy):

- [Ascendancy Converter v1.0][ascendancy-converter] by [Михаил Бесчетнов aka Terminus][extractor.ru]
- [Tone Extractor Tools][fadoli.tone-rebellion-extractor] by Franck M. ([@Fadoli][fadoli])
- [ascendancy][daumiller.ascendancy] utilities by Darcy ([@daumiller][daumiller])

The Tone Rebellion uses 64-color (6-bit) palette, and the palette data is stored in `.pal` files.

The file format is simple: every palette is exactly 768 bytes long, and stores 256 colors. Every color is stored in 3 bytes: `RGB`, where each byte holds a 6-bit value. To convert from these palette values to a modern 32-bit ARGB color, each value should be multiplied by 4.

[ascendancy-converter]: https://www.extractor.ru/files/051b8c7c6155fef1460fab189f7edb68/
[daumiller.ascendancy]: https://github.com/daumiller/ascendancy
[daumiller]: https://github.com/daumiller
[extractor.ru]: http://www.extractor.ru/
[fadoli.tone-rebellion-extractor]: https://github.com/Fadoli/Tone-rebellion-extractor
[fadoli]: https://github.com/Fadoli
