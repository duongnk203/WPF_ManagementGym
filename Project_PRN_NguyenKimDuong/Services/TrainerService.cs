using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repositories;

namespace Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository iTrainerRepository;
        private readonly IUserRepository iUserRepository;

        public TrainerService()
        {
            iTrainerRepository = new TrainerRepository();
            iUserRepository = new UserRepository();
        }

        public void AddTrainer(Trainer trainer)
        {
            iTrainerRepository.AddTrainer(trainer);
        }

        public void DeleteTrainer(int trainerId)
        {
            iTrainerRepository.DeleteTrainer(trainerId);
        }

        public Trainer GetTrainerById(int trainerId)
        {
             return iTrainerRepository.GetTrainerById(trainerId);
        }

        public List<Trainer> GetTrainers()
            => iTrainerRepository.GetTrainers();

        public void UpdateTrainer(Trainer trainer)
            => iTrainerRepository.UpdateTrainer(trainer);

    }
}
