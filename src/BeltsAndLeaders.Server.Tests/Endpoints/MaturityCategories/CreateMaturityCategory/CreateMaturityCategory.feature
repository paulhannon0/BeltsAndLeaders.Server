# VERB: POST
# PATH: /maturity-categories

Feature: Create Maturity Category

    Scenario: Create Maturity Category - Request Successful

        Given a valid request path for the 'Create Maturity Category' endpoint
        And a valid request body for the 'Create Maturity Category' endpoint
        When the POST request is made
        Then (201) Created is returned
        And the Location response header contains the ID of the new resource
        And the MaturityCategory record has been inserted into the database

    Scenario Outline: Create Maturity Category - Request Failure - Invalid body parameter

        Given a valid request path for the 'Create Maturity Category' endpoint
        And a request body for the 'Create Maturity Category' endpoint containing an invalid <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName |
            | Name          |

    Scenario: Create Maturity Category - Request Failure - Missing body parameter

        Given a valid request path for the 'Create Maturity Category' endpoint
        And a request body for the 'Create Maturity Category' endpoint with a missing <ParameterName> parameter
        When the POST request is made
        Then (400) Bad Request is returned

        Examples:
            | ParameterName |
            | Name          |
