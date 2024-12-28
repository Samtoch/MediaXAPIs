namespace MediaXAPIs.Utilities
{
    public class ImageUtilities
    {
        /// <summary>
        /// Converts an image file to a Base64 string.
        /// </summary>
        /// <param name="imagePath">The full qualified path of the image file.</param>
        /// <returns>A Base64 string representation of the image, ready to embed in HTML.</returns>
        public static string ConvertImageToBase64(string relativePath)
        {
            try
            {
                //string projectRoot = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(baseDirectory).FullName).FullName).FullName).FullName;

                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

                if (!File.Exists(relativePath))
                {
                    throw new FileNotFoundException("The specified image file was not found.", relativePath);
                }

                // Read all bytes from the image file
                byte[] imageBytes = File.ReadAllBytes(relativePath);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                // Determine the file type based on extension
                string fileExtension = Path.GetExtension(relativePath)?.ToLower();
                string mimeType = fileExtension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".bmp" => "image/bmp",
                    _ => throw new InvalidOperationException("Unsupported image format.")
                };

                // Return the Base64 string in data URI format
                return $"data:{mimeType};base64,{base64String}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting image to Base64: {ex.Message}");
                return null;
            }
        }
    }
}
