using crud.produto.DAO;
using crud.produto.Models;
using Microsoft.AspNetCore.Mvc;

namespace crud.produto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensController(ItemDAO itemDao) : ControllerBase
    {
        private readonly ItemDAO _itemDao = itemDao;

        [HttpGet]
        public IActionResult GetItens()
        {
            var itens = _itemDao.GetItens();
            return Ok(itens);
        }

        [HttpPost("criar")]
        public IActionResult CreateItem(Item item)
        {
            var createdId = _itemDao.CreateItem(item); // Captura o ID criado
            var createdItem = _itemDao.GetItemById(createdId); // Obtenha o item completo após a criação
            return CreatedAtAction(nameof(GetItemById), new { id = createdId }, createdItem);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            var item = _itemDao.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            _itemDao.UpdateItem(id, item);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            _itemDao.DeleteItem(id);
            return NoContent();
        }
    }
}
