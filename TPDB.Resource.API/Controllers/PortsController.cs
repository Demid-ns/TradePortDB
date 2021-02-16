using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using TPDB.Resource.API.Data;
using TPDB.Resource.API.Models;

namespace TPDB.Resource.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user, admin")]
    public class PortsController : ControllerBase
    {
        private readonly TPDBResourceContext db;

        //Инжектируем контекст приложения
        public PortsController(TPDBResourceContext context)
        {
            db = context;
        }

        //Возврат всего списка портов
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Port>>> GetAllPorts()
        {
            List<Port> ports= await db.Ports
                .Include(p => p.Commodities)
                .ThenInclude(p => p.Product)
                .ToListAsync();

            return Ok(ports);
        }

        //Возврат определенного порта (по айди из маршрута)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Port>> GetPortById(int id)
        {
            Port port = await db.Ports
                .Include(p => p.Commodities)
                .ThenInclude(p => p.Product)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (port == null)
            {
                return NotFound();
            }

            return Ok(port);
        }

        //Добавляем порт из JSON в теле POST-запроса
        [HttpPost]
        public async Task<ActionResult<Port>> PostPort([FromBody]PortPostRequest request)
        {
            if (request == null)
            {
                return BadRequest("Port from request is null");
            }

            //создаем объект Port для добавление в БД
            Port port = new Port()
            {
                System = request.System,
                Body = request.Body
            };

            //добавляем в таблицу портов созданный порт
            await db.Ports.AddAsync(port);
            await db.SaveChangesAsync();

            return Ok(port);
        }

        //Обновление товара из JSON в теле PUT-запроса
        [HttpPut]
        public async Task<ActionResult<Port>> PutPort([FromBody]PortPutRequest request)
        {
            if (request == null)
            {
                return BadRequest("Port from request is null");
            }

            //Находим в БД порт по айди из запроса
            Port port = await db.Ports.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (port == null)
            {
                return BadRequest($"Error while update. Port with ID {request.Id} not found");
            }

            //Изменяем свойства выбранного объекта port на значения из запроса
            port.System = request.System;
            port.Body = request.Body;

            await db.SaveChangesAsync();
            return Ok(port);
        }

        //Удаление определенного порта (по айди из маршрута)
        [HttpDelete("{id}")]
        public async Task<ActionResult<Port>> DeletePort(int id)
        {
            Port port = await db.Ports.SingleAsync(p => p.Id == id);

            if (port == null)
            {
                return BadRequest($"Error while delete. Port with ID {id} not found");
            }

            //удаляем из таблицы указанный порт
            db.Ports.Remove(port);
            await db.SaveChangesAsync();
            return Ok(port);
        }
    }
}