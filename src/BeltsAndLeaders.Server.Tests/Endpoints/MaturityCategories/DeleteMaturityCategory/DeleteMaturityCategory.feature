# VERB: DELETE
# PATH: /maturity-categories/{Id}

Feature: Delete Maturity Category

    Scenario: Request Successful

        Given a valid request path for the 'Delete Maturity Category' endpoint
        When the DELETE request is made
        Then (204) OK is returned
        And the MaturityCategory record has been deleted from the database
        And all child MaturityLevel records are deleted from the database

    Scenario: Request Failure - Invalid Id URL parameter

        Given a request path for the 'Delete Maturity Category' endpoint with an invalid Id parameter
        When the DELETE request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Maturity Category resource does not exist

        Given a request path for the 'Delete Maturity Category' endpoint with an ID for a non-existent resource
        When the DELETE request is made
        Then (404) Not Found is returned
