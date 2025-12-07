using ADOFromConsole.Model;
using Microsoft.Data.SqlClient;
using DotNetEnv;
using ADOFromConsole.Configuration;
using System.Drawing.Text;
using System.Data;
using ADOFromConsole.Mapper;
using ADOFromConsole.Repository;


namespace ADOFromConsole.Model
{
    class Program
    {

        static void Main()
        {

            
            
            
            DoctorRepository repo = new DoctorRepository();
            Doctor doc = new Doctor();
            doc.Firstname = "Maboule";
            doc.Lastname = "Docteur";
            doc.Service = "Chirurgient";
            int res = repo.Add(doc);
            if (res == -1) {
                Console.WriteLine("ajouter");
            }
            else
            {
                Console.WriteLine("Pas ajouter");
            }
            
            
            
            
            Task<List<Doctor>> doctors = repo.GetAllAsync();
            if (doctors.Result.Any())
            {
                foreach (var item in doctors.Result)
                {
                    Console.WriteLine($"{item.Id,2} : {item.Firstname,10}  {item.Lastname,-10}  {item.Service,30} {item.Ref}");
                }
            }




            //DbMapper mapper = new DbMapper();

            //DBAdoConnection connection = new DBAdoConnection();
            //string query = "SELECT * FROM Patients";
            //var command = connection.CreateCommand();
            //command.CommandText = query;
            //IEnumerable<Patient> result = connection.ExecuteReader(command,mapper.MapPatient);
            //if (result.Any())           
            //{
            //    foreach (var item in result)
            //    {
            //        Console.WriteLine($"{item.Id,2} : {item.Firstname,10}  {item.Lastname,-10}  {item.Email,30} {item.Phone,15}   "+item.BirthDate.ToString("dd-MM-yyyy")+" ");
            //    }
            //}



            //List<Doctor> lesDoctors = new List<Doctor>();






        }
    }
}