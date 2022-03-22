using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Regulator.Configuration
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }

    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<TEntity> GetById(TId id);
    }

    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>

    {
        private readonly DbContext dbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual Task<TEntity> GetById(TId id)
        {
            return Task.FromResult(dbContext.Set<TEntity>().First(i => i.Id.Equals(id)));
        }
    }


    public class ModuleRepository : RepositoryBase<Module, Guid>
    {
        public ModuleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Task<Module> GetById(Guid id)
        {
            return Task.FromResult(new Module
            {
                Id = id,
                Name = "Cupakabra"
            });
        }
    }

    public class UserRepository : RepositoryBase<User, Guid>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class CarRepository : RepositoryBase<Car, Guid>
    {
        public CarRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Task<Car> GetById(Guid id)
        {
            return Task.FromResult(new Car
            {
                Id = id,
                Type = "Trabant"
            });
        }
    }

    public class Car : IEntity<Guid>
    {
        public string Type { get; set; }
        public Guid Id { get; set; }
    }


    public class Module : IEntity<Guid>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class User : IEntity<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
    }


    public class DbContext
    {
        public IEnumerable<TEntity> Set<TEntity>()
        {
            return Enumerable.Empty<TEntity>();
        }
    }
}