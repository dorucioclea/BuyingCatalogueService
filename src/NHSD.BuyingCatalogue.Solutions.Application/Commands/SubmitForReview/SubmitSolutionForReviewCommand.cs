using MediatR;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.SubmitForReview
{
    public sealed class SubmitSolutionForReviewCommand : IRequest<SubmitSolutionForReviewCommandResult>
    {
        /// <summary>
        /// A value to uniquely identify a solution.
        /// </summary>
        public string SolutionId
        {
            get;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SubmitSolutionForReviewCommand"/> class.
        /// </summary>
        public SubmitSolutionForReviewCommand(string solutionId)
        {
            if (string.IsNullOrWhiteSpace(solutionId))
            {
                throw new System.ArgumentException("message", nameof(solutionId));
            }

            SolutionId = solutionId;
        }
    }
}