using BL;
using BL.Execptions;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{

    public class CustomerMenu
    {
        private readonly BusinessLayer<Customer> _bl;

        public CustomerMenu(BusinessLayer<Customer> bl)
        {
            _bl = bl;
        }



        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("======== CUSTOMERS MENU ========");
                Console.WriteLine("1) Add Customer");
                Console.WriteLine("2) List Customers");
                Console.WriteLine("3) Update Customer");
                Console.WriteLine("4) Delete Customer");
                Console.WriteLine("5) Find By Id");
                Console.WriteLine("0) Back");
                Console.WriteLine("--------------------------------");

                int choice = ConsoleInput.ReadInt("Choose: ", 0, 5);

                try
                {
                    switch (choice)
                    {
                        case 1: await AddAsync(); break;
                        case 2: await ListAsync(); break;
                        case 3: await UpdateAsync(); break;
                        case 4: await DeleteAsync(); break;
                        case 5: await FindAsync(); break;
                        case 0: return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ConsoleInput.Pause();
                }
            }
        }



        private async Task AddAsync()
        {
            try
            {
                var c = new Customer
                {
                    Id = ConsoleInput.ReadInt("Id: ", 1),
                    CustomerName = ConsoleInput.ReadString("Full Name: "),
                    Phone = ConsoleInput.ReadString("Phone: "),
                    Email = ConsoleInput.ReadString("Email: ")
                };

                await _bl.Add(c);
                Console.WriteLine("Customer added successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }



        private async Task ListAsync()
        {
            try
            {
                var customers = await _bl.GetAll();

                Console.WriteLine();
                Console.WriteLine("---- Customers ----");
                foreach (var c in customers)
                {
                    Console.WriteLine(
                        $"Id: {c.Id} | Name: {c.CustomerName} | Phone: {c.Phone} | Email: {c.Email}");
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }



        private async Task UpdateAsync()
        {
            try
            {
                int id = ConsoleInput.ReadInt("Enter Customer Id to update: ", 1);
                var existing = await _bl.GetById(id);

                if (existing == null || existing.Id == 0)
                {
                    Console.WriteLine("Customer not found.");
                    ConsoleInput.Pause();
                    return;
                }

                var updated = new Customer
                {
                    Id = id,
                    CustomerName = ConsoleInput.ReadString($"Full Name ({existing.CustomerName}): "),
                    Phone = ConsoleInput.ReadString($"Phone ({existing.Phone}): "),
                    Email = ConsoleInput.ReadString($"Email ({existing.Email}): ")
                };

                await _bl.Update(updated);
                Console.WriteLine("Customer updated successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }



        private async Task DeleteAsync()
        {
            try
            {
                int id = ConsoleInput.ReadInt("Enter Customer Id to delete: ", 1);
                await _bl.Delete(id);
                Console.WriteLine("Customer deleted successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }



        private async Task FindAsync()
        {
            try
            {
                int id = ConsoleInput.ReadInt("Enter Customer Id: ", 1);
                var c = await _bl.GetById(id);

                if (c == null || c.Id == 0)
                {
                    Console.WriteLine("Customer not found.");
                }
                else
                {
                    Console.WriteLine($"Id: {c.Id}");
                    Console.WriteLine($"Full Name: {c.CustomerName}");
                    Console.WriteLine($"Phone: {c.Phone}");
                    Console.WriteLine($"Email: {c.Email}");
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();


            }
        }
    }
}
