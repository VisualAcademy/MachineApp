using MachineApp.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace MachineApp.Models
{
    /// <summary>
    /// [2] Model Class: Machine 모델 클래스 == Machines 테이블과 일대일로 매핑
    /// Machine, MachineModel, MachineViewModel, MachineBase, MachineDto, MachineEntity, ...
    /// </summary>
    public class Machine : AuditableBase
    {
        /// <summary>
        /// Serial Number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Hardware Name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// [3] Repository Interface
    /// ASP.NET & Core를 다루는 기술 13장에서 발췌 - 
    /// CRUD 관련 메서드 이름을 지을 때에는 Add, Get, Update(Edit), Remove(Delete) 등의 단어를 많이 사용한다.
    /// 이러한 단어를 접두사 또는 접미사로 사용하는 것은 권장 사항이지 필수 사항은 아니다.
    /// </summary>
    public interface IMachineRepository
    {
        Task<Machine> AddMachineAsync(Machine machine);     // 입력
        Task<List<Machine>> GetMachinesAsync();             // 출력: GetAll(), GetAllMachines()
        Task<Machine> GetMachineByIdAsync(int id);          // 상세: GetById(), FindById()
        Task<Machine> EditMachineAsync(Machine machine);    // 수정: Update()
        Task DeleteMachine(int id);                         // 삭제: Remove()
    }
    
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

    /// <summary>
    /// [5] Repository Class
    /// </summary>
    public class MachineRepository : IMachineRepository
    {
        private readonly MachineDbContext _context;

        public MachineRepository(MachineDbContext context)
        {
            this._context = context;
        }

        // 입력
        public async Task<Machine> AddMachineAsync(Machine machine)
        {
            await _context.AddAsync<Machine>(machine);
            await _context.SaveChangesAsync();
            return machine; 
        }

        // 출력
        public async Task<List<Machine>> GetMachinesAsync()
        {
            return await _context.Machines.OrderByDescending(m => m.Id).ToListAsync();
        }

        // 상세보기
        public async Task<Machine> GetMachineByIdAsync(int id)
        {
            return await _context.Machines.Where(m => m.Id == id).SingleOrDefaultAsync();
        }

        // 수정
        public async Task<Machine> EditMachineAsync(Machine machine)
        {
            _context.Entry<Machine>(machine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return machine; 
        }

        // 삭제
        public async Task DeleteMachine(int id)
        {
            var machine = await _context.Machines.Where(m => m.Id == id).SingleOrDefaultAsync();
            if (machine != null)
            {
                _context.Machines.Remove(machine);
                await _context.SaveChangesAsync(); 
            }
        }
    }
}
