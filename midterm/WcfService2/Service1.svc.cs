using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "data/{umap}")]
        public string sendDataToMySql(string umap)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("insert into userdat(map) values('"+umap+"')", connection);
            command.ExecuteNonQuery();
            connection.Close();

            return "Task Performed!";

        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "insertudata/{descr}/{price}/{lon}/{lat}")]

        public string Insertudata(string discr, string price, float lon, float lat)
        {
            //Declare Connection by passing the connection string from the web config file
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

            //Open the connection
            conn.Open();

            //Declare the sql command
            SqlCommand cmd = new SqlCommand
                ("Insert into userdat(discr,price)values('" + discr + "','" + price + "')", conn);

            //Execute the insert query
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //close the connection
            conn.Close();
            SqlCommand cmd2 = new SqlCommand("Insert into userloc(longitude,latitude)values('" + lon + "','" + lat + "')", conn);
            cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            return "success";


            //Open the connection
            // lookup person with the requested id 
            
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
