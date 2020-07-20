# VERB: POST
# PATH: /maturity-levels

Feature: Create Maturity Level

    Scenario: Create Maturity Level - Request Successful

        Given a valid request path for the 'Create Maturity Level' endpoint
        And a valid request body for the 'Create Maturity Level' endpoint
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the MaturityLevel record has been inserted into the database

    Scenario Outline: Create Maturity Level - Request Failure - Invalid body parameter

        Given a valid request path for the 'Create Maturity Level' endpoint
        And a request body for the 'Create Maturity Level' endpoint containing an invalid <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName      |
            | MaturityCategoryId |
            | MaturityLevel      |
            | Description        |

    Scenario: Create Maturity Level - Request Failure - Missing body parameter

        Given a valid request path for the 'Create Maturity Level' endpoint
        And a request body for the 'Create Maturity Level' endpoint with a missing <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName      |
            | MaturityCategoryId |
            | MaturityLevel      |
            | Description        |
