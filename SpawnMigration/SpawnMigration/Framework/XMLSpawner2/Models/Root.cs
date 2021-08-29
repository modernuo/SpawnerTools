using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpawnMigration.Framework.XMLSpawner2.Models
{
    [XmlRoot("Spawns")]
    public class Root
    {
        [XmlElement("Points")]
        public List<Point> Points { get; set; }
    }
}
