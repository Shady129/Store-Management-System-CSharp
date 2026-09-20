using BL;
using Domains;
using UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class StoreConsoleApp
    {


        private readonly ItemMenu _itemMenu;
        private readonly CustomerMenu _customerMenu;
        private readonly OrderMenu _orderMenu;
        private readonly ReportsMenu _reportMenu;

        public StoreConsoleApp()
        {
            // Manual wiring (no DI)

            DAL.StorageType storageType = DAL.StorageType.JsonFile; // Set storage type here

            // Business layers
            var itemBL = new BusinessLayer<Item>(storageType);
            var customerBL = new BusinessLayer<Customer>(storageType);
            var orderBL = new BusinessLayer<Order>(storageType);
            var reportBl = new OrderReportsService(orderBL);

            // Menus
            _itemMenu = new ItemMenu(itemBL);
            _customerMenu = new CustomerMenu(customerBL);
            _orderMenu = new OrderMenu(orderBL, customerBL, itemBL);
            _reportMenu = new ReportsMenu(reportBl);
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("        STORE MANAGEMENT SYSTEM     ");
                Console.WriteLine("====================================");
                Console.WriteLine("1) Items");
                Console.WriteLine("2) Customers");
                Console.WriteLine("3) Orders");
                Console.WriteLine("4) Reports");
                Console.WriteLine("0) Exit");
                Console.WriteLine("------------------------------------");

                int choice = ConsoleInput.ReadInt("Choose: ", 0, 4);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            await _itemMenu.ShowAsync();
                            break;

                        case 2:
                            await _customerMenu.ShowAsync();
                            break;

                        case 3:
                            await _orderMenu.ShowAsync();
                            break;
                        case 4:
                            await _reportMenu.ShowAsync();
                            break;
                        case 0:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    //onsoleInput.Pause();
                }

            }
        }
    }
}