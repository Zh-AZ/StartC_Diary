using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartC_Diary
{
    /// <summary>
    /// Структура сотрудника
    /// </summary>
    struct Employee
    {
        /// <summary>
        /// Описание сотрудника
        /// </summary>
        public int id { get; set; }
        public DateTime date { get; set; }
        public string fullName { get; set; }
        public int age { get; set; }
        public int growth { get; set; }
        public DateTime birthsday { get; set; }
        public string place { get; set; }

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="fullName"></param>
        /// <param name="age"></param>
        /// <param name="growth"></param>
        /// <param name="birthsday"></param>
        /// <param name="place"></param>
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

        /// <summary>
        /// Вывод данных 
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{this.id} {this.date:g} {this.fullName} {this.age} {this.growth} {this.birthsday:d} {this.place}";
        }
    }
}