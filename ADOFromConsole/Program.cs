using System;
using ADOFromConsole.Model;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        // connection  basic
        string connectionString = "Server=***.**.**,1433;Initial Catalog=exoDb;Persist Security Info=True;User ID={user};Password={password};Encrypt=True;Trust Server Certificate=True";

        List<Doctor> lesDoctors = new List<Doctor>();

        // creation de la connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                // on ouvre la connection
                connection.Open();
                Console.WriteLine("connection reussit");

                // execution d'une requete
                string sql = "SELECT * FROM Doctors ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lesDoctors.Add(new Doctor
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Ref = reader["Ref"].ToString() ?? "N/A",
                                Firstname = reader["Firstname"].ToString() ?? "N/A",
                                Lastname = reader["LastName"].ToString() ?? "n/A",
                                Service = reader["Service"].ToString() ?? "N/A"
                            });
                        }
                    }
                }
                ;
            }catch(SqlException ex)
            {
                Console.WriteLine("Erreur de connection"+ex);
            }
        }
        if (lesDoctors.Any())
        {
            foreach (var doc in lesDoctors)
            {
                Console.WriteLine($"{doc.Id,2} : {doc.Ref,2} : {doc.Firstname,-8} {doc.Lastname,-8}  -  {doc.Service}");
            }
            {
                
            }
        }
    }
}
