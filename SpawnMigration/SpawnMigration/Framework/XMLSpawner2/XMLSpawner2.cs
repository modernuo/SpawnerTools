using SpawnMigration.Framework.XMLSpawner2.Models;
using SpawnMigration.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpawnMigration.Framework.XMLSpawner2
{
    public class XMLSpawner2
    {
        public XMLSpawner2(string sourcepath, string targetpath, string facet)
        {
            using (var fileStream = File.Open(sourcepath, FileMode.Open))
            {


                XmlSerializer serializer = new XmlSerializer(typeof(Root));
                var parsedObject = (Root)serializer.Deserialize(fileStream);

                List<JSONSpawn> JSONSpawners = new List<JSONSpawn>();

                foreach (var item in parsedObject.Points)
                {
                    // Save to model list NOTICE: Temp 
                    List<PointEntry> pointEntries = new List<PointEntry>();

                    if (item.Objects2 != "" && item.Objects2.Contains("OBJ="))
                    {
                        foreach (var obj in Regex.Split(item.Objects2, "OBJ="))
                        {
                            string[] spawnsplits = Regex.Split(obj, ":");

                            var pointEntry = new PointEntry();
                            pointEntry.Name = spawnsplits[0];
                            pointEntry.MX = Int32.Parse(Regex.Match(spawnsplits[1], @"\d+").Value);
                            pointEntry.SB = Int32.Parse(Regex.Match(spawnsplits[2], @"\d+").Value);
                            pointEntry.RT = Int32.Parse(Regex.Match(spawnsplits[3], @"\d+").Value);
                            pointEntry.TO = Int32.Parse(Regex.Match(spawnsplits[4], @"\d+").Value);
                            pointEntry.KL = Int32.Parse(Regex.Match(spawnsplits[5], @"\d+").Value);
                            pointEntry.RK = Int32.Parse(Regex.Match(spawnsplits[6], @"\d+").Value);
                            pointEntry.CA = Int32.Parse(Regex.Match(spawnsplits[7], @"\d+").Value);
                            pointEntry.DN = Int32.Parse(Regex.Match(spawnsplits[8], @"\d+").Value);
                            pointEntry.DX = Int32.Parse(Regex.Match(spawnsplits[9], @"\d+").Value);
                            pointEntry.SP = Int32.Parse(Regex.Match(spawnsplits[10], @"\d+").Value);
                            pointEntry.PR = Int32.Parse(Regex.Match(spawnsplits[11], @"\d+").Value);

                            pointEntries.Add(pointEntry);

                        }
                    }
                    else if (item.Objects2 != "")
                    {
                        string[] spawnsplits = Regex.Split(item.Objects2, ":");

                        var pointEntry = new PointEntry();
                        pointEntry.Name = spawnsplits[0];
                        pointEntry.MX = Int32.Parse(Regex.Match(spawnsplits[1], @"\d+").Value);
                        pointEntry.SB = Int32.Parse(Regex.Match(spawnsplits[2], @"\d+").Value);
                        pointEntry.RT = Int32.Parse(Regex.Match(spawnsplits[3], @"\d+").Value);
                        pointEntry.TO = Int32.Parse(Regex.Match(spawnsplits[4], @"\d+").Value);
                        pointEntry.KL = Int32.Parse(Regex.Match(spawnsplits[5], @"\d+").Value);
                        pointEntry.RK = Int32.Parse(Regex.Match(spawnsplits[6], @"\d+").Value);
                        pointEntry.CA = Int32.Parse(Regex.Match(spawnsplits[7], @"\d+").Value);
                        pointEntry.DN = Int32.Parse(Regex.Match(spawnsplits[8], @"\d+").Value);
                        pointEntry.DX = Int32.Parse(Regex.Match(spawnsplits[9], @"\d+").Value);
                        pointEntry.SP = Int32.Parse(Regex.Match(spawnsplits[10], @"\d+").Value);
                        pointEntry.PR = Int32.Parse(Regex.Match(spawnsplits[11], @"\d+").Value);

                        pointEntries.Add(pointEntry);
                    }

                    // Creates the output list needed for JSONSpawnElement
                    List<JSONSpawnEntry> JSONSpawnElementEntries = new List<JSONSpawnEntry>();
                    foreach (var spawnEntry in pointEntries)
                    {

                        JSONSpawnElementEntries.Add(
                            new JSONSpawnEntry()
                            {
                                maxCount = spawnEntry.MX,
                                name = spawnEntry.Name,
                                probability = 100

                            });
                    }

                    JSONSpawn entry = new JSONSpawn()
                    {
                        type = "Spawner",
                        location = new int[3] { Int32.Parse(item.CentreX), Int32.Parse(item.CentreY), Int32.Parse(item.CentreZ) },
                        map = item.Map,
                        count = Int32.Parse(item.MaxCount),
                        entries = JSONSpawnElementEntries,
                        homeRange = Int32.Parse(item.Range),
                        walkingRange = Int32.Parse(item.Range),
                        maxDelay = TimeSpan.FromMinutes(Int32.Parse(item.MaxDelay)).ToString(),
                        minDelay = TimeSpan.FromMinutes(Int32.Parse(item.MinDelay)).ToString()


                    };

                    if (entry.map.ToLower() == facet.ToLower())
                    {
                        JSONSpawners.Add(entry);
                    }
                                       



                }

                var jsonString = JsonSerializer.Serialize(JSONSpawners);
                File.WriteAllText(targetpath, jsonString);


            }
        }
    }
}
