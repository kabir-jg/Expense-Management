using ExpenseManagement.Api.Application.Common.Exceptions;
using ExpenseManagement.Api.Infrastructure.Persistence;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Departments.Commands;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IDepartmentHeadRepository _departmentHeadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository,
        IDepartmentHeadRepository departmentHeadRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _departmentHeadRepository = departmentHeadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(request.DepartmentId);
        if (department == null)
        {
            throw new NotFoundException($"Department with ID {request.DepartmentId} not found.");
        }

        var departmentHead = await _departmentHeadRepository.GetByDepartmentIdAsync(request.DepartmentId);

        try
        {
            _unitOfWork.BeginTransaction();

            if (departmentHead != null)
            {
                await _departmentHeadRepository.DeleteDepartmentHeadAsync(request.DepartmentId);
            }

            var isDeleted = await _departmentRepository.DeleteDepartmentAsync(request.DepartmentId);
            _unitOfWork.Commit();
            return isDeleted;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

    }
}