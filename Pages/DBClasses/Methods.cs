using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace lab_1_part_3.Pages.DBClasses
{
    public class Methods
    {

        public static string renamer(string imgName, string username)
        {

            string[] originalName = imgName.Split('.');
            originalName[0] = username;
            string newName = originalName[0] + "."+ originalName[1];
            return Directory.GetCurrentDirectory() + @"\wwwroot\images\" + newName;
        }
        public static string imgFinder(string username)
        {
            string imagesDir = Directory.GetCurrentDirectory() + @"\wwwroot\images\";
            DirectoryInfo imagesFolder = new DirectoryInfo(imagesDir);
            var fileListing = imagesFolder.GetFiles();
            foreach(var file in fileListing)
            {
                string[] originalName = file.Name.Split('.');
                if (originalName[0].Equals(username))
                {
                    return @"\images\"+ originalName[0] +"."+ originalName[1];
                }
            }
            return "";
        }       
        public static string[] splitter(string desiredSkills)
        {
            string[] splitArray = desiredSkills.Split(',');
            return splitArray;
        }
      
    }
}
