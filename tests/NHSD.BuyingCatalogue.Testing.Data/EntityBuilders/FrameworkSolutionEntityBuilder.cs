using System;
using System.Collections.Generic;
using System.Text;
using NHSD.BuyingCatalogue.Testing.Data.Entities;

namespace NHSD.BuyingCatalogue.Testing.Data.EntityBuilders
{
    public sealed class FrameworkSolutionEntityBuilder
    {
        private readonly FrameworkSolutionEntity _frameworkSolutionEntity;

        public static FrameworkSolutionEntityBuilder Create()
        {
            return new FrameworkSolutionEntityBuilder();
        }

        public FrameworkSolutionEntityBuilder()
        {
            _frameworkSolutionEntity = new FrameworkSolutionEntity()
            {
                FrameworkId = "NHSDGP001",
                SolutionId = "Sln1",
                IsFoundation = true
            };
        }

        public FrameworkSolutionEntityBuilder WithFrameworkId(string frameworkId)
        {
            _frameworkSolutionEntity.FrameworkId = frameworkId;
            return this;
        }

        public FrameworkSolutionEntityBuilder WithSolutionId(string solutionId)
        {
            _frameworkSolutionEntity.SolutionId = solutionId;
            return this;
        }

        public FrameworkSolutionEntityBuilder WithFoundation(bool isFoundation)
        {
            _frameworkSolutionEntity.IsFoundation = isFoundation;
            return this;
        }

        public FrameworkSolutionEntity Build()
        {
            return _frameworkSolutionEntity;
        }
    }
}
