# SpawnerTools


## SpawnMigration
The application makes it possible to migrate different types of spawners from other systems over to ModernUO. As the target file format, ModernUO uses the JSON format for all spawners.

Spawners of the following source systems are already possible to migrate: 
* **XMLSpawner2**

Since the entire worldspawn is usually located in a single XML file, individual facets can be extracted.

## Example

The programm consumes the following arguments:

![grafik](https://user-images.githubusercontent.com/4610892/131004352-f4ff1364-a767-4cc2-8166-7cfbc7436b79.png)

```bash
Open a command line interface
SpawnMigration.exe -m Migrate -n XMLSpawner2 -f Felucca -s C:/TMP/AccurateSpawn.xml -t C:/UOServer/ModernUO/Data/Spawns/felucca_accurate.json
```
