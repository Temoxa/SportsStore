using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {

        public int ProductID { get; set; }
        [UIHint("String")]
        [Required(ErrorMessage = "Введите имя продукта")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните описание продукта")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        [Range(typeof(decimal), "0,01", "1000,00", ErrorMessage = "Цена не может быть отрицательной")]
        [DataType(DataType.Currency, ErrorMessage = "Неверный тип данных")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Укажите категорию")]
        public string Category { get; set; }
    }
}
