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

        string displayMinute = "0";
        string displaySecond = "0";

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
            if (int.TryParse(timeParts[0], out int iHour) &&
                int.TryParse(timeParts[1], out int iMinute) &&
                int.TryParse(timeParts[2], out int iSecond))
            {
                if (selectedDay == null || timeParts.Length != 3)
                {
                    MessageBox.Show("Geen geldige invoer");
                    return;
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
                else if (selectedDay == "donderdag")
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

                bool isFound = false;

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
                                if (int.TryParse(fileParts[0], out int hour) &&
                                    int.TryParse(fileParts[1], out int minute) &&
                                    int.TryParse(fileParts[2], out int second))
                                {
                                    if (hour == iHour && minute == iMinute && second == iSecond)
                                    {
                                        SearchManager.SetPicture(file);
                                        isFound = true;

                                        string displaySecond = second.ToString();
                                        string displayMinute = minute.ToString();

                                        if (minute.ToString().Length == 1)
                                        {
                                            displayMinute = "0" + minute;
                                            
                                        }

                                        if(second.ToString().Length == 1)
                                        {
                                            displaySecond = "0" + second;
                                        }

                                        string text = "Foto gevonden met tijd: " + hour + ":" + displayMinute + ":" + displaySecond;
                                        SearchManager.SetSearchImageInfo(text);
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Geen geldige tijd!");
                                }
                            }
                        }
                    }
                    if (isFound) break;
                }

                if (!isFound)
                {
                    MessageBox.Show("Geen foto gevonden met deze tijd");
                }
            }
            else
            {
                MessageBox.Show("Geen geldige tijd!");
            }
        }
    }
}
