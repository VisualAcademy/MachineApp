using MachineApp.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineApp.Models
{
    /// <summary>
    /// [2] Model Class: Media 모델 클래스 == Medias 테이블과 일대일로 매핑
    /// Media, MediaModel, MediaViewModel, MediaBase, MediaDto, MediaEntity, ...
    /// </summary>
    [Table("MachinesMedias")]
    public class MachineMedia : AuditableBase
    {
        /// <summary>
        /// Serial Number
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MachineId { get; set; }
        public int MediaId { get; set; }
    }
}
