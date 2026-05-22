using ObavezeWPF.Model;
using ObavezeWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Service
{
    public class CategoryService
    {
        protected ICategoryRepository CategoryRepository { get; set; }
        public CategoryService(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public List<Category> GetAll() {  return CategoryRepository.GetAll(); }
        public Category? Get(int id) { return CategoryRepository.Get(id); }
        public void Save(Category category) { CategoryRepository.Save(category); }
    }
}
