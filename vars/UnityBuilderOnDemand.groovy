@Library('shared-library')_

def paramMap = [branch: "${params.BRANCH}", jobName: 'Unity Builder', slackChannel: "${params.SLACK_ID}"]
buildNotifyPipe paramMap
