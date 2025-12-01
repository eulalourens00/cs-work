using Microsoft.EntityFrameworkCore;

namespace отношения_сущностей
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Company comp1 = new Company { Name = "Foo" };
                Company comp2 = new Company { Name = "Dee" };

                User user1 = new User { Name = "Asweea", Company = comp1 };
                User user2 = new User { Name = "Lewrs", Company = comp2 };
                User user3 = new User { Name = "Medns", Company = comp2 };

                db.Companies.AddRange(comp1, comp2);
                db.Users.AddRange(user1, user2, user3);

                db.SaveChanges();

                //foreach(var user in db.Users.ToList())
                //{
                //    Console.WriteLine($"{user.Name} - {user.Company}");
                //}

                //жадная загрузка
                //var users = db.Users.
                //    Include(u => u.Company).ToList();
                //foreach (var user in users)
                //{
                //    Console.WriteLine($"{user.Name} - {user.Company}");
                //}
            }

            // явная загрузка
            using (ApplicationContext db = new ApplicationContext())
            {
                Company? company = db.Companies.FirstOrDefault();
                if(company != null)
                {
                    db.Users.Where(u => u.CompanyId == company.Id).Load();

                    Console.WriteLine(company.Name);
                    foreach(var u in company.Users)
                    {
                        Console.WriteLine(u.Name);
                    }
                }
            }

            // ленивая (неявная) загрузка

        }
    }
}
