# Scan for Anything #

![screenshot](Nexus%20image.jpg)

Make scanner rooms track anything* you want.

## Configuration ##

To track more objects in the scanner room, edit this mod's configuration file at `<Subnautica install path>\BepInEx\config\Scan for Anything\config.json`

For example:

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
Use if you want to track all life or all fragments _except_ for a few. Add the exceptions' `TechType`s to this list.

## TechTypes ##

An object's `TechType` is identical to its [Spawn ID](https://subnautica.fandom.com/wiki/Spawn_IDs_(Subnautica)#Raw_IDs_List), which you can lookup online.

Don't forget to put "quotes" around the text when adding to the configuration file, and a comma between each `TechType`.

## Limitations! ##

*Only blueprint fragments and objects with health can be added to the scanner room. This covers all creatures, plants, and resources.

Fragments are all grouped together in the scanner room menu, _not_ seperated by blueprint. Additionally, fragments that you already learned are still tracked. It keeps a sense of the unknown in your Subnautica exploration.

If you track specific creature eggs, some may still show under the general "Creature Eggs" entry.

## Recommended Mods ##

### [More Resources Discovery (by Scanner Room)](https://www.nexusmods.com/subnautica/mods/406) ###
Shows objects at the farthest ranges of your scanner room without requiring you to travel a wide circle around it. Normally, the scanner room only detects objects in areas you've visited since loading the game. This mod loads areas around the scanner room automatically over time.

### [Dynamic Scanner Blips](https://www.nexusmods.com/subnautica/mods/1160) ###
Makes scanner bullseyes smaller and more faded the farther away the objects are from you. These "bullseyes" are the circles shown in your mask when you have the scanner HUD chip equipped.

## Changelog
### 1.0.4
* Use Nautilus instead of the deprecated SMLHelper.
* `trackAllLife` no longer includes minor non-harvestable organisms from the mushroom forest or the obvious giant mushroom trees.
### 1.0.3
* Fix references to Subnautica code from before Living Large
### 1.0.2
* Compatible with Subnautica's Living Large update
### 1.0.1  
* Fix overwriting `config.json`