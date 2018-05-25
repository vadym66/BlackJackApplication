using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities;


namespace BlackJackApp.DataAccess.Interface
{
    public interface IPlayerRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Player GetById(int id);
        void Create();
        void Delete(int id);
        void Save();
    }
}
