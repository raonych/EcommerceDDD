using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Repository.Repositories;

namespace UnitTestEcommerceDDD
{
    [TestClass]
    public sealed class UnitTestEcommerce
    {
        [TestMethod]
        public async Task AddProductComSucesso()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);
                var produto = new Produto
                {
                    Descricao = string.Concat("Descrição Test DDD", DateTime.Now.ToString()),
                    QtdEstoque = 10,
                    Nome = string.Concat("Nome Test DDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "835ad18f-d841-4834-bce5-9011d8b9b5d1"
                };

                await _IServiceProduct.AddProduct(produto);

                Assert.IsFalse(produto.Notitycoes.Any());
            }
            catch (Exception) 
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task AddProductComValidacaoCampoObrigatorio()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);
                var produto = new Produto
                {
                   
                };

                await _IServiceProduct.AddProduct(produto);

                Assert.IsTrue(produto.Notitycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ListarProdutosUsuario()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();

                var listaProdutos = await _IProduct.ListarProdutosUsuario("835ad18f-d841-4834-bce5-9011d8b9b5d1");

                Assert.IsTrue(listaProdutos.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listaProdutos = await _IProduct.ListarProdutosUsuario("835ad18f-d841-4834-bce5-9011d8b9b5d1");


                var produto = await _IProduct.GetEntityById(listaProdutos.LastOrDefault().Id); 

                Assert.IsTrue(produto != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listaProdutos = await _IProduct.ListarProdutosUsuario("835ad18f-d841-4834-bce5-9011d8b9b5d1");


                var ultimoProduto = listaProdutos.LastOrDefault();

                await _IProduct.Delete(ultimoProduto);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}
