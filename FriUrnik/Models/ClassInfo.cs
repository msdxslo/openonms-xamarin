using System;
using System.Collections.Generic;
using System.Text;

namespace FriUrnik.Models
{
    public class ClassInfo
    {
        public string Name { get; set; }

        public string ClassRoom { get; set; }

        public string Time { get; set; }

        public List<string> Lecturers{ get; set; }
    }
}
