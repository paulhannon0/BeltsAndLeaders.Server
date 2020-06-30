# VERB: GET
# PATH: /users/{Id}

Feature: Get User

    Scenario: Get User - Request Successful

        Given a valid request path for the 'Get User' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the User record can be found in the response body

    Scenario: Get User - Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get User' endpoint with an invalid Id parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Get User - Request Failure - User resource does not exist

        Given a request path for the 'Get User' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
