# VERB: GET
# PATH: /maturity-categories/{Id}

Feature: Get Maturity Category

    Scenario: Get Maturity Category - Request Successful

        Given a valid request path for the 'Get Maturity Category' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the MaturityCategory record can be found in the response body

    Scenario: Get Maturity Category - Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get Maturity Category' endpoint with an invalid Id parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Get Maturity Category - Request Failure - Maturity Category resource does not exist

        Given a request path for the 'Get Maturity Category' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
