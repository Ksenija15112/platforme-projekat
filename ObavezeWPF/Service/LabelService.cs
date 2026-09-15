using ObavezeWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Service
{
    public class LabelService
    {
        ILabelRepository labelrepository { get; set; }

        public LabelService(ILabelRepository labelrepository)
        {
            this.labelrepository = labelrepository;
        }

        public void Dodaj(Model.Label label)
        {
            labelrepository.Dodaj(label);
        }

        public List<Model.Label> Iscitaj()
        {
            return labelrepository.Iscitaj();
        }
        public void Izmeni(Model.Label label)
        {
            labelrepository.Izmeni(label);
        }

        public void Obrisi(int id)
        {
            TaskLabelRepository taskLabelRepository = new TaskLabelRepository();
            taskLabelRepository.ObrisiLabelu(id);

            labelrepository.Obrisi(id);
        }

    }
}
