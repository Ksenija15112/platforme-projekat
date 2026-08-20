using ObavezeWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Service
{
    public class UserService
    {
        IUserRepository userrepository { get; set; }

        public UserService(IUserRepository userrepository)
        {
            this.userrepository = userrepository;
        }

        public void Dodaj(Model.User user)
        {
            userrepository.Dodaj(user);
        }

        public List<Model.User> Iscitaj()
        {
            return userrepository.Iscitaj();
        }
        public void Izmeni(Model.User user)
        {
            userrepository.Izmeni(user);
        }

        public void Obrisi(int id)
        {
            userrepository.Obrisi(id);
        }

        public Model.User? PronadjiPoEmailu(string email)
        {
            return  userrepository.PronadjiPoEmailu(email);
        }

    }
}

