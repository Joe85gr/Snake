using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class SaveData
    {
        public List<(string Name, float Score)> Scores { get; set; } = new List<(string Name, float Score)>();
    }
}