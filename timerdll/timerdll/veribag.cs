using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timerdll
{
    public class veribag
    {
        public  SqlConnection con;

        public void baglanti(string veribaglantisi)
        {




            con = new SqlConnection(veribaglantisi);


        }
        public void bagac()
        {

            if (con.State == ConnectionState.Closed)
            {
                

                con.Open();
            

            }
        }
        public  void bagkapat()
        {
            if (con.State == ConnectionState.Open)
           {     con.Close();
          

            }
        }

    }
}
