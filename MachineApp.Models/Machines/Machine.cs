using MachineApp.Models.Common;
using System.ComponentModel.DataAnnotations;

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
}
