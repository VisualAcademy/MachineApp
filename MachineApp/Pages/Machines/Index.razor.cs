using MachineApp.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DulPager;

namespace MachineApp.Pages.Machines
{
    public partial class Index
    {
        [Inject]
        public IMachineRepository MachineRepository { get; set; }

        public List<Machine> _machines { get; set; }

        private DulPagerBase pager = new DulPagerBase()
        { 
            PageNumber = 1,
            PageIndex = 0,
            PageSize = 3,
            PagerButtonCount = 5
        };

        protected override async Task OnInitializedAsync()
        {
            var machineSet = await MachineRepository.GetMachinesPageAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = machineSet.TotalRecords; 
            _machines = machineSet.Records.ToList();
        }

        // Pager 버튼 클릭 콜백 메서드
        private async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            var machineSet = await MachineRepository.GetMachinesPageAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = machineSet.TotalRecords; // 총 레코드 수
            _machines = machineSet.Records.ToList(); // 페이징 처리된 레코드

            StateHasChanged(); // 현재 컴포넌트 재로드(Pager Refresh) 
        }
    }
}
