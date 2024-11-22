using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SOLID_Principles.Domain;
using SOLID_Principles.Infrastructure;
using SOLID_Principles.Services.DTOs.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Principles.Services.ExpenseService;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext ctx;
    private readonly IMapper mapper;

    public ExpenseService(AppDbContext context, IMapper autoMapper)
    {
        ctx = context;
        mapper = autoMapper;
    }

    public async Task<IEnumerable<Expense>> GetExpenses(Expression<Func<Expense,bool>> qry = null, CancellationToken token = default)
    {
        var expenses = await ctx.Set<Expense>().Where(qry).ToListAsync(token);
        return expenses;
    }

    public async Task<Expense?> GetExpense(int id, CancellationToken token = default)
    {
        var expense = await ctx.Set<Expense>().SingleOrDefaultAsync(e => e.Id.Equals(id), token);
        return expense;
    }

    public async Task<Expense?> AddExpense(PostExpenseDTO model, CancellationToken token = default)
    {
        try
        {
            var expense = mapper.Map<Expense>(model);
            await ctx.AddAsync(expense, token);
            await ctx.SaveChangesAsync(token);
            return expense;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }
}
