using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public interface IUserRepository
    {
            void Dodaj(Model.User user);

            List<Model.User> Iscitaj();

            void Izmeni(Model.User user);

            void Obrisi(int Id);

        
    }
}
