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
    public class PostProdutoUnitTestes : IClassFixture<ProdutosUnitTesteController>
    {
        private readonly ProdutosController _produtosController;

        public PostProdutoUnitTestes(ProdutosUnitTesteController controller)
        {
            _produtosController = new ProdutosController(controller.repositorio, controller.mapper);
        }

        //metodos de testes para POST
        [Fact]
        public async Task PostProduto_Return_CreatedStatusCode()
        {
            // Arrange  
            var novoProdutoDto = new ProdutoDTO
            {
                Nome = "Novo Produto",
                Descricao = "Descrição do Novo Produto",
                Preco = 10.99m,
                ImagemUrl = "imagemfake1.jpg",
                CategoriaId = 2
            };

            // Act  
            var data = await _produtosController.Post(novoProdutoDto);

            // Assert  
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task PostProduto_Return_BadRequest()
        {
            ProdutoDTO prod = null;

            // Act              
            var data = await _produtosController.Post(prod);

            // Assert  
            var badRequestResult = data.Result.Should().BeOfType<BadRequestResult>();
            badRequestResult.Subject.StatusCode.Should().Be(400);
        }
    }
}

