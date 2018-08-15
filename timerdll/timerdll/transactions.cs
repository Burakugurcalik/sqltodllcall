using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Data.SqlClient;
using System.Data;

namespace timerdll
{
   public  class transactions
    {
        static veribag baglanti = new veribag();
        static Timer _timer; // From System.Timers
        static List<applist> _l;
        static List<applist> _l2=new List<applist>();

        static List<applist> appinglist = new List<applist>();
        static int appli = 0;
        static string cum = "";
        static int val = 100;

        public  List<applist> DateLisst // Gets the results
        {
            get
            {
                if (_l == null) // Lazily initialize the timer
                {
                    Start(); // Start the timer
                }
                return _l;
               // Return the list of dates
            }
        }
        
        public void databasecon(int applicaions,string sql,int refleshinterval)
        {
            val = refleshinterval;
            cum = sql;
            appli = applicaions;
    


        }
        void Start()
        {
            _l = new List<applist>(); // Allocate the list
            _l2 = new List<applist>();
            _timer = new Timer(val); // Set up the timer for 3 seconds
                                      //
                                      // Type "_timer.Elapsed += " and press tab twice.
                                      //
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true; // Enable it
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
              kayitgetir(appli, cum);
          
             
                _l2 = _l;
            }
            catch (Exception)
            {

                _l=_l2;
            }
            
           
        }

        static void kayitgetir(int applicaionname, string sqlcümlesi)
        {

            if (_l!=null)
            {
                _l.Clear();
            }

            baglanti.baglanti(sqlcümlesi);
            SqlCommand cmd = new SqlCommand("kayitgetir", baglanti.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@gapplicationname", applicaionname);
            baglanti.bagac();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                applist appl = new applist();
                appl.Id = Convert.ToInt32(dr["Id"].ToString());
                appl.Name = dr["Name"].ToString();
                appl.Type = dr["Type"].ToString();
                appl.Value = dr["Value"].ToString();
                appl.Isactive = Convert.ToBoolean(dr["IsActive"].ToString());
                appl.Applicationname= dr["ApplicaitonName"].ToString();
                _l.Add(appl);
            }
            baglanti.bagkapat();
          

        }
        public string kayitekle(string name,string Type,String Value,Boolean IsActive,int Applicationname)
        {
            string mesaj = "a";


            try
            {
                if (Type == "String")
                {
                    string a = Convert.ToString(Value);
                }
               else if (Type=="Int")
                {
                    int b = Convert.ToInt32(Value);
                }
               else if (Type=="Boolean")
                {
                    Boolean b = Convert.ToBoolean(Value);
                }
                else if (Type=="Double")
                {
                    Double d = Convert.ToDouble(Value);
                }

            }
            catch (Exception)
            {

                mesaj = "Gönderilen Değer Hatalı!";
            }
            if (mesaj.Length<=1)
            {
                try
                {
                    baglanti.baglanti(cum);
                    SqlCommand cmd = new SqlCommand("KayitEkle", baglanti.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NAme", name);
                    cmd.Parameters.AddWithValue("@Type", Type);
                    cmd.Parameters.AddWithValue("@value", Value);
                    cmd.Parameters.AddWithValue("@IsActive", IsActive);
                    cmd.Parameters.AddWithValue("@gapplicationname", Applicationname);
                    baglanti.bagac();
                cmd.ExecuteNonQuery();
               
                    mesaj = "Kayıt Başarı ile Eklenmiştir!";
                    baglanti.bagkapat();
                }
                catch (Exception)
                {

                    mesaj = "Kayıt Ekleme Sırasında Sorun Oluştu!";
                }
               
            }
      

            return mesaj;
        }
        public string kayitguncelle(string name, string Type, String Value, Boolean IsActive, int Applicationname,int Id)
        {
            string mesaj = "a";


            try
            {
                if (Type == "String")
                {
                    string a = Convert.ToString(Value);
                }
                else if (Type == "Int")
                {
                    int b = Convert.ToInt32(Value);
                }
                else if (Type == "Boolean")
                {
                    Boolean b = Convert.ToBoolean(Value);
                }
                else if (Type == "Double")
                {
                    Double d = Convert.ToDouble(Value);
                }

            }
            catch (Exception)
            {

                mesaj = "Gönderilen Değer Hatalı!";
            }
            if (mesaj.Length <= 1)
            {
                try
                {
                    baglanti.baglanti(cum);
                    SqlCommand cmd = new SqlCommand("kayitguncelle", baglanti.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NAme", name);
                    cmd.Parameters.AddWithValue("@Type", Type);
                    cmd.Parameters.AddWithValue("@value", Value);
                    cmd.Parameters.AddWithValue("@IsActive", IsActive);
                    cmd.Parameters.AddWithValue("@gapplicationname", Applicationname);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    baglanti.bagac();
                    cmd.ExecuteNonQuery();

                    mesaj = "Kayıt Başarı ile Güncellenmiştir!";
                    baglanti.bagkapat();
                }
                catch (Exception)
                {

                    mesaj = "Kayıt Ekleme Sırasında Sorun Oluştu!";
                }

            }


            return mesaj;
        }
        public DataTable appgetir()
        {

            if (_l != null)
            {
                _l.Clear();
            }

            baglanti.baglanti(cum);
            SqlCommand cmd = new SqlCommand("uygulamagetir", baglanti.con);
            cmd.CommandType = CommandType.StoredProcedure;
       
            baglanti.bagac();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            baglanti.bagkapat();

            return dt;
        }
        public T GetValue<T>(string key)
        {
            var list = new List<applist>();
            var item = list.Find(x => x.Name == key);
            if (item!=null)
            {
                string a= item.Value.ToString();
                return (T)Convert.ChangeType(a, typeof(T));
            }

            throw new ArgumentNullException($"{key} not found.");
        }
      
    }
}
