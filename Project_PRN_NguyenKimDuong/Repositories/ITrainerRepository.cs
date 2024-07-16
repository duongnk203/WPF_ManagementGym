using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ITrainerRepository
    {
        List<Trainer> GetTrainers();
        Trainer GetTrainerById(int trainerId);
        void AddTrainer(Trainer trainer);
        void UpdateTrainer(Trainer trainer);
        void DeleteTrainer(int trainerId);
    }
}
