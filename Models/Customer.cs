namespace plataformatcc.Models
{
    public class Customer
    {
        private Guid id = Guid.NewGuid();
        private string name = string.Empty;
        private string surname = string.Empty;
        private string email = string.Empty;
        private DateTime birthdate;
        private DateTime created_at;
        private DateTime update_at;
        private bool active; 

        public Guid Id 
        {
            set{id = value;}
            get{return id;}
        }   
        

        public string Name
        {
            get{return name;}
            set{name = value;}
        }

        public string Surname
        {
            get{return surname;}
            set{surname = value;}
        }
        public string Email
        {
            get{return email;}
            set{email = value;}
        }

        public DateTime Birthdate
        {
            get{return birthdate;}
            set{birthdate = value;}
        }

        public DateTime Created_at
        {
            get{return created_at;}
            set{created_at = value;}
        }

        public DateTime Update_at
        {
            get{return update_at;}
            set{update_at = value;}
        }

        public bool Active
        {
            get{return active;}
            set{active = value;}
        }

        public Customer(){}

        public Customer(string name, string surname, string email, DateTime birthdate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            Birthdate = birthdate;
        }      

    
    }
}