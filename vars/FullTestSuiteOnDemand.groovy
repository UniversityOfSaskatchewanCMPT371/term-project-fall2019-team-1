@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Full Test Suite', slackChannel: "${env.SLACK_ID}", jobType: 'OnDemand']
buildNotifyPipe paramMap
