#!/usr/bin/env groovy

def getCurrentBranch() {
    return sh (
        script: 'git rev-parse --abbrev-ref HEAD',
        returnStdout: true
    ).trim()
}

def call(Map pipeParams) {
    def scmVars
    node {
        stage('Clone source code') {
            scmVars = checkout scm
        }
    }

    if(pipeParams.branch.substring(2) == scmVars.GIT_BRANCH) {
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