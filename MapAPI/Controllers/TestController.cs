using MapAPI.Data;
using MapAPI.Models.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.HPRtree;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace MapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MapSQLContext _context;
        public TestController(MapSQLContext _MapSQLContext)
        {
            _context = _MapSQLContext;
        }

        

        [HttpGet("Test")]
        public ActionResult Get()
        {
            //var test = _context.Test.Take(1);

            var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            var pntAUR = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(7.5404, 53.4837));
            // Create a point at Emden (lat=53.3646, long=7.1559)
            var pntLER = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(7.1559, 53.3646));
            // Create a point at Leer (lat=53.2476, long=7.4550)
            var pntEMD = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(25, 120));
            var mpnt = gf.CreateMultiPoint(new[] { pntAUR, pntLER, pntEMD });

            DateTime date1 = new DateTime(1500, 1, 1);
            GeoData data = new GeoData
            {
                Tag = 1,
                Geo = pntEMD,
                Chinese = "測試",
                English = "Test",
                Date = date1,
                Describe = "Describe"
            };
            _context.GeoData.Add(data);
            _context.SaveChanges();
            return Ok("Test");

            /*var test = _context.GeoData.Take(1);
            

            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new GeometryConverter()
                },
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };

            string jsonString = JsonSerializer.Serialize(test, options);
            return Ok(jsonString);*/
        }

    }
}
