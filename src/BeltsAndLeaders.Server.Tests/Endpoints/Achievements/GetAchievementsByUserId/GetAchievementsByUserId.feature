# VERB: GET
# PATH: /users/{UserId}/achievements

Feature: Get Achievements by User ID

    Scenario: Get Achievements by User ID - Request Successful

        Given a valid request path for the 'Get Achievements by User ID' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the Achievement record can be found in the response body

    Scenario: Get Achievements by User ID - Request Failure - Invalid Id URL parameter

        Given a request path for the 'Get Achievements by User ID' endpoint with an invalid UserId parameter
        When the GET request is made
        Then (400) Bad Request is returned

    Scenario: Get Achievements by User ID - Request Failure - User resource does not exist

        Given a request path for the 'Get Achievements by User ID' endpoint with an ID for a non-existent resource
        When the GET request is made
        Then (404) Not Found is returned
