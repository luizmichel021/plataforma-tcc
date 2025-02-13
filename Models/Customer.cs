using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class Customer
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _email;
        private DateOnly _brithdate;
        private DateTime _created_at;
        private DateTime _update_at;

        public int Id 
        {
            get{return _id;}
            set{_id = value;}   
        }

        public string Name
        {
            get{return _name;}
            set{_name = value;}
        }

        public string Surname
        {
            get{return _surname;}
            set{_surname = value;}
        }

        public string Email
        {
            get{return _email;}
            set{_email = value;}
        }

        public DateOnly Brithdate
        {
            get{return _brithdate;}
            set{_brithdate = value;}
        }

        public DateTime Created_at
        {
            get{return _created_at;}
            set{_created_at = value;}
        }

        public DateTime Update_at
        {
            get{return _update_at;}
            set{_update_at = value;}
        }

        public Customer(){}

        public Customer(int _id, string _name, string _surname, string _email, DateOnly _birthdate, DateTime _created_at, DateTime _update_at)
        {
            Id = _id;
            Name = _name;
            Surname = _surname;
            Email = _email;
            Brithdate = _birthdate;
            Created_at = _created_at;
            Update_at = _update_at;

        }
        


        
    }
}