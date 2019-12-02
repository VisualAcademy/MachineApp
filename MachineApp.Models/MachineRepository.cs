using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineApp.Models
{
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
        public async Task DeleteMachineAsync(int id)
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
