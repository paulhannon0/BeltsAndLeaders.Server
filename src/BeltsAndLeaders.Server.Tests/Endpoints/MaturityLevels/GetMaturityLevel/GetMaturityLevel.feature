# VERB: GET
# PATH: /maturity-levels/{Id}

Feature: Get Maturity Level

    Scenario: Get Maturity Level - Request Successful

        Given a valid request path for the 'Get Maturity Level' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the MaturityLevel record can be found in the response body

    Scenario: Get Maturity Level - Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get Maturity Level' endpoint with an invalid Id parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Get Maturity Level - Request Failure - Maturity Level resource does not exist

        Given a request path for the 'Get Maturity Level' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
