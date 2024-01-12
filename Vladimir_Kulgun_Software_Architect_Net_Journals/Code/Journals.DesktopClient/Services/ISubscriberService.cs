using System.Collections.Generic;
using System.Threading.Tasks;
using Journals.DesktopClient.Models;

namespace Journals.DesktopClient.Services
{
    public interface ISubscriberService
    {
        Task<int> Login(string userName);

        Task<IEnumerable<Journal>> GetJournals(int userId);

        Task<byte[]> GetJournal(int userId, int journalId);
    }
}