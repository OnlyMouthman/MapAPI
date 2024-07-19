using MapAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using MapAPI.Models.Test;
using MapAPI.Models.Controller;
using System.Xml.Linq;

namespace MapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeometryController : ControllerBase
    {
        private readonly MapSQLContext _context;
        public GeometryController(MapSQLContext _MapSQLContext)
        {
            _context = _MapSQLContext;
        }

        /// <summary>
        /// 取得搜群資料
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("SearchName")]
        public ActionResult Get(string search)
        {
            //https://github.com/NetTopologySuite/NetTopologySuite/wiki/GettingStarted
            var data = _context.GeoData.Where(w => w.Chinese == search);
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new GeometryConverter()
                },
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };
            string jsonString = JsonSerializer.Serialize(data, options);
            return Ok(jsonString);
        }

        /// <summary>
        /// 存入point
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="chinese"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("SetPoint")]
        public ActionResult Get(MyPoint MyData)
        {
            var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            var point = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(MyData.Longitude, MyData.Latitude));


            DateTime? date1 = MyData.Date;
            GeoData data = new GeoData
            {
                Tag = 1,
                Geo = point,
                Chinese = MyData.Chinese,
                English = MyData.English,
                Date = date1,
                Describe = MyData.Describe
            };
            _context.GeoData.Add(data);
            _context.SaveChanges();

            return Ok("OK");
        }

        [HttpGet("SetPolygon")]
        public ActionResult Get()
        {
            var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            var ply1 = gf.CreatePolygon(new[] {
                new NetTopologySuite.Geometries.Coordinate(23.000211702386608, 120.20001226250344),
                new NetTopologySuite.Geometries.Coordinate(22.997751705006976, 120.19900963877257),
                new NetTopologySuite.Geometries.Coordinate(22.99500495947909, 120.1978589196683),
                new NetTopologySuite.Geometries.Coordinate(22.99486063548646, 120.19780779510972),
                new NetTopologySuite.Geometries.Coordinate(22.99432726176107, 120.19767146226121),
                new NetTopologySuite.Geometries.Coordinate(22.993091083447638,  120.19737834758234),
                new NetTopologySuite.Geometries.Coordinate(22.991468974815888, 120.19698980052505),
                new NetTopologySuite.Geometries.Coordinate(22.990753609563356, 120.19681256871979),
                new NetTopologySuite.Geometries.Coordinate(22.990747334412262, 120.19688073479847),
                new NetTopologySuite.Geometries.Coordinate(22.991447012830434,  120.19706137466261),
                new NetTopologySuite.Geometries.Coordinate(22.993069507399866, 120.19744444559944),
                new NetTopologySuite.Geometries.Coordinate(22.99483278184036, 120.19786366646343),
                new NetTopologySuite.Geometries.Coordinate(22.994964555953587, 120.19792160763137),
                new NetTopologySuite.Geometries.Coordinate(22.99645850099445,  120.19853907453938),
                new NetTopologySuite.Geometries.Coordinate(22.99781614335143, 120.19912515007064),
                new NetTopologySuite.Geometries.Coordinate(22.9988237294083, 120.19953958743855),
                new NetTopologySuite.Geometries.Coordinate(23.000181245452367, 120.20010679751817),
                new NetTopologySuite.Geometries.Coordinate(23.000211702386608, 120.20001226250344),
            });

            DateTime? date1 = new DateTime(2024, 7, 18);
            GeoData data = new GeoData
            {
                Tag = 1,
                Geo = ply1,
                Chinese = "",
                English = "",
                Date = date1,
                Describe = ""
            };
            _context.GeoData.Add(data);
            _context.SaveChanges();

            return Ok("OK");
        }
    }
}
