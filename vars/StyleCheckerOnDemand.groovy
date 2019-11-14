@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Style Checker', slackChannel: "${env.SLACK_ID}"]
buildNotifyPipe paramMap
