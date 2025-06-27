using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace poe_final
{
    public class voice
    {
        public async Task PlaySoundAsync(string fileName)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string audioPath = Path.Combine(baseDir, fileName);

                if (!File.Exists(audioPath))
                {
                    // Try going up two directories for project structure
                    audioPath = Path.Combine(baseDir, "..", "..", fileName);
                    if (!File.Exists(audioPath)) return;
                }

                await Task.Run(() =>
                {
                    using (var player = new SoundPlayer(audioPath))
                    {
                        player.PlaySync(); // PlaySync to ensure completion
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio error: {ex.Message}");
            }
        }
    }
}