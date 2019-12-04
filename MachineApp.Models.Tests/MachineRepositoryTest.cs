// Install-Package Microsoft.EntityFrameworkCore.SqlServer
// Install-Package Microsoft.EntityFrameworkCore.InMemory
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MachineApp.Models.Tests
{
    //[6] Test Class
    [TestClass]
    public class MachineRepositoryTest
    {
        //[6][1] Test Method
        [TestMethod]
        public async Task MachineRepositoryMethodAllTest()
        {
            //[6][1][0] DbContextOptions 개체 생성
            var options = new DbContextOptionsBuilder<MachineDbContext>()
                .UseInMemoryDatabase(databaseName: "MachineApp").Options;

            //[6][1][1] Add() Method Test
            using (var context = new MachineDbContext(options))
            //[6][1][1][1] DbContext 개체 생성 및 데이터 입력
            {
                //[?] Repository Object Creation
                var repository = new MachineRepository(context);
                var machine = new Machine() { Name = "[1] T7910", Created = DateTime.Now };
                await repository.AddMachineAsync(machine);
                await context.SaveChangesAsync(); //[!]
            }
            //[6][1][1][2] 제대로 입력되었는지 테스트 
            using (var context = new MachineDbContext(options))
            {
                Assert.AreEqual(1, await context.Machines.CountAsync());
                var machine = await context.Machines.Where(m => m.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("[1] T7910", machine.Name);
            }

            //[6][1][2] GetAll() Method Test
            //[6][1][2][1] DbContext 개체 생성 및 추가 데이터 입력
            using (var context = new MachineDbContext(options))
            {
                await (new MachineRepository(context)).AddMachineAsync(
                    new Machine { Name = "[2] Rugged Extreame", Created = DateTime.Now });
                context.Machines.Add(new Machine { Name = "[3] Alienware Aurora R8", Created = DateTime.Now });
                await context.SaveChangesAsync(); //[!]
            }
            //[6][1][2][2] 제대로 출력되는지 테스트 
            using (var context = new MachineDbContext(options))
            {
                var repository = new MachineRepository(context);
                var machines = await repository.GetMachinesAsync(); //[?] 전체 레코드 가져오기 
                Assert.AreEqual(3, machines.Count()); // 현재까지 3개 테스트
            }

            //[6][1][3] GetById() Method Test
            //[6][1][3][1] DbContext 개체 생성 및 추가 데이터 입력
            using (var context = new MachineDbContext(options))
            {
                await (new MachineRepository(context)).AddMachineAsync(
                    new Machine { Name = "[4] Level 10 Limited Edtion", Created = DateTime.Now });
                await context.SaveChangesAsync(); //[!]
            }
            //[6][1][3][2] 제대로 출력되는지 테스트 
            using (var context = new MachineDbContext(options))
            {
                var repository = new MachineRepository(context);
                var alienware = await repository.GetMachineByIdAsync(3);
                Assert.IsTrue(alienware.Name.Contains("Alienware"));
                Assert.AreEqual("[3] Alienware Aurora R8", alienware.Name);
            }

            //[6][1][4] Edit() Method Test
            //[6][1][4][1] DbContext 개체 생성 및 추가 데이터 입력
            using (var context = new MachineDbContext(options))
            {
                await (new MachineRepository(context)).AddMachineAsync(
                    new Machine { Name = "[5] Surface Pro", Created = DateTime.Now });
                await context.SaveChangesAsync(); //[!]
            }
            //[6][1][4][2] 제대로 수정되는지 테스트 
            using (var context = new MachineDbContext(options))
            {
                var repository = new MachineRepository(context);
                var surface = await repository.GetMachineByIdAsync(5);
                surface.Name = "[5] 서피스 프로";
                await repository.EditMachineAsync(surface);
                await context.SaveChangesAsync();

                var newSurface = await repository.GetMachineByIdAsync(5);
                Assert.AreEqual("[5] 서피스 프로", newSurface.Name);
            }

            //[6][1][5] Delete() Method Test
            //[6][1][5][1] DbContext 개체 생성 및 추가 데이터 입력
            using (var context = new MachineDbContext(options))
            {
                // Empty
            }

            //[6][1][5][2] 제대로 삭제되는지 테스트 
            using (var context = new MachineDbContext(options))
            {
                var repository = new MachineRepository(context);
                await repository.DeleteMachineAsync(5);
                await context.SaveChangesAsync();

                Assert.AreEqual(4, await context.Machines.CountAsync());
                Assert.IsNull(await repository.GetMachineByIdAsync(5));
            }
            //[6][1][6] GetMachinesPageAsync() Method Test
            //[6][1][6][1] DbContext 개체 생성 및 추가 데이터 입력
            using (var context = new MachineDbContext(options))
            {
                // Empty
            }

            //[6][1][6][2] 2번째 페이지의 First 항목 체크
            using (var context = new MachineDbContext(options))
            {
                int pageIndex = 1;
                int pageSize = 2;

                var repository = new MachineRepository(context);
                var machines = await repository.GetMachinesPageAsync(pageIndex, pageSize); // 4, 3, [2, 1]

                Assert.AreEqual("[2] Rugged Extreame", machines.Records.FirstOrDefault().Name);
                Assert.AreEqual(4, machines.TotalRecords); 
            }
        }
    }
}
