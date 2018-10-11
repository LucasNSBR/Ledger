using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Tests.Mocks
{
    public class FakeTicketCategoryRepository : ITicketCategoryRepository
    {
        private List<TicketCategory> _categories;

        public FakeTicketCategoryRepository()
        {
            _categories = new List<TicketCategory>();
        }

        public IQueryable<TicketCategory> GetAllCategories()
        {
            return _categories.AsQueryable();
        }

        public TicketCategory GetById(Guid id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public void Register(TicketCategory category)
        {
            _categories.Add(category);
        }

        public void Update(TicketCategory category)
        {
            int index = _categories.FindIndex(a => a.Id == category.Id);
            _categories[index] = category;
        }
    }
}
