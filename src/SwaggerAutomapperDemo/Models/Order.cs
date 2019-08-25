using System;
using System.Collections.Generic;
using System.Linq;

namespace SwaggerAutomapperDemo.Models
{

    /// <summary>
    /// Order --> OrderDto 使原有复杂的结构简化； DTO(Data Transfer Object):数据传输对象
    /// </summary>
    public class Order
    {
        private readonly IList<OrderLineItem> _orderLineItems = new List<OrderLineItem>();
        public Customer Customer { get; set; }
        public OrderLineItem[] GetOrderLineItems()
        {
            return _orderLineItems.ToArray();
        }
        public void AddOrderLineItem(Product product, int quantity)
        {
            _orderLineItems.Add(new OrderLineItem(product, quantity));
        }
        public decimal GetTotal()
        {
            return _orderLineItems.Sum(li => li.GetTotal());
        }
    }

    public class Product
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
    }

    public class OrderLineItem
    {
        public OrderLineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal GetTotal()
        {
            return Quantity * Product.Price;
        }
    }

    public class Customer
    {
        public string Name { get; set; }
    }




    public class OrderDto
    {
        //AutoMapper在做解析的时候会按照PascalCase
        public string CustomerName { get; set; }//PascalCase命名 混合使用大小写字母来构成变量和函数的名字，单词首字母要大写
        public decimal Total { get; set; }
    }
}
