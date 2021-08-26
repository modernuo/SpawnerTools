using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpawnMigration.Shared.Models
{
    public class JSONSpawnEntry
    {
        public int maxCount { get; set; }
        public string name { get; set; }
        public int probability { get; set; }
    }
}
