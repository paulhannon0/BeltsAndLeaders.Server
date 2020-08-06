# VERB: POST
# PATH: /achievements

Feature: Create Achievement

    Scenario: Create Achievement - Request Successful - No Existing Belt - White Belt Achievement

        Given a user with 0 total maturity points
        And a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint with a White belt achievement
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt value updated to None
        And the relevant User has had their TotalMaturityPoints value updated to 1

    Scenario: Create Achievement - Request Successful - No Existing Belt - Green Belt Achievement

        Given a user with 0 total maturity points
        And a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint with a Green belt achievement
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt value updated to None
        And the relevant User has had their TotalMaturityPoints value updated to 2

    Scenario: Create Achievement - Request Successful - No Existing Belt - Black Belt Achievement

        Given a user with 0 total maturity points
        And a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint with a Black belt achievement
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt value updated to None
        And the relevant User has had their TotalMaturityPoints value updated to 3

    Scenario: Create Achievement - Request Successful - No Existing Belt - Upgrade to White Belt

        Given a user with 4 total maturity points
        And a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint with a White belt achievement
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt value updated to White
        And the relevant User has had their TotalMaturityPoints value updated to 5

    Scenario: Create Achievement - Request Successful - Existing White Belt - Upgrade to Green Belt

        Given a user with 9 total maturity points including 2 green belt and 1 black belt achievements
        And a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint with a White belt achievement
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt value updated to Green
        And the relevant User has had their TotalMaturityPoints value updated to 10

    Scenario: Create Achievement - Request Successful - Existing Green Belt - Upgrade to Black Belt

        Given a user with 14 total maturity points including 3 black belt achievements
        And a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint with a White belt achievement
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt value updated to Black
        And the relevant User has had their TotalMaturityPoints value updated to 15

    Scenario Outline: Create Achievement - Request Failure - Invalid body parameter

        Given a valid request path for the 'Create Achievement' endpoint
        And a request body for the 'Create Achievement' endpoint containing an invalid <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName   |
            | UserId          |
            | MaturityLevelId |
            | AchievementDate |
            | Comment         |

    Scenario: Create Achievement - Request Failure - Missing body parameter

        Given a valid request path for the 'Create Achievement' endpoint
        And a request body for the 'Create Achievement' endpoint with a missing <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName   |
            | UserId          |
            | MaturityLevelId |
            | AchievementDate |
            | Comment         |
