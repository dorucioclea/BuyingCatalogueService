Feature:  Supplier Edit Native Mobile Hardware Requirements
    As a Supplier
    I want to Edit the Native Mobile Hardware Requirements Section
    So that I can ensure the information is correct

Background:
    Given Organisations exist
        | Name     |
        | GPs-R-Us |
    And Suppliers exist
        | Id    | OrganisationName |
        | Sup 1 | GPs-R-Us         |
    And Solutions exist
        | SolutionID | SolutionName   | OrganisationName | SupplierStatusId | SupplierId |
        | Sln1       | MedicOnline    | GPs-R-Us         | 1                | Sup 1      |
@3600
Scenario: 1. Native Mobile Hardware Requirements is updated
    Given SolutionDetail exist
        | Solution | SummaryDescription             | FullDescription   | ClientApplication                                                                        |
        | Sln1     | An full online medicine system | Online medicine 1 | { "ClientApplicationTypes": ["browser-based"],"BrowsersSupported" : [ "IE8", "Opera" ] } |
    When a PUT request is made to update the native-mobile-hardware-requirements section for solution Sln1
        | HardwareRequirements |
        | New Hardware         |
    Then a successful response is returned
    And SolutionDetail exist
        | Solution | SummaryDescription             | FullDescription   | ClientApplication                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
        | Sln1     | An full online medicine system | Online medicine 1 | { "ClientApplicationTypes": ["browser-based"], "BrowsersSupported" : [ "IE8", "Opera" ], "MobileResponsive": null, "Plugins": null, "MinimumConnectionSpeed": null, "MinimumDesktopResolution": null, "HardwareRequirements": null, "NativeMobileHardwareRequirements": "New Hardware", "NativeDesktopHardwareRequirements": null, "AdditionalInformation": null, "MobileFirstDesign": null, "NativeMobileFirstDesign": null, "MobileOperatingSystems": null, "MobileConnectionDetails": null, "MobileMemoryAndStorage": null, "MobileThirdParty": null, "NativeMobileAdditionalInformation": null } |

@3600
Scenario: 2. Solution is not found
    Given a Solution Sln2 does not exist
    When a PUT request is made to update the native-mobile-hardware-requirements section for solution Sln2
       | HardwareRequirements      |
       | New Hardware Requirements |
    Then a response status of 404 is returned 

@3600
Scenario: 3. Service Failure
    Given the call to the database to set the field will fail
    When a PUT request is made to update the native-mobile-hardware-requirements section for solution Sln1
        | HardwareRequirements      |
        | New Hardware Requirements |
    Then a response status of 500 is returned

@3600
Scenario: 4. Solution id is not present in the request
    When a PUT request is made to update the native-mobile-hardware-requirements section with no solution id
        | HardwareRequirements      |
        | New Hardware Requirements |
    Then a response status of 400 is returned
