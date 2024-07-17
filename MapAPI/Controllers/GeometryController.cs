using MapAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using MapAPI.Models.Test;
using MapAPI.Models.Controller;

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


    }
}
