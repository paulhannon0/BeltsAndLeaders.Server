# VERB: DELETE
# PATH: /widgets/{Id}

Feature: Delete Widget

    Scenario: Request Successful

        Given a valid request path for the 'Delete Widget' endpoint
        When the DELETE request is made
        Then (204) OK is returned
        And the Widget record has been deleted from the database

    Scenario: Request Failure - Invalid Id URL parameter

        Given a request path for the 'Delete Widget' endpoint with an invalid Id parameter
        When the DELETE request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Widget resource does not exist

        Given a request path for the 'Delete Widget' endpoint with an ID for a non-existent resource
        When the DELETE request is made
        Then (404) Not Found is returned
