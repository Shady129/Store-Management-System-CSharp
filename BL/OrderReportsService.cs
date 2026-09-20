using BL.Execptions;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderReportsService
    {

        private readonly BusinessLayer<Order> _orderBL;

        public OrderReportsService(BusinessLayer<Order> orderBL)
        {
            _orderBL = orderBL;
        }



        public async Task<TotalSalesSummary> GetTotalSalesSummary()
        {
            try
            {
                var orders = await _orderBL.GetAll();

                return new TotalSalesSummary
                {
                    TotalOrders = orders.Count,
                    TotalRevenue = orders.Sum(o => o.price * o.Quantity)
                };
            }
            catch (BusinessExecption)
            {
                // rethrow to keep same behavior for UI
                throw;
            }
            catch (Exception ex)
            {
                // optional: wrap in business exception if you want
                throw new BusinessExecption("Reports error", "Failed to build total sales summary", ex);
            }
        }





        public async Task<List<SalesByCustomer>> GetSalesByCustomer()
        {
            try
            {
                var orders = await _orderBL.GetAll();

                return orders
                    .GroupBy(o => new { o.CustomerId, o.CustomerName })
                    .Select(g => new SalesByCustomer
                    {
                        CustomerId = g.Key.CustomerId,
                        CustomerName = g.Key.CustomerName,
                        OrdersCount = g.Count(),
                        TotalRevenue = g.Sum(x => x.price * x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalRevenue)
                    .ToList();
            }
            catch (BusinessExecption) { throw; }
            catch (Exception ex)
            {
                throw new BusinessExecption(
                    "Reports error",
                    "Failed to build sales by customer",
                    ex);
            }
        }



        public async Task<List<SalesByItem>> GetSalesByItem()
        {
            try
            {
                var orders = await _orderBL.GetAll();

                return orders
                    .GroupBy(o => new { o.ItemId, o.ItemName })
                    .Select(g => new SalesByItem
                    {
                        ItemId = g.Key.ItemId,
                        ItemName = g.Key.ItemName,
                        TotalQuantity = g.Sum(x => x.Quantity),
                        TotalRevenue = g.Sum(x => x.price * x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalRevenue)
                    .ToList();
            }
            catch (BusinessExecption) { throw; }
            catch (Exception ex)
            {
                throw new BusinessExecption(
                    "Reports error",
                    "Failed to build sales by item",
                    ex);
            }
        }



        public async Task<List<SalesByCustomer>> GetTopCustomers(int top)
        {
            var report = await GetSalesByCustomer();
            return report.Take(top).ToList();
        }



        public async Task<List<SalesByItem>> GetTopItems(int top)
        {
            var report = await GetSalesByItem();
            return report.Take(top).ToList();
        }
    }
}
