using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Interfaces;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoXUnitTest.UnidadeTestes
{
    public class ProdutosUnitTesteController
    {
        public IUnitOfWork repositorio;
        public IMapper mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionUnitTest =
            "Data Source=DESKTOP-GJ9N82I\\SQLEXPRESS;DataBase=WebApi;Integrated Security=SSPI;TrustServerCertificate=True";

        static ProdutosUnitTesteController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionUnitTest).Options;
        }

        public ProdutosUnitTesteController()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProdutoDTOMappingProfile());
            });

            mapper = config.CreateMapper();
            var context = new AppDbContext(dbContextOptions);
            repositorio = new UnitOfWork(context);

        }
    }
}
    
