namespace Models.Product;

public class Product
{
    private Guid id = Guid.NewGuid();
    private string name;
    private string description;
    private float price;
    private int quantity;
    private DateTime created_at;
    private DateTime updated_at;
    private bool active;

    public Product(){}

    public Product(string name, string description, float price, int quantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }

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
    
    public string Description
    {
        get{return description;}
        set{description = value;}
    }

    public float Price
    {
        get{return price;}
        set{price = value;}
    }

    public int Quantity
    {
        get{return quantity;}
        set{quantity = value;}
    }

    public bool Active
    {
        get{return active;}
        set{active = value;}
    }
}