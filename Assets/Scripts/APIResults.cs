// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections.Generic;

public class Access
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Address
{
    public string label { get; set; }
    public string countryCode { get; set; }
    public string countryName { get; set; }
    public string stateCode { get; set; }
    public string state { get; set; }
    public string county { get; set; }
    public string city { get; set; }
    public string street { get; set; }
    public string postalCode { get; set; }
    public string houseNumber { get; set; }
}

public class Category
{
    public string id { get; set; }
    public string name { get; set; }
    public bool primary { get; set; }
}

public class Chain
{
    public string id { get; set; }
    public string name { get; set; }
}

public class Contact
{
    public List<Phone> phone { get; set; }
    public List<Email> email { get; set; }
    public List<Www> www { get; set; }
    public List<Fax> fax { get; set; }
    public List<TollFree> tollFree { get; set; }
}

public class Email
{
    public string value { get; set; }
    public List<Category> categories { get; set; }
}

public class Fax
{
    public string value { get; set; }
    public List<Category> categories { get; set; }
}

public class FoodType
{
    public string id { get; set; }
    public string name { get; set; }
    public bool primary { get; set; }
}

public class Place
{
    public string title { get; set; }
    public string id { get; set; }
    public string language { get; set; }
    public string ontologyId { get; set; }
    public string resultType { get; set; }
    public Address address { get; set; }
    public Position position { get; set; }
    public List<Access> access { get; set; }
    public int distance { get; set; }
    public List<Category> categories { get; set; }
    public List<Reference> references { get; set; }
    public List<Contact> contacts { get; set; }
    public List<OpeningHour> openingHours { get; set; }
    public Payment payment { get; set; }
    public List<FoodType> foodTypes { get; set; }
    public List<Chain> chains { get; set; }
}

public class Method
{
    public string id { get; set; }
    public bool accepted { get; set; }
    public List<string> currencies { get; set; }
}

public class OpeningHour
{
    public List<Category> categories { get; set; }
    public List<string> text { get; set; }
    public bool isOpen { get; set; }
    public List<Structured> structured { get; set; }
}

public class Payment
{
    public List<Method> methods { get; set; }
}

public class Phone
{
    public string value { get; set; }
    public List<Category> categories { get; set; }
}

public class Position
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Reference
{
    public Supplier supplier { get; set; }
    public string id { get; set; }
}

public class Root
{
    public List<Place> places { get; set; }
    public int offset { get; set; }
    public int count { get; set; }
    public int limit { get; set; }
}

public class Structured
{
    public string start { get; set; }
    public string duration { get; set; }
    public string recurrence { get; set; }
}

public class Supplier
{
    public string id { get; set; }
}

public class TollFree
{
    public string value { get; set; }
    public List<Category> categories { get; set; }
}

public class Www
{
    public string value { get; set; }
    public List<Category> categories { get; set; }
}

