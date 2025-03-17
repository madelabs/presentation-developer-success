using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace N_Tier.API.Controllers;

[Authorize]
public class CustomerController : ApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = todoItemService;
    }
    
    [HttpGet("id:guid")]
    public Task<IActionResult> ViewCustomerProfile()
    {
        return View();
    }
    
    publi
}
