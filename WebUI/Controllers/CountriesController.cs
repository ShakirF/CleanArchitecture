using CleanArchitecture.Application.Countries.Commons.CreateCountry;
using CleanArchitecture.Application.Countries.Commons.DeleteCountry;
using CleanArchitecture.Application.Countries.Commons.UpdateCountry;
using CleanArchitecture.Application.Countries.Queries;
using CleanArchitecture.Domain.Entitises;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Collections.Specialized;
using System.Net;

namespace CleanArchitecture.WebUI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await Mediator.Send(new GetCountriesQuery());
            return Ok(value: countries);
        }
    [HttpPost]
    public async Task<IActionResult> Post(CreateCountryCommand command)
    {
        await Mediator.Send(command);
        return Ok(command);
    }

    [HttpPut]
    public async Task<IActionResult> put(UpdateCountryCommand command)
    {
        await Mediator.Send(command);
        return Ok(command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCountryCommand(id));
        return Ok(HttpStatusCode.OK);
    }
}

