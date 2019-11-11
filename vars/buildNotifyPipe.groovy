#!/usr/bin/env groovy

/// <summary>
/// Runs an given job on a given branch and outputs the result to jenkins
/// Inputs: Map buildResult [jobName: String Name of Job, branch: String name of branch]
/// Outputs: None
/// Pre-Conditions: Inputted job and branch excist within Jenkins and GitHub
/// Post-Conditions: Inputted job is build and results are posted to slack
/// <authors> sch923, Sam Horovatin
def call(Map pipeParams) {

    //gets trigger branch
    def scmVars
    node {
        stage('Clone source code') {
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
                            UnityBuildResults = build(job: pipeParams.jobName, parameters: [string(name: 'BRANCH', value: pipeParams.branch)], propagate: true, wait: true) 
                            println UnityBuildResults.getRawBuild().getLog()
                        }
                        
                    }
                }
            }
            post {
                always {
                    script {
                        slackNotifier UnityBuildResults.result.toString()
                        cleanWs()
                    }
                }
            }
        }
    }
}