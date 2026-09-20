using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IRepository<T>
    {



        public Task<List<T>> GetAll();

        public Task<T> GetById(int id);

        public Task Add(T model);

        public Task Update(T model);

        public Task Delete(int id);



    }
}
