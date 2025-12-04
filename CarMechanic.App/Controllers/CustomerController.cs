
using CarMechanic.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanic.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController: ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAll()
    {
        var customers = await _customerService.GetCustomers();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(string id)
    {
        var customer = await _customerService.GetCustomerById(id);
        
    }
}