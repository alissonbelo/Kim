using Domain.Entities;
using Domain.Validation;

namespace Domain.UnitTest;

public class PersonTest
{
    [Fact]
    public void ValidPersonCreation()
    {
        string name = "Ted Mosby";
        string document = "12345678901";
        string email = "ted.mosby@example.com";
        string phone = "1234567890";
        
        var person = new Person(name, document, email, phone);
        
        Assert.NotNull(person);
        Assert.Equal(name, person.Name);
        Assert.Equal(document, person.Document);
        Assert.Equal(email, person.Email);
        Assert.Equal(phone, person.Phone);
    }

    [Fact]
    public void InvalidPersonCreation_EmptyName()
    {
        string name = "";
        string document = "12345678901";
        string email = "ted.mosby@example.com";
        string phone = "1234567890";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }

    [Fact]
    public void InvalidPersonCreation_LongName()
    {

        string name = new string('A', 255);
        string document = "12345678901";
        string email = "ted.mosby@example.com";
        string phone = "1234567890";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
    
    [Fact]
    public void InvalidPersonCreation_EmptyDocument()
    {
        string name = "Ted Mosby";
        string document = "";
        string email = "ted.mosby@example.com";
        string phone = "1234567890";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
    
    [Fact]
    public void InvalidPersonCreation_DocumentInvalid()
    {

        string name = "Ted Mosby";
        string document = "1111111111";
        string email = "ted.mosby@example.com";
        string phone = "1234567890";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
    
    [Fact]
    public void InvalidPersonCreation_EmptyEmail()
    {
        string name = "Ted Mosby";
        string document = "11111111111";
        string email = "";
        string phone = "1234567890";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
    
    [Fact]
    public void InvalidPersonCreation_EmailInvalid()
    {

        string name = "Ted Mosby";
        string document = "1111111111";
        string email = "john.com";
        string phone = "1234567890";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
    
    [Fact]
    public void InvalidPersonCreation_EmptyPhone()
    {
        string name = "Ted Mosby";
        string document = "11111111111";
        string email = "ted.mosby@example.com";
        string phone = "";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
    
    [Fact]
    public void InvalidPersonCreation_PhoneInvalid()
    {

        string name = "Ted Mosby";
        string document = "1111111111";
        string email = "ted.mosby@example.com";
        string phone = "111111111";
        
        Assert.Throws<DomainValidation>(() => new Person(name, document, email, phone));
    }
}