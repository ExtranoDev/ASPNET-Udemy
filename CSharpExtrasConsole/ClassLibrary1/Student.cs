using System;
using System.Collections.Generic;

namespace College
{
    // one-to-one class definition
    public class Student
    {
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public Branch branch { get; set; } // contains reference to object of Branch class that represents the branch that the current student belongs to
    }
    // one to many class definition
    public class Student2
    {
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public List<Examination> examinations { get; set; }
    }
}


