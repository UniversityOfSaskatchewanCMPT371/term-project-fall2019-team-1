@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Unity Builder', slackChannel: ]
def buildResults = buildNotifyPipe paramMap

gitNotifier buildResults.result.toString()

//pushes results to slack
try{
    slackNotifier buildResults, "${env.SLACK_ID}"
} catch (Exception e) {
    slackNotifier buildResults, 'jenkins'
}

cleanWs()