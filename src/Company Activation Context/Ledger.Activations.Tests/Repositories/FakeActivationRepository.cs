using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace Ledger.Activations.Tests.Repositories
{
    public class FakeActivationRepository : IActivationRepository
    {
        private List<Activation> _activations;

        public FakeActivationRepository()
        {
            _activations = new List<Activation>()
            {
                new Activation(
                    new Company(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"), 
                    new Owner("Lucas Pereira Campos", 20, new Cpf("981.153.856-99")))
                    ),
                new Activation(
                    new Company(new Guid("775808dc-0c6a-4826-9711-b0ea6cf2e72c"),
                    new Owner("Pedro Henrique Pereira Campos", 12, new Cpf("150.992.359-00")))
                    ),
                new Activation(
                    new Company(new Guid("9c0e0aa4-2618-4158-9714-dee8dd94b5ad"),
                    new Owner("Otacilio Rocha Campos", 40, new Cpf("515.110.991-05")))
                    ),
            };
        }

        public Activation FirstOrDefault { get; internal set; }

        public List<Activation> GetActivations()
        {
            return _activations;
        }

        public Activation GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Register(Activation activation)
        {
            throw new NotImplementedException();
        }

        public void Update(Activation activation)
        {
            throw new NotImplementedException();
        }
    }
}
