# VERB: GET
# PATH: /maturity-categories/{MaturityCategoryId}/maturity-levels

Feature: Get Maturity Levels by Category ID

    Scenario: Get Maturity Levels by Category ID - Request Successful

        Given a valid request path for the 'Get Maturity Levels by Category ID' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the MaturityLevel record can be found in the response body

    Scenario: Get Maturity Levels by Category ID - Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get Maturity Levels by Category ID' endpoint with an invalid MaturityCategoryId parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Get Maturity Levels by Category ID - Request Failure - Maturity Category resource does not exist

        Given a request path for the 'Get Maturity Levels by Category ID' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
