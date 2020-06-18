# VERB: POST
# PATH: /widgets

Feature: Create Widget

    Scenario: Request Successful

        Given a valid request path for the 'Create Widget' endpoint
        And a valid request body for the 'Create Widget' endpoint
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Widget record has been inserted into the database

    Scenario: Request Failure - Invalid Name body parameter

        Given a valid request path for the 'Create Widget' endpoint
        And a request body for the 'Create Widget' endpoint containing an invalid Name parameter
        When the POST request is made
        Then (400) Bad Request is returned
