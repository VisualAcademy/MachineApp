using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MachineApp.Models
{
    /// <summary>
    /// [4] DbContext Class
    /// </summary>
    public class MachineDbContext : DbContext
    {
        // Install-Package Microsoft.EntityFrameworkCore.SqlServer
        // Install-Package Microsoft.EntityFrameworkCore.InMemory
        // Install-Package System.Configuration.ConfigurationManager
        public MachineDbContext()
        {
            // Empty
        }

        public MachineDbContext(DbContextOptions<MachineDbContext> options)
            : base(options)
        {
            // 공식과 같은 코드 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역: 
            // App.config 또는 Web.config의 연결 문자열 사용
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
                    "ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Machines 테이블의 Created 열은 자동으로 GetDate() 제약 조건을 부여하기 
            modelBuilder.Entity<Machine>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        /// <summary>
        /// MachineApp 
        /// </summary>
        public DbSet<Machine> Machines { get; set; }
    }
}
