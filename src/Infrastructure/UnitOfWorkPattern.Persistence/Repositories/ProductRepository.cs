using UnitOfWorkPattern.Application.Interfaces.Repositories;
using UnitOfWorkPattern.Domain.Entities;
using UnitOfWorkPattern.Persistence.Context;

namespace UnitOfWorkPattern.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}
