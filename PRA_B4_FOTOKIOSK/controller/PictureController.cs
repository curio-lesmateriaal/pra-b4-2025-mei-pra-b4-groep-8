using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            PicturesToDisplay.Clear();

            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                var now = DateTime.Now;
                int dayToday = (int)now.DayOfWeek;

                string folderName = Path.GetFileName(dir); // bijv. "0_Zondag"
                string[] parts = folderName.Split('_');

                if (parts.Length > 0 && int.TryParse(parts[0], out int folderDay) && folderDay == dayToday)
                {
                    /**
                     *
                     * dir string is de map waar de fotos in staan. Bijvoorbeeld:
                     * \fotos\0_Zondag
                     */
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string fileName = Path.GetFileName(file); // bijv. "10_05_30_id8824.jpg"
                        string[] fileParts = fileName.Split('_');

                        if (fileParts.Length > 3)
                        {
                            int hour = int.Parse(fileParts[0]);
                            int minute = int.Parse(fileParts[1]);
                            int second = int.Parse(fileParts[2]);

                            DateTime fileDateTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);

                            TimeSpan timeDifference = now - fileDateTime;

                            if (timeDifference.TotalMinutes <= 30 && timeDifference.TotalMinutes >= 2 )
                            {
                                bool add = false;

                                foreach (KioskPhoto photo in PicturesToDisplay)
                                {
                                    
                                    //Foto wordt DateTime
                                    var fotoDate = DateTime.Parse(photo.Source.Split("\\")[2].Split("_id")[0].Replace("_", ":"));
                                    if (fotoDate.AddSeconds(60) != fileDateTime)
                                        continue;

                                    // Zoek de positie (index) van de huidige foto in de lijst
                                    int index = PicturesToDisplay.IndexOf(photo);

                                    // Voeg de nieuwe foto in op dezelfde plek als de gevonden foto.
                                    // Dit betekent dat de nieuwe foto net vóór de bestaande wordt geplaatst.
                                    PicturesToDisplay.Insert(index, new KioskPhoto() { Id = 0, Source = file });

                                    // Zet de 'add' vlag op true om aan te geven dat de foto al is toegevoegd,
                                    // zodat we hem later niet opnieuw onderaan de lijst toevoegen.
                                    add = true;

                                    // Stop de foreach-lus, want we hebben al een geschikte plek gevonden
                                    break;

                                }

                                if (!add)
                                {
                                    PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                                }
                            }
                        }
                    }                   
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }
    }
}
