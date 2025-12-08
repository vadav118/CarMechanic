
using CarMechanic.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanic.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _customerService.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpGet("{id:int}/works")]
    public async Task<ActionResult<List<Work>>> GetWorks(int id)
    {
        return (await _customerService.GetWorks(id)).ToList();
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
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
    

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
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