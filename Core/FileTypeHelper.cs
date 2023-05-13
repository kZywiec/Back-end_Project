using System;

namespace Core 
{
    // 
    // Pomyśłałem że może się przydać jeśli nie to do usunięcia. /Krystian
    // 

    public static class FileTypeHelper
	{

        /// <summary>
        /// Regognation of file type. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns> File type </returns>
        public static string GetFileType(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            if (fileBytes.Length < 4)
            {
                return "Unknown";
            }

            if (fileBytes[0] == 0x25 && fileBytes[1] == 0x50 && fileBytes[2] == 0x44 && fileBytes[3] == 0x46)
            {
                return "PDF";
            }

            if (fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47)
            {
                return "PNG";
            }

            if (fileBytes[0] == 0x50 && fileBytes[1] == 0x4B && fileBytes[2] == 0x03 && fileBytes[3] == 0x04)
            {
                return "DOCX";
            }

            if (fileBytes[0] == 0xEF && fileBytes[1] == 0xBB && fileBytes[2] == 0xBF)
            {
                return "TXT";
            }

            return "Unknown";
        }
    }
}