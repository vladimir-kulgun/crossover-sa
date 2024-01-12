namespace Journals.DesktopClient.Services
{
    public interface IUserSettings
    {
        int UserId { get; set; }
        string UserName { get; set; }

        int JournalId { get; set; }
        string JournalName { get; set; }
    }
}