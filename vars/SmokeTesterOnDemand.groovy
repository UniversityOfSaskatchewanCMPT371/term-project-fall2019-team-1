@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Smoke Tester', slackChannel: "${env.SLACK_ID}", jobType: 'OnDemand']
buildNotifyPipe paramMap