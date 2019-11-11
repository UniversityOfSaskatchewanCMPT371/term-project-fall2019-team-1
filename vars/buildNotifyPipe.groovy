#!/usr/bin/env groovy

/// <summary>
/// Runs a given job on a given branch and outputs the result to jenkins
/// Inputs: Map buildResult [jobName: String Name of Job, branch: String name of branch, slackChannel: String name of slack channel to post to]
/// Outputs: None
/// Pre-Conditions: Inputted job and branch excist within Jenkins and GitHub
/// Post-Conditions: Inputted job is build and results are posted to slack
/// <authors> sch923, Sam Horovatin
def call(Map pipeParams) {

    //gets trigger branch
    def scmVars
    node {
        stage('Clone Source Sode') {
            scmVars = checkout scm
        }
    }

    //Only runs if input branch is identical to trigger branch
    if(pipeParams.branch == scmVars.GIT_BRANCH) { 
        pipeline {
            agent any
            environment {
                UnityBuildResults = ''
            }
            stages {
                stage('Build') {
                    steps {
                        script {
                            //builds the inputted "jobName" job
                            UnityBuildResults = build(job: pipeParams.jobName, parameters: [string(name: 'BRANCH', value: pipeParams.branch)], propagate: true, wait: true) 
                            println UnityBuildResults.getRawBuild().getLog()
                        }
                        
                    }
                }
            }
            post {
                always {
                    script {
                        //pushes results to slack
                        try{
                            slackNotifier UnityBuildResults.result.toString(),  pipeParams.slackChannel
                        }catch{
                            slackNotifier UnityBuildResults.result.toString(),  '#jenkins'
                        }
                        cleanWs()
                    }
                }
            }
        }
    }
}