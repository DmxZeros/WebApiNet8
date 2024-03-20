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
    public class GetProdutoUnitTestes:  IClassFixture<ProdutosUnitTesteController>
    {
        private readonly ProdutosController _produtosController;

        public GetProdutoUnitTestes(ProdutosUnitTesteController produtosController)
        {
            _produtosController = new ProdutosController(produtosController.repositorio, produtosController.mapper);
        }

        [Fact]
        public async Task GetProdutoById_OkResult()
        {
            //arrange
            var prodId = 2;

            ////Assert (xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //act
            var data = await _produtosController.Get(prodId);

            //assert(fluentassertions)
            data.Result.Should().BeOfType<OkObjectResult>() //verifica se o resultado é do tipo OkObjectResult.
                .Which.StatusCode.Should().Be(200); //verifica se o código de status do OkObjectResult é 200.
        }

        [Fact]
        public async Task GetProdutoById_Return_NotFound()
        {
            //Arrange  
            var prodId = 999;

            // Act  
            var data = await _produtosController.Get(prodId);

            // Assert  
            data.Result.Should().BeOfType<NotFoundObjectResult>()
                        .Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetProdutoById_ReturnBadRequest()
        {
            //arrange
            var prodId = 2000;

            //act
            var data = await _produtosController.Get(prodId);

            //assert(fluentassertions)
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetProdutos_Return_ListOfProdutoDTO()
        {
            // Act  
            var data = await _produtosController.Get();

            // Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>() //Verifica se o valor do OkObjectResult é atribuível a IEnumerable<ProdutoDTO>.
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetProdutos_Return_BadRequestResult()
        {
            // Act  
            var data = await _produtosController.Get();

            //Assert
            data.Result.Should().BeOfType<BadRequestResult>();
        }
    }
}

