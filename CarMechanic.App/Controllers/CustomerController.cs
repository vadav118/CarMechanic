
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
        if (customer is null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
        var existingCustomer = await _customerService.GetCustomerById(customer.Id);
        if (existingCustomer is not null)
        {
            return Conflict();
        }
        await _customerService.AddCustomer(customer);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        var oldCustomer = await _customerService.GetCustomerById(id);

        if (oldCustomer is null)
        {
            return NotFound();
        }
        await _customerService.UpdateCustomer(customer);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingCustomer = await _customerService.GetCustomerById(id);
        if (existingCustomer is null)
        {
            return NotFound();
        }
        await _customerService.DeleteCustomer(id);
        return Ok();
    }
}