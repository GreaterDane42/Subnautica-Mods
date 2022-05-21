# Scan for Anything #

![screenshot](Nexus%20image.jpg)

Make scanner rooms track anything you want.

## Configuration ##

Edit this mod's `config.json` to add objects to your scanner rooms. For example:

```json
{
    "trackAllFragments": true,
    "trackAllLife": false,
    "track": [
        "PurpleBrainCoral",
        "Bladderfish"
    ],
    "exclude": [
        "SeaglideFragment"
    ]
}
```
This adds all blueprint fragments _except_ seaglide fragments to the scanner room, plus brain coral and bladderfish.

### `trackAllFragments`  
Track all blueprint fragments by setting this `true`.

### `trackAllLife`  
Track all creatures and plants by setting this `true`. 

Technically, it tracks all objects with health except fish schools, eggs, and base pieces. (If you want those for some reason, add them to the `track` list.) There may be other unexpected objects with health, let me know if any slipped through.

### `track`  
Track specific objects by adding their `TechType` to this list.

### `exclude`  
Only use if you want to track all life or fragments _except_ for a few. Add the exceptions' `TechType`s to this list.

## TechType ##

The TechType of an object is identical to its [Spawn ID](https://subnautica.fandom.com/wiki/Spawn_IDs_(Subnautica)#Raw_IDs_List).

Don't forget to put "quotes" around the text when adding to the configuration file, and a comma between each TechType.

## Limitations! ##

Only blueprint fragments and objects with health can be added to the scanner room. This covers all creatures, plants, and resources, so I don't envision allowing much more.

Fragments are all grouped together in the scanner room menu, _not_ seperated by blueprint. Additionally, fragments that you already learned are still tracked. I might change this, but I might not. It keeps a sense of the unknown in your Subnautica exploration.

If you track specific creature eggs, some may still show under the general "Creature Eggs" entry. I'm not sure why, and I don't care enough to find out.
