namespace Shared.Models
{
    public class UploadedImage
    {
        public string NewImageFileExtenstion { get; set; }
        
        //Base64 is basically a string that represents binary

        public string NewImageBase64Content { get; set; }

        public string OldImagePath { get; set; }
    }
}
