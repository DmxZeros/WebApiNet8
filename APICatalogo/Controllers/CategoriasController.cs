using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            try
            {
                var categorias = _unitOfWork.CategoriaRepository.GetAll();

                if(categorias is null) 
                {
                    return NotFound("Não existem categorias");
                }

                var categoriasDTO = categorias.ToCategoriaDTOList();

                return Ok(categoriasDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]  
        public ActionResult<CategoriaDTO> Get(int id)
        {
            try
            {
                var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound($"Categoria com id= {id} não encontrada");
                }

                var categoriaDTO = categoria.ToCategoriaDTO();

                return Ok(categoriaDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
        {
            try
            {
                if (categoriaDTO is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var categoria = categoriaDTO.ToCategoria();

                var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
                _unitOfWork.Commit();

                var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = novaCategoriaDTO.CategoriaId }, novaCategoriaDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpPut ("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO) 
        {
            try
            {
                if (id != categoriaDTO.CategoriaId)
                {
                    return BadRequest("Dados inválidos");
                }

                var categoria = categoriaDTO.ToCategoria();
                var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
                _unitOfWork.Commit();

                var categoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();

                return Ok(categoriaAtualizadaDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
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

                var categoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();

                return Ok(categoriaExcluidaDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                               "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
