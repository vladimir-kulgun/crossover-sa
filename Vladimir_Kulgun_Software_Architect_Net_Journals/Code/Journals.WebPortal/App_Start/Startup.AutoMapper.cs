using AutoMapper;

namespace Journals.WebPortal
{
    public partial class Startup
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Configuration.AllowNullCollections = true;

            Mapper.CreateMap<Dom.Models.Journal, ViewModel.JournalViewModel>();
            Mapper.CreateMap<Dom.Models.Journal, Models.JournalModel>();
        }
    }
}