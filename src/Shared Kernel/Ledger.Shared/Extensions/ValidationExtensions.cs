using Ledger.Shared.Notifications;
using LilValidation.Core;
using System.Collections.Generic;

namespace Ledger.Shared.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Add ValidationErrors to a DomainNotification list.
        /// </summary>
        /// <param name="validationErrors">This list of validation errors</param>
        /// <param name="notifier">INotifier object that carries the list</param>
        public static void AddToNotifier(this IReadOnlyList<ValidationError> validationErrors, IDomainNotifier notifier)
        {
            foreach (ValidationError error in validationErrors)
            {
                notifier.AddNotification(error.ErrorCode, error.ErrorDescription);
            }
        }
    }
}
