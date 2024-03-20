using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoXUnitTest.UnidadeTestes
{
    public class DeleteProdutoUnitTestes: IClassFixture<ProdutosUnitTesteController>
    {
        private readonly ProdutosController _produtosController;

        public DeleteProdutoUnitTestes(ProdutosUnitTesteController controller)
        {
            _produtosController = new ProdutosController(controller.repositorio, controller.mapper);
        }

        //testes para o Delete
        [Fact]
        public async Task DeleteProdutoById_Return_OkResult()
        {
            var prodId = 3;

            // Act
            var result = await _produtosController.Delete(prodId) as ActionResult<ProdutoDTO>;

            // Assert  
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.Result.Should().BeOfType<OkObjectResult>(); // Verifica se o resultado é OkResult
        }

        [Fact]
        public async Task DeleteProdutoById_Return_NotFound()
        {
            // Arrange  
            var prodId = 999;

            // Act
            var result = await _produtosController.Delete(prodId) as ActionResult<ProdutoDTO>;

            // Assert  
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.Result.Should().BeOfType<NotFoundObjectResult>(); // Verifica se o resultado é NotFoundResult

        }
    }
}
