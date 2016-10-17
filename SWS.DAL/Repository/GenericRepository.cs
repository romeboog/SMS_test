using SMS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Domain;
using System.Data.Entity;

namespace SMS.DAL.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal SMSEntities context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            context = new SMSEntities();
            dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            // 取得所有資料
            IQueryable<TEntity> query = dbSet;

            // 如果有Where條件篩選
            if (filter != null)
                query = query.Where(filter);

            // 如果有排序
            if (orderBy != null)
                return orderBy(query);
            else
                return query;

        }

        /// <summary>取得某筆資料</summary>
        /// <param name="id">PRIMARY KEY </param>
        /// <returns></returns>
        public TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        /// <summary>取得某筆資料</summary>
        /// <param name="year">年度 </param>
        /// <param name="org">機關 </param>
        /// <param name="dept">部門 </param>
        /// <param name="soft_id">軟體編號 </param>
        /// <returns></returns>        
        public TEntity GetByID(object year, object org, object dept, object soft_id)
        {
            return dbSet.Find(year,org,dept,soft_id);
        }
        public TEntity GetByID(object year, object org, object dept, object soft_id, object detail_id)
        {
            return dbSet.Find(year, org, dept, soft_id,detail_id);
        }
        public TEntity GetByUserInfo(object user_org, object user_dept, object user_id)
        {
            return dbSet.Find(user_org, user_dept, user_id);
        }

        /// <summary>修改資料</summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>刪除某筆資料 by ID</summary>
        /// <param name="id">PRIMARY KEY </param>
        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            context.SaveChanges();
        }
        /// <summary>BD02:取得特定部門資料</summary>
        public TEntity GetByDeptInfo(object year, object org, object dept)
        {
            return dbSet.Find(year, org, dept);
        }
        /// <summary>刪除某筆資料 by Entity</summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            //假如Entity處於Detached狀態，就先Attach起來，這樣才能順利移除
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        /// <summary>新增一筆資料</summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

    }
}
