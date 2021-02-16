using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPDB.Resource.API.Data;
using TPDB.Resource.API.Models;

namespace TPDB.Resource.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user, admin")]
    public class ProductsController : ControllerBase
    {
        private readonly TPDBResourceContext db;

        //Инжектируем контекст приложения
        public ProductsController(TPDBResourceContext context)
        {
            db = context;
        }

        //Возврат всего списка продуктов
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            List<Product> products = await db.Products.ToListAsync();

            return Ok(products);
        }

        //Возврат определенного продукта (по айди из маршрута)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            Product product = await db.Products
                .SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //Добавляем продукт из JSON в теле POST-запроса
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody]ProductPostRequest request)
        {
            if (request == null)
            {
                return BadRequest("Product from request is null");
            }

            //создаем объект Product для добавление в БД
            Product product = new Product()
            {
                Name = request.Name
            };

            //добавляем в таблицу продуктов созданный продукт
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        //Обновление продукта из JSON в теле PUT-запроса
        [HttpPut]
        public async Task<ActionResult<Port>> PutProduct([FromBody]ProductPutRequest request)
        {
            if (request == null)
            {
                return BadRequest("Product from request is null");
            }

            //Находим в БД продукт по айди из запроса
            Product product = await db.Products.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                return BadRequest($"Error while update. Product with ID {request.Id} not found");
            }

            //Изменяем свойства выбранного объекта product на значения из запроса
            product.Name = request.Name;

            await db.SaveChangesAsync();
            return Ok(product);
        }

        //Удаление определенного продукта (по айди из маршрута)
        [HttpDelete("{id}")]
        public async Task<ActionResult<Port>> DeleteProduct(int id)
        {
            Product product = await db.Products.SingleAsync(p => p.Id == id);
            //Находим товары, что связаны с продуктом
            List<Commodity> relatedCommodities = await db.Commodities
                .Include(c => c.Product)
                .Where(c => c.Product == product)
                .ToListAsync();

            if (product == null)
            {
                return BadRequest($"Error while delete. Product with ID {id} not found");
            }

            //удаляем из таблицы указанный продукт и товары,
            //что связаны с этим продуктом
            db.Commodities.RemoveRange(relatedCommodities);
            db.Products.RemoveRange(product);

            await db.SaveChangesAsync();
            return Ok(product);
        }
    }
}