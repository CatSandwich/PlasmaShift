using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leaderboard
{
    public interface ILeaderboard
    {
        Task<IReadOnlyList<ILeaderboardEntry>> GetEntries();
        Task AddEntry(string name, int score);
    }

    public interface ILeaderboardEntry
    {
        string Name { get; }
        int Score { get; }
    }
}