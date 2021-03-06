# VERB: PUT
# PATH: /maturity-levels/{Id}

Feature: Update Maturity Level

    Scenario: Request Successful

        Given a valid request path for the 'Update Maturity Level' endpoint
        And a valid request body for the 'Update Maturity Level' endpoint
        When the PUT request is made
        Then (204) OK is returned
        And the MaturityLevel record has been updated in the database

    Scenario: Request Failure - Invalid Id URL parameter

        Given a request path for the 'Update Maturity Level' endpoint with an invalid Id parameter
        When the PUT request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Maturity Level resource does not exist

        Given a request path for the 'Update Maturity Level' endpoint with an ID for a non-existent resource
        When the PUT request is made
        Then (404) Not Found is returned
