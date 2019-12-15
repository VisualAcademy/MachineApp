using System.Threading.Tasks;

namespace MachineApp.Models
{
    /// <summary>
    /// [3] Repository Interface
    /// </summary>
    public interface IMachineMediaRepository
    {
        Task AddMachineMediaAsync(int machineId, int mediaId);
        Task<Media> GetMediasByMachineIdAsync(int machineId);
        Task<Machine> GetMachinesByMediaIdAsync(int mediaId);
        Task DeleteMediaAsync(int machineId, int mediaId);
    }
}
