using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwaggerAutomapperDemo.Models;

namespace SwaggerAutomapperDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly User _user;
        public ValuesController(IMapper mapper)
        {
            _mapper = mapper;
            _user = new User() { Name = "rohmeng", Id = 10 };
        }

        /// <summary>
        /// Gets the user dto.
        /// </summary>
        /// <returns>The user dto.</returns>
        [HttpGet]
        public ActionResult<UserDTO> GetUserDto()
        {
            var dto = _mapper.Map<UserDTO>(_user);
            return dto;
        }

        /// <summary>
        /// Gets the order dto.
        /// </summary>
        /// <returns>The order dto.</returns>
        [HttpGet]
        public ActionResult<OrderDto> GetOrderDto()
        {
            var customer = new Customer
            {
                Name = "Rohmeng"
            };
            var order = new Order
            {
                Customer = customer
            };
            var bosco = new Product
            {
                Name = "Bosco",
                Price = 4.99m
            };
            order.AddOrderLineItem(bosco, 15);
            var dto = _mapper.Map<OrderDto>(order);
            return dto;
        }

        [HttpGet]
        public ActionResult<CalendarEventForm> GetCalendarEventForm()
        {
            var calendarEvent = new CalendarEvent
            {
                EventDate = DateTime.Now,
                Title = "Company Holiday Party"
            };
            var dto = _mapper.Map<CalendarEventForm>(calendarEvent);
            return dto;
        }

        /// <summary>
        /// automapper 映射集合
        /// </summary>
        /// <returns>The list.</returns>
        [HttpGet]
        public IActionResult MappingList()
        {
            var sources = new[]
                    {
                        new Source {SomeValue = 5},
                        new Source {SomeValue = 6},
                        new Source {SomeValue = 7}
                    };
            IEnumerable<Destination> ienumerableDest = _mapper.Map<Source[], IEnumerable<Destination>>(sources);
            ICollection<Destination> icollectionDest = _mapper.Map<Source[], ICollection<Destination>>(sources);
            IList<Destination> ilistDest = _mapper.Map<Source[], IList<Destination>>(sources);
            List<Destination> listDest = _mapper.Map<Source[], List<Destination>>(sources);
            return new JsonResult(ienumerableDest);
        }

        [HttpGet]
        public IActionResult MappingInClude()
        {
            var sources = new[]
                    {
                        new ParentSource(),
                        new ChildSource(),
                        new ParentSource()
                    };
            //配置和执行映射
            var destinations = _mapper.Map<ParentSource[], ParentDestination[]>(sources);
            return new JsonResult(new { v1 = destinations[0].GetType().ToString(), v2 = destinations[1].GetType().ToString(), v3 = destinations[2].GetType().ToString() });
        }

        /// <summary>
        /// Mappings the nest.嵌套映射
        /// </summary>
        /// <returns>The nest.</returns>
        [HttpGet]
        public IActionResult MappingNest()
        {
            var source = new OuterSource
            {
                Value = 5,
                Inner = new InnerSource { OtherValue = 15 }
            };
            var destinations = _mapper.Map<OuterSource, OuterDest>(source);
            return new JsonResult(new { destinations.Value, destinations.Inner.OtherValue });
        }

        /// <summary>
        /// api 接口说明（注释）
        /// </summary>
        /// <returns>The get.</returns>
        [HttpGet("id")]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            return new string[] { "value1", "value2", id.ToString() };
        }

    }
}
