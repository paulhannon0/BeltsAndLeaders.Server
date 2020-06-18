# VERB: GET
# PATH: /widgets/{Id}

Feature: Get Widget

    Scenario: Request Successful

        Given a valid request path for the 'Get Widget' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the Widget record can be found in the response body

    Scenario: Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get Widget' endpoint with an invalid Id parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Widget resource does not exist

        Given a request path for the 'Get Widget' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
