Feature:  Supplier Edit Native Desktop Memory, Storage, Processing and Resolution
    As a Supplier
    I want to Edit the Native Desktop Memory, Storage, Processing and Resolution Section
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
@3620
Scenario: 1. Native Desktop Memory, Storage, Processing and Resolution is updated
    Given SolutionDetail exist
        | Solution | SummaryDescription             | FullDescription   | ClientApplication                                |
        | Sln1     | An full online medicine system | Online medicine 1 | { "ClientApplicationTypes": ["native-desktop"] } |
    When a PUT request is made to update the native-desktop-memory-and-storage section for solution Sln1
        | MinimumMemoryRequirement | StorageRequirementsDescription | MinimumCpu | RecommendedResolution |
        | 512                      | SSD > HDD                      | 1337 Ghz   | 1x1 px                |
    Then a successful response is returned
    And SolutionDetail exist
        | Solution | SummaryDescription             | FullDescription   | ClientApplication                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
        | Sln1     | An full online medicine system | Online medicine 1 | { "ClientApplicationTypes": ["native-desktop"], "BrowsersSupported": [], "MobileResponsive": null, "Plugins": null, "MinimumConnectionSpeed": null, "MinimumDesktopResolution": null, "HardwareRequirements": null, "NativeMobileHardwareRequirements": null, "NativeDesktopHardwareRequirements": null, "AdditionalInformation": null, "MobileFirstDesign": null, "NativeMobileFirstDesign": null, "MobileOperatingSystems": null, "MobileConnectionDetails": null, "MobileMemoryAndStorage": null, "MobileThirdParty": null, "NativeMobileAdditionalInformation": null, "NativeDesktopOperatingSystemsDescription": null, "NativeDesktopMinimumConnectionSpeed": null,  "NativeDesktopThirdParty": null, "NativeDesktopMemoryAndStorage": { "MinimumMemoryRequirement": "512", "StorageRequirementsDescription": "SSD > HDD", "MinimumCpu": "1337 Ghz", "RecommendedResolution" : "1x1 px" } } |

@3620
Scenario: 2. Solution is not found
    Given a Solution Sln2 does not exist
    When a PUT request is made to update the native-desktop-memory-and-storage section for solution Sln2
        | MinimumMemoryRequirement | StorageRequirementsDescription | MinimumCpu | RecommendedResolution |
        | 512                      | SSD > HDD                      | 1337 Ghz   | 1x1 px                |
    Then a response status of 404 is returned 

@3620
Scenario: 3. Service Failure
    Given the call to the database to set the field will fail
    When a PUT request is made to update the native-desktop-memory-and-storage section for solution Sln1
        | MinimumMemoryRequirement | StorageRequirementsDescription | MinimumCpu | RecommendedResolution |
        | 512                      | SSD > HDD                      | 1337 Ghz   | 1x1 px                |
    Then a response status of 500 is returned

@3620
Scenario: 4. Solution id is not present in the request
    When a PUT request is made to update the native-desktop-memory-and-storage section with no solution id
        | MinimumMemoryRequirement | StorageRequirementsDescription | MinimumCpu | RecommendedResolution |
        | 512                      | SSD > HDD                      | 1337 Ghz   | 1x1 px                |
    Then a response status of 400 is returned