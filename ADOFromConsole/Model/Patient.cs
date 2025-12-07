using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFromConsole.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string RegNational { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public Patient(int id, string regNational, string firstname, string lastname, string email, string phone, DateTime birthDate)
        {
            Id = id;
            RegNational = regNational;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
        }
    }
}
