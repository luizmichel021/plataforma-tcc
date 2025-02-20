using System.ComponentModel.DataAnnotations;

namespace plataformatcc.Models
{
    public class Customer
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthdate;
        private DateTime _created_at;
        private DateTime _update_at;
        private bool _active; 

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

        public DateTime Birthdate
        {
            get{return _birthdate;}
            set{_birthdate = value;}
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

        public bool Active
        {
            get{return _active;}
            set{_active = value;}
        }

        public Customer(){}

        public Customer(int _id, string _name, string _surname, string _email, DateTime _birthdate, DateTime _created_at, DateTime _update_at,bool active)
        {
            Id = _id;
            Name = _name;
            Surname = _surname;
            Email = _email;
            Birthdate = _birthdate;
            Created_at = _created_at;
            Update_at = _update_at;
            Active = _active;
        }

        public Customer(string _name, string _surname, string _email, DateTime _birthdate)
        {
            Name = _name;
            Surname = _surname;
            Email = _email;
            Birthdate = _birthdate;
        }

    
    }
}