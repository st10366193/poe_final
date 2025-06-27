using System.Drawing;
using System.IO;
using System;

namespace poe_final
{
    public class logo
    {
        public logo()
        {
            // Define the path to the ASCII art image
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = path.Replace("bin\\Debug\\", "");
            string full_path = Path.Combine(new_path, "ascii-text-art.jpg");

            // Load the ASCII art image
            try
            {

                Bitmap logo = new Bitmap(full_path);
                logo = new Bitmap(logo, new Size(50, 25));

                // Display the ASCII art image
                for (int height = 0; height < logo.Height; height++)
                {
                    for (int width = 0; width < logo.Width; width++)
                    {
                        // Get the color of the current pixel
                        Color pixel = logo.GetPixel(width, height);
                        int color = (pixel.R + pixel.G + pixel.B) / 3;

                        // Determine the ASCII character to display based on the color
                        char ascii_design = color > 650 ? '.' :
                                            color > 350 ? '*' :
                                            color > 250 ? '0' :
                                            color > 80 ? '#' : '@';
                        Console.Write(ascii_design);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error loading image: " + error.Message);
            }
        }
    }
}