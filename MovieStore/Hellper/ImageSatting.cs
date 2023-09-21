namespace MovieStore.Hellper
{
    public static class ImageSatting
    {
        public static string UploadImage(IFormFile File , string Folder)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Folder);

            var fileNmae = Guid.NewGuid+File.FileName; 
            var FilePath = Path.Combine(FolderPath, fileNmae);
            var Fs = new FileStream(FilePath, FileMode.Create); 
            File.CopyTo(Fs);
            return fileNmae; 
        }

        public static void DeleteImage(string Filename, string FolderNmae)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderNmae, Filename);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
