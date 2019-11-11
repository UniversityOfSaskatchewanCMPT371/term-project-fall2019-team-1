@Library('shared-library')_

pipeline {
    agent any
    environment {
        BranchToBeBuild = '*/develop' //Indicates what branch will be build by Unity Builder
        JobName = 'Unity Builder'
        UnityBuildResults = ''
    }
    stages {
        stage('Build') {
            steps {
                script {
                    UnityBuildResults = build(job: JobName, parameters: [string(name: 'BRANCH', value: BranchToBeBuild)], propagate: true, wait: true) 
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