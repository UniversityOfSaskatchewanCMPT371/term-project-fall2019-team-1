@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Unity Builder', slackChannel: "${env.SLACK_ID}", jobType: 'OnDemand']
buildNotifyPipe paramMap
