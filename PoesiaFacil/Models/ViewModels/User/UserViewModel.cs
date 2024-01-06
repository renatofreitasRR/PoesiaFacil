using PoesiaFacil.Entities;

namespace PoesiaFacil.Models.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ArtisticName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool ShowOnlyArtisticName { get; set; }
        public bool IsActive { get; set; }
        public IList<Poem>? Poems { get; set; }
        public IList<string>? Connections { get; set; }
    }
}
