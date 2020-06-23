# VERB: POST
# PATH: /users

Feature: Create User

    Scenario: Request Successful

        Given a valid request path for the 'Create User' endpoint
        And a valid request body for the 'Create User' endpoint
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the User record has been inserted into the database

    Scenario: Request Failure - Invalid Name body parameter

        Given a valid request path for the 'Create User' endpoint
        And a request body for the 'Create User' endpoint containing an invalid Name parameter
        When the POST request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Invalid Email body parameter

        Given a valid request path for the 'Create User' endpoint
        And a request body for the 'Create User' endpoint containing an invalid Email parameter
        When the POST request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Invalid SpecialistArea body parameter

        Given a valid request path for the 'Create User' endpoint
        And a request body for the 'Create User' endpoint containing an invalid SpecialistArea parameter
        When the POST request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Invalid ChampionStartDate body parameter

        Given a valid request path for the 'Create User' endpoint
        And a request body for the 'Create User' endpoint containing an invalid ChampionStartDate parameter
        When the POST request is made
        Then (400) Bad Request is returned
