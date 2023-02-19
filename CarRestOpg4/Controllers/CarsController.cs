using Microsoft.AspNetCore.Mvc;
using CarRestOpg4.Repositories;
using CarCLOpg1;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRestOpg4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsRepository _repository;

        public CarsController(CarsRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            List<Car> carsToReturn = _repository.GetAll();
            if (carsToReturn.Count < 1) return NotFound("No cars found");
            return Ok(carsToReturn);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Car?> Get(int id)
        {
            Car? foundCar = _repository.GetById(id);
            if (foundCar == null) return NotFound("No such car, id: " + id);
            return Ok(foundCar);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car value)
        {
            try
            {
                Car? addedCar = _repository.Add(value);
                return Created($"/{addedCar.Id}", addedCar);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Car?> Put(int id, [FromBody] Car value)
        {
            try
            {
                Car? updatedCar = _repository.Update(id, value);
                if (updatedCar == null) return NotFound("No such car, id: " + id);
                return Ok(updatedCar);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Car?> Delete(int id)
        {
            Car? deletedCar = _repository.Delete(id);
            if (deletedCar == null) return NotFound("No such car, id: " + id);
            return Ok(deletedCar);
        }
    }
}
