using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Studenci.Models
{
    /* The class Student has properties IndexNumber, Fname, Lname, Birthdate, Email, MothersName, FathersName and Studies */
    class Student
    {
        /* Used to convert the date format from yyyy/MM/dd to dd/MM/yyyy. */
        private DateTime _birthdate;

        public string IndexNumber { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        /* Used to convert the date format from yyyy/MM/dd to dd/MM/yyyy. */
        [JsonPropertyName("birthday")]
        /* A property that is used to convert the date format from yyyy/MM/dd to dd/MM/yyyy. */
        public string Birthdatestring
        {
            /* Converting the date format from yyyy/MM/dd to dd/MM/yyyy. */
            get { return _birthdate.ToString("dd/MM/yyyy"); }
            /* Converting the date format from yyyy/MM/dd to dd/MM/yyyy. */
            set { _birthdate = DateTime.ParseExact(value, "yyyy/MM/dd", null); }
        }

        /* Used to ignore the property when serializing the object. */
        [JsonIgnore]
        /* Used to convert the date format from yyyy/MM/dd to dd/MM/yyyy. */
        public DateTime Birthdate
        {
            get { return _birthdate; }
            set { _birthdate = value; }
        }

        public string Email { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }
        public Studies Studies { get; set; }
    }
}