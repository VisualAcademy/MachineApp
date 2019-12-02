﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineApp.Models
{
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
        Task DeleteMachineAsync(int id);                    // 삭제: Remove()
    }
}
