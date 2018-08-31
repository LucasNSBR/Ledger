using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.Activations.Domain.Specifications.ActivationSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Activations.Tests.Mocks
{
    public class FakeActivationRepository : IActivationRepository
    {
        private List<Activation> _activations;

        public FakeActivationRepository()
        {
            _activations = new List<Activation>();
        }

        public List<Activation> GetActivations()
        {
            return _activations;
        }

        public Activation GetById(Guid id)
        {
            ActivationIdSpecification spec = new ActivationIdSpecification(id);

            return _activations.AsQueryable().FirstOrDefault(spec.ToExpression());
        }

        public void Register(Activation activation)
        {
            _activations.Add(activation);
        }

        public void Update(Activation activation)
        {
            int index = _activations.FindIndex(a => a.Id == activation.Id);
            _activations[index] = activation;
        }
    }
}
