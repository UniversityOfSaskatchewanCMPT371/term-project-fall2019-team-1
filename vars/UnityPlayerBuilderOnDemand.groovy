@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Unity Player Builder', slackChannel: "${env.SLACK_ID}", jobType: 'OnDemand']
buildNotifyPipe paramMap