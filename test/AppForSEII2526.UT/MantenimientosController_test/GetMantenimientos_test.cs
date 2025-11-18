using AppForSEII2526.API.DTOs.MantenimientosDTO;
using AppForSEII2526.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.MantenimientosController_test
{
    public class GetMantenimientos_test : AppForSEII25264SqliteUT
    {
        public GetMantenimientos_test() //constructor de la clase que simula los datos para los test
        {
            var mantenimientos = new List<Maintenance>()
            {
                new Maintenance(1,"Mant1",6,70),
                new Maintenance(2,"Mant2",3,35),
                new Maintenance(3,"Mant3",13,58)
            };
            var tipos = new List<MaintenanceType>()
            {
                new MaintenanceType(1,"Motor", mantenimientos[0]),
                new MaintenanceType(2,"Interior", mantenimientos[1]),
                new MaintenanceType(3,"Motor",mantenimientos[2]),
            };

            _context.AddRange(tipos);
            _context.AddRange(mantenimientos);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_GetMantenimientos_OK()
        {
            var mantenimientosDTOs = new List<MantenimientoDTO>() {
        new MantenimientoDTO(1,"Mant1","Motor",70,6),
        new MantenimientoDTO(2,"Mant2","Interior",35,3),
        new MantenimientoDTO(3,"Mant3","Motor",58,13)
    };

            var mantDTOsTC1 = new List<MantenimientoDTO>() { mantenimientosDTOs[0]};
            var mantDTOsTC2 = new List<MantenimientoDTO>() { mantenimientosDTOs[1] };
            var mantDTOsTC3 = new List<MantenimientoDTO>() { mantenimientosDTOs[2] }; 
            var mantDTOsTC4 = new List<MantenimientoDTO>() { mantenimientosDTOs[0], mantenimientosDTOs[1], mantenimientosDTOs[2] }; // si no hay filtros, devuelve todos

            var allTests = new List<object[]>
    {
        new object[] { "Mant1", "Motor", mantDTOsTC1 },   // filtra por nombre y tipo → devuelve todos los que coincidan
        new object[] { null, "Interior", mantDTOsTC2 },   // filtra solo por tipo → devuelve Mant2
        new object[] { "Mant3", null, mantDTOsTC3 },      // filtra solo por nombre → devuelve Mant1
        new object[] { null, null, mantDTOsTC4 }          // sin filtros → devuelve todos
    };
            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_GetMantenimientos_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMantenimientos_OK_test(string? name, string? type, IList<MantenimientoDTO> expectedMantenimientos) //esto es esto { null, null, movieDTOsTC1 }
        {
            // Arrange
            var controller = new MantenimientosController(_context, null);

            // Act
            var result = await controller.GetMantenimientos(name,type);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var MantenimientoDTOsActual = Assert.IsType<List<MantenimientoDTO>>(okResult.Value);
            Assert.Equal(expectedMantenimientos, MantenimientoDTOsActual);

        }
    }
}
