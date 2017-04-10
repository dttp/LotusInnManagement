using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class HousesController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<House> GetHouses()
        {
            return Execute(session => HouseService.GetHouses(session.User.House?.Id));
        }

        [AcceptVerbs("GET")]
        public House GetById(string id)
        {
            return Execute(session => HouseService.GetById(id));
        }

        [AcceptVerbs("POST")]
        public House Add(House house)
        {
            return Execute(session => HouseService.AddHouse(house));
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Update(string id, House house)
        {
            return Execute(session =>
            {
                HouseService.UpdateHouse(house);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                HouseService.DeleteHouse(id);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }
    }
}