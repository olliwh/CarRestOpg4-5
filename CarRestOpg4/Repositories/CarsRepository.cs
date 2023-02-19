using CarCLOpg1;
namespace CarRestOpg4.Repositories
{
    public class CarsRepository
    {
        private int _nextId = 1;
        private List<Car> Data;

        public CarsRepository()
        {
            Data = new List<Car>()
            {
                new Car() { Id = _nextId++, Model = "v1000", Price = 40, LicensePlate = "BO12345" },
                new Car() { Id = _nextId++, Model = "v2000", Price = 50, LicensePlate = "JA23456" }
            };
        }
        public List<Car> GetAll()
        {
            return new List<Car>(Data);
        }
        public Car? GetById(int id)
        {
            return Data.Find(x => x.Id == id);
        }
        public Car Add(Car car)
        {
            car.Id = _nextId++;
            Data.Add(car);
            return car;
        }
        public Car? Delete(int id)
        {
            Car? carToDelete = GetById(id);
            if (carToDelete == null) return null;
            Data.Remove(carToDelete);
            return carToDelete;
        }
        public Car? Update(int id, Car car)
        {
            Car? carToUpdate = GetById(id);
            if (carToUpdate == null) return null;
            carToUpdate.Price = car.Price;
            carToUpdate.Model = car.Model;
            carToUpdate.LicensePlate = car.LicensePlate;
            return carToUpdate;
        }
    }
}
