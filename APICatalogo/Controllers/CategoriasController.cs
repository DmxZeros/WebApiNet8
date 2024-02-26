using APICatalogo.Context;
using APICatalogo.Interfaces;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        //private readonly IRepository<Categoria> _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _unitOfWork.CategoriaRepository.GetAll();

                return Ok(categorias);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]  
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound($"Categoria com id= {id} não encontrada");
                }

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }


        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
                _unitOfWork.Commit();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoriaCriada.CategoriaId }, categoriaCriada);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpPut ("{id:int}")]
        public ActionResult Put(int id, Categoria categoria) 
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Dados inválidos");
                }

                _unitOfWork.CategoriaRepository.Update(categoria);
                _unitOfWork.Commit();

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com id= {id} não encontrada");
                }

                var categoriaExcluida = _unitOfWork.CategoriaRepository.Delete(categoria);
                _unitOfWork.Commit();
                
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                               "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
