using System.Collections.Generic;

namespace task2
{
    class CarsCatalog
    {
        private List<Car> cars = new List<Car>();

        public void Add(Car car) => cars.Add(car);

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= cars.Count)
                    return "Нет такой машины";
                var car = cars[index];
                return $"{car.Name}, двигатель: {car.Engine}";
            }
        }
    }
}