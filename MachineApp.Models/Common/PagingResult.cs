using System.Collections.Generic;

namespace MachineApp.Models.Common
{
    /// <summary>
    /// Paging 처리된 레코드셋과 전체 레코드 카운
    /// </summary>
    /// <typeparam name="T">Model Class</typeparam>
    public struct PagingResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }

        public PagingResult(IEnumerable<T> items, int totalRecords)
        {
            Records = items;
            TotalRecords = totalRecords;
        }
    }
}
