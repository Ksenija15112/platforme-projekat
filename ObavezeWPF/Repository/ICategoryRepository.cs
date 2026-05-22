using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public interface ICategoryRepository
    {
        bool IsInitialized();
        List<Category> GetAll();
        Category? Get(int id);
        void Save(Category category);
    }
}
