using ADOFromConsole.Configuration;
using ADOFromConsole.Mapper;
using ADOFromConsole.Model;
using System.Data;

namespace ADOFromConsole.Repository
{
    public class DoctorRepository
    {
       
        private readonly DBAdoConnection _connection = new DBAdoConnection();

        public DoctorRepository(){}

        public  List<Doctor> GetAll()
        {
            DbMapper mapper = new DbMapper();
            List<Doctor> doctors = new List<Doctor>();
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM Doctors";
            IEnumerable<Doctor> res = _connection.ExecuteReader(command,mapper.MapDoctor);
            if(res.Any())
            {
               
                return doctors = res.ToList();
            }
            else
            {
                throw new Exception($"No data {nameof(Doctor)}");
            }
        }
       
        public async  Task<List<Doctor>> GetAllAsync()
        {
            DbMapper mapper = new DbMapper();
            List<Doctor> doctors = new List<Doctor>();
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM Doctors";
            IAsyncEnumerable<Doctor> res = _connection.ExecuteReaderAsync(command,mapper.MapDoctor);
            if(res != null)
            {
                await foreach (var item in res)
                {
                    doctors.Add(item);
                }
                return doctors;
            }
            else
            {
                throw new Exception($"No data {nameof(Doctor)}");
            }
        }


        public int Add(Doctor doctor)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO [Doctors] (Ref,Firstname,Lastname,Service)  VALUES (@ref,@firstname,@lastname,@service)";
            command.AddParameter("firstname", DbType.String,doctor.Firstname);
            command.AddParameter("lastname", DbType.String,doctor.Lastname);
            command.AddParameter("ref", DbType.Guid,Guid.NewGuid());
            command.AddParameter("service", DbType.String,doctor.Service);

            var res = _connection.ExecuteScalar(command);
            return res != null ? (int)res : -1;


        }



    }
}
