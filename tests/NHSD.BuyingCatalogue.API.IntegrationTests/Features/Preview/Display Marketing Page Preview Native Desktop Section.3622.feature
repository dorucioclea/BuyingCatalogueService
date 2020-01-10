Feature: Display Marketing Page Preview Native Desktop Section
	As a Catalogue User
    I want to manage Marketing Page Information for the Client Application Types Section
    So that I can ensure the information is correct

Background:
    Given Organisations exist
        | Name     |
        | GPs-R-Us |
    And Suppliers exist
        | Id    | OrganisationName |
        | Sup 1 | GPs-R-Us         |
    And Solutions exist
        | SolutionID | SolutionName | OrganisationName | SupplierStatusId | SupplierId |
        | Sln1       | MedicOnline  | GPs-R-Us         | 1                | Sup 1      |
    And SolutionDetail exist
        | Solution | SummaryDescription | FullDescription   | ClientApplication                                                                                                                                                                                                                                                                                                                                                         |
        | Sln1     | Online Description | Online medicine 1 | { "ClientApplicationTypes" : [ "native-desktop"], "NativeDesktopHardwareRequirements": "A native desktop hardware requirement","NativeDesktopOperatingSystemsDescription": "A native desktop OS description", "NativeDesktopMinimumConnectionSpeed": "2Mbps", "NativeDesktopThirdParty": { "ThirdPartyComponents": "Components", "DeviceCapabilities": "Capabilities" } } |

@3605
Scenario:1. Get Solution Preview contains client application types native-desktop answers for all data
    When a GET request is made for solution preview Sln1
    Then a successful response is returned
    And the solution client-application-types section is returned
    And the solution native-desktop native-desktop-hardware-requirements section contains hardware-requirements with value A native desktop hardware requirement
    And the solution native-desktop native-desktop-connection-details section contains minimum-connection-speed with value 2Mbps
    And the solution native-desktop native-desktop-operating-systems section contains operating-systems-description with value A native desktop OS description
    And the solution native-desktop native-desktop-third-party section contains third-party-components with value Components
    And the solution native-desktop native-desktop-third-party section contains device-capabilities with value Capabilities
    