# VERB: POST
# PATH: /users

Feature: Create User

    Scenario: Create User - Request Successful

        Given a valid request path for the 'Create User' endpoint
        And a valid request body for the 'Create User' endpoint
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the User record has been inserted into the database

    Scenario Outline: Create User - Request Failure - Invalid body parameter

        Given a valid request path for the 'Create User' endpoint
        And a request body for the 'Create User' endpoint containing an invalid <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName     |
            | Name              |
            | Email             |
            | SpecialistArea    |
            | ChampionStartDate |

    Scenario: Create User - Request Failure - Missing body parameter

        Given a valid request path for the 'Create User' endpoint
        And a request body for the 'Create User' endpoint with a missing <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName  |
            | Name           |
            | Email          |
            | SpecialistArea |
