using SpawnMigration.Framework.XMLSpawner2.Models;
using SpawnMigration.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SpawnMigration.Framework.XMLSpawner2
{
    public class XMLSpawner2
    {
        /// <summary>
        /// Converts an XML from XMLSpawner2 into a MUO json file
        /// </summary>
        /// <param name="sourcePath">A specific .xml file or a path containing multiple</param>
        /// <param name="targetPath">A specific .json file to save to or a path that all will be saved to</param>
        /// <param name="facet"></param>
        public XMLSpawner2(string sourcePath, string targetPath, string facet)
        {
            var attributes = File.GetAttributes(sourcePath);
            var files = attributes.HasFlag(FileAttributes.Directory) ? Directory.GetFiles(sourcePath) : new[] { sourcePath };
            
            foreach (string currentFile in files)
            {
                using (var fileStream = File.Open(currentFile, FileMode.Open))
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

                                var pointEntry = new PointEntry
                                {
                                    Name = spawnsplits[0],
                                    MX = int.Parse(Regex.Match(spawnsplits[1], @"\d+").Value),
                                    SB = int.Parse(Regex.Match(spawnsplits[2], @"\d+").Value),
                                    RT = int.Parse(Regex.Match(spawnsplits[3], @"\d+").Value),
                                    TO = int.Parse(Regex.Match(spawnsplits[4], @"\d+").Value),
                                    KL = int.Parse(Regex.Match(spawnsplits[5], @"\d+").Value),
                                    RK = int.Parse(Regex.Match(spawnsplits[6], @"\d+").Value),
                                    CA = int.Parse(Regex.Match(spawnsplits[7], @"\d+").Value),
                                    DN = int.Parse(Regex.Match(spawnsplits[8], @"\d+").Value),
                                    DX = int.Parse(Regex.Match(spawnsplits[9], @"\d+").Value),
                                    SP = int.Parse(Regex.Match(spawnsplits[10], @"\d+").Value),
                                    PR = int.Parse(Regex.Match(spawnsplits[11], @"\d+").Value)
                                };

                                pointEntries.Add(pointEntry);
                            }
                        }
                        else if (item.Objects2 != "")
                        {
                            string[] spawnsplits = Regex.Split(item.Objects2, ":");

                            var pointEntry = new PointEntry
                            {
                                Name = spawnsplits[0],
                                MX = int.Parse(Regex.Match(spawnsplits[1], @"\d+").Value),
                                SB = int.Parse(Regex.Match(spawnsplits[2], @"\d+").Value),
                                RT = int.Parse(Regex.Match(spawnsplits[3], @"\d+").Value),
                                TO = int.Parse(Regex.Match(spawnsplits[4], @"\d+").Value),
                                KL = int.Parse(Regex.Match(spawnsplits[5], @"\d+").Value),
                                RK = int.Parse(Regex.Match(spawnsplits[6], @"\d+").Value),
                                CA = int.Parse(Regex.Match(spawnsplits[7], @"\d+").Value),
                                DN = int.Parse(Regex.Match(spawnsplits[8], @"\d+").Value),
                                DX = int.Parse(Regex.Match(spawnsplits[9], @"\d+").Value),
                                SP = int.Parse(Regex.Match(spawnsplits[10], @"\d+").Value),
                                PR = int.Parse(Regex.Match(spawnsplits[11], @"\d+").Value)
                            };

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
                            location = new int[3]
                                { int.Parse(item.CentreX), int.Parse(item.CentreY), int.Parse(item.CentreZ) },
                            map = item.Map,
                            count = int.Parse(item.MaxCount),
                            entries = JSONSpawnElementEntries,
                            homeRange = int.Parse(item.Range),
                            walkingRange = int.Parse(item.Range),
                            maxDelay = TimeSpan.FromMinutes(int.Parse(item.MaxDelay)).ToString(),
                            minDelay = TimeSpan.FromMinutes(int.Parse(item.MinDelay)).ToString()
                        };

                        if (string.Equals(entry.map, facet, StringComparison.CurrentCultureIgnoreCase))
                        {
                            JSONSpawners.Add(entry);
                        }
                    }
                    
                    var jsonString = JsonSerializer.Serialize(JSONSpawners, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    var fileName = Path.GetFileName(targetPath);

                    File.WriteAllText(
                        string.IsNullOrEmpty(fileName)
                            ? Path.Combine(targetPath, $"{Path.GetFileNameWithoutExtension(currentFile)}.json")
                            : targetPath, jsonString);
                }
            }
        }
    }
}