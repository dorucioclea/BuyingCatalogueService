Feature: Suppliers Edit Solution Integrations Section
    As a Supplier
    I want to Edit the Solution Integrations Section
    So that I can make sure the information is correct

    Background:
        Given Suppliers exist
            | Id    | SupplierName |
            | Sup 1 | Supplier 1   |
        And Solutions exist
            | SolutionID | SolutionName   | SupplierStatusId | SupplierId |
            | Sln1       | MedicOnline    | 1                | Sup 1      |
            | Sln2       | TakeTheRedPill | 1                | Sup 1      |
            | Sln3       | PracticeMgr    | 1                | Sup 1      |
        And SolutionDetail exist
            | Solution | IntegrationsUrl                   |
            | Sln1     | An original integrations url      |
            | Sln2     | Another original integrations url |

    @3667
    Scenario: 1. Solution integrations section data is updated
        When a PUT request is made to update the integrations section for solution Sln1
            | IntegrationsUrl        |
            | A new integrations url |
        Then a successful response is returned
        And SolutionDetail exist
            | Solution | IntegrationsUrl                   |
            | Sln1     | A new integrations url            |
            | Sln2     | Another original integrations url |
        And Last Updated has updated on the SolutionDetail for solution Sln1

    @3667
    Scenario: 2. Solution integrations section data is updated with trimmed whitespace
        When a PUT request is made to update the integrations section for solution Sln1
            | IntegrationsUrl                             |
            | "           A new integrations url        " |
        Then a successful response is returned
        And SolutionDetail exist
            | Solution | IntegrationsUrl                   |
            | Sln1     | A new integrations url            |
            | Sln2     | Another original integrations url |
        And Last Updated has updated on the SolutionDetail for solution Sln1

    @3667
    Scenario: 3. Solution integrations section data is not created on update if no SolutionDetail
        Given a SolutionDetail Sln3 does not exist
        When a PUT request is made to update the integrations section for solution Sln3
            | IntegrationsUrl        |
            | A new integrations url |
        Then a response status of 500 is returned

    @3667
    Scenario: 4. Solution not found
        Given a Solution Sln4 does not exist
        When a PUT request is made to update the integrations section for solution Sln4
            | IntegrationsUrl        |
            | A new integrations url |
        Then a response status of 404 is returned

    @3667
    Scenario: 5. Service failure
        Given the call to the database to set the field will fail
        When a PUT request is made to update the integrations section for solution Sln1
            | IntegrationsUrl        |
            | A new integrations url |
        Then a response status of 500 is returned

    @3667
    Scenario: 6. Solution id not present in request
        When a PUT request is made to update the integrations section with no solution id
            | IntegrationsUrl        |
            | A new integrations url |
        Then a response status of 400 is returned
