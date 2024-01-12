using System.Collections.Generic;
using Journals.Dom.Models;

namespace Journals.Dom.Services
{
    public interface ISubscriberServices
    {
        Subscriber FindSubscriber(string subscriberName);
        
        IEnumerable<Journal> FindJounalsBySubscriber(int subscriberId);
        byte[] FindJounalBySubscriber(int subscriberId, int journalId);
    }
}