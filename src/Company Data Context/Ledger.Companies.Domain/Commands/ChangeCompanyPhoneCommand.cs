﻿using Ledger.Shared.Commands;
using System;

namespace Ledger.Companies.Domain.Commands
{
    public class ChangeCompanyPhoneCommand : Command
    {
        public Guid CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        
        public override void Validate()
        {
        }
    }
}