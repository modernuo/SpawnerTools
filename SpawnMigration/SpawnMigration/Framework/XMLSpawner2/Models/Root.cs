using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
