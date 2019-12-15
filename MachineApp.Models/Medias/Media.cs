using MachineApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MachineApp.Models
{
    /// <summary>
    /// [2] Model Class: Media 모델 클래스 == Medias 테이블과 일대일로 매핑
    /// Media, MediaModel, MediaViewModel, MediaBase, MediaDto, MediaEntity, ...
    /// </summary>
    public class Media : AuditableBase
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
