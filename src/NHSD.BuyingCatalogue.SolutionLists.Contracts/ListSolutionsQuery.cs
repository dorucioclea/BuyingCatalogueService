using System;
using System.Collections.Generic;
using MediatR;

namespace NHSD.BuyingCatalogue.SolutionLists.Contracts
{
    /// <summary>
    /// Represents the query paramters for the get all solutions request.
    /// </summary>
    public sealed class ListSolutionsQuery : IRequest<ISolutionList>
    {
        /// <summary>
        /// Gets the filter criteria for this query.
        /// </summary>
        private ListSolutionsFilter Filter { get; } = new ListSolutionsFilter();

        /// <summary>
        /// A list of capability Ids with no duplicates.
        /// </summary>
        public ISet<Guid> CapabilityIdList => Filter.Capabilities;

        /// <summary>
        /// Filter to foundation solutions.
        /// </summary>
        public bool IsFoundation => Filter.IsFoundation;

        /// <summary>
        /// Initialises a new instance of the <see cref="ListSolutionsQuery"/> class.
        /// </summary>
        public ListSolutionsQuery()
        {

        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ListSolutionsQuery"/> class.
        /// </summary>
        /// <param name="capabilityIdList">List of capability identifiers to filter on.</param>
        public ListSolutionsQuery(ListSolutionsFilter filter) => Filter = filter ?? throw new System.ArgumentNullException(nameof(filter));
    }
}
