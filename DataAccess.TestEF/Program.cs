using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace DataAccess.TestEF
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new DataContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));

                //var bank1 = new Bank { NameRus = "bank-1" };
                //unitOfWork.BankRepository.Add(bank1);
                //unitOfWork.Save();

                //var city1 = new City { NameRus = "city1", NameLat = "city1", Url = "url1" };
                //unitOfWork.CityRepository.Add(city1);
                //unitOfWork.Save();

                var bank2 = await unitOfWork.BankRepository.GetAllAsync();
                var city2 = await unitOfWork.CityRepository.GetAllAsync();

                Branch branch1 = new Branch { Adr = "adr1", Name = "name1", Bank = bank2.FirstOrDefault(i=>i.NameRus.Equals("bank-1")), City = city2.FirstOrDefault(i=>i.NameRus.Equals("city1")) };
                unitOfWork.BranchRepository.Add(branch1);
                unitOfWork.Save();


                var bank3 = await unitOfWork.BankRepository.GetAllAsync();
                var city3 = await unitOfWork.CityRepository.GetAllAsync();

                Branch branch = new Branch
                {
                    Name = "name-1",
                    Adr = "adr-1",
                    Bank = bank3.FirstOrDefault(i => i.NameRus.Equals("Паритетбанк")),
                    City = city3.FirstOrDefault(i => i.NameRus.Equals("Брест"))
                };

                unitOfWork.BranchRepository.Add(branch);

                unitOfWork.Save();

            }

            Console.ReadLine();
        }
    }
}
