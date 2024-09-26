// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;

[Serializable]
public class Access
{
    public double lat;
    public double lng;
}

[Serializable]
public class Address
{
    public string label;
    public string countryCode;
    public string countryName;
    public string stateCode;
    public string state;
    public string county;
    public string city;
    public string street;
    public string postalCode;
    public string houseNumber;
}

[Serializable]
public class Category
{
    public string id;
    public string name;
    public bool primary;
}

[Serializable]
public class Chain
{
    public string id;
    public string name;
}

[Serializable]
public class Contact
{
    public List<Phone> phone;
    public List<Email> email;
    public List<Www> www;
    public List<Fax> fax;
    public List<TollFree> tollFree;
}

[Serializable]
public class Email
{
    public string value;
    public List<Category> categories;
}

[Serializable]
public class Fax
{
    public string value;
    public List<Category> categories;
}

[Serializable]
public class FoodType
{
    public string id;
    public string name;
    public bool primary;
}

[Serializable]
public class Item
{
    public string title;
    public string id;
    public string language;
    public string ontologyId;
    public string resultType;
    public Address address;
    public Position position;
    public List<Access> access;
    public int distance;
    public List<Category> categories;
    public List<Reference> references;
    public List<Contact> contacts;
    public List<OpeningHour> openingHours;
    public Payment payment;
    public List<FoodType> foodTypes;
    public List<Chain> chains;
}

[Serializable]
public class Method
{
    public string id;
    public bool accepted;
    public List<string> currencies;
}

[Serializable]
public class OpeningHour
{
    public List<Category> categories;
    public List<string> text;
    public bool isOpen;
    public List<Structured> structured;
}

[Serializable]
public class Payment
{
    public List<Method> methods;
}

[Serializable]
public class Phone
{
    public string value;
    public List<Category> categories;
}

[Serializable]
public class Position
{
    public double lat;
    public double lng;
}

[Serializable]
public class Reference
{
    public Supplier supplier;
    public string id;
}

[Serializable]
public class Root
{
    public List<Item> items;
    public int offset;
    public int count;
    public int limit;

    public override string ToString()
    {
        return $"Offset: {offset}, Count: {count}, Limit: {limit}";
    }
}

[Serializable]
public class Structured
{
    public string start;
    public string duration;
    public string recurrence;
}

[Serializable]
public class Supplier
{
    public string id;
}

[Serializable]
public class TollFree
{
    public string value;
    public List<Category> categories;
}

[Serializable]
public class Www
{
    public string value;
    public List<Category> categories;
}

