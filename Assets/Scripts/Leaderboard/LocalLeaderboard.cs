using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Leaderboard
{
    public class LocalLeaderboard : ILeaderboard
    {
        private string FilePath;
        private List<Entry> Entries;
        
        private LocalLeaderboard()
        {
        }

        public static async Task<LocalLeaderboard> LoadFrom(string filePath)
        {
            if (!File.Exists(filePath))
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
            
            return new LocalLeaderboard
            {
                FilePath = filePath,
                Entries = JsonUtility.FromJson<List<Entry>>(await File.ReadAllTextAsync(filePath))
            };
        }

        public Task<IReadOnlyList<ILeaderboardEntry>> GetEntries()
        {
            IReadOnlyList<ILeaderboardEntry> entries = new ReadOnlyCollection<ILeaderboardEntry>(Entries
                .Cast<ILeaderboardEntry>()
                .OrderByDescending(entry => entry.Score)
                .ToList());
            
            return Task.FromResult(entries);
        }

        public async Task AddEntry(string name, int score)
        {
            Entries.Add(new Entry
            {
                Name = name,
                Score = score
            });

            await File.WriteAllTextAsync(FilePath, JsonUtility.ToJson(Entries));
        }

        public class Entry : ILeaderboardEntry
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }
    }
}
