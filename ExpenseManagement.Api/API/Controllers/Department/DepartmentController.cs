using ExpenseManagement.Api.Application.Common.Models;
using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Application.Features.Departments.Commands;
using ExpenseManagement.Api.Application.Features.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.API.Controllers.Department;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize (Policy = "CanCreateDepartment")]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentCommand command)
    {
       var result = await _mediator.Send(command);
        var response = ApiResponse<DepartmentDTO>.SuccessResponse("Department created successfully", result);
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    [Authorize (Policy = "CanUpdateDepartment")]
    public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<object>.FailureResponse("Department ID mismatch", 400));
        }
        
        var result = await _mediator.Send(command);
        var response = ApiResponse<UpdateDepartmentDTO>.SuccessResponse("Department updated successfully", result);
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize (Policy = "CanViewDepartment")]
    public async Task<IActionResult> GetDepartments()
    {
        var result = await _mediator.Send(new GetDepartmentsQuery());
        var response = ApiResponse<List<GetDepartmentDTO>>.SuccessResponse("Departments retrieved successfully", result);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [Authorize (Policy = "CanViewDepartment")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
       var result = await _mediator.Send(new GetDepartmentByIdQuery { DepartmentId = id });
        var response = ApiResponse<GetDepartmentDTO?>.SuccessResponse("Department retrieved successfully", result);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "CanDeleteDepartment")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var result = await _mediator.Send(new DeleteDepartmentCommand { DepartmentId = id });
        if (!result)
        {
            return NotFound(ApiResponse<object>.FailureResponse($"Department with ID {id} not found."));
        }
        
        var response = ApiResponse<object>.SuccessResponse("Department deleted successfully", true);
        return Ok(response);
    }
}