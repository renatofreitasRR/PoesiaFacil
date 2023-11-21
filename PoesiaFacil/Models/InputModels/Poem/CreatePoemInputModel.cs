namespace PoesiaFacil.Models.InputModels.Poem
{
    public class CreatePoemInputModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
