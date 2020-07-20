# VERB: DELETE
# PATH: /maturity-levels/{Id}

Feature: Delete Maturity Level

    Scenario: Request Successful

        Given a valid request path for the 'Delete Maturity Level' endpoint
        When the DELETE request is made
        Then (204) OK is returned
        And the MaturityLevel record has been deleted from the database

    Scenario: Request Failure - Invalid Id URL parameter

        Given a request path for the 'Delete Maturity Level' endpoint with an invalid Id parameter
        When the DELETE request is made
        Then (400) Bad Request is returned

    Scenario: Request Failure - Maturity Level resource does not exist

        Given a request path for the 'Delete Maturity Level' endpoint with an ID for a non-existent resource
        When the DELETE request is made
        Then (404) Not Found is returned
