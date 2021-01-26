using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPDB.Resource.API.Data;
using TPDB.Resource.API.Models;

namespace TPDB.Resource.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommoditiesController : ControllerBase
    {
        private readonly TPDBResourceContext db;

        //Инжектируем контекст приложения
        public CommoditiesController(TPDBResourceContext context)
        {
            db = context;
        }

        //Возврат всего списка товаров
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commodity>>> Get()
        {
            return await db.Commodities.Include(c => c.Product).ToListAsync();
        }

        //Возврат определенного товара (по айди из маршрута)
        [HttpGet("{id}")]
        public async Task<ActionResult<Commodity>> Get(int id)
        {
            return await db.Commodities.Include(c => c.Product).SingleOrDefaultAsync(c => c.Id == id);
        }

        //Добавляем товар из JSON в теле POST-запроса
        [HttpPost]
        public async Task<ActionResult<Commodity>> Post([FromBody]CommodityPostRequest request)
        {
            if (request == null)
            {
                return BadRequest("Commodity from request is null");
            }

            //Находим порт по айди из запроса
            Port port = await db.Ports.FindAsync(request.PortId);
            //Находим продукт по имени из запроса
            Product product = await db.Products
                .SingleOrDefaultAsync(p => p.Name.ToLower() == request.ProductName.ToLower());

            //проверка port и product на наличие в бд
            if (port == null)
            {
                return BadRequest($"Error while add. Port with ID {request.PortId} not found");
            }
            if (product == null)
            {
                return BadRequest($"Error while update. Product with name {request.ProductName} not found");
            }

            //создаем объект Commodity для добавление в БД
            Commodity commodity = new Commodity()
            {
                Product = product,
                Price = request.Price,
                Quantity = request.Quantity,
                ForImport = request.ForImport
            };

            //добавляем в таблицу товаров созданный товар
            port.Commodities.Add(commodity);
            await db.SaveChangesAsync();

            return Ok(commodity);
        }

        //Обновление товара из JSON в теле PUT-запроса
        [HttpPut]
        public async Task<ActionResult<Commodity>> Put([FromBody]CommodityPutRequest request)
        {
            if (request == null)
            {
                return BadRequest("Commodity from request is null");
            }

            //находим товар по айди из запроса
            Commodity commodity = await db.Commodities.SingleOrDefaultAsync(c => c.Id == request.CommodityId);
            //Находим порт по айди из запроса
            Port port = await db.Ports.FindAsync(request.PortId);
            //Находим продукт по имени из запроса
            Product product = await db.Products
                .SingleOrDefaultAsync(p => p.Name.ToLower() == request.ProductName.ToLower());

            //проверка port, commodity и product на наличие в бд
            if (commodity == null)
            {
                return BadRequest($"Error while update. Commodity with ID {request.CommodityId} not found");
            }
            if (port == null)
            {
                return BadRequest($"Error while update. Port with ID {request.PortId} not found");
            }
            if (product == null)
            {
                return BadRequest($"Error while update. Product with name {request.ProductName} not found");
            }

            //Изменяем свойства выбранного объекта commodity на значения из запроса
            commodity.Product = product;
            commodity.Price = request.Price;
            commodity.Quantity = request.Quantity;
            commodity.ForImport = request.ForImport;

            await db.SaveChangesAsync();
            return Ok(commodity);
        }

        //Удаление определенного товара (по айди из маршрута)
        [HttpDelete("{id}")]
        public async Task<ActionResult<Commodity>> Delete(int id)
        {
            Commodity commodity = await db.Commodities.Include(c => c.Product).SingleAsync(c => c.Id == id);

            if (commodity == null)
            {
                return BadRequest($"Error while delete. Commodity with ID {id} not found");
            }

            db.Commodities.Remove(commodity);
            await db.SaveChangesAsync();
            return Ok(commodity);
        }
    }
}