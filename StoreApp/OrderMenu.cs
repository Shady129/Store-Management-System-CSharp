using BL;
using BL.Execptions;
using Domains;
using StoreApp;

namespace UI
{
    public class OrderMenu
    {
        private readonly BusinessLayer<Order> _orderBL;
        private readonly BusinessLayer<Customer> _customerBL;
        private readonly BusinessLayer<Item> _itemBL;

        public OrderMenu(
            BusinessLayer<Order> orderBL,
            BusinessLayer<Customer> customerBL,
            BusinessLayer<Item> itemBL)
        {
            _orderBL = orderBL;
            _customerBL = customerBL;
            _itemBL = itemBL;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== ORDERS MENU ==========");
                Console.WriteLine("1) Create Order");
                Console.WriteLine("2) List Orders");
                Console.WriteLine("3) Find Order By Id");
                Console.WriteLine("4) Update Order");
                Console.WriteLine("5) Delete Order");
                Console.WriteLine("0) Back");
                Console.WriteLine("--------------------------------");

                int choice = ConsoleInput.ReadInt("Choose: ", 0, 5);

                try
                {
                    switch (choice)
                    {
                        case 1: await CreateAsync(); break;
                        case 2: await ListAsync(); break;
                        case 3: await FindAsync(); break;
                        case 4: await UpdateAsync(); break;
                        case 5: await DeleteAsync(); break;
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




        private async Task CreateAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("----- CREATE ORDER -----");

                var order = new Order
                {
                    Id = ConsoleInput.ReadInt("Order Id: ", 1),
                    OrderDate = DateTime.Now
                };

                await FillCustomerAndItem(order);

                order.Quantity = ConsoleInput.ReadInt("Quantity: ", 1);


                var item = await _itemBL.GetById(order.ItemId);



                if (item.Quantity == 0)
                {
                    Console.WriteLine("Item is out of stock.");
                    ConsoleInput.Pause();
                    return;
                }

                


                if (order.Quantity > item.Quantity)
                {
                    Console.WriteLine("Requested quantity exceeds available stock.");
                    ConsoleInput.Pause();
                    return;
                }

                await _orderBL.Add(order);

                item.Quantity -= order.Quantity;

                await _itemBL.Update(item);

                Console.WriteLine("Order created successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("Business logic problem.");
                ConsoleInput.Pause();
            }
        }


        private async Task UpdateAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("----- UPDATE ORDER -----");

                int id = ConsoleInput.ReadInt("Enter Order Id: ", 1);
                var existing = await _orderBL.GetById(id);

                if (existing == null || existing.Id == 0)
                {
                    Console.WriteLine("Order not found.");
                    ConsoleInput.Pause();
                    return;
                }



                int oldItemId = existing.ItemId;
                int oldQuantity = existing.Quantity;

                existing.OrderDate = DateTime.Now;

                await FillCustomerAndItem(existing);


                var oldItem = await _itemBL.GetById(oldItemId);


                existing.Quantity = ConsoleInput.ReadInt($"Quantity ({existing.Quantity}): ", 1);


                var newItem = await _itemBL.GetById(existing.ItemId);

                bool itemChanged = oldItemId != existing.ItemId;


                if (itemChanged)
                {
                    oldItem.Quantity += oldQuantity;
                    await _itemBL.Update(oldItem);
                }


                int availableQuantity = itemChanged
                      ? newItem.Quantity
                      : newItem.Quantity + oldQuantity;


                if (existing.Quantity > availableQuantity)
                {
                    Console.WriteLine("Requested quantity exceeds available stock.");
                    ConsoleInput.Pause();
                    return;
                }


                if (itemChanged)
                {
                    oldItem.Quantity += oldQuantity;
                    await _itemBL.Update(oldItem);
                }



                newItem.Quantity = availableQuantity - existing.Quantity;


                await _itemBL.Update(newItem);

                await _orderBL.Update(existing);

                Console.WriteLine("Order updated successfully.");
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
                Console.Clear();
                var orders = await _orderBL.GetAll();

                Console.WriteLine("----- ORDERS LIST -----");
                foreach (var o in orders)
                {
                    Console.WriteLine(
                        $"Id: {o.Id} | Date: {o.OrderDate:d} | " +
                        $"Customer: {o.CustomerName} | Item: {o.ItemName} | " +
                        $"Price: {o.price} | Qty: {o.Quantity} | " +
                        $"Total: {o.price * o.Quantity}");
                }

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
                Console.Clear();
                int id = ConsoleInput.ReadInt("Enter Order Id: ", 1);
                var order = await _orderBL.GetById(id);

                if (order == null || order.Id == 0)
                {
                    Console.WriteLine("Order not found.");
                }
                else
                {
                    Console.WriteLine("----- ORDER DETAILS -----");
                    Console.WriteLine($"Order Id      : {order.Id}");
                    Console.WriteLine($"Order Date    : {order.OrderDate}");
                    Console.WriteLine($"Customer      : {order.CustomerName}");
                    Console.WriteLine($"Item          : {order.ItemName}");
                    Console.WriteLine($"Price         : {order.price}");
                    Console.WriteLine($"Quantity      : {order.Quantity}");
                    Console.WriteLine($"Total         : {order.price * order.Quantity}");
                }

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
                Console.Clear();
                int id = ConsoleInput.ReadInt("Enter Order Id to delete: ", 1);

                var order = await _orderBL.GetById(id);

                

                if (order == null || order.Id == 0)
                {
                    Console.WriteLine("Order not found.");
                    ConsoleInput.Pause();
                    return;
                }

                var item = await _itemBL.GetById(order.ItemId);

                item.Quantity += order.Quantity;

                await _itemBL.Update(item);


                await _orderBL.Delete(id);

            

                Console.WriteLine("Order deleted successfully.");
                ConsoleInput.Pause();
            }
            catch (BusinessExecption ex)
            {
                Console.WriteLine("business logic problem");
                ConsoleInput.Pause();
            }
        }



        private async Task FillCustomerAndItem(Order order)
        {
            // Customers
            var customers = await _customerBL.GetAll();

            Console.WriteLine("\nCustomers:");
            foreach (var c in customers)
                Console.WriteLine($"Id: {c.Id} | {c.CustomerName}");

            order.CustomerId = ConsoleInput.ReadInt(
                $"Customer Id ({order.CustomerId}): ", 1);

            var customer = customers.FirstOrDefault(c => c.Id == order.CustomerId);
            if (customer == null)
                throw new BusinessExecption("", "");
            order.CustomerName = customer.CustomerName;

            // Items
            var items = await _itemBL.GetAll();

            Console.WriteLine("\nItems:");
            foreach (var i in items)
                Console.WriteLine($"Id: {i.Id} | {i.Name} | Price: {i.Price}");

            order.ItemId = ConsoleInput.ReadInt(
                $"Item Id ({order.ItemId}): ", 1);

            
            var item = items.FirstOrDefault(i => i.Id == order.ItemId);

            if (item == null)
                throw new BusinessExecption("", "");
            order.ItemName = item.Name;
            order.price = item.Price;
        }
    }
}
