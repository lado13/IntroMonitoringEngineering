namespace IntroMonitoringEngineering.ViewModel.Image
{
    public class EditImageViewModel
    {

        public int Id { get; set; }
        public string AltText { get; set; }
        public string CurrentImageUrl { get; set; } 
        public IFormFile NewImage { get; set; } 
        public int DetailId { get; set; } 


    }
}
