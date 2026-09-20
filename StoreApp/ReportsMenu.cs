using BL;
using BL.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class ReportsMenu
    {

        private readonly OrderReportsService _reports;

        public ReportsMenu(OrderReportsService reports)
        {
            _reports = reports;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== REPORTS MENU ==========");
                Console.WriteLine("1) Total Sales Summary");
                Console.WriteLine("2) Sales By Customer");
                Console.WriteLine("3) Sales By Item");
                Console.WriteLine("4) Top Customers (Top 5)");
                Console.WriteLine("5) Top Items (Top 5)");
                Console.WriteLine("0) Back");
                Console.WriteLine("----------------------------------");

                int choice = ConsoleInput.ReadInt("Choose: ", 0, 5);

                switch (choice)
                {
                    case 1: await TotalSalesSummaryAsync(); break;
                    case 2: await SalesByCustomerAsync(); break;
                    case 3: await SalesByItemAsync(); break;
                    case 4: await TopCustomersAsync(5); break;
                    case 5: await TopItemsAsync(5); break;
                    case 0: return;
                }
            }
        }

        private async Task TotalSalesSummaryAsync()
        {
            try
            {
                Console.Clear();
                var summary = await _reports.GetTotalSalesSummary();

                Console.WriteLine("----- TOTAL SALES SUMMARY -----");
                Console.WriteLine($"Total Orders : {summary.TotalOrders}");
                Console.WriteLine($"Total Revenue: {summary.TotalRevenue}");

                ConsoleInput.Pause();
            }
            catch (BusinessExecption)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }

        private async Task SalesByCustomerAsync()
        {
            try
            {
                Console.Clear();
                var report = await _reports.GetSalesByCustomer();

                Console.WriteLine("----- SALES BY CUSTOMER -----");
                foreach (var r in report)
                {
                    Console.WriteLine(
                        $"CustomerId: {r.CustomerId} | Name: {r.CustomerName} | " +
                        $"Orders: {r.OrdersCount} | Revenue: {r.TotalRevenue}");
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }

        private async Task SalesByItemAsync()
        {
            try
            {
                Console.Clear();
                var report = await _reports.GetSalesByItem();

                Console.WriteLine("----- SALES BY ITEM -----");
                foreach (var r in report)
                {
                    Console.WriteLine(
                        $"ItemId: {r.ItemId} | Name: {r.ItemName} | " +
                        $"Qty Sold: {r.TotalQuantity} | Revenue: {r.TotalRevenue}");
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }

        private async Task TopCustomersAsync(int top)
        {
            try
            {
                Console.Clear();
                var report = await _reports.GetTopCustomers(top);

                Console.WriteLine($"----- TOP {top} CUSTOMERS -----");
                int rank = 1;
                foreach (var r in report)
                {
                    Console.WriteLine($"{rank}) {r.CustomerName} | Revenue: {r.TotalRevenue}");
                    rank++;
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }

        private async Task TopItemsAsync(int top)
        {
            try
            {
                Console.Clear();
                var report = await _reports.GetTopItems(top);

                Console.WriteLine($"----- TOP {top} ITEMS -----");
                int rank = 1;
                foreach (var r in report)
                {
                    Console.WriteLine($"{rank}) {r.ItemName} | Revenue: {r.TotalRevenue}");
                    rank++;
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }
    }
}


