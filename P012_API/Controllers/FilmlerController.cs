using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmlerController : ControllerBase
    {
        private readonly DataContext _context;

        public FilmlerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Film>>> Get()
        {
            var data = await _context.Film.ToListAsync();

            return Ok(data);    //200 kodunu dön, veriyi liste olarak dön
        }

        //FilmId sini gönderelim ve o Id nin Film bilgileri gelsin
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetById(int id)
        {
            //veritabanındaki film tablosuna gitik, id si, bize parametre olarak gelen veri ile aynı olan satırı data değişkenine 
            //taşıdık.
            //var data = _context.Film.Where(t=>t.Id==filmId);
            var data = _context.Film.FindAsync(id);
            // if(data is null)
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        //Post -> Create
        [HttpPost]

        public async Task<ActionResult> Add(Film film)
        {
            try
            {
                _context.Film.Add(film);
                await _context.SaveChangesAsync();
            }
            catch (Exception hata)
            {
                return NoContent();
            }

            return Ok("Başarılı");
        }

        //Put -> Update
        [HttpPut]
        public async Task<ActionResult<Film>> Edit(Film film)
        {
            /*
             * 1. db den film datasına erişmeliyiz
             * 2. eriştiğimiz bu data içinde, parametre olarak gödnerilen verileri değiştirmeliyiz.
             * 3. db ye update olarak yazılmasını isteyeceğiz
             */

            var data = await _context.Film.Where(t => t.Id == film.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return NotFound();
            }

            data.YonetmenAdi = film.YonetmenAdi;
            data.FilmAdi = film.FilmAdi;
            data.Kategori = film.Kategori;
            data.CikisYili = film.CikisYili;
            data.Basrol = film.Basrol;

            _context.Update(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Film>>> Delete(Film film)
        {
            var deleted = await _context.Film.FindAsync(film.Id);
            if (deleted == null)
                return BadRequest(string.Empty);

            _context.Remove(deleted);
            await _context.SaveChangesAsync();

            return Ok(await _context.Film.ToListAsync());
        }
    }
}
