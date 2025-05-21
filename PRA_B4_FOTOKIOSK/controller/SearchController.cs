using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }
        

        // Start methode die wordt aangeroepen wanneer de zoek pagina opent.
        public void Start()
        {

        }

        // Wordt uitgevoerd wanneer er op de Zoeken knop is geklikt
        public void SearchButtonClick()
        {
            // Access the ComboBox through the Window property
            var selectedItem = Window.cbDagenVanDeWeek.SelectedItem as ComboBoxItem;
            string selectedDay = selectedItem?.Content as string;
            string time = SearchManager.GetSearchInput();
            int selectedFolder = -1;

            string[] timeParts = time.Split(':');
            int ihour = int.Parse(timeParts[0]);
            int iminute = int.Parse(timeParts[1]);
            int isecond = int.Parse(timeParts[2]);

            if (selectedDay == null || timeParts.Length != 3)
            {
                MessageBox.Show("Geen geldige invoer");
                
            }

           
            if (selectedDay == "maandag")
            {
                 selectedFolder = 1;
            }

            else if (selectedDay == "dinsdag")
            {
                 selectedFolder = 2;
            }
            else if (selectedDay == "woensdag")
            {
                 selectedFolder = 3;
            }
            else if  (selectedDay == "donderdag")
            {
                 selectedFolder = 4;
            }
            else if (selectedDay == "vrijdag")
            {
                 selectedFolder = 5;
            }
            else if (selectedDay == "zaterdag")
            {
                 selectedFolder = 6;
            }
            else if (selectedDay == "zondag")
            {
                 selectedFolder = 0;
            }


            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string folderName = Path.GetFileName(dir); // bijv. "0_Zondag"
                string[] parts = folderName.Split('_');

                if (parts.Length > 0 && int.TryParse(parts[0], out int folderDay) && folderDay == selectedFolder)
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string fileName = Path.GetFileName(file); // bijv. "10_05_30_id8824.jpg"
                        string[] fileParts = fileName.Split('_');

                        if (fileParts.Length > 3)
                        {
                            int hour = int.Parse(fileParts[0]);
                            int minute = int.Parse(fileParts[1]);
                            int second = int.Parse(fileParts[2]);

                            if (hour == ihour && minute == iminute && second == isecond)
                            {
                                SearchManager.SetPicture(file);
                            }
                            else
                            {
                                
                                MessageBox.Show("Geen foto gevonden met deze tijd");
                                break;

                            }
                           
                        }
                    }
                }
            }
        }


    }
} 




