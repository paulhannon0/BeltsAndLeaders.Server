# VERB: GET
# PATH: /achievements/{Id}

Feature: Get Achievement

    Scenario: Get Achievement - Request Successful

        Given a valid request path for the 'Get Achievement' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the Achievement record can be found in the response body

    Scenario: Get Achievement - Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get Achievement' endpoint with an invalid Id parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Get Achievement - Request Failure - Achievement resource does not exist

        Given a request path for the 'Get Achievement' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
