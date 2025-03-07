using Microsoft.EntityFrameworkCore;
using System.Numerics;
using webApiFoeNative.Models;

namespace webApiFoeNative.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи между Doctor и DoctorSpecialization
            modelBuilder.Entity<DoctorsModel>()
                .HasOne(d => d.SpecialisationModel) // У доктора одна специализация
                .WithMany(s => s.DoctorsModels) // У специализации много докторов
                .HasForeignKey(d => d.CategoryId) // Внешний ключ
                .OnDelete(DeleteBehavior.SetNull); // При удалении специализации, у доктора SpecializationId станет null

            // Настройка индексов (опционально)
            modelBuilder.Entity<DoctorsModel>()
                .HasIndex(d => d.CategoryId);

            modelBuilder.Entity<SpecialisationModel>()
                .HasIndex(s => s.Name)
                .IsUnique(); // Уникальное имя специализации

            modelBuilder.Entity<SpecialisationModel>()
                .HasData(
                new SpecialisationModel
                {
                    Id = 1,
                    Name = "Гинеколог"
                },
                new SpecialisationModel
                {
                    Id = 2,
                    Name = "ЛОР"
                },
                new SpecialisationModel
                {
                    Id = 3,
                    Name = "Невролог"
                },
                new SpecialisationModel
                {
                    Id = 4,
                    Name = "Дермотолог"
                },
                new SpecialisationModel
                {
                    Id = 5,
                    Name = "Гастроэнтеролог"
                },
                new SpecialisationModel
                {
                    Id = 6,
                    Name = "Эндокринолог"
                },
                new SpecialisationModel
                {
                    Id = 7,
                    Name = "Психиатр"
                },
                new SpecialisationModel
                {
                    Id = 8,
                    Name = "Офтальмолог"
                },
                new SpecialisationModel
                {
                    Id = 9,
                    Name = "Терапевт"
                },
                new SpecialisationModel
                {
                    Id = 10,
                    Name = "Психотерапевт"
                }
                );
        }

        public DbSet<DoctorsModel> Doctors { get; set; }
        public DbSet<SpecialisationModel> Specialisation { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
