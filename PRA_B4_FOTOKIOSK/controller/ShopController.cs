using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {

        public static Home? Window { get; set; }

        public void Start()
        {
            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");





            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55, Description = "Omschrijving 1" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 15x20", Price = 4.00, Description = "Omschrijving 2" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Sleutelhanger", Price = 7.00, Description = "Omschrijving 3" });
            ShopManager.Products.Add(new KioskProduct() { Name = "mok", Price = 9.33, Description = "Omschrijving 4" });
            ShopManager.Products.Add(new KioskProduct() { Name = "T-shirt", Price = 12.69, Description = "Omschrijving 5" });

            foreach (KioskProduct item in ShopManager.Products)
            {
                ShopManager.AddShopPriceList(item.Name + ": €" + item.Price + "\n");
            }



            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
            int? fotoId = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();

            if (selectedProduct == null)
            {
                MessageBox.Show("Selecteer een product");
                return;
            }

            if (fotoId == null)
            {
                MessageBox.Show("Vul een fotoId in");
                return;
            }

            if (amount == null)
            {
                MessageBox.Show("Vul een aantal in");
                return;
            }


              double final = selectedProduct.Price * (int)amount;

              string finalString = final.ToString();

            ShopManager.SetShopReceipt("Bon: \n");

            List<OrderedProduct> list = new List<OrderedProduct>();

            list.Add(new OrderedProduct((int)fotoId, selectedProduct.Name, (int)amount, final));

            ShopManager.AddShopReceipt("FotoId: " + fotoId + "\nProduct: " + selectedProduct.Name + "\nAantal: " + amount + "\nTotaal Prijs: €" + finalString + "\n\n");



           
            // Get the base directory (e.g., bin\Debug\net6.0-windows)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Go up three levels to reach the project root (where the .csproj is)
            string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;

            // Combine with your file name
            string filePath = Path.Combine(projectRoot, "Bon.txt");

            string tekst = "Bon: " + "\nFotoId: " + fotoId + "\nProduct: " + selectedProduct.Name + "\nAantal: " + amount + "\nTotaal Prijs: €" + finalString + "\n\n";

           

            File.WriteAllText(filePath, tekst);






















        }
        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }
        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
        }

    }
}
