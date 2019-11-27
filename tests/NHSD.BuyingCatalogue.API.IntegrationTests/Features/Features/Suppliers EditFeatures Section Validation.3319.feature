Feature: Suppliers Edit Solution Description Section Validation
    As a Supplier
    I want to Edit the About Solution Section
    So that I can make sure the information is validated

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
        | Solution | AboutUrl | SummaryDescription             | FullDescription     | Features                          |
        | Sln1     | UrlSln1  | An full online medicine system | Online medicine 1   | [ "Appointments", "Prescribing" ] |

@3319
Scenario: 1. No features are filled out
    Given a request with ten features
    When the update features request is made for Sln1
    Then a successful response is returned
    And Solutions exist
        | SolutionID | SolutionName   |
        | Sln1       | MedicOnline    |
    And SolutionDetail exist
        | Solution | AboutUrl | SummaryDescription             | FullDescription     | Features                        |
        | Sln1     | UrlSln1  | An full online medicine system | Online medicine 1   | ["","","","","","","","","",""] |

@3319
Scenario: 2. listing-1 exceeds the character limit
    Given a request with ten features
    And feature at position 1 is a string of 101 characters
    When the update features request is made for Sln1
    Then a response status of 400 is returned
    And the features response required field contains listing-1
    And Solutions exist
        | SolutionID | SolutionName   |
        | Sln1       | MedicOnline    |
    And SolutionDetail exist
        | Solution | AboutUrl | SummaryDescription             | FullDescription     | Features                          |
        | Sln1     | UrlSln1  | An full online medicine system | Online medicine 1   | [ "Appointments", "Prescribing" ] |

@3319
Scenario: 3. listing-1 & listing-3 are within the character limit. listing-5 & listing-8 exceeds the character limit
    Given a request with ten features
    And feature at position 1 is a string of 10 characters
    And feature at position 3 is a string of 70 characters
    And feature at position 5 is a string of 101 characters
    And feature at position 8 is a string of 101 characters
    When the update features request is made for Sln1
    Then a response status of 400 is returned
    And the features response required field contains listing-5,listing-8
    And Solutions exist
        | SolutionID | SolutionName   |
        | Sln1       | MedicOnline    |
    And SolutionDetail exist
        | Solution | AboutUrl | SummaryDescription             | FullDescription     | Features                          |
        | Sln1     | UrlSln1  | An full online medicine system | Online medicine 1   | [ "Appointments", "Prescribing" ] |
