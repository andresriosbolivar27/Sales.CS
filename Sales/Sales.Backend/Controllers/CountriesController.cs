
namespace Sales.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Sales.Backend.Data;
    using Sales.Shared.Entities;

    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CountriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Countries.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var currenCountry = await _dataContext.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (currenCountry == null)
            {
                return NotFound();
            }

            return Ok(currenCountry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> GetAsync(int id, Country country)
        {
            var currenCountry = await _dataContext.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (currenCountry == null)
            {
                return NotFound();
            }

            currenCountry.Name = country.Name;
            _dataContext.Countries.Update(currenCountry);
            await _dataContext.SaveChangesAsync();

            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _dataContext.Add(country);
           await _dataContext.SaveChangesAsync();

            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var currenCountry = await _dataContext.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (currenCountry == null)
            {
                return NotFound();
            }
            
            _dataContext.Countries.Remove(currenCountry);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
