# VERB: GET
# PATH: /maturity-levels

Feature: Get All Maturity Levels

    Scenario: Get All Maturity Levels - Request Successful

        Given a valid request path for the 'Get All Maturity Levels' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the MaturityLevel records can be found in the response body
