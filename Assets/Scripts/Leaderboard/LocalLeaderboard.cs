using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Leaderboard
{
    public class LocalLeaderboard
    {
        private List<Entry> Entries;
        
        private LocalLeaderboard()
        {
        }

        public static LocalLeaderboard Load()
        {
            if (!PlayerPrefs.HasKey("Leaderboard"))
            {
                Debug.LogWarning("No leaderboard found. Creating...");
                return new LocalLeaderboard
                {
                    Entries = new List<Entry>
                    {
                        new() { Name = "Josh", Score = 10000 },
                        new() { Name = "Elijah", Score = 8000 },
                        new() { Name = "Evan", Score = 6000 }
                    }
                };
            }

            LeaderboardData data = LeaderboardData.Deserialize(PlayerPrefs.GetString("Leaderboard"));
            
            return new LocalLeaderboard
            {
                Entries = data.Entries
            };
        }

        public List<Entry> GetEntries()
        {
            return Entries
                .OrderByDescending(entry => entry.Score)
                .ToList();
        }

        public void AddEntry(string name, int score)
        {
            Entries.Add(new Entry
            {
                Name = name,
                Score = score
            });

            PlayerPrefs.SetString("Leaderboard", new LeaderboardData
            {
                Entries = Entries
            }.Serialize());
        }

        [Serializable]
        public class Entry
        {
            public string Name;
            public int Score;
        }

        [Serializable]
        public class LeaderboardData
        {
            public List<Entry> Entries;

            public static LeaderboardData Deserialize(string data)
            {
                return new LeaderboardData
                {
                    Entries = data.Split("\n")
                        .Select(line => new Entry
                        {
                            Name = line.Split("|")[0],
                            Score = int.Parse(line.Split("|")[1])
                        }).ToList()
                };
            }

            public string Serialize()
            {
                return string.Join("\n", Entries.Select(entry => $"{entry.Name}|{entry.Score}"));
            }
        }
    }
}
