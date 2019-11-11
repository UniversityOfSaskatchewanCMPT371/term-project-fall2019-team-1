@Library('shared-library')_

def paramMap = [branch: "${BRANCH}", jobName: 'Unity Builder', slackChannel: "${SLACK_ID}"]
buildNotifyPipe paramMap
