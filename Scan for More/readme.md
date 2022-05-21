# Scan for Anything #

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
This adds all tech fragments _except_ seaglide fragments to the scanner room, plus brain coral and bladderfish.

Note that fragments are all grouped together in the scanner room menu, _not_ seperated by blueprint. Additionally, fragments that you already learned are still tracked. I might change this, but I might not. It keeps a sense of the unknown in your Subnautica exploration.

`trackAllFragments`
Track all tech fragments by setting this `true`.

`trackAllLife`
Track all creatures and plants by setting this `true`. 

Technically, it tracks all objects with health except fish schools, eggs, and base pieces. (If you want those for some reason, add them to the `track` list.) There may be other unexpected objects with health, let me know if any slipped through.

`track`
Track specific objects by adding their `TechType` to this list.

`exclude`
Only use if you want to track all life or fragments _except_ for a few. Add the exceptions' `TechType`s to this list.

## Limitations ##

Only tech fragments and objects with health can be added to the scanner room. This covers all creatures, plants, and resources, so I don't envision allowing much more.

If you track specific creature eggs, some may still show under the general "Creature Eggs" entry. I'm not sure why, and I don't care enough to find out.

## TechType ##

The TechType of an object is simply its Spawn ID surrounded by "quotes".

https://subnautica.fandom.com/wiki/Spawn_IDs_(Subnautica)
For creatures and plants, check the "Raw IDs List" near the bottom.