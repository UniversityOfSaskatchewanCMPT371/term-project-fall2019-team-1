#!/usr/bin/env groovy

def call(Map pipeParams) {
    if(pipeParams.branch == env.BRANCH_NAME) {
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