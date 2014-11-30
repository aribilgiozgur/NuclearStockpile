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

        // Sadece view için kullanılmakta
        public String WarheadTypeText { get; set; }

        public SqlConnection cnn = new SqlConnection("Server=.;Database=NuclearDB; Trusted_Connection=true;");


        public Missile(List<String> parameterArray)
        {
            try
            {
                this.WarheadTypeId = int.Parse(parameterArray[0]);
                this.MissileName = parameterArray[1];
                this.MissileRange = double.Parse(parameterArray[2]);
                this.FuelType = parameterArray[3];
            }
            catch (Exception ex) {
                //throw new Exception();
            }

        }

        public Missile(int Id)
        {
            String sql = "select * from Missiles where MissileId = @MissileId";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@MissileId", Id);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                this.MissileId = Id;
                this.WarheadTypeId = int.Parse(reader["WarheadTypeId"].ToString());
                this.MissileName = reader["MissileName"].ToString();
                this.MissileRange = double.Parse(reader["MissileRange"].ToString());
                this.FuelType = reader["FuelType"].ToString();
                WarheadType w = new WarheadType(this.WarheadTypeId);
                this.WarheadTypeText = w.WarheadTypeName;
                
            }
            cnn.Close();

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

     
        public static List<Missile> GetMissiles()
        {
            SqlConnection cnn = new SqlConnection("Server=.;Database=NuclearDB; Trusted_Connection=true;");             


            List<Missile> missiles = new List<Missile>();

            String sql = "select * from Missiles";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                /*int missileId = int.Parse(reader["MissileId"].ToString());
                Missile m = new Missile(missileId);*/
                Missile m = new Missile(reader.GetInt32(0));
                missiles.Add(m);
            }
            cnn.Close();
            return missiles;
        }

        public void Save() {
            String sql = "update Missiles set"
                +" WarheadTypeId=@WarheadTypeId,"
                +" MissileName=@MissileName,"
                +" MissileRange=@MissileRange,"
                +" FuelType=@FuelType"
                +" where MissileId = @MissileId";

            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@WarheadTypeId", this.WarheadTypeId);
            cmd.Parameters.AddWithValue("@MissileName", this.MissileName);
            cmd.Parameters.AddWithValue("@MissileRange", this.MissileRange);
            cmd.Parameters.AddWithValue("@FuelType", this.FuelType);
            cmd.Parameters.AddWithValue("@MissileId", this.MissileId);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public void Delete() {
            String sql = "delete from Missiles where MissileId = @MissileId";

            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@MissileId", this.MissileId);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }
      

    
    }
}