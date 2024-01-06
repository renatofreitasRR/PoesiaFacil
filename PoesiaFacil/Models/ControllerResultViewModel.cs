namespace PoesiaFacil.Models
{
    public class ControllerResultViewModel
    {
        public List<string> Errors { get; set; } = new List<string>();

        public object? Data { get; set; }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }
    }
}
