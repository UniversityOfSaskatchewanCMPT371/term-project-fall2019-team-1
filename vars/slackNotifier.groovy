#!/usr/bin/env groovy

/// <summary>
/// Writes a message to the groups slack based on jenkins pipeline result
/// Inputs: String buildResult
/// Outputs: None
/// Pre-Conditions: None
/// Post-Conditions: Designated channel in slack has new post
/// <authors> https://medium.com/@lvthillo/send-slack-notifications-in-jenkins-pipelines-using-a-shared-library-873ca876f72c
def call(String buildResult) {
  if ( buildResult == "SUCCESS" ) {
    slackSend color: "good", message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} was successful. Build Link: ${env.BUILD_URL}"
  }
  else if( buildResult == "FAILURE" ) { 
    slackSend color: "danger", message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} was failed. Build Link: ${env.BUILD_URL}"
  }
  else if( buildResult == "UNSTABLE" ) { 
    slackSend color: "warning", message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} was unstable. Build Link: ${env.BUILD_URL}"
  }
  else {
    slackSend color: "danger", message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} its resulat was unclear. Build Link: ${env.BUILD_URL}"	
  }
}