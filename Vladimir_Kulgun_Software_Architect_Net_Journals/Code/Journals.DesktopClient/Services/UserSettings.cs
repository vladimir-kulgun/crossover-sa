namespace Journals.DesktopClient.Services
{
    class UserSettings : IUserSettings
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int JournalId { get; set; }
        public string JournalName { get; set; }
    }
}