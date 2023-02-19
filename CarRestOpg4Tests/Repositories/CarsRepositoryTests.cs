using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRestOpg4.Repositories;
using CarCLOpg1;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRestOpg4.Repositories.Tests
{
    [TestClass()]
    public class CarsRepositoryTests
    {
        Car car = new Car() { Model = "V3000", Price = 100000, LicensePlate = "OK12345" };
        CarsRepository _repo = new CarsRepository();

        [TestMethod()]
        public void GetAllTest()
        {
            List<Car> cars = _repo.GetAll();
            Assert.IsNotNull(cars);
            Assert.AreEqual(2,cars.Count);
            var hs = new HashSet<Car>();

            foreach (var c in cars)
            {
                Assert.IsInstanceOfType(c, typeof(Car));
                hs.Add(c);
            }
            Assert.AreEqual(hs.Count,cars.Count);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Car? carToFind = _repo.GetById(2);
            Assert.IsNotNull(carToFind);
            Assert.IsInstanceOfType(carToFind, typeof(Car));
            Assert.AreEqual(carToFind.Id, 2);
            Assert.AreEqual(carToFind.Model, "v2000");
            Assert.IsNull(_repo.GetById(5));
        }

        [TestMethod()]
        public void AddTest()
        {
            _repo.Add(car);
            Assert.AreEqual(_repo.GetAll().Count, 3);
            Assert.AreEqual(car.Id, 3);
            Car? addedCar = _repo.GetById(3);
            Assert.AreEqual(addedCar?.Model, "V3000");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            _repo.Delete(1);
            Assert.IsNull(_repo.GetById(1));
            Assert.AreEqual(_repo.GetAll().Count, 1);
            Assert.IsNull(_repo.Delete(5));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Car? oldCar = _repo.GetById(1);
            _repo.Update(1, car);
            Car? updatedCar = _repo.GetById(1);
            Assert.IsNotNull(updatedCar);
            Assert.AreEqual(updatedCar.Model, car.Model);
            Assert.AreNotEqual(updatedCar.LicensePlate, "BO12345");
            Assert.IsNull(_repo.Update(5, car));
            Assert.AreSame(oldCar, updatedCar);
            Assert.AreEqual(oldCar, updatedCar);


        }
    }
}