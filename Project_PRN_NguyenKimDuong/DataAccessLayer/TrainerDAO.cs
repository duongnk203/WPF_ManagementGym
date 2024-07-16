using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TrainerDAO
    {
        public static List<Trainer> GetTrainers()
        {
            var listTrainers = new List<Trainer>();
            try
            {
                using var context = new GymManagementContext();
                listTrainers = context.Trainers.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return listTrainers;
        }

        public static Trainer GetTrainerById(int trainerId)
        {
            var trainer = new Trainer();    
            try
            {
                using var context = new GymManagementContext();
                trainer = context.Trainers.FirstOrDefault(t => t.TrainerId == trainerId);
            }
            catch (Exception)
            {
                throw;
            }
            return trainer;
        }

        public static void AddTrainer(Trainer trainer)
        {
            try
            {
                using var context = new GymManagementContext();
                context.Trainers.Add(trainer);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateTrainer(Trainer trainer)
        {
            using var context = new GymManagementContext();
            context.Trainers.Update(trainer);
            context.SaveChanges();
        }
    }
}
