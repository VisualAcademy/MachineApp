using MachineApp.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        Task<Machine> AddMachineAsync();                    // 입력
        Task<List<Machine>> GetMachinesAsync();             // 출력: GetAll(), GetAllMachines()
        Task<Machine> GetMachineByIdAsync();                // 상세: GetById(), FindById()
        Task<Machine> EditMachineAsync(Machine machine);    // 수정: Update()
        Task DeleteMachine(int id);                         // 삭제: Remove()
    }

    /// <summary>
    /// [4] Repository Class
    /// </summary>
    public class MachineRepository : IMachineRepository
    {
        public Task<Machine> AddMachineAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Machine>> GetMachinesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Machine> GetMachineByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Machine> EditMachineAsync(Machine machine)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMachine(int id)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// [5] DbContext Class
    /// </summary>
    public class MachineDbContext
    {

    }
}
