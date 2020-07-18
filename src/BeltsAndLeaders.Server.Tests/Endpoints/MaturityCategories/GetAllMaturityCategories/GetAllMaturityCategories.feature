# VERB: GET
# PATH: /maturity-categories

Feature: Get All Maturity Categories

    Scenario: Get All Maturity Categories - Request Successful

        Given a valid request path for the 'Get All Maturity Categories' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the MaturityCategory records can be found in the response body
