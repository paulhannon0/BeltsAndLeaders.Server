# VERB: GET
# PATH: /users

Feature: Get All Users

    Scenario: Get All Users - Request Successful

        Given a valid request path for the 'Get All Users' endpoint
        When the GET request is made
        Then (200) OK is returned
        And the User records can be found in the response body
