using Jsc.TaskManager.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Jsc.TaskManager.DAL
{
    public class DomainRepository<T> where T : class, IStorageEntity
    {
        public virtual IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IEnumerable<T> result;

            using (var context = new TaskMgrDbContext())
            {
                var qry = BuildQuery(navigationProperties, context);

                result =
                    qry
                        .AsNoTracking()
                        .ToArray();
            }

            return result;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var context = new TaskMgrDbContext())
            {
                var qry = BuildQuery(navigationProperties, context);

                return
                    qry
                        .AsNoTracking()
                        .Where(where)
                        .ToList();
            }
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var context = new TaskMgrDbContext())
            {
                var qry = BuildQuery(navigationProperties, context);

                return
                    qry
                        .AsNoTracking()
                        .FirstOrDefault(where);
            }
        }

        public virtual void Add(params T[] items)
        {
            using (var context = new TaskMgrDbContext())
            {
                SetEntityStates(items, EntityState.Added, context);
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new TaskMgrDbContext())
            {
                SetEntityStates(items, EntityState.Deleted, context);
                context.SaveChanges();
            }
        }

        public virtual void Update(params T[] items)
        {
            using (var context = new TaskMgrDbContext())
            {
                SetEntityStates(items, EntityState.Modified, context);
                context.SaveChanges();
            }
        }

        internal static void SetEntityStates(T[] items, EntityState state, TaskMgrDbContext context)
        {
            foreach (var item in items)
            {
                context.Entry(item).State = state;
            }
        }

        internal static DbSet<T> BuildQuery(Expression<Func<T, object>>[] navigationProperties, TaskMgrDbContext context)
        {
            var qry = context.Set<T>();

            foreach (var nav in navigationProperties)
            {
                qry.Include(nav);
            }

            return qry;
        }
    }
}
