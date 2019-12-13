Feature:  Display Marketing Page Form BrowserAdditionalInformation Validation on Edit
    As a Supplier
    I want to manage Marketing Page Information for the Solution's BrowserAdditionalInformation
    So that I can ensure the information is correct & valid

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
    And SolutionDetail exist
        | Solution | SummaryDescription             | FullDescription   | ClientApplication                                                                                                                                                                                                                                                                  |
        | Sln1     | An full online medicine system | Online medicine 1 | { "AdditionalInformation": "Some Info", "ClientApplicationTypes": ["browser-based"],"BrowsersSupported" : [ "IE8", "Opera" ], "MobileResponsive": false, "Plugins" : {"Required" : true, "AdditionalInformation": "orem ipsum" }, "HardwareRequirements": "Hardware Information" } |

@3601
Scenario: 1. AdditionalInformation exceeds the maxLength
    Given additional-information on BrowserAdditionalInformation is a string of 501 characters
    When a PUT request is made to update solution Sln1 additional-information section
    Then a response status of 400 is returned
    And the additional-information maxLength field contains additional-information

@3601
Scenario: 2. AdditionalInformation is set to null
    Given additional-information on BrowserAdditionalInformation is null
    When a PUT request is made to update solution Sln1 additional-information section
    Then a successful response is returned
    And SolutionDetail exist
        | Solution | SummaryDescription             | FullDescription   | ClientApplication                                                                                                                                                                                                                                                                                                                                                         |
        | Sln1     | An full online medicine system | Online medicine 1 | { "ClientApplicationTypes": ["browser-based"],"BrowsersSupported" : [ "IE8", "Opera" ], "MobileResponsive": false, "Plugins" : {"Required" : true, "AdditionalInformation": "orem ipsum" }, "MinimumConnectionSpeed": null, "MinimumDesktopResolution": null, "HardwareRequirements": "Hardware Information", "AdditionalInformation": null, "MobileFirstDesign": null  } |
