using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NuclearProject.Models
{
    public class Missile
    {
        public int MissileId { get; set; }
        public int WarheadTypeId { get; set; }
        public String MissileName { get; set; }
        public double MissileRange { get; set; }
        public String FuelType { get; set; }

        public SqlConnection cnn = new SqlConnection("Server=.;Database=NuclearDB; Trusted_Connection=true;");


        public Missile()
        {

        }
        

        public void InsertMissile() {
            String sql = "insert into Missiles"
            + " (WarheadTypeId,MissileName,MissileRange,FuelType)"
            + " VALUES"
            + " (@WarheadTypeId,@MissileName,@MissileRange,@FuelType)";
      
            SqlCommand cmd = new SqlCommand(sql,cnn);
            cmd.Parameters.AddWithValue("@WarheadTypeId",this.WarheadTypeId);
            cmd.Parameters.AddWithValue("@MissileName", this.MissileName);
            cmd.Parameters.AddWithValue("@MissileRange", this.MissileRange);
            cmd.Parameters.AddWithValue("@FuelType", this.FuelType);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public Missile GetMissile(int Id) {

            Missile m = new Missile();
            
            String sql = "select * from Missiles where MissileId = @MissileId";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@MissileId", Id);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                m.WarheadTypeId = int.Parse(reader["WarheadTypeId"].ToString());
                m.MissileName = reader["MissileName"].ToString();
                m.MissileRange = double.Parse(reader["MissileRange"].ToString());
                m.FuelType = reader["FuelType"].ToString();
            }
            cnn.Close();
            return m;
        }
        public List<Missile> GetMissiles()
        {
            List<Missile> missiles = new List<Missile>();

            String sql = "select * from Missiles";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Missile m = new Missile();
                m.WarheadTypeId = int.Parse(reader["WarheadTypeId"].ToString());
                m.MissileName = reader["MissileName"].ToString();
                m.MissileRange = double.Parse(reader["MissileRange"].ToString());
                m.FuelType = reader["FuelType"].ToString();
                missiles.Add(m);
            }
            cnn.Close();
            return missiles;
        }

    }
}