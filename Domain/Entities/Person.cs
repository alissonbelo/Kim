using System.Text.Json.Serialization;
using Domain.Validation;

namespace Domain.Entities;

public sealed class Person : BaseEntity
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public Person(string name, string document, string email, string phone)
    {
        ValidateCustomer(name, document, email, phone);
    }
    
    public Person( ) { }

    [JsonConstructor]
    public Person(Guid id, string name, string document, string email, string phone)
    {
        DomainValidation.When(string.IsNullOrEmpty(id.ToString()) || !Guid.TryParse(id.ToString(), out _),
            "ID inválido.");
        Id = id;
    }

    private void ValidateCustomer(string name, string document, string email, string phone)
    {
        Console.WriteLine(name.Length);
        DomainValidation.When(string.IsNullOrEmpty(name),
            "Nome Inválido. O nome é obrigatório");

        DomainValidation.When(name.Length < 3 || name.Length > 250,
            "Nome Inválido. O nome deve ter entre 3 à 250 caracteres.");
        
        DomainValidation.When(string.IsNullOrEmpty(document),
            "CPF Inválido. O CPF é obrigatório");

        DomainValidation.When(document.Length != 11,
            "CPF Inválido. O CPF deve ter 11 caracteres.");
        
        DomainValidation.When(string.IsNullOrEmpty(email),
            "E-mail Inválido. O E-mail é obrigatório");

        DomainValidation.When(email.Length < 3 || email.Length > 150,
            "E-mail Inválido. O E-mail deve ter entre 3 à 150 caracteres.");
        
        DomainValidation.When(string.IsNullOrEmpty(phone),
            "Telefone Inválido. O Telefone é obrigatório");

        DomainValidation.When(phone.Length < 9 || phone.Length > 11,
            "Telefone Inválido. O Telefone deve ter entre 10 à 11 caracteres.");

        Name = name;
        Document = document;
        Email = email;
        Phone = phone;
    }
}