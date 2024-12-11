using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Là đối tượng DbContext, cung cấp kết nối và quản lý truy cập vào CSDL 
        private readonly ApplicationDbContext _db;
        // Tập hợp đại diện cho bảng hoặc 1 khung nhìn trong CSDL
        // Nó cho phép thực hiện các thao tác CRUD trên bảng tương ứng
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = db.Set<T>();
            // _db.Categories = dbSet
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        // Lấy thực thể dựa trên điều kiện filter
        // filter là 1 biểu thức lambda
        // VD: T là Category thì có thể truyền vào c => c.Id == 1
        public T Get(Expression<Func<T, bool>> filter)
        {
            // IQueryable<T> cho phép xây dựng các truy vấn LINQ mà không thực thi ngay lập tức
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remote(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
