using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Studenci.Models
{
    /* The class Uczelnia has three properties: CreatedAt, Author and Students. The first one is of type DateTime, the
    second one is of type string and the third one is of type IEnumerable<Student> */
    class Uczelnia
    {
        private DateTime _createdAt;

        [JsonPropertyName("createdAt")]
        public string CreatedAtString
        {
            get { return _createdAt.ToString("dd/MM/yyyy"); }
            set { _createdAt = DateTime.ParseExact(value, "yyyy/MM/dd", null); }
        }

        [JsonIgnore]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        public string Author { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<ActiveStudies> ActiveStudies { get; set; }
    }
}