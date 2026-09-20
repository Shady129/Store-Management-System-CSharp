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
    public class ItemMenu
    {


        private readonly BusinessLayer<Item> _bl;

        public ItemMenu(BusinessLayer<Item> bl)
        {
            _bl = bl;
        }


        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== ITEMS MENU ==========");
                Console.WriteLine("1) Add Item");
                Console.WriteLine("2) List Items");
                Console.WriteLine("3) Update Item");
                Console.WriteLine("4) Delete Item");
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
                var item = new Item
                {
                    Id = ConsoleInput.ReadInt("Id: ", 1),
                    Name = ConsoleInput.ReadString("Name: "),
                    Price = ConsoleInput.ReadDecimal("Price: ", 0),
                    Quantity = ConsoleInput.ReadInt("Quantity: ", 0)
                };

                await _bl.Add(item);
                Console.WriteLine("Item added successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
            }
        }



        private async Task ListAsync()
        {
            try
            {
                var items = await _bl.GetAll();

                Console.WriteLine();
                Console.WriteLine("---- Items ----");
                foreach (var i in items)
                {
                    Console.WriteLine($"Id: {i.Id} | Name: {i.Name} | Price: {i.Price} | Qty: {i.Quantity}");
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
            }
        }



        private async Task UpdateAsync()
        {
            try
            {
                int id = ConsoleInput.ReadInt("Enter Item Id to update: ", 1);
                var existing = await _bl.GetById(id);

                // if your GetById returns new T() when not found, you may need:
                if (existing == null || existing.Id == 0)
                {
                    Console.WriteLine("Item not found.");
                    ConsoleInput.Pause();
                    return;
                }

                var updated = new Item
                {
                    Id = id,
                    Name = ConsoleInput.ReadString($"Name ({existing.Name}): "),
                    Price = ConsoleInput.ReadDecimal($"Price ({existing.Price}): ", 0),
                    Quantity = ConsoleInput.ReadInt($"Quantity ({existing.Quantity}): ", 0)
                };

                await _bl.Update(updated);
                Console.WriteLine("Item updated successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
            }
        }



        private async Task DeleteAsync()
        {
            try
            {
                int id = ConsoleInput.ReadInt("Enter Item Id to delete: ", 1);
                await _bl.Delete(id);
                Console.WriteLine("Item deleted successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
            }
        }



        private async Task FindAsync()
        {
            try
            {
                int id = ConsoleInput.ReadInt("Enter Item Id: ", 1);
                var item = await _bl.GetById(id);

                if (item == null || item.Id == 0)
                {
                    Console.WriteLine("Item not found.");
                }
                else
                {
                    Console.WriteLine($"Id: {item.Id}");
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Price: {item.Price}");
                    Console.WriteLine($"Quantity: {item.Quantity}");
                }

                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");

            }
        }
    }
}