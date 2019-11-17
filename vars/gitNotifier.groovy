#!/usr/bin/env groovy

/// <summary>
/// Updates GitHub based on the success or falure of a test.
/// Inputs: The result of a build
/// Pre-Conditions: Inputted job and branch excist within Jenkins and GitHub
/// Post-Conditions: Inputted job is build and results are posted to GitHub
/// <authors> sch923, Sam Horovatin
def call(String buildResultl) {
    Account = 'syncrotron'
    CredentialsId = 'sch923'
    if ( buildResult == "SUCCESS" ) {
        githubNotify account: Account, credentialsId: CredentialsId, description: 'Jenkins Returned Successfully', status: buildResult
    }
    else if( buildResult == "FAILURE" ) { 
        githubNotify account: Account, credentialsId: CredentialsId, description: 'Jenkins Returned a Falure', status: buildResult
    }
    else if( buildResult == "UNSTABLE" ) { 
        githubNotify account: Account, credentialsId: CredentialsId, description: 'Jenkins Returned an Unstable State', status: 'ERROR'
    else {

    }
}