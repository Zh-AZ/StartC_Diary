using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartC_Diary
{
    struct Employee
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string fullName { get; set; }
        public int age { get; set; }
        public int growth { get; set; }
        public DateTime birthsday { get; set; }
        public string place { get; set; }

        public Employee(int id, DateTime date, string fullName, int age, int growth, DateTime birthsday, string place)
        {
            this.id = id;
            this.date = date;
            this.fullName = fullName;
            this.age = age;
            this.growth = growth;
            this.birthsday = birthsday;
            this.place = place;
        }

        public string Printeres()
        {
            return $"{this.id} {this.date:g} {this.fullName} {this.age} {this.growth} {this.birthsday:d} {this.place}";
        }
    }
}