using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    //interfejs
    public interface ILabelRepository
    {
        void Dodaj(Model.Label label);

        List<Model.Label> Iscitaj();

        void Izmeni(Model.Label label);

        void Obrisi(int Id);
        
    }
}
