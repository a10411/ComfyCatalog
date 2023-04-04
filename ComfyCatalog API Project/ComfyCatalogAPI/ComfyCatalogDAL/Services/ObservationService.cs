using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ComfyCatalogDAL.Services
{
    public class ObservationService
    {
        public static async Task<List<Observation>> GetAllObservations(string conString)
        {
            var obsList = new List<Observation>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Observation");
                cmd.CommandType = CommandType.Text; 
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Observation observation = new Observation(rdr);
                    obsList.Add(observation);
                }
                rdr.Close();
                con.Close();
            }
            return obsList;
        }


        public static async Task<List<Observation>> GetObservationByProduct(string conString, int productID )
        {
            var obsList = new List<Observation>();
            using(SqlConnection con = new SqlConnection( conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Observation where productID = {productID}", con);
                cmd.CommandType = CommandType.Text; 
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Observation observation = new Observation(rdr);
                    obsList.Add(observation);
                }
                rdr.Close();
                con.Close();
            }
            return obsList;
        }

       
    }
}
