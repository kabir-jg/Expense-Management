using ExpenseManagement.Api.Application.DTOs;
using ExpenseManagement.Api.Domain.Entities;
using ExpenseManagement.Api.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.ExpenseCategories.Commands;

public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand, ExpenseCategoryDTO>
{
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private readonly IMapper _mapper;

    public CreateExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
    {
        _expenseCategoryRepository = expenseCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseCategoryDTO> Handle
        (CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var expneseCategory = await _expenseCategoryRepository.CreateExpenseCategoryAsync(new ExpenseCategory
        {
            Name = request.Name,
            IsActive = true
        });
        return _mapper.Map<ExpenseCategoryDTO>(expneseCategory);
    }
}