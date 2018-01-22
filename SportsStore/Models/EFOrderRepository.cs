using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> Orders => context.Orders
                                            .Include(o => o.Lines) //Запрос связанных данных помимо Ordres еще и Lines в объекте Order
                                                .ThenInclude(p => p.Product); //В объект Line загружаем инфу по Product, как бы продолжение цепочки

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product)); //Указываем EfCore что Product уже добавлены и можно их юзать для ссылки
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
