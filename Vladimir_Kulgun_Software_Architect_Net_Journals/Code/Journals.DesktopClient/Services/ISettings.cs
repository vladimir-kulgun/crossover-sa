namespace Journals.DesktopClient.Services
{
    /// <summary>
    ///     Defines application settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        ///     Gets the web API host.
        /// </summary>
        /// <value>
        ///     The web API host.
        /// </value>
        string WebApiHost { get; }
    }
}