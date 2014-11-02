using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NuclearProject.Models
{
    public class WarheadType
    {
        public int WarheadTypeId { get; set; }
        public String WarheadTypeName { get; set; }

        public SqlConnection cnn = new SqlConnection("Server=.;Database=NuclearDB; Trusted_Connection=true;");


        public WarheadType()
        {

        }

        // Create
        public bool InsertWarhead() {
            bool isInserted = false;
            cnn.Open();
            try
            {
                String sql = "insert into warheads (WarheadTypeName) values (@Name)";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Name", this.WarheadTypeName);
                cmd.ExecuteNonQuery();
                isInserted = true;
                
            }
            catch (Exception ex) {
                String msg = ex.Message;
            }
            if(cnn.State==ConnectionState.Open) cnn.Close();

            return isInserted;
        }
        // Select
        public List<WarheadType> GetAll() {
            List<WarheadType> warheads = new List<WarheadType>();
            String sql = "select * from Warheads";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                
                WarheadType wt = new WarheadType();                
                wt.WarheadTypeId = reader.GetInt32(0);                
                wt.WarheadTypeName = reader["WarheadTypeName"].ToString();
                warheads.Add(wt);
            }
            cnn.Close();

            return warheads;
        }
        // View
        public WarheadType(int warheadId)
        {            
            String sql = "select * from Warheads where WarheadTypeId = @Id";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Id", warheadId);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {                
                this.WarheadTypeId = reader.GetInt32(0);
                this.WarheadTypeName = reader.GetString(1);
            }
            cnn.Close();
        }

        // Update
        public bool UpdateWarhead() {
            bool isUpdated = false;

            String sql = "update Warheads set WarheadTypeName = @Name where WarheadTypeId = @Id";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Name", this.WarheadTypeName);
            cmd.Parameters.AddWithValue("@Id", this.WarheadTypeId);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            return isUpdated;
        }

        // Delete
    }
}