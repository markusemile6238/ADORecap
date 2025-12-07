using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFromConsole.Model
{
    public class Doctor
    {
        public int? Id { get; set; }
        public string? Ref { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Service { get; set; }

        public Doctor()
        {

        }
        public Doctor(int id, string @ref, string firstname, string lastname, string service)
        {
            Id = id;
            Ref = @ref;
            Firstname = firstname;
            Lastname = lastname;
            Service = service;
        }

    }
}
