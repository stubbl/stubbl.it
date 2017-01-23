module Actions
open canopy
open runner 

let GoToHomePage baseUrl = 
    url baseUrl

let stubsUrl () =
    runner.safelyGetUrl() == "/stubs"

let LogIn _ =
    let url = runner.safelyGetUrl()
    click "#login"
    "input[name='email']" << "test@stubbl.it"
    "input[name='password']" << "testpass"
    click ".auth0-lock-submit"
    waitForElement "#sidebar-wrapper"