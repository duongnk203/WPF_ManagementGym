using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        public void AddTrainer(Trainer trainer)
            => TrainerDAO.AddTrainer(trainer);

        public void DeleteTrainer(int trainerId)
        {
            throw new NotImplementedException();
        }

        public Trainer GetTrainerById(int trainerId)
            => TrainerDAO.GetTrainerById(trainerId);

        public List<Trainer> GetTrainers()
            => TrainerDAO.GetTrainers();

        public void UpdateTrainer(Trainer trainer)
            => TrainerDAO.UpdateTrainer(trainer);
    }
}
