using System.Collections.Generic;

namespace SpawnMigration.Shared.Models
{
    public class JSONSpawn
    {
        public string type { get; set; }
        public int[] location { get; set; }
        public string map { get; set; }
        public int count { get; set; }
        public List<JSONSpawnEntry> entries { get; set; }
        public int homeRange { get; set; }
        public int walkingRange { get; set; }
        public string maxDelay { get; set; }
        public string minDelay { get; set; }
    }
}
