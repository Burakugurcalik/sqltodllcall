using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timerdll
{
    public class applist
    {
         int id;
        string name;
        string type;
        string values;
        Boolean isactive;
        string applicationname;


        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
        public string Value
        {
            get
            {
                return values;
            }

            set
            {
                values = value;
            }
        }
        public Boolean Isactive
        {
            get
            {
                return isactive;
            }

            set
            {
                isactive = value;
            }
        }
        public string Applicationname
        {
            get
            {
                return applicationname;
            }

            set
            {
                applicationname = value;
            }
        }
    }
}
