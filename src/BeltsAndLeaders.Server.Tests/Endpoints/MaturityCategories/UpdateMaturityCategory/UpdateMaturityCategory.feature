# VERB: PUT
# PATH: /maturity-categories/{Id}

Feature: Update Maturity Category

    Scenario: Request Successful

        Given a valid request path for the 'Update Maturity Category' endpoint
        And a valid request body for the 'Update Maturity Category' endpoint
        When the PUT request is made
        Then (204) OK is returned
        And the MaturityCategory record has been updated in the database

    Scenario: Request Failure - Invalid Id URL parameter

        Given a request path for the 'Update Maturity Category' endpoint with an invalid Id parameter
        When the PUT request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Maturity Category resource does not exist

        Given a request path for the 'Update Maturity Category' endpoint with an ID for a non-existent resource
        When the PUT request is made
        Then (404) Not Found is returned
