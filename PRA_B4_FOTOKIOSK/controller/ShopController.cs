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
        private List<OrderedProduct> orderedProducts = new List<OrderedProduct>();


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

            // Voeg het nieuwe product toe aan de lijst
            orderedProducts.Add(new OrderedProduct((int)fotoId, selectedProduct.Name, (int)amount, final));

            // Bouw de bon op met alle producten
            StringBuilder bon = new StringBuilder();
            double totaal = 0;
            bon.AppendLine("Bon:");
            foreach (var product in orderedProducts)
            {
                bon.AppendLine(
                    $"FotoId: {product.photoId}\nProduct: {product.productName}\nAantal: {product.amount}\nTotaal Prijs: €{product.totalPrice:0.00}\n"
                );
                totaal += product.totalPrice;
            }
            bon.AppendLine($"Totaal te betalen: €{totaal:0.00}");

            ShopManager.SetShopReceipt(bon.ToString());

            // Schrijf de bon naar het project root (waar de .csproj staat)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(projectRoot, "Bon.txt");
            File.WriteAllText(filePath, bon.ToString());
        }


        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            orderedProducts.Clear();
            ShopManager.SetShopReceipt("Eindbedrag\n€");
        }
        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
        }

    }
}
