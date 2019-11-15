Feature: Display Marketing Page Public Browser Based Section
	As a Catalogue User
    I want to manage Marketing Page Information for the Client Application Types Section
    So that I can ensure the information is correct

Background:
    Given Organisations exist
        | Name     |
        | GPs-R-Us |
        | Drs. Inc |
    And Suppliers exist
        | Id    | OrganisationName |
        | Sup 1 | GPs-R-Us         |
        | Sup 2 | Drs. Inc         |
    And Solutions exist
        | SolutionID | SolutionName        | OrganisationName | SupplierStatusId | SupplierId |
        | Sln1       | MedicOnline         | GPs-R-Us         | 1                | Sup 1      |
        | Sln2       | TakeTheRedPill      | Drs. Inc         | 1                | Sup 2      |
        | Sln3       | PracticeMgr         | Drs. Inc         | 1                | Sup 2      |
        | Sln4       | SubStandardPractice | GPs-R-Us         | 1                | Sup 1      |
        | Sln5       | Banana              | Drs. Inc         | 1                | Sup 2      |
        | Sln6       | Water Bottle        | Drs. Inc         | 1                | Sup 2      |
    And SolutionDetail exist
        | Solution | SummaryDescription          | FullDescription         | ClientApplication                                                                                                                               |
        | Sln1     |                             | Online medicine 1       | { "ClientApplicationTypes" : [ "browser-based", "native-desktop" ], "BrowsersSupported": ["Google Chrome", "Edge"], "MobileResponsive": false } |
        | Sln3     | Eye opening experience      | Eye opening6            | { "ClientApplicationTypes" : [ "browser-based" ], "BrowsersSupported": [ ], "MobileResponsive": null }                                          |
        | Sln4     | Fully fledged GP system     | Fully fledged GP 12     | { "ClientApplicationTypes" : [ "browser-based" ], "BrowsersSupported": [ ], "MobileResponsive": true }                                          |
        | Sln5     | Not Quite fledged GP system | Not Quite fledged GP 16 | { "ClientApplicationTypes" : [ "browser-based" ], "BrowsersSupported": ["Google Chrome", "Edge", "Safari"] }                                    |
        | Sln6     | Fruit delivery system       | Banana 1152             | { "ClientApplicationTypes" : [ "browser-based" ], "Plugins": { "Required": true, "AdditionalInformation": "Colourful water extension" } }       |

@3322
Scenario:1. Get Solution Public contains client application types browser based answers for all data
    When a GET request is made for solution public Sln1
    Then a successful response is returned
    And the solution client-application-types section is returned
    And the solution client-application-types section contains Browsers
        | Browser       |
        | Google Chrome |
        | Edge          |
    And the solution client-application-types section contains mobile responsive with value no

@3322
Scenario:2. Get Solution Public contains client application types browser based mobile responsive answer
    When a GET request is made for solution public Sln4
    Then a successful response is returned
    And the solution client-application-types section contains mobile responsive with value yes
    And the solution client-application-types section contains Browsers
        | Browser       |

@3322
Scenario:3. Get Solution Public contains client application types browser based browser supported answer
    When a GET request is made for solution public Sln5
    Then a successful response is returned
    And the solution client-application-types section is returned
    And the solution client-application-types section contains Browsers
        | Browser       |
        | Google Chrome |
        | Edge          |
        | Safari        |
    And the solution client-application-types section contains mobile responsive with value null

@2793
Scenario:4. Get Solution Public contains client application types browser based plugin required answer
    When a GET request is made for solution public Sln6
    Then a successful response is returned
    And the solution client-application-types section is returned
    And the solution client-application-types section contains plugin required with value yes
    And the solution client-application-types section contains Browsers
        | Browser       |
    And the solution client-application-types section contains mobile responsive with value null

@2793
Scenario:5. Get Solution Public contains client application types browser based plugin detail answer
    When a GET request is made for solution public Sln6
    Then a successful response is returned
    And the solution client-application-types section is returned
    And the solution client-application-types section contains plugin detail with value Colourful water extension
    And the solution client-application-types section contains Browsers
        | Browser       |
    And the solution client-application-types section contains mobile responsive with value null