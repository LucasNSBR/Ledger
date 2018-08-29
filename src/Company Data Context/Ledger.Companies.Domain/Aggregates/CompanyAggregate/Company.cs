using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Companies.Domain.Aggregates.CompanyAggregate
{
    public class Company : Entity<Company>, IAggregateRoot
    {
        public bool Active { get; private set; }
        public string Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public string Description { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public InscricaoEstadual InscricaoEstadual { get; private set; }
        public Address Address { get; private set; }

        protected Company() { }

        public Company(string name, EmailAddress email, string description, Cnpj cnpj, InscricaoEstadual inscricaoEstadual)
        {
            Active = false;

            Name = name;
            Email = email;
            Description = description;
            Cnpj = cnpj;
            InscricaoEstadual = inscricaoEstadual;
        }

        public Company(Guid id, string name, EmailAddress email, string description, Cnpj cnpj, InscricaoEstadual inscricaoEstadual)
        {
            Active = false;

            Id = id;
            Name = name;
            Email = email;
            Description = description;
            Cnpj = cnpj;
            InscricaoEstadual = inscricaoEstadual;
        }

        public void ChangeAddress(Address address)
        {
            Address = address;
        }

        public void ChangePhone(PhoneNumber phone)
        {
            Phone = phone;
        }

        public void SetActive()
        {
            if (!Active)
            {
                Active = true;
                //AddEvent(new CompanyActivatedEvent());
            }
            else
                AddNotification("Erro de ativação", "A empresa já está ativada.");
        }

        public void SetInactive()
        {
            if (Active)
            {
                Active = false;
                //AddEvent(new CompanyInactivedEvent());
            }
            else
                AddNotification("Erro de desativação", "A empresa já está desativada.");
        }
    }
}
