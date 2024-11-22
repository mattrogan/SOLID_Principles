using SOLID_Principles.Domain;
using SOLID_Principles.Services.DTOs.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Principles.Services.ExpenseService;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetExpenses(Expression<Func<Expense, bool>> qry, CancellationToken token = default);
    Task<Expense?> GetExpense(int id, CancellationToken token = default);
    Task<Expense?> AddExpense(PostExpenseDTO model, CancellationToken token = default);

}
