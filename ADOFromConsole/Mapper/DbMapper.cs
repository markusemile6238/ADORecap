using ADOFromConsole.Helper;
using ADOFromConsole.Model;
using System.Data;

namespace ADOFromConsole.Mapper
{
    public class DbMapper
    {
        public Doctor MapDoctor(IDataRecord record)
        {
            return new Doctor(
                (int)record["Id"],
                (string)(record["Ref"].ToString() ?? "N/A"),
                (string)record["Firstname"],
                (string)record["Lastname"],
                (string)record["Service"]

                );
        }
        public Patient MapPatient(IDataRecord record)
        {
            return new Patient(
                (int)record["Id"],
                (string)(record["RegNational"].ToString() ?? "N/A"),
                (string)record["Firstname"],
                (string)record["Lastname"],
                (string)record["Email"],
                (string)record["Phone"],
                Convert.ToDateTime(record["birthDate"])
                );
                
        }
    }
}
