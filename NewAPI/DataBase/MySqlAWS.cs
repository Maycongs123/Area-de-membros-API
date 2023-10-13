using System.Configuration;

namespace NewAPI.DataBase
{
    public class MySqlAWS
    {
        public string ConnectionString { get; set; }

        public MySqlAWS() {
            //ConnectionString =
            //     "Server=database-members.ceursjcoeniq.us-east-1.rds.amazonaws.com;" +
            //     "Database=dbmembers;" +
            //     "User=adminedgar;" +
            //     "Password=w(4E_3&w;";
            ConnectionString =
                 "Server=db-undefined.c7whbgkbhqc5.us-east-1.rds.amazonaws.com;" +
                 "Database=areamembros;" +
                 "User=lpadmin;" +
                 "Password=Changge!2023;";
        }


    }


}
