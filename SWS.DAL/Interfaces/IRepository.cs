using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity,bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null);

        TEntity GetByID(object id);

        TEntity GetByID(object year, object org, object dept, object soft_id);
        //SM02刪除
        TEntity GetByID(object year, object org, object dept, object soft_id, object detail_id);

       //BD03刪除
        TEntity GetByUserInfo(object org, object dept, object user_id);
        TEntity GetByDeptInfo(object year, object org, object dept);
        void Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        void Insert(TEntity entity);
    }
}
