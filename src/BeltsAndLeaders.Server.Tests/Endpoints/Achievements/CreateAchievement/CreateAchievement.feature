# VERB: POST
# PATH: /achievements

Feature: Create Achievement

    Scenario: Create Achievement - Request Successful

        Given a valid request path for the 'Create Achievement' endpoint
        And a valid request body for the 'Create Achievement' endpoint
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the Achievement record has been inserted into the database
        And the relevant User has had their Belt and MaturityLevel values updated in the database

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
