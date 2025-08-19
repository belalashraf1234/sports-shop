namespace AutoEcommerce.UploadService.Dto;


    public class UploadResult
    {
        public bool Succeeded { get; set; }
        public List<string> FileNames { get; set; }
        public string Error { get; set; }
    }
