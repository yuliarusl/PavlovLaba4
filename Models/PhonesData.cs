using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class PhonesData
    {
        public Guid Id { get; set; } = Guid.Empty; // Уникальный ID
        public string Manufacturer { get; set; } // Производитель
        public string Model { get; set; } // Модель
        public string ModelNum { get; set; } // Наименование модел
        public int Storage { get; set; } // Объем памяти
        public double Cost { get; set; } // Стоимость
        public int Year { get; set; } // Год выпуска
        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();

            if (string.IsNullOrWhiteSpace(Manufacturer)) validationResult.Append($"Manufacturer не может быть пустым!");
            if (string.IsNullOrWhiteSpace(Model)) validationResult.Append($"Model не может быть пустым!");
            if (string.IsNullOrWhiteSpace(ModelNum)) validationResult.Append($"ModelNum не может быть пустым!");
            if (Storage <= 0) validationResult.Append($"Storage не может быть отрицательным!"); // Объем памяти не может быть отрицательм
            if (Cost <= 0) validationResult.Append("Cost не может быть отрицательным!"); // Стоимость не может быть отрицательной
            if (Year <= 1980 || Year > DateTime.Now.Year) validationResult.Append($"Ошибка в вводе параметра Year"); // Год выпуска не может быть меньше 1980 и больше текущего
            return validationResult;
        }

        public override string ToString()
        {
            return $"{Manufacturer} {Model} {ModelNum}/{Storage}\nCost:{Cost}\nYear:{Year}";
        }

    }
}
