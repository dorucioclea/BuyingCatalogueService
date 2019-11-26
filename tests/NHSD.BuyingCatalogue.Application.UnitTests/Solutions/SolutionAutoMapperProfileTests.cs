using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Configuration;
using FluentAssertions;
using NHSD.BuyingCatalogue.Contracts;
using NHSD.BuyingCatalogue.Contracts.Solutions;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionFeatures;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionSummary;
using NHSD.BuyingCatalogue.Solutions.Application.Domain;
using NHSD.BuyingCatalogue.Solutions.Application.Mapping;
using NHSD.BuyingCatalogue.Solutions.Application.Queries.GetSolutionById;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Application.UnitTests.Infrastructure.Mapping
{
    [TestFixture]
    public sealed class SolutionAutoMapperProfileTests
    {
        private SolutionAutoMapperProfile _solutionAutoMapperProfile;

        [SetUp]
        public void SetUp()
        {
            _solutionAutoMapperProfile = new SolutionAutoMapperProfile();
        }

        public static IEnumerable<KeyValuePair<Type, Type>> SupportedMappings()
        {
            yield return new KeyValuePair<Type, Type>(typeof(UpdateSolutionSummaryViewModel), typeof(Solution));
            yield return new KeyValuePair<Type, Type>(typeof(UpdateSolutionFeaturesViewModel), typeof(Solution));
            yield return new KeyValuePair<Type, Type>(typeof(Solution), typeof(SolutionDto));
            yield return new KeyValuePair<Type, Type>(typeof(Solution), typeof(ISolution));
            yield return new KeyValuePair<Type, Type>(typeof(ClientApplication), typeof(ClientApplicationDto));
            yield return new KeyValuePair<Type, Type>(typeof(ClientApplication), typeof(IClientApplication));
            yield return new KeyValuePair<Type, Type>(typeof(Plugins), typeof(PluginsDto));
            yield return new KeyValuePair<Type, Type>(typeof(Plugins), typeof(IPlugins));
            yield return new KeyValuePair<Type, Type>(typeof(Contact), typeof(ContactDto));
            yield return new KeyValuePair<Type, Type>(typeof(Contact), typeof(IContact));
        }

        [Test]
        public void Profile_MatchesMapping()
        {
            var configs = ((IProfileConfiguration)_solutionAutoMapperProfile).TypeMapConfigs;

            var mappings = configs.Select(tmc => new KeyValuePair<Type, Type>(tmc.SourceType, tmc.DestinationType));

            SupportedMappings().Should().BeEquivalentTo(mappings);
        }
    }
}